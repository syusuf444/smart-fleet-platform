# Design.md

# Smart Fleet Platform - UI/UX Design Guidelines

## Design Philosophy

Smart Fleet Platform should feel:

* Modern
* Enterprise-grade
* Clean
* Fast
* Data-driven
* Professional

Primary users are Fleet Managers and Operations Teams who spend long hours using the application.

The UI must prioritize:

* Readability
* Efficiency
* Minimal clicks
* Clear visual hierarchy

---

# Design Language

## Style

Modern SaaS Dashboard

Inspired by:

* Azure Portal
* Microsoft Fabric
* Datadog
* Jira
* Uber Fleet
* Samsara

Characteristics:

* Clean layouts
* Card-based design
* Minimal clutter
* Consistent spacing
* Clear data visualization

---

# Color Palette

## Primary

Blue

```css
#2563EB
```

Used For:

* Buttons
* Links
* Active Navigation
* Highlights

---

## Secondary

Slate Gray

```css
#475569
```

Used For:

* Labels
* Secondary Text

---

## Success

Green

```css
#16A34A
```

Used For:

* Active Status
* Success Messages

---

## Warning

Amber

```css
#F59E0B
```

Used For:

* Maintenance Due
* Warning Alerts

---

## Error

Red

```css
#DC2626
```

Used For:

* Errors
* Critical Alerts

---

## Background

```css
#F8FAFC
```

---

## Card Background

```css
#FFFFFF
```

---

## Border Color

```css
#E2E8F0
```

---

# Typography

## Font Family

Primary:

```text
Inter
```

Fallback:

```text
Segoe UI
Roboto
sans-serif
```

---

## Heading Sizes

### Page Title

32px

Weight:

700

---

### Section Title

24px

Weight:

600

---

### Card Title

18px

Weight:

600

---

### Body Text

14px

Weight:

400

---

### Table Content

13px

Weight:

400

---

# Layout Standards

## Maximum Width

```text
1440px
```

---

## Grid System

12 Column Grid

---

## Page Padding

```text
24px
```

---

## Card Padding

```text
20px
```

---

## Border Radius

```text
12px
```

---

# Navigation Design

## Sidebar Navigation

Position:

Left Side

Width:

```text
260px
```

Menu Items:

* Dashboard
* Vehicles
* Drivers
* Trips
* Maintenance
* Fuel
* Analytics
* AI Assistant
* Settings

---

## Top Header

Contains:

* Search
* Notifications
* User Profile
* Theme Switch

Height:

```text
64px
```

---

# Dashboard Design

## KPI Cards

Display:

* Total Vehicles
* Active Vehicles
* Active Trips
* Drivers Available
* Maintenance Due

Layout:

5 Cards Per Row

---

## Charts Section

Charts:

* Fleet Utilization
* Fuel Trends
* Maintenance Cost
* Trip Analytics

---

## Recent Activity

Timeline Widget

Shows:

* Vehicle Created
* Driver Assigned
* Trip Started
* Maintenance Completed

---

# Vehicle Module UI

## Vehicle List Page

Components:

* Search Bar
* Filters
* Data Table

Columns:

* Registration Number
* Vehicle Type
* Status
* Driver
* Mileage
* Actions

Actions:

* View
* Edit
* Delete

---

## Vehicle Details Page

Sections:

### Vehicle Information

### Driver Information

### Maintenance History

### Fuel Records

### Trip History

---

# Driver Module UI

## Driver List

Columns:

* Name
* License Number
* Phone
* Status

Actions:

* Edit
* Deactivate
* Assign Vehicle

---

# Trip Module UI

## Trip Dashboard

Cards:

* Active Trips
* Scheduled Trips
* Completed Trips

---

## Trip Details

Map View

Displays:

* Route
* Current Position
* ETA

---

# Maintenance Module UI

## Maintenance Dashboard

Widgets:

* Upcoming Services
* Overdue Services
* Maintenance Cost

---

## Maintenance Table

Columns:

* Vehicle
* Service Type
* Scheduled Date
* Status

---

# Fuel Module UI

## Fuel Dashboard

Charts:

* Monthly Consumption
* Cost Analysis
* Fuel Efficiency

---

# AI Assistant UI

## AI Chat Panel

Layout:

Right Side Drawer

Features:

* Natural Language Queries
* Fleet Insights
* Recommendations

Example Queries:

"Show vehicles needing maintenance"

"Which driver has highest utilization?"

"Predict next service date"

---

# Tables Design

## Table Style

Header:

```css
background: #F1F5F9;
font-weight: 600;
```

Row Hover:

```css
background: #F8FAFC;
```

---

# Form Design

## Inputs

Height:

```text
44px
```

Border Radius:

```text
8px
```

---

## Buttons

Primary

Blue Filled

Secondary

Outlined

Danger

Red Filled

---

# Status Badges

## Active

Green

---

## Inactive

Gray

---

## Maintenance Due

Amber

---

## Critical

Red

---

## In Progress

Blue

---

# Notifications

Position:

Top Right

Types:

* Success
* Error
* Warning
* Information

Auto Close:

5 Seconds

---

# Loading States

Use:

* Skeleton Loaders
* Spinner Only For Actions

Never show blank screens.

---

# Empty States

Every module must include:

* Illustration
* Helpful Message
* Call To Action Button

Example:

"No Vehicles Found"

Button:

"Add Vehicle"

---

# Dark Mode

Supported:

Yes

Dark Background:

```css
#0F172A
```

Card Background:

```css
#1E293B
```

Text:

```css
#F8FAFC
```

---

# Mobile Responsiveness

Breakpoints

## Mobile

0 - 767px

## Tablet

768 - 1023px

## Desktop

1024px+

---

# Accessibility Standards

WCAG AA Compliance

Requirements:

* Keyboard Navigation
* Proper Contrast Ratio
* Screen Reader Support
* Visible Focus States

---

# React UI Standards

Component Library

Material UI

Folder Structure

```text
src/
 ├── pages/
 ├── components/
 ├── layouts/
 ├── features/
 ├── hooks/
 ├── services/
 ├── store/
 ├── routes/
 └── assets/
```

---

# Future UI Enhancements

* Real-Time Vehicle Map
* AI Copilot Panel
* Drag & Drop Trip Planner
* Fleet Heatmaps
* Driver Scorecards
* Predictive Maintenance Dashboard

---

# Design Goal

The application should look like a modern enterprise SaaS product that could realistically compete with:

* Samsara
* Fleetio
* Verizon Connect
* Motive
* Geotab

while remaining simple, clean, and highly usable for daily fleet operations.
