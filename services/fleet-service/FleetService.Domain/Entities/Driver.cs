namespace FleetService.Domain.Entities;

public class Driver
{
    public Guid Id { get; set; }

    public string EmployeeCode { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string LicenseNumber { get; set; } = string.Empty;

    public DateTime LicenseExpiryDate { get; set; }

    public DateTime JoiningDate { get; set; }

    public string Status { get; set; } = "Active";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public string CreatedBy { get; set; } = "system";

    public string? UpdatedBy { get; set; }

    public bool IsDeleted { get; set; }
}
