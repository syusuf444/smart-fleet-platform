# AppFlow.md

# Smart Fleet Platform - Application Flow

## Overview

Smart Fleet Platform is an enterprise fleet management system that enables organizations to manage vehicles, drivers, trips, maintenance, fuel consumption, geofencing, alerts, and AI-powered insights.

---

# User Roles

## Super Admin

- Manage organizations
- Manage subscription plans
- View platform analytics
- Manage all users

## Fleet Manager

- Manage vehicles
- Manage drivers
- Assign trips
- View reports
- Manage maintenance schedules
- Monitor fleet health

## Dispatcher

- Create trips
- Assign vehicles
- Assign drivers
- Monitor active trips

## Driver

- View assigned trips
- Update trip status
- Report incidents
- View vehicle details

---

# Authentication Flow

## Login

User enters:

- Email
- Password

System validates credentials.

### Success

Generate:

- JWT Access Token
- Refresh Token

Redirect to Dashboard

### Failure

Show:

- Invalid Credentials

---

# User Registration Flow

Admin creates user.

Input:

- Name
- Email
- Role
- Password

System:

- Creates identity record
- Sends welcome email (future)

---

# Dashboard Flow

After login:

Dashboard loads:

- Fleet Summary
- Active Vehicles
- Active Trips
- Drivers Available
- Vehicles Under Maintenance
- Fuel Statistics
- Alerts

---

# Vehicle Management Flow

## Vehicle List

Fleet Manager opens:

Fleet → Vehicles

System displays:

- Vehicle Number
- Vehicle Type
- Status
- Driver Assigned
- Mileage

Actions:

- Add Vehicle
- Edit Vehicle
- Delete Vehicle
- View Details

---

## Add Vehicle Flow

User clicks:

Add Vehicle

Enter:

- Registration Number
- Make
- Model
- Year
- Vehicle Type
- Fuel Type
- Capacity

Submit

System:

- Validates input
- Saves vehicle
- Publishes Kafka Event

VehicleCreated Event

---

## Edit Vehicle Flow

User updates:

- Vehicle Information
- Status
- Capacity

System:

- Updates database
- Publishes Kafka Event

VehicleUpdated Event

---

## Delete Vehicle Flow

User selects vehicle.

System:

- Soft Delete
- Publish VehicleDeleted Event

---

# Driver Management Flow

## Driver List

Fleet → Drivers

Display:

- Driver Name
- License Number
- Phone Number
- Status

Actions:

- Add
- Edit
- Deactivate

---

## Driver Assignment Flow

Fleet Manager selects:

Vehicle

Select:

Driver

Assign

System:

- Creates assignment
- Logs history
- Publishes Kafka Event

---

# Trip Management Flow

## Create Trip

Dispatcher creates trip.

Input:

- Source
- Destination
- Driver
- Vehicle
- Start Date
- Expected End Date

Submit

System:

- Creates trip
- Updates vehicle status
- Publishes Kafka Event

TripCreated

---

## Start Trip

Driver clicks:

Start Trip

System:

- Updates status
- Records start timestamp

Status:

In Progress

---

## Complete Trip

Driver clicks:

Complete Trip

System records:

- End Time
- Distance
- Fuel Consumed

Status:

Completed

Publish:

TripCompleted Event

---

# Maintenance Management Flow

## Maintenance Dashboard

Shows:

- Upcoming Services
- Overdue Services
- Maintenance Cost

---

## Schedule Maintenance

Fleet Manager enters:

- Vehicle
- Service Type
- Service Date
- Vendor

Submit

System:

- Creates maintenance record
- Generates reminder

---

## Maintenance Completion

Update:

- Cost
- Service Notes
- Service Date

Vehicle status returns to:

Active

---

# Fuel Management Flow

## Fuel Entry

Input:

- Vehicle
- Fuel Quantity
- Cost
- Odometer Reading

Submit

System calculates:

- Fuel Efficiency
- Fuel Cost Trends

---

# GPS Tracking Flow

## Live Tracking

Fleet Manager opens:

Live Map

System displays:

- Vehicle Location
- Speed
- Route
- Driver

Updates every few seconds

---

# Geofence Flow

## Create Geofence

Input:

- Name
- Latitude
- Longitude
- Radius

Save

---

## Geofence Event

Vehicle enters area

System generates:

- Entry Event

Vehicle exits area

System generates:

- Exit Event

Kafka Event Published

---

# Notification Flow

System generates alerts for:

- Maintenance Due
- Vehicle Offline
- Fuel Anomaly
- Geofence Violation
- Driver Behavior

Notification Channels:

- In-App
- Email
- SMS (Future)

---

# Reporting Flow

Reports Available:

## Fleet Utilization

Shows:

- Vehicle Usage
- Idle Time

## Fuel Analytics

Shows:

- Fuel Trends
- Cost Analysis

## Maintenance Analytics

Shows:

- Maintenance Cost
- Downtime

## Driver Performance

Shows:

- Trip Efficiency
- Safety Score

---

# AI Module Flow

## Fleet Health Prediction

Input:

- Vehicle Data
- Maintenance History
- Fuel Consumption

AI predicts:

- Failure Risk
- Service Recommendation

---

## Route Optimization

Input:

- Source
- Destination
- Traffic Data

AI generates:

- Optimal Route
- ETA

---

## Fleet Assistant

User asks:

"Which vehicles require maintenance?"

AI queries platform.

Returns:

- Vehicle List
- Risk Level
- Recommended Actions

---

# Audit Logging Flow

Every action logs:

- User
- Action
- Timestamp
- Entity
- Old Value
- New Value

Stored for compliance and tracking.

---

# Event Driven Architecture Flow

Vehicle Created
↓
Kafka Event
↓
Notification Service
↓
Analytics Service
↓
Audit Service

Vehicle Updated
↓
Kafka Event
↓
Analytics Update

Trip Completed
↓
Kafka Event
↓
Fuel Analytics
↓
Reporting Service

Maintenance Completed
↓
Kafka Event
↓
AI Prediction Engine

---

# Future Mobile App Flow

Driver Login

↓

View Assigned Trips

↓

Start Trip

↓

Navigation

↓

Complete Trip

↓

Upload Proof of Delivery

↓

Trip Closed
