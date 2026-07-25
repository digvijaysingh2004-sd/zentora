# Database Design & Schema 🗄️

Zentora HRMS relies on **Microsoft SQL Server** for its relational database engine. Connectivity is handled via raw ADO.NET SQL clients configured inside `Web.config`.

---

## 🗺️ Entity-Relationship Schema

The database consists of tables tracking credentials, employee details, role definitions, permission lists, modules, and work history:

```
┌─────────────────┐       ┌─────────────────┐
│  LoginDetails   │ 1   * │ EmployeeDetails │
│  (Credentials)  ├───────┤   (Full Data)   │
└────────┬────────┘       └────────┬────────┘
         │ 1                       │ 1
         │                         │
         │ *                       │ *
┌────────┴────────┐       ┌────────┴────────┐
│      Roles      │ 1   * │ RolePermissions │
│  (Admin, HR)    ├───────┤ (Menu Access)   │
└─────────────────┘       └────────┬────────┘
                                   │ *
                                   │ 1
                          ┌────────┴────────┐
                          │ Modules/Routes  │
                          │ (Parent, Child) │
                          └─────────────────┘
```

---

## 📝 Key Tables Description

### 1. `EmployeeDetails`
Stores comprehensive employee profiles, contact details, organization structural properties, and reference records.
*   `Id` (INT, PK, Identity): Employee unique identifier.
*   `EmpCode` (VARCHAR): Unique employee identification code (e.g. `EMP-001`).
*   `FirstName`, `MiddleName`, `LastName` (NVARCHAR): Full name parameters.
*   `Email`, `Phone` (NVARCHAR): Primary contact keys.
*   `Designation`, `Department`, `Branch` (NVARCHAR): Placement properties.
*   `ProfileImage` (NVARCHAR): Path to uploaded avatar files.
*   `IsActive` (BIT): Flag for status (Active/Inactive).
*   `IsDeleted` (BIT): Soft delete tracking.

### 2. `LoginDetails`
Manages application access credentials mapped to Employee profiles.
*   `Id` (INT, PK): Identifier.
*   `EmpId` (INT, FK): Reference to `EmployeeDetails.Id`.
*   `Username` (NVARCHAR): Login name.
*   `Password` (NVARCHAR): Account password.
*   `RoleType` (NVARCHAR): String representation of user role (e.g., `Employee`, `Admin`).

### 3. `Roles` & `RolePermissions`
Manages system access permissions mapping.
*   `Roles`: Stores `RoleId` and `RoleName`.
*   `RolePermissions`: Maps `RoleId` to specific module items (`ParentModuleId`, `ChildModuleId`, `SubChildModuleId`) and defines permitted mutations via `ActionId` (1 = `view`, 2 = `add`, 3 = `edit`, 4 = `delete`).

### 4. Modules Tables (`ParentModules`, `ChildModules`, `SubChildModules`)
Stores the dynamic hierarchy of navigation menus and matching URL routes (e.g. `/Staff/Employees`).

---

## 🔒 Stored Procedures

The system utilizes optimized stored procedures for transactional stability and verification:
*   `ValidateUser`: Verifies incoming UserIdentifier (Email/Username) and Password against active login records, returning employee profiles, designations, and role data.
