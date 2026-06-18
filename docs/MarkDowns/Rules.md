# Rules.md

# Smart Fleet Platform - AI Development Rules

Version: 1.0

Last Updated: 2026-06-15

---

# Purpose

These rules are mandatory for all AI agents and developers working on Smart Fleet Platform.

Goals:

* Maintain clean architecture
* Maintain enterprise standards
* Prevent inconsistent implementations
* Prevent duplicated logic
* Ensure production-ready code

---

# General Rules

## Rule 1

Never break Clean Architecture.

Allowed Dependency Flow:

API
↓
Application
↓
Domain

Infrastructure may depend on Application and Domain.

Domain must never depend on any other layer.

---

## Rule 2

Always follow SOLID principles.

Every new implementation must follow:

* Single Responsibility Principle
* Open Closed Principle
* Liskov Substitution Principle
* Interface Segregation Principle
* Dependency Inversion Principle

---

## Rule 3

Avoid code duplication.

If logic already exists:

Reuse it.

Do not create duplicate implementations.

---

## Rule 4

Use dependency injection everywhere.

Never instantiate services manually.

Bad:

new VehicleService()

Good:

constructor injection

---

## Rule 5

No business logic inside Controllers.

Controllers should only:

* Receive requests
* Validate request models
* Send commands/queries
* Return responses

Business logic belongs in handlers.

---

# Backend Rules

## Rule 6

Every feature must use CQRS.

Required:

Commands

Queries

Handlers

Validators

DTOs

Controllers

Repositories

---

## Rule 7

All requests must use MediatR.

Controllers must not directly call repositories.

Flow:

Controller
→ MediatR
→ Handler
→ Repository

---

## Rule 8

Use FluentValidation.

Never place validation logic inside controllers.

Create dedicated validators.

---

## Rule 9

Repository Pattern is mandatory.

Database access only through repositories.

Never access DbContext directly from controllers.

---

## Rule 10

Use async/await everywhere.

Bad:

.SaveChanges()

Good:

.SaveChangesAsync()

---

## Rule 11

Every API endpoint must return consistent responses.

Standard Response:

{
"success": true,
"message": "Vehicle created successfully",
"data": {}
}

Error Response:

{
"success": false,
"message": "Validation failed",
"errors": []
}

---

# Database Rules

## Rule 12

Use Entity Framework Core.

No raw SQL unless performance requires it.

---

## Rule 13

Every schema change requires migration.

Command:

dotnet ef migrations add MigrationName

Never modify database manually.

---

## Rule 14

All tables must include:

CreatedAt

UpdatedAt

CreatedBy

UpdatedBy

IsDeleted

---

## Rule 15

Use soft delete.

Never physically delete records.

Use:

IsDeleted = true

---

## Rule 16

Primary Keys:

Use Guid

Example:

VehicleId

DriverId

UserId

---

# API Rules

## Rule 17

Every API must have Swagger documentation.

Required:

* Summary
* Request examples
* Response examples

---

## Rule 18

JWT Authentication required for secured APIs.

Public Endpoints:

* Login
* Register
* Health Check

Everything else requires authentication.

---

## Rule 19

Use API Versioning.

Format:

/api/v1/vehicles

---

## Rule 20

Global Exception Middleware mandatory.

No try-catch blocks in controllers.

---

# Kafka Rules

## Rule 21

All domain events must be published to Kafka.

Examples:

VehicleCreated

VehicleUpdated

DriverCreated

MaintenanceScheduled

---

## Rule 22

Event Names

Format:

entity.action

Examples:

vehicle.created

vehicle.updated

driver.created

---

## Rule 23

Kafka messages must use DTOs.

Never publish entity models directly.

---

# Logging Rules

## Rule 24

Use Serilog.

Never use Console.WriteLine.

---

## Rule 25

Log:

Information

Warnings

Errors

Critical Failures

---

## Rule 26

Never log:

Passwords

Tokens

Connection Strings

Sensitive Data

---

# Frontend Rules

## Rule 27

Frontend Stack

Mandatory:

React

TypeScript

Redux Toolkit

React Query

Material UI

---

## Rule 28

Never use JavaScript.

Use TypeScript only.

---

## Rule 29

Use Feature-Based Folder Structure.

Example:

features/

vehicles/

drivers/

maintenance/

---

## Rule 30

All API calls must go through a central API service.

No direct axios calls inside components.

---

## Rule 31

Components must be reusable.

Avoid duplicated UI.

---

# Security Rules

## Rule 32

Passwords must be hashed.

Use:

ASP.NET Identity

or

BCrypt

---

## Rule 33

Never store secrets in code.

Use:

appsettings

environment variables

Azure Key Vault

---

## Rule 34

Enable CORS properly.

Never allow unrestricted origins in production.

---

## Rule 35

Validate every user input.

Assume all inputs are malicious.

---

# AI Rules

## Rule 36

Before implementing anything:

Read:

1. PRD.md
2. TechSpec.md
3. AppFlow.md
4. Design.md
5. Schema.md
6. ImplementationPlan.md
7. Tracker.md
8. Rules.md

---

## Rule 37

Update Tracker.md after every completed task.

Mandatory.

---

## Rule 38

Do not rewrite existing modules unless fixing bugs.

Extend existing functionality.

---

## Rule 39

Always preserve Clean Architecture.

No shortcuts.

---

## Rule 40

When uncertain:

Choose enterprise-grade implementation.

Never choose quick hacks.

---

# Current Development Target

Current Phase:

Phase 2

Current Module:

Driver Management

Next Tasks:

* Driver Entity
* Driver Repository
* Driver Commands
* Driver Queries
* Driver Validators
* Driver Controller
* Driver Migration
* Driver Kafka Events

After Driver Module:

1. Maintenance Module
2. Fuel Module
3. Notification Service
4. React Frontend
5. AI Service
6. Azure Deployment

---

# Success Criteria

The platform should be capable of:

* Managing thousands of vehicles
* Supporting multiple organizations
* Real-time Kafka messaging
* AI-powered fleet insights
* Enterprise-grade security
* Cloud-native deployment
* Production-ready scalability

No implementation should compromise these goals.
