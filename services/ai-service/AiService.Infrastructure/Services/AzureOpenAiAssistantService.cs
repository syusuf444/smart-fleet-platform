using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using AiService.Application.DTOs;
using AiService.Application.Interfaces;
using AiService.Infrastructure.Options;
using Microsoft.Extensions.Options;

namespace AiService.Infrastructure.Services;

public class AzureOpenAiAssistantService : IAiAssistantService
{
    private const string SystemPrompt =
        "You are Smart Fleet AI Assistant. Provide concise, operationally useful logistics and fleet-management insights. Use verified FleetService context when supplied, answer specific vehicle questions directly from that data, and never claim you lack access when context is present. If context says a vehicle was not found, say so; do not invent specifications.";

    private const string AgentSystemPrompt = """
        You are Smart Fleet's action planner. You can plan MULTIPLE actions in a single response.
        Return exactly one JSON object matching this schema:
        {"assistantMessage":"string","reasoning":"string","actions":[{"action":"none|scheduleTrip|createMaintenance|logFuel|changeVehicleStatus|assignDriver","trip":null|{"vehicleNumber":null,"driverEmail":null,"source":null,"destination":null,"startDate":null,"endDate":null},"maintenance":null|{"vehicleNumber":null,"serviceType":null,"description":null,"scheduledDate":null,"cost":null,"vendor":null},"fuel":null|{"vehicleNumber":null,"fuelDate":null,"quantity":null,"cost":null,"odometerReading":null},"vehicleStatus":null|{"vehicleNumber":null,"newStatus":null,"reason":null},"driverAssignment":null|{"vehicleNumber":null,"driverEmail":null,"unassign":false}}]}

        Rules:
        - The application has already detected which action types the user is requesting. You will be told the detected actions. Copy those action values exactly into the actions array.
        - If no action is detected, use actions: [{"action":"none"}] and answer the user normally in assistantMessage.
        - For each action, populate only the matching draft object (trip, maintenance, fuel, vehicleStatus, or driverAssignment) and set all others to null.
        - Extract only values the user supplied or that are present in verified context. Never invent identifiers, dates, vendors, or costs; leave unknown fields null.
        - Dates must be ISO 8601 when the user supplied enough information to determine them.
        - In the "reasoning" field, explain your choices: why you picked certain vehicles, drivers, dates, or values. Mention any assumptions you made.
        - Do not claim that any action has been executed: the user must review and confirm each draft.
        - If the user asks for an action they are not permitted to do, explain this in assistantMessage and omit that action from the actions array.
        - Valid vehicle statuses for changeVehicleStatus are: Active, InMaintenance, Decommissioned.
        """;

    private static readonly JsonSerializerOptions PlanJsonOptions =
        new(JsonSerializerDefaults.Web);

    private static readonly Regex TripActionRequest = new(
        @"\b(schedule|book|arrange|create|add|plan)\w*\b.{0,80}\b(trips?|journeys?|routes?)\b|\b(trips?|journeys?|routes?)\b.{0,80}\b(schedule|book|arrange|create|add|plan)\w*\b",
        RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);

    private static readonly Regex MaintenanceActionRequest = new(
        @"\b(schedule|book|arrange|create|add)\w*\b.{0,80}\b(maintenance|service|repair|inspection|oil\s+change)\b|\b(maintenance|service|repair|inspection|oil\s+change)\b.{0,80}\b(schedule|book|arrange|create|add)\w*\b",
        RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);

    private static readonly Regex FuelActionRequest = new(
        @"\b(log|record|add|enter|create)\w*\b.{0,80}\b(fuel|refuel|gas|petrol|diesel)\b|\b(fuel|refuel|gas|petrol|diesel)\b.{0,80}\b(log|record|add|enter|create)\w*\b",
        RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);

    private static readonly Regex VehicleStatusRequest = new(
        @"\b(change|update|set|mark|move)\w*\b.{0,80}\b(status|active|decommission\w*|in\s*maintenance)\b|\b(status|active|decommission\w*|in\s*maintenance)\b.{0,80}\b(change|update|set|mark|move)\w*\b",
        RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);

    private static readonly Regex DriverAssignmentRequest = new(
        @"\b(assign|reassign|unassign|allocate|deallocate|release)\w*\b.{0,80}\b(driver|vehicle)\b|\b(driver|vehicle)\b.{0,80}\b(assign|reassign|unassign|allocate|deallocate|release)\w*\b",
        RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);

    private readonly HttpClient _httpClient;

    private readonly AzureOpenAiOptions _options;

    private readonly AiProviderOptions _providerOptions;

    public AzureOpenAiAssistantService(
        HttpClient httpClient,
        IOptions<AzureOpenAiOptions> options,
        IOptions<AiProviderOptions> providerOptions)
    {
        _httpClient = httpClient;
        _options = options.Value;
        _providerOptions = providerOptions.Value;
    }

    public async Task<AiResponseDto> AskAsync(
        AiChatRequestDto request,
        CancellationToken cancellationToken)
    {
        var prompt = string.IsNullOrWhiteSpace(request.Context)
            ? request.Message
            : $"Context:\n{request.Context}\n\nQuestion:\n{request.Message}";

        try
        {
            var answer = await SendChatAsync(prompt, cancellationToken);

            return new AiResponseDto
            {
                Success = true,
                Message = "AI assistant response generated successfully",
                Data = answer
            };
        }
        catch (InvalidOperationException ex)
        {
            return new AiResponseDto
            {
                Success = false,
                Message = ex.Message,
                Data = null
            };
        }
        catch (Exception ex)
        {
            return new AiResponseDto
            {
                Success = false,
                Message = $"AI assistant request failed: {ex.Message}",
                Data = null
            };
        }
    }

    public async Task<AiResponseDto> AnalyzeFleetHealthAsync(
        FleetHealthAnalysisRequestDto request,
        CancellationToken cancellationToken)
    {
        var prompt =
            "Analyze fleet health and return prioritized recommendations.\n\n" +
            $"Vehicle Summary:\n{request.VehicleSummary}\n\n" +
            $"Maintenance Summary:\n{request.MaintenanceSummary}\n\n" +
            $"Fuel Summary:\n{request.FuelSummary ?? "Not provided"}";

        try
        {
            var answer = await SendChatAsync(prompt, cancellationToken);

            return new AiResponseDto
            {
                Success = true,
                Message = "Fleet health analysis generated successfully",
                Data = answer
            };
        }
        catch (InvalidOperationException ex)
        {
            return new AiResponseDto
            {
                Success = false,
                Message = ex.Message,
                Data = null
            };
        }
        catch (Exception ex)
        {
            return new AiResponseDto
            {
                Success = false,
                Message = $"Fleet health request failed: {ex.Message}",
                Data = null
            };
        }
    }

    public async Task<AiActionPlanDto> PlanActionAsync(
        AiActionPlanRequestDto request,
        IReadOnlyCollection<string> permittedActions,
        CancellationToken cancellationToken)
    {
        var allowedActions = permittedActions
            .Intersect(AllKnownActions, StringComparer.Ordinal)
            .ToArray();
        var requestedActions = DetectRequestedActions(request.Message);
        var deniedActions = requestedActions
            .Where(action => !allowedActions.Contains(action, StringComparer.Ordinal))
            .ToList();
        var approvedActions = requestedActions
            .Where(action => allowedActions.Contains(action, StringComparer.Ordinal))
            .ToList();

        if (requestedActions.Count > 0 && approvedActions.Count == 0)
        {
            return new AiActionPlanDto
            {
                AssistantMessage = "Your role isn't allowed to prepare " +
                    (requestedActions.Count == 1 ? "that action" : "those actions") +
                    ". You can still ask me for fleet information or recommendations.",
                Reasoning = $"Detected intent: {string.Join(", ", requestedActions)}. None are permitted for the current role."
            };
        }

        var deniedNote = deniedActions.Count > 0
            ? $"\nNote: the user also requested [{string.Join(", ", deniedActions)}] but their role does not permit those actions. Mention this in assistantMessage.\n"
            : "";

        var prompt = $"Actions detected by the application: [{string.Join(", ", approvedActions.DefaultIfEmpty("none"))}]. " +
                     $"Return exactly these actions in the actions array; do not select different actions.\n" +
                     deniedNote +
                     $"Permitted actions for this signed-in user's role: {JsonSerializer.Serialize(allowedActions)}.\n" +
                     $"Verified FleetService context (if any): {request.Context ?? "none"}\n" +
                     $"User request: {request.Message}";

        try
        {
            var content = await SendChatAsync(prompt, cancellationToken, AgentSystemPrompt, maxTokens: 1200);
            return ParseActionPlan(content, approvedActions);
        }
        catch (Exception ex) when (!cancellationToken.IsCancellationRequested &&
            (ex is InvalidOperationException or HttpRequestException or TaskCanceledException or JsonException))
        {
            return CreateFallbackPlan(approvedActions);
        }
    }

    private static readonly HashSet<string> AllKnownActions = new(StringComparer.Ordinal)
    {
        "scheduleTrip", "createMaintenance", "logFuel", "changeVehicleStatus", "assignDriver"
    };

    private static List<string> DetectRequestedActions(string message)
    {
        var actions = new List<string>();
        if (TripActionRequest.IsMatch(message)) actions.Add("scheduleTrip");
        if (MaintenanceActionRequest.IsMatch(message)) actions.Add("createMaintenance");
        if (FuelActionRequest.IsMatch(message)) actions.Add("logFuel");
        if (VehicleStatusRequest.IsMatch(message)) actions.Add("changeVehicleStatus");
        if (DriverAssignmentRequest.IsMatch(message)) actions.Add("assignDriver");
        return actions;
    }

    private static AiActionPlanDto ParseActionPlan(
        string content,
        List<string> requestedActions)
    {
        var jsonStart = content.IndexOf('{');
        var jsonEnd = content.LastIndexOf('}');
        if (jsonStart < 0 || jsonEnd <= jsonStart)
        {
            return CreateFallbackPlan(requestedActions);
        }

        AiActionPlanDto? plan;
        try
        {
            plan = JsonSerializer.Deserialize<AiActionPlanDto>(
                content[jsonStart..(jsonEnd + 1)],
                PlanJsonOptions);
        }
        catch (JsonException)
        {
            plan = null;
        }

        if (plan is null)
        {
            return CreateFallbackPlan(requestedActions);
        }

        // Sanitise each action draft
        var sanitisedActions = new List<ActionDraftDto>();
        foreach (var draft in plan.Actions)
        {
            if (!AllKnownActions.Contains(draft.Action) && draft.Action != "none")
            {
                continue;
            }

            NormaliseDraft(draft);
            sanitisedActions.Add(draft);
        }

        // If the LLM returned no usable actions but we detected intent, build fallback drafts
        if (sanitisedActions.Count == 0 && requestedActions.Count > 0)
        {
            foreach (var action in requestedActions)
            {
                sanitisedActions.Add(CreateEmptyDraft(action));
            }
        }
        else if (sanitisedActions.Count == 0)
        {
            sanitisedActions.Add(new ActionDraftDto { Action = "none" });
        }

        // Override actions to match what the application detected
        if (requestedActions.Count > 0)
        {
            var overridden = new List<ActionDraftDto>();
            foreach (var action in requestedActions)
            {
                var match = sanitisedActions.FirstOrDefault(
                    d => d.Action.Equals(action, StringComparison.Ordinal));
                overridden.Add(match ?? CreateEmptyDraft(action));
            }
            sanitisedActions = overridden;
        }

        plan.Actions = sanitisedActions;

        if (string.IsNullOrWhiteSpace(plan.AssistantMessage))
        {
            plan.AssistantMessage = requestedActions.Count == 0
                ? "How can I help with fleet operations?"
                : "Review the drafts below and confirm each one to send it to Fleet Management.";
        }

        plan.Reasoning ??= string.Empty;

        return plan;
    }

    private static void NormaliseDraft(ActionDraftDto draft)
    {
        switch (draft.Action)
        {
            case "scheduleTrip":
                draft.Trip ??= new TripActionDraftDto();
                draft.Maintenance = null;
                draft.Fuel = null;
                draft.VehicleStatus = null;
                draft.DriverAssignment = null;
                break;
            case "createMaintenance":
                draft.Maintenance ??= new MaintenanceActionDraftDto();
                draft.Trip = null;
                draft.Fuel = null;
                draft.VehicleStatus = null;
                draft.DriverAssignment = null;
                break;
            case "logFuel":
                draft.Fuel ??= new FuelActionDraftDto();
                draft.Trip = null;
                draft.Maintenance = null;
                draft.VehicleStatus = null;
                draft.DriverAssignment = null;
                break;
            case "changeVehicleStatus":
                draft.VehicleStatus ??= new VehicleStatusActionDraftDto();
                draft.Trip = null;
                draft.Maintenance = null;
                draft.Fuel = null;
                draft.DriverAssignment = null;
                break;
            case "assignDriver":
                draft.DriverAssignment ??= new DriverAssignmentActionDraftDto();
                draft.Trip = null;
                draft.Maintenance = null;
                draft.Fuel = null;
                draft.VehicleStatus = null;
                break;
            default:
                draft.Trip = null;
                draft.Maintenance = null;
                draft.Fuel = null;
                draft.VehicleStatus = null;
                draft.DriverAssignment = null;
                break;
        }
    }

    private static ActionDraftDto CreateEmptyDraft(string action)
    {
        var draft = new ActionDraftDto { Action = action };
        NormaliseDraft(draft);
        return draft;
    }

    private static AiActionPlanDto CreateFallbackPlan(List<string> requestedActions)
    {
        var plan = new AiActionPlanDto
        {
            AssistantMessage = requestedActions.Count == 0
                ? "I couldn't prepare a response right now. Please try again."
                : "I've opened drafts for your request. Complete any missing fields, review them, and confirm when they are correct.",
            Reasoning = "Fallback plan generated because the AI response could not be parsed."
        };

        if (requestedActions.Count == 0)
        {
            plan.Actions.Add(new ActionDraftDto { Action = "none" });
        }
        else
        {
            foreach (var action in requestedActions)
            {
                plan.Actions.Add(CreateEmptyDraft(action));
            }
        }

        return plan;
    }

    private async Task<string> SendChatAsync(
        string userPrompt,
        CancellationToken cancellationToken,
        string? systemPrompt = null,
        int maxTokens = 800)
    {
        var useOllama = _providerOptions.Provider.Equals(
            "Ollama",
            StringComparison.OrdinalIgnoreCase);
        var requestUri = useOllama
            ? GetOllamaRequestUri()
            : GetAzureOpenAiRequestUri();

        using var request = new HttpRequestMessage(HttpMethod.Post, requestUri);

        if (useOllama)
        {
            request.Headers.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "ollama");
        }
        else
        {
            request.Headers.Add("api-key", _options.ApiKey);
        }

        request.Content = JsonContent.Create(new AzureOpenAiChatRequest
        {
            Model = useOllama ? _providerOptions.Model : null,
            Messages =
            [
                new AzureOpenAiMessage("system", systemPrompt ?? SystemPrompt),
                new AzureOpenAiMessage("user", userPrompt)
            ],
            Temperature = 0.2m,
            MaxTokens = maxTokens
        });

        using var response =
            await _httpClient.SendAsync(request, cancellationToken);

        response.EnsureSuccessStatusCode();

        var chatResponse =
            await response.Content.ReadFromJsonAsync<AzureOpenAiChatResponse>(
                cancellationToken);

        return chatResponse?.Choices.FirstOrDefault()?.Message.Content
            ?? string.Empty;
    }

    private string GetOllamaRequestUri()
    {
        if (string.IsNullOrWhiteSpace(_providerOptions.Endpoint) ||
            string.IsNullOrWhiteSpace(_providerOptions.Model))
        {
            throw new InvalidOperationException(
                "Ollama configuration is missing. Configure AI:Endpoint and AI:Model.");
        }

        return $"{_providerOptions.Endpoint.TrimEnd('/')}/chat/completions";
    }

    private string GetAzureOpenAiRequestUri()
    {
        if (!_providerOptions.Provider.Equals(
                "AzureOpenAI",
                StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException(
                $"Unsupported AI provider '{_providerOptions.Provider}'. Supported providers are Ollama and AzureOpenAI.");
        }

        if (string.IsNullOrWhiteSpace(_options.Endpoint) ||
            string.IsNullOrWhiteSpace(_options.ApiKey) ||
            string.IsNullOrWhiteSpace(_options.DeploymentName))
        {
            throw new InvalidOperationException(
                "Azure OpenAI configuration is missing. Configure AzureOpenAI:Endpoint, AzureOpenAI:ApiKey, and AzureOpenAI:DeploymentName.");
        }

        return $"{_options.Endpoint.TrimEnd('/')}/openai/deployments/{_options.DeploymentName}/chat/completions?api-version={_options.ApiVersion}";
    }

    private sealed class AzureOpenAiChatRequest
    {
        [JsonPropertyName("model")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Model { get; init; }

        [JsonPropertyName("messages")]
        public IReadOnlyCollection<AzureOpenAiMessage> Messages { get; init; } =
            Array.Empty<AzureOpenAiMessage>();

        [JsonPropertyName("temperature")]
        public decimal Temperature { get; init; }

        [JsonPropertyName("max_tokens")]
        public int MaxTokens { get; init; }
    }

    private sealed record AzureOpenAiMessage(
        [property: JsonPropertyName("role")] string Role,
        [property: JsonPropertyName("content")] string Content);

    private sealed class AzureOpenAiChatResponse
    {
        [JsonPropertyName("choices")]
        public IReadOnlyCollection<AzureOpenAiChoice> Choices { get; init; } =
            Array.Empty<AzureOpenAiChoice>();
    }

    private sealed class AzureOpenAiChoice
    {
        [JsonPropertyName("message")]
        public AzureOpenAiMessageResponse Message { get; init; } = new();
    }

    private sealed class AzureOpenAiMessageResponse
    {
        [JsonPropertyName("content")]
        public string Content { get; init; } = string.Empty;
    }
}
