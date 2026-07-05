# Zentora HRMS 🚀

Zentora HRMS is a modern, enterprise-ready **Human Resource Management System** built with **ASP.NET MVC 5** and **MS SQL Server**. It provides a comprehensive suite of modules designed to streamline HR administration, staff management, attendance tracking, leave applications, recruitment, payroll processing, performance evaluation, asset tracking, and more.

---

## 🌟 Key Features

*   **Employee Management**: Complete staff records, designation tracking, promotions, awards, and digital document acknowledgments.
*   **Attendance & Time Tracking**: Office shifts, locations, attendance policies, and regularization workflows.
*   **Leave Management**: Categories, allocation, policies, and approvals.
*   **Recruitment & Onboarding**: Job postings, candidate tracking, interview rounds, assessments, and onboarding checklists.
*   **Payroll**: Basic salary structures, payslip generation, and transaction history.
*   **Asset Management**: Register hardware/assets, track depreciations, and manage employee assignments.
*   **Performance Tracking**: Key indicators, goal planning, review cycles, and feedback.
*   **Announcements & Meetings**: Meeting room allocations, minutes, action items, and company-wide bulletins.

---

## 🛠️ Tech Stack

*   **Backend**: C# / .NET Framework 4.7.2
*   **Framework**: ASP.NET MVC 5
*   **Database**: Microsoft SQL Server
*   **Frontend**: Razor Views, HTML5, CSS3 (using Tabler Icons & responsive design), JavaScript/jQuery
*   **Automation**: PowerShell Scripting for project scaffolding and database schema sync.

---

## ⚙️ Local Development Setup

To get Zentora HRMS running on your local machine, follow these steps:

### 1. Database Setup
1. Open **SQL Server Management Studio (SSMS)**.
2. Create a new database named `Zentora`.
3. Open and execute the following SQL scripts in order to create the tables, stored procedures, and seed data:
   *   `zentora_script.sql` (Creates base schema and seeds default users & configurations)
   *   `zentora_core_features.sql` (Enables Staff, HR, Recruitment, Leaves, and Payroll tables)
   *   `zentora_missing_tables.sql` (Creates auxiliary/missing schema tables)
   *   `update_routes.sql` (Updates application routing and navigation states in the DB)

### 2. Scaffold Controllers & Views
Run the PowerShell setup scripts to generate additional modules and integrate them into the project workspace:
```powershell
# Step 1: Scaffold initial missing views & controller actions
.\create_remaining.ps1

# Step 2: Scaffold training, performance, and asset management modules
.\create_remaining_final.ps1

# Step 3: Run formatting/nesting fix script for controller classes
.\fix.ps1
```

### 3. Open in Visual Studio
1. Open the solution file `zentora.sln` using **Visual Studio 2022** or **2019**.
2. Restore the NuGet packages (Right-click the Solution and select **Restore NuGet Packages**).
3. Ensure the connection string in `zentoraHRMS/Web.config` matches your local SQL Server instance:
   ```xml
   <connectionStrings>
       <add name="Zentora" connectionString="Data Source=localhost;Initial Catalog=Zentora;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;" providerName="System.Data.SqlClient" />
   </connectionStrings>
   ```
4. Build the solution (`Ctrl + Shift + B`) and run the application (`F5` or `Ctrl + F5`).

---

## 🔒 Security Best Practices (Crucial for Public Repo)

Since this repository is public, **never commit real credentials or production secrets** to GitHub.

### Gitignore Coverage
A robust `.gitignore` file is included in this repository. It automatically prevents the following sensitive files from being pushed:
*   Visual Studio build output (`bin/`, `obj/`, `Debug/`, `Release/`)
*   User-specific configuration files (`*.user`, `*.suo`, `.vs/`)
*   NuGet packages (`packages/`)
*   Local SQL Server database files (`*.mdf`, `*.ldf`)
*   External configuration files (`*.local.config`, `*.secrets.config`, `secrets.config`, `appSettings.config`, `connectionStrings.config`)

### Externalizing Config Secrets
If you need to define production connection strings or secret keys, load them dynamically from external files that are gitignored. For example:
```xml
<!-- In Web.config -->
<connectionStrings configSource="connectionStrings.config" />
<appSettings file="appSettings.config">
    <!-- Non-sensitive configurations here -->
</appSettings>
```
Create a local `connectionStrings.config` and `appSettings.config` file on your machine and do not commit them.

### Default Seed Passwords
> [!IMPORTANT]
> The database seed script `zentora_script.sql` contains default credentials for testing (e.g., `superadmin` / `superadmin@123`, `admin` / `Admin@123`). Change these passwords immediately after logging into any deployed environment.
