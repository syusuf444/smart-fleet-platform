using System.Security.Claims;

namespace AiService.API.Authorization;

public static class AiActionPermissions
{
    public const string ScheduleTrip = "scheduleTrip";
    public const string CreateMaintenance = "createMaintenance";
    public const string LogFuel = "logFuel";
    public const string ChangeVehicleStatus = "changeVehicleStatus";
    public const string AssignDriver = "assignDriver";

    public static readonly IReadOnlyCollection<string> AllActions = new[]
    {
        ScheduleTrip,
        CreateMaintenance,
        LogFuel,
        ChangeVehicleStatus,
        AssignDriver
    };

    public static IReadOnlyCollection<string> GetPermittedActions(ClaimsPrincipal user)
    {
        var actions = new List<string>();
        if (IsInAnyRole(user, "Admin", "SuperAdmin", "FleetManager", "Dispatcher"))
        {
            actions.Add(ScheduleTrip);
        }
        if (IsInAnyRole(user, "Admin", "SuperAdmin", "FleetManager", "MaintenanceTechnician"))
        {
            actions.Add(CreateMaintenance);
        }
        if (IsInAnyRole(user, "Admin", "SuperAdmin", "FleetManager"))
        {
            actions.Add(LogFuel);
        }
        if (IsInAnyRole(user, "Admin", "SuperAdmin"))
        {
            actions.Add(ChangeVehicleStatus);
        }
        if (IsInAnyRole(user, "Admin", "SuperAdmin", "FleetManager", "Dispatcher"))
        {
            actions.Add(AssignDriver);
        }

        return actions;
    }

    private static bool IsInAnyRole(ClaimsPrincipal user, params string[] roles) =>
        roles.Any(user.IsInRole);
}
