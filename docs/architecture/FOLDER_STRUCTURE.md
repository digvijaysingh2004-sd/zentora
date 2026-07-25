# Project Directory & Folder Layout 📂

Zentora HRMS follows a standard enterprise-level **ASP.NET MVC 5** directory layout.

---

## 🗂️ Global Workspace Structure

Below is the directory tree of the root directory and the main application project:

```
zentora/                         # Solution Root
├── zentora.sln                  # Visual Studio Solution File
├── docs/                        # System Documentation Directory
│   ├── architecture/            # Architecture & Design Docs
│   └── development/             # Sprints & Development Guidelines
└── zentoraHRMS/                 # Main Web Application Project Folder
    ├── App_Start/               # Configuration Registrations
    ├── Controllers/             # MVC Controllers (Handlers)
    ├── Filters/                 # Global Action Interceptors (Filters)
    ├── Helpers/                 # Core Utility Classes
    ├── Models/                  # Data Transfer Objects (DTOs) & Data Models
    ├── Views/                   # Razor Page Markup files (.cshtml)
    │   ├── Shared/              # Shared Pages & Common Layout components
    │   └── Staff/               # Directory-specific view templates
    ├── Content/                 # Stylesheets, vendor plugins, & CSS assets
    ├── Scripts/                 # Front-end JavaScript modules
    └── Web.config               # Core application and server settings
```

---

## 🔍 Directory Breakdown

### 1. `App_Start/`
Defines initialization rules launched when the IIS web server starts up:
*   `BundleConfig.cs`: Manages static files bundling and minification (CSS, JS).
*   `FilterConfig.cs`: Registers global filters (e.g. `HandleErrorAttribute`, `PermissionActionFilter`).
*   `RouteConfig.cs`: Defines default routing rules (e.g., matching `/Controller/Action/Id`).

### 2. `Controllers/`
Contains C# controller classes that process client requests. Key modules include:
*   `AuthController.cs`: Manages login, authentication validations, session setup, and logout.
*   `StaffController.cs`: Handles operations regarding employee directory records and credentials management.
*   `HRController.cs`: Handles dynamic company configurations like departments, locations, and office locations.

### 3. `Filters/`
Hosts security filter classes:
*   `PermissionActionFilter.cs`: The core route permission interceptor that evaluates user access for every request path.

### 4. `Helpers/`
Utility classes providing reusable services:
*   `PermissionManager.cs`: Direct connectivity helper that loads, evaluates, and caches RBAC system settings from database configurations.

### 5. `Views/`
Contains Razor frontend templates.
*   `Shared/`: Contains layout files (e.g., `_Layout.cshtml`, `_Sidebar.cshtml`, `Header.cshtml`) used across the application to render unified layouts.
*   `Staff/`: Razor pages regarding directory components (`Employees.cshtml`, `Users.cshtml`).
