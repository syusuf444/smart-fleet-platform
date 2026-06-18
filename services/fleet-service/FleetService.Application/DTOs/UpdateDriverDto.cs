namespace FleetService.Application.DTOs;

public class UpdateDriverDto
{
    public string EmployeeCode { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string LicenseNumber { get; set; } = string.Empty;

    public DateTime LicenseExpiryDate { get; set; }

    public DateTime JoiningDate { get; set; }

    public string Status { get; set; } = "Active";
}
