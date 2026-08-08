using FleetService.Domain.Entities;
using FleetService.Infrastructure.Persistence;

namespace FleetService.API.Extensions;

public static class FleetDbSeeder
{
    public static void Seed(FleetDbContext context)
    {
        var now = DateTime.UtcNow;

        var sampleVehicles = new List<Vehicle>
        {
            new Vehicle { Id = Guid.NewGuid(), VehicleNumber = "MH01AB1001", Manufacturer = "Volvo", Model = "FMX", Year = 2023, FuelCapacity = 420, Status = "Active", CreatedAt = now },
            new Vehicle { Id = Guid.NewGuid(), VehicleNumber = "MH01AB1002", Manufacturer = "Tata", Model = "Signa", Year = 2022, FuelCapacity = 360, Status = "Maintenance", CreatedAt = now },
            new Vehicle { Id = Guid.NewGuid(), VehicleNumber = "MH01AB1003", Manufacturer = "Scania", Model = "G500", Year = 2021, FuelCapacity = 450, Status = "Inactive", CreatedAt = now },
            new Vehicle { Id = Guid.NewGuid(), VehicleNumber = "MH01AB1004", Manufacturer = "Ashok Leyland", Model = "Partner", Year = 2024, FuelCapacity = 380, Status = "Active", CreatedAt = now },
            new Vehicle { Id = Guid.NewGuid(), VehicleNumber = "MH01AB1005", Manufacturer = "Volvo", Model = "FMX", Year = 2019, FuelCapacity = 350, Status = "Active", CreatedAt = now },
            new Vehicle { Id = Guid.NewGuid(), VehicleNumber = "MH01AB1006", Manufacturer = "Mahindra", Model = "Blazo", Year = 2020, FuelCapacity = 330, Status = "Maintenance", CreatedAt = now },
            new Vehicle { Id = Guid.NewGuid(), VehicleNumber = "MH01AB1007", Manufacturer = "Tata", Model = "Prima", Year = 2023, FuelCapacity = 400, Status = "Inactive", CreatedAt = now },
            new Vehicle { Id = Guid.NewGuid(), VehicleNumber = "MH01AB1008", Manufacturer = "Volvo", Model = "FH16", Year = 2024, FuelCapacity = 480, Status = "Active", CreatedAt = now },
            new Vehicle { Id = Guid.NewGuid(), VehicleNumber = "MH01AB1009", Manufacturer = "MAN", Model = "TGX", Year = 2021, FuelCapacity = 440, Status = "Maintenance", CreatedAt = now },
            new Vehicle { Id = Guid.NewGuid(), VehicleNumber = "MH01AB1010", Manufacturer = "Tata", Model = "LPT", Year = 2018, FuelCapacity = 400, Status = "Active", CreatedAt = now },
        };

        var sampleDrivers = new List<Driver>
        {
            new Driver { Id = Guid.NewGuid(), EmployeeCode = "DRV-0001", FirstName = "Aarav", LastName = "Patel", PhoneNumber = "+919800001001", Email = "aarav.patel@example.com", LicenseNumber = "DL-AAA111222", LicenseExpiryDate = now.AddYears(2), JoiningDate = now.AddYears(-2), Status = "Active", CreatedAt = now, CreatedBy = "system" },
            new Driver { Id = Guid.NewGuid(), EmployeeCode = "DRV-0002", FirstName = "Meera", LastName = "Shah", PhoneNumber = "+919800001002", Email = "meera.shah@example.com", LicenseNumber = "DL-BBB333444", LicenseExpiryDate = now.AddYears(1), JoiningDate = now.AddYears(-1), Status = "Assigned", CreatedAt = now, CreatedBy = "system" },
            new Driver { Id = Guid.NewGuid(), EmployeeCode = "DRV-0003", FirstName = "Rohan", LastName = "Iyer", PhoneNumber = "+919800001003", Email = "rohan.iyer@example.com", LicenseNumber = "DL-CCC555666", LicenseExpiryDate = now.AddYears(3), JoiningDate = now.AddYears(-3), Status = "Inactive", CreatedAt = now, CreatedBy = "system" },
            new Driver { Id = Guid.NewGuid(), EmployeeCode = "DRV-0004", FirstName = "Priya", LastName = "Rao", PhoneNumber = "+919800001004", Email = "priya.rao@example.com", LicenseNumber = "DL-DDD777888", LicenseExpiryDate = now.AddMonths(18), JoiningDate = now.AddYears(-1), Status = "Assigned", CreatedAt = now, CreatedBy = "system" },
            new Driver { Id = Guid.NewGuid(), EmployeeCode = "DRV-0005", FirstName = "Sameer", LastName = "Khan", PhoneNumber = "+919800001005", Email = "sameer.khan@example.com", LicenseNumber = "DL-EEE999000", LicenseExpiryDate = now.AddYears(2), JoiningDate = now.AddYears(-4), Status = "Active", CreatedAt = now, CreatedBy = "system" },
            new Driver { Id = Guid.NewGuid(), EmployeeCode = "DRV-0006", FirstName = "Nisha", LastName = "Agarwal", PhoneNumber = "+919800001006", Email = "nisha.agrawal@example.com", LicenseNumber = "DL-FFF111222", LicenseExpiryDate = now.AddYears(2), JoiningDate = now.AddYears(-1), Status = "On Leave", CreatedAt = now, CreatedBy = "system" },
            new Driver { Id = Guid.NewGuid(), EmployeeCode = "DRV-0007", FirstName = "Vikram", LastName = "Das", PhoneNumber = "+919800001007", Email = "vikram.das@example.com", LicenseNumber = "DL-GGG333444", LicenseExpiryDate = now.AddYears(4), JoiningDate = now.AddYears(-5), Status = "Active", CreatedAt = now, CreatedBy = "system" },
            new Driver { Id = Guid.NewGuid(), EmployeeCode = "DRV-0008", FirstName = "Kavya", LastName = "Menon", PhoneNumber = "+919800001008", Email = "kavya.menon@example.com", LicenseNumber = "DL-HHH555666", LicenseExpiryDate = now.AddYears(2), JoiningDate = now.AddYears(-2), Status = "Assigned", CreatedAt = now, CreatedBy = "system" },
            new Driver { Id = Guid.NewGuid(), EmployeeCode = "DRV-0009", FirstName = "Aditya", LastName = "Desai", PhoneNumber = "+919800001009", Email = "aditya.desai@example.com", LicenseNumber = "DL-III777888", LicenseExpiryDate = now.AddYears(1), JoiningDate = now.AddYears(-3), Status = "Inactive", CreatedAt = now, CreatedBy = "system" },
            new Driver { Id = Guid.NewGuid(), EmployeeCode = "DRV-0010", FirstName = "Shruti", LastName = "Yadav", PhoneNumber = "+919800001010", Email = "shruti.yadav@example.com", LicenseNumber = "DL-JJJ999000", LicenseExpiryDate = now.AddYears(3), JoiningDate = now.AddYears(-4), Status = "Active", CreatedAt = now, CreatedBy = "system" },
        };

        foreach (var vehicle in sampleVehicles)
        {
            if (!context.Vehicles.Any(v => v.VehicleNumber == vehicle.VehicleNumber))
            {
                context.Vehicles.Add(vehicle);
            }
        }

        foreach (var driver in sampleDrivers)
        {
            if (!context.Drivers.Any(d => d.Email == driver.Email || d.EmployeeCode == driver.EmployeeCode))
            {
                context.Drivers.Add(driver);
            }
        }

        context.SaveChanges();

        // Ensure driver statuses include assigned and active categories used by the UI.
        var drivers = context.Drivers.OrderBy(d => d.CreatedAt).ToList();
        for (int i = 0; i < drivers.Count; i++)
        {
            if (i < 3 && drivers[i].Status != "Assigned")
            {
                drivers[i].Status = "Assigned";
                drivers[i].UpdatedAt = now;
                drivers[i].UpdatedBy = "seeder";
            }
        }

        context.SaveChanges();

        var allVehicles = context.Vehicles.OrderBy(v => v.VehicleNumber).ToList();
        var maintenanceTypes = new[] { "Oil Change", "Brake Inspection", "Tire Rotation", "Engine Tune-Up", "Emission Check" };
        var maintenanceStatuses = new[] { "Completed", "Scheduled", "In Progress" };
        var maintenanceVendors = new[] { "AutoCare", "BrakeMasters", "TirePros", "EngineWorks", "EmissionPlus" };

        for (var index = 0; index < allVehicles.Count; index++)
        {
            var vehicle = allVehicles[index];
            var existingMaint = context.MaintenanceRecords.Count(m => m.VehicleId == vehicle.Id);
            for (int m = existingMaint; m < 3; m++)
            {
                var status = maintenanceStatuses[m % maintenanceStatuses.Length];
                var completedDate = status == "Completed" || status == "In Progress"
                    ? (DateTime?)now.AddDays(-14 + (m * 7))
                    : null;

                context.MaintenanceRecords.Add(new MaintenanceRecord
                {
                    Id = Guid.NewGuid(),
                    VehicleId = vehicle.Id,
                    ServiceType = maintenanceTypes[m % maintenanceTypes.Length],
                    Description = $"Sample {maintenanceTypes[m % maintenanceTypes.Length].ToLower()} for {vehicle.VehicleNumber}",
                    ScheduledDate = now.AddDays(-21 + (m * 10)),
                    CompletedDate = completedDate,
                    Cost = 120.00m + (m * 85.00m) + (index * 10),
                    Vendor = maintenanceVendors[m % maintenanceVendors.Length],
                    Status = status,
                    CreatedAt = now,
                    CreatedBy = "system"
                });
            }

            var existingFuel = context.FuelRecords.Count(f => f.VehicleId == vehicle.Id);
            var baseOdometer = 15000 + (index * 750);
            for (int f = existingFuel; f < 4; f++)
            {
                context.FuelRecords.Add(new FuelRecord
                {
                    Id = Guid.NewGuid(),
                    VehicleId = vehicle.Id,
                    FuelDate = now.AddDays(-7 * (f + 1)),
                    Quantity = 80 + (f * 15) + (index % 3 * 5),
                    Cost = 4200.00m + (f * 320.00m) + (index * 25),
                    OdometerReading = baseOdometer + (f * 650),
                    CreatedAt = now,
                    CreatedBy = "system"
                });
            }
        }

        context.SaveChanges();
    }
}
