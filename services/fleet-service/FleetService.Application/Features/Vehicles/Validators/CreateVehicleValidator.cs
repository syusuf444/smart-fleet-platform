using FleetService.Application.DTOs;
using FluentValidation;

namespace FleetService.Application.Features.Vehicles.Validators;

public class CreateVehicleValidator
    : AbstractValidator<CreateVehicleDto>
{
    public CreateVehicleValidator()
    {
        RuleFor(x => x.VehicleNumber)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Manufacturer)
            .NotEmpty();

        RuleFor(x => x.Model)
            .NotEmpty();

        RuleFor(x => x.Year)
            .GreaterThan(2000);

        RuleFor(x => x.FuelCapacity)
            .GreaterThan(0);
    }
}