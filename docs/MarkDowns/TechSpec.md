# TechSpec.md

# Smart Fleet Platform - Technical Specification

## Project Overview

Smart Fleet Platform is a cloud-ready, microservices-based fleet management system designed for vehicle tracking, trip management, maintenance scheduling, fuel analytics, driver management, and AI-powered fleet intelligence.

The platform follows Clean Architecture principles, CQRS pattern, Event-Driven Architecture, and Domain-Driven Design concepts.

---

# Architecture

## Architecture Style

* Microservices Architecture
* Event-Driven Architecture
* Clean Architecture
* CQRS Pattern
* Repository Pattern
* Domain Driven Design (DDD)

---

# Technology Stack

## Backend

* .NET 10
* ASP.NET Core Web API
* C#
* Entity Framework Core
* MediatR
* FluentValidation
* AutoMapper

---

## Frontend

* React
* TypeScript
* Redux Toolkit
* React Router
* Axios
* Material UI

---

## Database

* Microsoft SQL Server 2022

Databases:

### Identity Database

SmartFleetIdentityDb

Stores:

* Users
* Roles
* Permissions
* Refresh Tokens

### Fleet Database

SmartFleetFleetDb

Stores:

* Vehicles
* Drivers
* Trips
* Maintenance
* Fuel Records

---

## Messaging

Apache Kafka

Purpose:

* Inter-service communication
* Event publishing
* Event consumption

Topics:

* vehicle-created
* vehicle-updated
* vehicle-deleted
* trip-created
* trip-completed
* maintenance-created
* maintenance-completed

---

## API Gateway

Ocelot

Responsibilities:

* Centralized Routing
* Authentication
* Rate Limiting
* Request Aggregation

---

## Authentication

JWT Authentication

Token Types:

### Access Token

* Validity: 60 Minutes

### Refresh Token

* Validity: 7 Days

---

## Logging

Serilog

Sinks:

* Console
* File
* Seq

Log Levels:

* Information
* Warning
* Error
* Critical

---

## Monitoring

### Current

* Seq

### Future

* Prometheus
* Grafana

---

## Containerization

Docker

Services:

* SQL Server
* Kafka
* Zookeeper
* Seq

Managed through:

* Docker Compose

---

# Microservices

---

## Identity Service

### Purpose

Authentication and Authorization

### Port

5057

### Database

SmartFleetIdentityDb

### Responsibilities

* Login
* Register
* JWT Generation
* Refresh Tokens
* Role Management

### APIs

POST /api/auth/login

POST /api/auth/register

POST /api/auth/refresh-token

GET /api/users

---

## Fleet Service

### Purpose

Fleet Operations

### Port

5081

### Database

SmartFleetFleetDb

### Responsibilities

* Vehicle Management
* Driver Management
* Trip Management
* Maintenance Management

### APIs

GET /api/vehicles

GET /api/vehicles/{id}

POST /api/vehicles

PUT /api/vehicles/{id}

DELETE /api/vehicles/{id}

---

## API Gateway

### Purpose

Single Entry Point

### Port

5000

### Responsibilities

* Route Requests
* JWT Validation
* Service Discovery (Future)

---

# Security

## Authentication

JWT Bearer Token

Authorization Header:

Bearer {token}

---

## Password Security

ASP.NET Identity Password Hasher

---

## API Security

* HTTPS
* JWT Validation
* Role Based Authorization

Roles:

* SuperAdmin
* FleetManager
* Dispatcher
* Driver

---

# Kafka Event Contracts

## VehicleCreatedEvent

```json
{
  "vehicleId": 1,
  "registrationNumber": "MH01AB1234",
  "vehicleType": "Truck",
  "createdAt": "2026-01-01T10:00:00Z"
}
```

## VehicleUpdatedEvent

```json
{
  "vehicleId": 1,
  "updatedAt": "2026-01-01T10:00:00Z"
}
```

## TripCompletedEvent

```json
{
  "tripId": 100,
  "distance": 350,
  "fuelConsumed": 40
}
```

---

# API Standards

## Naming Convention

Controllers

* VehicleController
* DriverController
* TripController

Services

* VehicleService
* DriverService
* TripService

Repositories

* VehicleRepository
* DriverRepository
* TripRepository

---

## Response Format

Success

```json
{
  "success": true,
  "message": "Vehicle created successfully",
  "data": {}
}
```

Failure

```json
{
  "success": false,
  "message": "Validation failed",
  "errors": []
}
```

---

# Validation

Implemented using:

FluentValidation

Examples:

* Registration Number Required
* Vehicle Type Required
* Driver License Required

---

# Exception Handling

Global Exception Middleware

Handles:

* Validation Errors
* Business Exceptions
* Database Exceptions
* Unexpected Errors

Response:

```json
{
  "success": false,
  "message": "An error occurred"
}
```

---

# Caching (Future)

Redis

Use Cases:

* Dashboard Statistics
* Frequently Accessed Vehicles
* Fleet Summary Data

---

# AI Integration

Provider:

Azure OpenAI

Models:

* GPT-4o
* GPT-4.1

Use Cases:

* Fleet Assistant
* Route Optimization
* Predictive Maintenance
* Natural Language Queries

---

# CI/CD

Platform:

Azure DevOps

Pipeline Stages:

1. Build
2. Test
3. Docker Build
4. Security Scan
5. Deploy

---

# Testing Strategy

## Unit Testing

Framework:

xUnit

Libraries:

* FluentAssertions
* Moq

Coverage Target:

80%+

---

## Integration Testing

Framework:

ASP.NET Core Test Host

Database:

SQL Server Test Database

---

# Development Standards

* Clean Architecture Mandatory
* CQRS for Business Operations
* Repository Pattern
* Dependency Injection
* Async/Await Everywhere
* No Business Logic in Controllers
* API First Design
* Event Publishing for Critical Operations

---

# Current Project Status

Completed:

* Clean Architecture Setup
* Fleet Service API
* SQL Server Integration
* Docker Infrastructure
* Kafka Integration
* JWT Authentication
* Identity Service
* Ocelot Gateway Foundation
* Serilog Logging

In Progress:

* API Gateway Routing
* Driver Module
* Trip Module

Planned:

* React Frontend
* AI Assistant
* Predictive Maintenance
* Real-time GPS Tracking
* Redis Caching
* Kubernetes Deployment
* Azure Deployment
