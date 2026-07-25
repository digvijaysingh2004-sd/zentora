# Architecture Overview 🏗️

Zentora HRMS is structured as an enterprise-grade **ASP.NET MVC 5** web application running on **.NET Framework 4.7.2** with a **Microsoft SQL Server** relational database.

---

## 🛠️ Architectural Blueprint

The application follows the classic Model-View-Controller (MVC) architectural pattern, separating logic, presentation, and data management cleanly:

```
┌────────────────────────────────────────────────────────┐
│                   Presentation Layer                   │
│         (Razor Views, HTML5, CSS3, jQuery, Bootstrap)  │
└───────────────────────────┬────────────────────────────┘
                            │ HTTP Requests
                            ▼
┌────────────────────────────────────────────────────────┐
│                   Controller Layer                     │
│        (Staff, HR, Attendance, Payroll, Auth, Modules) │
└───────────────────────────┬────────────────────────────┘
                            │ Model Bindings
                            ▼
┌────────────────────────────────────────────────────────┐
│                      Model Layer                       │
│             (ViewModel classes & DTOs)                 │
└───────────────────────────┬────────────────────────────┘
                            │ ADO.NET SqlClient (SQL/SP)
                            ▼
┌────────────────────────────────────────────────────────┐
│                   Data Storage Layer                   │
│                  (Microsoft SQL Server)                │
└────────────────────────────────────────────────────────┘
```

---

## 🔑 Key Layers & Design Patterns

### 1. Presentation Layer (Views)
*   **Razor Templates**: Renders dynamic server-side HTML.
*   **Asset Management**: Layouts are modularized with `@Html.Partial` (e.g., `Header.cshtml`) and `@Html.Action` (e.g., dynamic `SidebarController`).
*   **Client Validation**: jQuery handles front-end behavior and form validations (`validateForm`), combined with SweetAlert/Toastr for notification dialogs.

### 2. Business Logic Layer (Controllers)
*   Controller files are grouped by business domains (e.g., `StaffController`, `PayrollController`, `LeaveController`).
*   Handles model binding, request routing, session authentication checks, and database transaction queries.

### 3. Data Access Layer
*   Uses **ADO.NET** (`SqlConnection`, `SqlCommand`, `SqlDataReader`) to execute raw SQL queries and Stored Procedures directly against SQL Server.
*   Ensures strong transactional safety and efficiency by avoiding heavy ORM overhead.

### 4. Security Architecture
*   **Session-based Authentication**: Session state is initialized upon login (`Session["UserId"]`, `Session["RoleType"]`).
*   **Global Filters**: A global `PermissionActionFilter` intercepts routing to validate URL requests and API payloads against cached role permissions.
