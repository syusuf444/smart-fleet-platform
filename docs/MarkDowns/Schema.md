# Schema.md

# Smart Fleet Platform - Database Schema

## Database Strategy

The platform uses Database-per-Service architecture.

### Identity Database

```text
SmartFleetIdentityDb
```

### Fleet Database

```text
SmartFleetFleetDb
```

---

# Common Audit Columns

Every business table must contain:

```sql
Id UNIQUEIDENTIFIER PRIMARY KEY
CreatedBy NVARCHAR(100)
CreatedOn DATETIME2
ModifiedBy NVARCHAR(100)
ModifiedOn DATETIME2
IsDeleted BIT
```

---

# IDENTITY DATABASE

# Users

```sql
Users
```

| Column        | Type             |
| ------------- | ---------------- |
| Id            | UNIQUEIDENTIFIER |
| FirstName     | NVARCHAR(100)    |
| LastName      | NVARCHAR(100)    |
| Email         | NVARCHAR(255)    |
| UserName      | NVARCHAR(100)    |
| PhoneNumber   | NVARCHAR(20)     |
| PasswordHash  | NVARCHAR(MAX)    |
| IsActive      | BIT              |
| LastLoginDate | DATETIME2        |

---

# Roles

```sql
Roles
```

| Column      | Type             |
| ----------- | ---------------- |
| Id          | UNIQUEIDENTIFIER |
| Name        | NVARCHAR(100)    |
| Description | NVARCHAR(500)    |

Examples:

* SuperAdmin
* FleetManager
* Dispatcher
* Driver

---

# UserRoles

```sql
UserRoles
```

| Column | Type             |
| ------ | ---------------- |
| UserId | UNIQUEIDENTIFIER |
| RoleId | UNIQUEIDENTIFIER |

---

# RefreshTokens

```sql
RefreshTokens
```

| Column     | Type             |
| ---------- | ---------------- |
| Id         | UNIQUEIDENTIFIER |
| UserId     | UNIQUEIDENTIFIER |
| Token      | NVARCHAR(MAX)    |
| ExpiryDate | DATETIME2        |
| IsRevoked  | BIT              |

---

# FLEET DATABASE

# Vehicles

```sql
Vehicles
```

| Column             | Type             |
| ------------------ | ---------------- |
| Id                 | UNIQUEIDENTIFIER |
| RegistrationNumber | NVARCHAR(50)     |
| VIN                | NVARCHAR(100)    |
| Make               | NVARCHAR(100)    |
| Model              | NVARCHAR(100)    |
| Year               | INT              |
| VehicleType        | NVARCHAR(50)     |
| FuelType           | NVARCHAR(50)     |
| Capacity           | DECIMAL(18,2)    |
| OdometerReading    | DECIMAL(18,2)    |
| Status             | NVARCHAR(50)     |

Status Values:

* Active
* InTrip
* Maintenance
* Inactive

---

# Drivers

```sql
Drivers
```

| Column            | Type             |
| ----------------- | ---------------- |
| Id                | UNIQUEIDENTIFIER |
| EmployeeCode      | NVARCHAR(50)     |
| FirstName         | NVARCHAR(100)    |
| LastName          | NVARCHAR(100)    |
| PhoneNumber       | NVARCHAR(20)     |
| Email             | NVARCHAR(255)    |
| LicenseNumber     | NVARCHAR(100)    |
| LicenseExpiryDate | DATETIME2        |
| JoiningDate       | DATETIME2        |
| Status            | NVARCHAR(50)     |

Status:

* Active
* Assigned
* Inactive

---

# VehicleAssignments

Tracks driver assignment history.

```sql
VehicleAssignments
```

| Column       | Type             |
| ------------ | ---------------- |
| Id           | UNIQUEIDENTIFIER |
| VehicleId    | UNIQUEIDENTIFIER |
| DriverId     | UNIQUEIDENTIFIER |
| AssignedDate | DATETIME2        |
| ReleasedDate | DATETIME2 NULL   |

---

# Trips

```sql
Trips
```

| Column       | Type             |
| ------------ | ---------------- |
| Id           | UNIQUEIDENTIFIER |
| TripNumber   | NVARCHAR(50)     |
| VehicleId    | UNIQUEIDENTIFIER |
| DriverId     | UNIQUEIDENTIFIER |
| Source       | NVARCHAR(500)    |
| Destination  | NVARCHAR(500)    |
| StartDate    | DATETIME2        |
| EndDate      | DATETIME2        |
| Distance     | DECIMAL(18,2)    |
| FuelConsumed | DECIMAL(18,2)    |
| Status       | NVARCHAR(50)     |

Status:

* Scheduled
* InProgress
* Completed
* Cancelled

---

# TripLocations

Stores GPS tracking points.

```sql
TripLocations
```

| Column     | Type             |
| ---------- | ---------------- |
| Id         | UNIQUEIDENTIFIER |
| TripId     | UNIQUEIDENTIFIER |
| Latitude   | DECIMAL(18,8)    |
| Longitude  | DECIMAL(18,8)    |
| RecordedAt | DATETIME2        |

---

# MaintenanceRecords

```sql
MaintenanceRecords
```

| Column        | Type             |
| ------------- | ---------------- |
| Id            | UNIQUEIDENTIFIER |
| VehicleId     | UNIQUEIDENTIFIER |
| ServiceType   | NVARCHAR(100)    |
| Description   | NVARCHAR(MAX)    |
| ScheduledDate | DATETIME2        |
| CompletedDate | DATETIME2        |
| Cost          | DECIMAL(18,2)    |
| Vendor        | NVARCHAR(255)    |
| Status        | NVARCHAR(50)     |

Status:

* Scheduled
* InProgress
* Completed

---

# FuelRecords

```sql
FuelRecords
```

| Column          | Type             |
| --------------- | ---------------- |
| Id              | UNIQUEIDENTIFIER |
| VehicleId       | UNIQUEIDENTIFIER |
| FuelDate        | DATETIME2        |
| Quantity        | DECIMAL(18,2)    |
| Cost            | DECIMAL(18,2)    |
| OdometerReading | DECIMAL(18,2)    |

---

# Incidents

Driver-reported incidents.

```sql
Incidents
```

| Column       | Type             |
| ------------ | ---------------- |
| Id           | UNIQUEIDENTIFIER |
| VehicleId    | UNIQUEIDENTIFIER |
| DriverId     | UNIQUEIDENTIFIER |
| TripId       | UNIQUEIDENTIFIER |
| IncidentType | NVARCHAR(100)    |
| Description  | NVARCHAR(MAX)    |
| ReportedAt   | DATETIME2        |
| Severity     | NVARCHAR(50)     |

Severity:

* Low
* Medium
* High
* Critical

---

# Geofences

```sql
Geofences
```

| Column       | Type             |
| ------------ | ---------------- |
| Id           | UNIQUEIDENTIFIER |
| Name         | NVARCHAR(100)    |
| Latitude     | DECIMAL(18,8)    |
| Longitude    | DECIMAL(18,8)    |
| RadiusMeters | DECIMAL(18,2)    |

---

# GeofenceEvents

```sql
GeofenceEvents
```

| Column     | Type             |
| ---------- | ---------------- |
| Id         | UNIQUEIDENTIFIER |
| GeofenceId | UNIQUEIDENTIFIER |
| VehicleId  | UNIQUEIDENTIFIER |
| EventType  | NVARCHAR(50)     |
| EventTime  | DATETIME2        |

Event Types:

* Enter
* Exit

---

# Notifications

```sql
Notifications
```

| Column  | Type             |
| ------- | ---------------- |
| Id      | UNIQUEIDENTIFIER |
| UserId  | UNIQUEIDENTIFIER |
| Title   | NVARCHAR(255)    |
| Message | NVARCHAR(MAX)    |
| Type    | NVARCHAR(50)     |
| IsRead  | BIT              |

Types:

* Success
* Warning
* Error
* Info

---

# AuditLogs

Tracks all user activity.

```sql
AuditLogs
```

| Column     | Type             |
| ---------- | ---------------- |
| Id         | UNIQUEIDENTIFIER |
| UserId     | UNIQUEIDENTIFIER |
| Action     | NVARCHAR(255)    |
| EntityName | NVARCHAR(255)    |
| EntityId   | UNIQUEIDENTIFIER |
| OldValues  | NVARCHAR(MAX)    |
| NewValues  | NVARCHAR(MAX)    |
| ActionDate | DATETIME2        |

---

# AI TABLES

# FleetPredictions

Stores AI prediction history.

```sql
FleetPredictions
```

| Column           | Type             |
| ---------------- | ---------------- |
| Id               | UNIQUEIDENTIFIER |
| VehicleId        | UNIQUEIDENTIFIER |
| PredictionType   | NVARCHAR(100)    |
| PredictionResult | NVARCHAR(MAX)    |
| ConfidenceScore  | DECIMAL(5,2)     |
| GeneratedAt      | DATETIME2        |

Examples:

* Maintenance Prediction
* Failure Prediction
* Fuel Prediction

---

# RELATIONSHIPS

Vehicle

1 → Many Trips

Vehicle

1 → Many MaintenanceRecords

Vehicle

1 → Many FuelRecords

Vehicle

1 → Many VehicleAssignments

Driver

1 → Many Trips

Driver

1 → Many VehicleAssignments

Trip

1 → Many TripLocations

Trip

1 → Many Incidents

Vehicle

1 → Many Incidents

Vehicle

1 → Many GeofenceEvents

User

1 → Many Notifications

User

1 → Many AuditLogs

---

# INDEXES

Vehicles

```sql
IX_Vehicles_RegistrationNumber
IX_Vehicles_Status
```

Drivers

```sql
IX_Drivers_LicenseNumber
IX_Drivers_Status
```

Trips

```sql
IX_Trips_Status
IX_Trips_StartDate
IX_Trips_VehicleId
```

MaintenanceRecords

```sql
IX_MaintenanceRecords_Status
IX_MaintenanceRecords_ScheduledDate
```

FuelRecords

```sql
IX_FuelRecords_VehicleId
IX_FuelRecords_FuelDate
```

AuditLogs

```sql
IX_AuditLogs_ActionDate
IX_AuditLogs_UserId
```

---

# V1 MVP TABLES

Build First:

1. Users
2. Roles
3. UserRoles
4. Vehicles
5. Drivers
6. VehicleAssignments
7. Trips
8. MaintenanceRecords
9. FuelRecords

---

# V2 TABLES

Build Next:

1. TripLocations
2. Incidents
3. Notifications
4. AuditLogs
5. Geofences
6. GeofenceEvents

---

# V3 TABLES

Build Last:

1. FleetPredictions
2. AI Analytics Tables
3. ML Training Data Tables
