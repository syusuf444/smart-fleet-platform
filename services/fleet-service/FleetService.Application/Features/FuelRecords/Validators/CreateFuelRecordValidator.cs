using FleetService.Application.DTOs;
using FluentValidation;

namespace FleetService.Application.Features.FuelRecords.Validators;

public class CreateFuelRecordValidator
    : AbstractValidator<CreateFuelRecordDto>
{
    public CreateFuelRecordValidator()
    {
        RuleFor(x => x.VehicleId)
            .NotEmpty();

        RuleFor(x => x.FuelDate)
            .LessThanOrEqualTo(DateTime.UtcNow);

        RuleFor(x => x.Quantity)
            .GreaterThan(0);

        RuleFor(x => x.Cost)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.OdometerReading)
            .GreaterThanOrEqualTo(0);
    }
}
