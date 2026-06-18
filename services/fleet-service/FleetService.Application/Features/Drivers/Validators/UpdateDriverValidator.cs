using FleetService.Application.DTOs;
using FluentValidation;

namespace FleetService.Application.Features.Drivers.Validators;

public class UpdateDriverValidator
    : AbstractValidator<UpdateDriverDto>
{
    private static readonly string[] AllowedStatuses =
    [
        "Active",
        "Assigned",
        "Inactive"
    ];

    public UpdateDriverValidator()
    {
        RuleFor(x => x.EmployeeCode)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .MaximumLength(20);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(255);

        RuleFor(x => x.LicenseNumber)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.LicenseExpiryDate)
            .GreaterThan(DateTime.UtcNow.Date);

        RuleFor(x => x.JoiningDate)
            .LessThanOrEqualTo(DateTime.UtcNow.Date);

        RuleFor(x => x.Status)
            .Must(status => AllowedStatuses.Contains(status))
            .WithMessage("Status must be Active, Assigned, or Inactive.");
    }
}
