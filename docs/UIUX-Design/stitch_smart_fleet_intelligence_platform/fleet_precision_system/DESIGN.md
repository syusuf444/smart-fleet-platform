---
name: Fleet Precision System
colors:
  surface: '#faf8ff'
  surface-dim: '#d9d9e5'
  surface-bright: '#faf8ff'
  surface-container-lowest: '#ffffff'
  surface-container-low: '#f3f3fe'
  surface-container: '#ededf9'
  surface-container-high: '#e7e7f3'
  surface-container-highest: '#e1e2ed'
  on-surface: '#191b23'
  on-surface-variant: '#434655'
  inverse-surface: '#2e3039'
  inverse-on-surface: '#f0f0fb'
  outline: '#737686'
  outline-variant: '#c3c6d7'
  surface-tint: '#0053db'
  primary: '#004ac6'
  on-primary: '#ffffff'
  primary-container: '#2563eb'
  on-primary-container: '#eeefff'
  inverse-primary: '#b4c5ff'
  secondary: '#565e74'
  on-secondary: '#ffffff'
  secondary-container: '#dae2fd'
  on-secondary-container: '#5c647a'
  tertiary: '#943700'
  on-tertiary: '#ffffff'
  tertiary-container: '#bc4800'
  on-tertiary-container: '#ffede6'
  error: '#ba1a1a'
  on-error: '#ffffff'
  error-container: '#ffdad6'
  on-error-container: '#93000a'
  primary-fixed: '#dbe1ff'
  primary-fixed-dim: '#b4c5ff'
  on-primary-fixed: '#00174b'
  on-primary-fixed-variant: '#003ea8'
  secondary-fixed: '#dae2fd'
  secondary-fixed-dim: '#bec6e0'
  on-secondary-fixed: '#131b2e'
  on-secondary-fixed-variant: '#3f465c'
  tertiary-fixed: '#ffdbcd'
  tertiary-fixed-dim: '#ffb596'
  on-tertiary-fixed: '#360f00'
  on-tertiary-fixed-variant: '#7d2d00'
  background: '#faf8ff'
  on-background: '#191b23'
  surface-variant: '#e1e2ed'
typography:
  display:
    fontFamily: Inter
    fontSize: 30px
    fontWeight: '700'
    lineHeight: 38px
    letterSpacing: -0.02em
  headline-md:
    fontFamily: Inter
    fontSize: 20px
    fontWeight: '600'
    lineHeight: 28px
  headline-sm:
    fontFamily: Inter
    fontSize: 16px
    fontWeight: '600'
    lineHeight: 24px
  body-lg:
    fontFamily: Inter
    fontSize: 16px
    fontWeight: '400'
    lineHeight: 24px
  body-md:
    fontFamily: Inter
    fontSize: 14px
    fontWeight: '400'
    lineHeight: 20px
  body-sm:
    fontFamily: Inter
    fontSize: 13px
    fontWeight: '400'
    lineHeight: 18px
  label-bold:
    fontFamily: Inter
    fontSize: 12px
    fontWeight: '600'
    lineHeight: 16px
    letterSpacing: 0.05em
  mono-data:
    fontFamily: Inter
    fontSize: 13px
    fontWeight: '500'
    lineHeight: 18px
    letterSpacing: -0.01em
rounded:
  sm: 0.125rem
  DEFAULT: 0.25rem
  md: 0.375rem
  lg: 0.5rem
  xl: 0.75rem
  full: 9999px
spacing:
  container-max: 1440px
  sidebar-width: 260px
  gutter: 16px
  margin-desktop: 24px
  stack-xs: 4px
  stack-sm: 8px
  stack-md: 16px
  stack-lg: 24px
---

## Brand & Style
This design system is engineered for high-utility enterprise environments where data density and clarity are paramount. The aesthetic is **Corporate Modern**, drawing inspiration from cloud infrastructure consoles. It prioritizes a logical information hierarchy, utilizing a structured layout to manage complex fleet logistics.

The brand personality is reliable, systematic, and precise. It avoids unnecessary ornamentation, focusing instead on functional aesthetics that reduce cognitive load for operators managing hundreds of assets simultaneously. The emotional response should be one of control, stability, and professional efficiency.

## Colors
The palette is rooted in a professional "Deep Corporate Blue" for structural elements and navigation, providing a grounded frame for data. 

- **Primary Action**: `#2563EB` is used exclusively for interactive elements like primary buttons and active selection states.
- **Surface**: The main workspace uses `#F8FAFC`, providing a cool, low-strain background that allows white cards to pop.
- **Semantic Logic**: Status colors are vibrant but used purposefully. Emerald, Amber, and Crimson are reserved for asset health and alerts to ensure immediate visual triage in high-density tables.

## Typography
Inter is used across the entire system for its exceptional legibility in data-heavy contexts. 

- **Data Tables**: Use `body-sm` or `mono-data` for row content to maximize information density without sacrificing readability.
- **KPIs**: Use `display` for primary metrics to ensure they stand out against the surrounding administrative text.
- **Hierarchy**: Headlines use a tighter letter-spacing and heavier weights to provide clear section anchoring.

## Layout & Spacing
The system utilizes a **12-column fluid grid** nested within a fixed 1440px container for desktop. 

- **Sidebar**: A fixed-width (260px) high-contrast left navigation persists across all views.
- **Information Density**: A 4px baseline grid governs all spacing. Components should default to "Compact" spacing (8px internal padding) to allow more rows of data per screen.
- **Responsibility**: On tablet, the sidebar collapses to an icon-only rail. On mobile, the grid reflows to a single column with 16px margins.

## Elevation & Depth
Depth is created through a combination of **Tonal Layers** and **Soft Ambient Shadows**.

1. **Level 0 (Background)**: `#F8FAFC` - The base canvas.
2. **Level 1 (Cards/Tables)**: Pure White `#FFFFFF` with a very subtle 1px border (`#E2E8F0`) and a soft, diffused shadow (`0 1px 3px rgba(15, 23, 42, 0.08)`).
3. **Level 2 (Modals/Popovers)**: White with a more pronounced shadow to indicate focus and interaction priority.
4. **Interactive States**: Elements like table rows use a subtle background tint (`#F1F5F9`) on hover rather than a shadow change, maintaining a flat, professional feel during navigation.

## Shapes
This design system uses a **Soft** shape language. 

- **Standard Elements**: Buttons, input fields, and small cards use a `0.25rem` (4px) radius. This provides a modern touch while maintaining the "precise" architectural feel of an enterprise tool.
- **Badges**: Status badges (Active, Maintenance, Critical) use a slightly higher `0.5rem` radius to distinguish them from interactive buttons.
- **KPI Widgets**: Use the standard `0.25rem` to ensure they align perfectly with the grid.

## Components

### Sidebar
- **Style**: High-contrast, using `#0F172A` background. 
- **Links**: Active states use a left-edge 4px `#2563EB` border and a subtle background highlight.

### Data Tables
- **Header**: Light gray background (`#F1F5F9`) with `label-bold` typography.
- **Rows**: 40px minimum height. On hover, apply a background color of `#F8FAFC`.
- **Dividers**: 1px solid `#E2E8F0`.

### Status Badges
- **Active**: Soft green background with dark green text.
- **Alerts**: Use a "Dot + Label" pattern where the dot carries the semantic color for quick scanning.

### Buttons
- **Primary**: Solid `#2563EB` with white text.
- **Secondary**: Ghost style with `#64748B` border and text.

### KPI Widgets
- **Structure**: White card, `headline-sm` title, `display` metric, and a small sparkline or percentage trend indicator at the bottom.

### Inputs
- **Field**: White background, 1px `#CBD5E1` border. On focus, 1px `#2563EB` border with a 2px soft blue outer glow.