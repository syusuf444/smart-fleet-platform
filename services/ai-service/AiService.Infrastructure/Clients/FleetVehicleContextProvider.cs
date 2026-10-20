using System.Net.Http.Json;
using System.Text.Json;
using System.Text.RegularExpressions;
using AiService.Application.Interfaces;

namespace AiService.Infrastructure.Clients;

public sealed class FleetVehicleContextProvider : IFleetVehicleContextProvider
{
    private static readonly Regex RegistrationPattern = new(
        @"\b[A-Z]{2}\s?\d{1,2}\s?[A-Z]{1,3}\s?\d{1,4}\b",
        RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

    private static readonly JsonSerializerOptions JsonOptions =
        new(JsonSerializerDefaults.Web);

    private readonly HttpClient _httpClient;

    public FleetVehicleContextProvider(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string?> GetVehicleContextAsync(
        string question,
        string? authorizationHeader,
        CancellationToken cancellationToken)
    {
        var registrationMatch = RegistrationPattern.Match(question);
        var isVehicleStatusLookup = AsksAboutVehicleStatus(question);
        if (!registrationMatch.Success && !isVehicleStatusLookup)
        {
            return null;
        }

        if (string.IsNullOrWhiteSpace(authorizationHeader))
        {
            throw new UnauthorizedAccessException(
                "Sign in to look up live fleet vehicle details.");
        }

        var fleetResponse = await GetFleetDataAsync<List<FleetVehicle>>(
            "fleet/Vehicles",
            authorizationHeader,
            cancellationToken);

        if (!registrationMatch.Success)
        {
            var normalizedQuestion = NormalizeLookupText(question);
            var matches = fleetResponse?.Data?
                .Where(vehicle =>
                {
                    var makeAndModel = NormalizeLookupText($"{vehicle.Manufacturer} {vehicle.Model}");
                    return makeAndModel.Length > 0 && normalizedQuestion.Contains(makeAndModel, StringComparison.Ordinal);
                })
                .ToArray() ?? [];

            if (matches.Length == 0)
            {
                var requestedName = ExtractVehicleName(question);
                return string.IsNullOrWhiteSpace(requestedName)
                    ? null
                    : $"FleetService lookup found no vehicle matching '{requestedName}'. Tell the user no matching fleet vehicle was found; do not guess its status.";
            }

            return "Verified FleetService status for matching vehicle(s): " +
                   string.Join("; ", matches.Select(vehicle =>
                       $"{vehicle.Manufacturer} {vehicle.Model} ({vehicle.VehicleNumber}) is {vehicle.Status}")) +
                   ". Answer the user's status question directly using these records.";
        }

        var registration = Regex.Replace(
            registrationMatch.Value,
            @"\s+",
            string.Empty).ToUpperInvariant();

        var vehicle = fleetResponse?.Data?.FirstOrDefault(candidate =>
            string.Equals(
                candidate.VehicleNumber,
                registration,
                StringComparison.OrdinalIgnoreCase));

        if (vehicle is null)
        {
            return $"FleetService lookup found no vehicle registered as {registration}. " +
                   "Tell the user that no matching vehicle was found; do not guess its specifications.";
        }

        var context = $"Verified FleetService record for {vehicle.VehicleNumber}: " +
               $"manufacturer {vehicle.Manufacturer}, model {vehicle.Model}, " +
               $"year {vehicle.Year}, fuel capacity {vehicle.FuelCapacity} litres, " +
               $"status {vehicle.Status}. Use these verified values to answer the user's question directly.";

        if (AsksAboutMaintenance(question))
        {
            context += " " + await GetMaintenanceContextAsync(
                vehicle,
                authorizationHeader,
                cancellationToken);
        }

        if (AsksAboutFuelRecords(question))
        {
            context += " " + await GetFuelContextAsync(
                vehicle,
                authorizationHeader,
                cancellationToken);
        }

        return context;
    }

    private async Task<string> GetMaintenanceContextAsync(
        FleetVehicle vehicle,
        string authorizationHeader,
        CancellationToken cancellationToken)
    {
        var response = await GetFleetDataAsync<List<MaintenanceRecord>>(
            "fleet/MaintenanceRecords",
            authorizationHeader,
            cancellationToken);
        var records = response?.Data?
            .Where(record => !record.IsDeleted &&
                string.Equals(record.VehicleId, vehicle.Id, StringComparison.OrdinalIgnoreCase))
            .ToArray() ?? [];

        if (records.Length == 0)
        {
            return $"No maintenance records are registered for {vehicle.VehicleNumber}. " +
                   "Tell the user no matching maintenance history was found.";
        }

        return $"Maintenance records for {vehicle.VehicleNumber}: " +
               string.Join("; ", records.Select(record =>
                   $"{record.ServiceType}: {record.Description}, status {record.Status}, " +
                   $"scheduled {record.ScheduledDate}, completed {record.CompletedDate ?? "not completed"}, " +
                   $"cost {record.Cost} from {record.Vendor}")) + ". Use these records to answer directly.";
    }

    private async Task<string> GetFuelContextAsync(
        FleetVehicle vehicle,
        string authorizationHeader,
        CancellationToken cancellationToken)
    {
        var response = await GetFleetDataAsync<List<FuelRecord>>(
            "fleet/FuelRecords",
            authorizationHeader,
            cancellationToken);
        var records = response?.Data?
            .Where(record => !record.IsDeleted &&
                string.Equals(record.VehicleId, vehicle.Id, StringComparison.OrdinalIgnoreCase))
            .OrderByDescending(record => record.FuelDate)
            .ToArray() ?? [];

        if (records.Length == 0)
        {
            return $"No fuel records are registered for {vehicle.VehicleNumber}. " +
                   "Tell the user no matching fuel history was found.";
        }

        return $"Fuel records for {vehicle.VehicleNumber}: " +
               string.Join("; ", records.Select(record =>
                   $"date {record.FuelDate}, quantity {record.Quantity} litres, " +
                   $"cost {record.Cost}, odometer {record.OdometerReading}")) + ". Use these records to answer directly.";
    }

    private async Task<FleetApiResponse<T>?> GetFleetDataAsync<T>(
        string path,
        string authorizationHeader,
        CancellationToken cancellationToken)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, path);
        request.Headers.TryAddWithoutValidation("Authorization", authorizationHeader);

        using var response = await _httpClient.SendAsync(request, cancellationToken);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<FleetApiResponse<T>>(
            JsonOptions,
            cancellationToken);
    }

    private static bool AsksAboutMaintenance(string question) =>
        ContainsAny(question, "maintenance", "service history", "service record", "repair", "inspection");

    private static bool AsksAboutVehicleStatus(string question) =>
        Regex.IsMatch(question, @"\b(status|state)\b", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

    private static string NormalizeLookupText(string value) =>
        Regex.Replace(value, @"[^a-zA-Z0-9]+", " ").Trim().ToLowerInvariant();

    private static string ExtractVehicleName(string question)
    {
        var match = Regex.Match(question,
            @"\b(?:of|for)\s+(?:the\s+)?(.+?)(?:[?.!,]|$)",
            RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
        if (!match.Success)
        {
            return string.Empty;
        }

        var name = Regex.Replace(match.Groups[1].Value, @"\b(vehicle|status|state)\b", "", RegexOptions.IgnoreCase).Trim();
        return Regex.Replace(name, @"\s+", " ");
    }

    private static bool AsksAboutFuelRecords(string question) =>
        ContainsAny(question, "fuel history", "fuel record", "fuel usage", "fuel consumption", "fuel spend", "fuel cost", "refuel", "refill") ||
        question.Contains("fuel", StringComparison.OrdinalIgnoreCase) &&
        !question.Contains("capacity", StringComparison.OrdinalIgnoreCase);

    private static bool ContainsAny(string value, params string[] terms) =>
        terms.Any(term => value.Contains(term, StringComparison.OrdinalIgnoreCase));

    private sealed class FleetApiResponse<T>
    {
        public bool Success { get; init; }

        public T? Data { get; init; }
    }

    private sealed class FleetVehicle
    {
        public string Id { get; init; } = string.Empty;

        public string VehicleNumber { get; init; } = string.Empty;

        public string Manufacturer { get; init; } = string.Empty;

        public string Model { get; init; } = string.Empty;

        public int Year { get; init; }

        public decimal FuelCapacity { get; init; }

        public string Status { get; init; } = string.Empty;
    }

    private sealed class MaintenanceRecord
    {
        public string VehicleId { get; init; } = string.Empty;

        public string ServiceType { get; init; } = string.Empty;

        public string Description { get; init; } = string.Empty;

        public string ScheduledDate { get; init; } = string.Empty;

        public string? CompletedDate { get; init; }

        public decimal Cost { get; init; }

        public string Vendor { get; init; } = string.Empty;

        public string Status { get; init; } = string.Empty;

        public bool IsDeleted { get; init; }
    }

    private sealed class FuelRecord
    {
        public string VehicleId { get; init; } = string.Empty;

        public string FuelDate { get; init; } = string.Empty;

        public decimal Quantity { get; init; }

        public decimal Cost { get; init; }

        public decimal OdometerReading { get; init; }

        public bool IsDeleted { get; init; }
    }
}
