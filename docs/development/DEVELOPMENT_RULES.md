# Development Rules & Security Policies ⚠️

This document lists architectural constraints, coding rules, and security policies that all developers must follow when writing code for Zentora HRMS.

---

## 🔒 Security Mandates

### 1. Prevent SQL Injection
*   **Never** concatenate raw input parameters directly inside SQL strings.
*   **Always** use parameterized parameters with ADO.NET `SqlParameter` or use Stored Procedures:
    ```csharp
    // CORRECT
    cmd.Parameters.AddWithValue("@EmpId", empId);
    ```

### 2. Authorization Enforcement
*   **Always** register new view controllers or page routes under the permission modules database tables (`ParentModules`, `ChildModules`, `SubChildModules`).
*   **Never** bypass the `PermissionActionFilter` unless it is a public-facing authentication endpoint (must be added to the bypass list inside [PermissionActionFilter.cs](file:///C:/digvijayProjects/Projects/zentora/zentoraHRMS/Filters/PermissionActionFilter.cs)).

---

## 🗄️ Database & Table Rules

### 1. Mandatory Table Columns
Whenever creating a new database table, you **must** include the following standard auditing and tracking columns:
*   `CreateDate` (DATETIME): Stores the date and time when the record was created.
*   `CreateBy` (INT/NVARCHAR): Stores the unique ID/Username of the user who created the record.
*   `UpdateDate` (DATETIME): Stores the date and time when the record was last updated.
*   `UpdateBy` (INT/NVARCHAR): Stores the unique ID/Username of the user who last updated the record.
*   `SystemAddon` (DATETIME): Stores the date and time when the record was first inserted into the database table (should default to `GETDATE()` on insertion).

### 2. User ID Tracking
*   The `CreateBy` column must save the ID or username of the logged-in user who created the record.
*   The `UpdateBy` column must save the ID or username of the logged-in user who last modified the record.

---


## ⚡ Performance Rules

### 1. Caching
*   Configure configurations that do not change during runtime (like roles, system modules, or route paths) using memory cache helpers (e.g. `PermissionManager.GetProtectedRoutes()`) to avoid loading them from the database on every request.

### 2. Connection Lifecycle
*   **Always** wrap database objects (`SqlConnection`, `SqlCommand`, `SqlDataReader`) in `using` blocks to guarantee connections are closed and disposed of promptly:
    ```csharp
    using (SqlConnection conn = new SqlConnection(connectionString)) {
        conn.Open();
        // Execute queries...
    }
    ```

---

## 🎨 UI Guidelines

*   **No Inline Styles**: Define custom CSS rules inside layout stylesheets (`/Content/`) rather than writing them inline on HTML elements.
*   **Consistent Icons**: Utilize **Lucide Icons** exclusively for dashboard UI metrics and buttons to maintain visual harmony.
*   **Dynamic Response Feedback**: Any form mutation (create, edit, delete) should be handled via AJAX requests and verified with toast notifications.
