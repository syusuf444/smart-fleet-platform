using FleetService.Application.DTOs;
using FluentValidation;

namespace FleetService.Application.Features.MaintenanceRecords.Validators;

public class CreateMaintenanceRecordValidator
    : AbstractValidator<CreateMaintenanceRecordDto>
{
    private static readonly string[] AllowedStatuses =
    [
        "Scheduled",
        "InProgress",
        "Completed"
    ];

    public CreateMaintenanceRecordValidator()
    {
        RuleFor(x => x.VehicleId)
            .NotEmpty();

        RuleFor(x => x.ServiceType)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Description)
            .MaximumLength(2000);

        RuleFor(x => x.ScheduledDate)
            .GreaterThanOrEqualTo(DateTime.UtcNow.Date);

        RuleFor(x => x.Cost)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Vendor)
            .NotEmpty()
            .MaximumLength(255);

        RuleFor(x => x.Status)
            .Must(status => AllowedStatuses.Contains(status))
            .WithMessage("Status must be Scheduled, InProgress, or Completed.");
    }
}
