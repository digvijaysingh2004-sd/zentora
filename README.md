# Zentora HRMS 🚀

[![Build Status](https://img.shields.io/badge/build-passing-brightgreen.svg)]()
[![Framework](https://img.shields.io/badge/.NET%20Framework-4.7.2-blue.svg)]()
[![MVC Version](https://img.shields.io/badge/ASP.NET%20MVC-5.0-orange.svg)]()
[![Database](https://img.shields.io/badge/Database-MS%20SQL%20Server-red.svg)]()
[![Platform](https://img.shields.io/badge/Platform-Windows-lightgrey.svg)]()

Zentora HRMS is a modern, enterprise-ready **Human Resource Management System** built with **ASP.NET MVC 5** and **MS SQL Server**. Designed to streamline organizational administration, Zentora provides a comprehensive, secure, and user-centric platform for managing workforce activities, processing operations, and driving employee performance.

---

## 🌟 Key Modules & Features

### 👥 1. Employee & Staff Management
*   **Comprehensive Profiles**: Manage full employee lifecycle data, from contact information to designations and departments.
*   **Dynamic Lifecycle Tracking**: Track internal movements such as promotions, lateral transfers, and designations.
*   **Document Management**: Store and organize digital documents, contracts, and employee policy acknowledgements.

### 📅 2. Leave & Attendance Management
*   **Leave Workflow**: Flexible leave policies, customized categories, entitlement calculations, and automated approval queues.
*   **Attendance Tracking**: Support for office shifts, check-in/check-out tracking, location rules, and biometric integration configurations.
*   **Regularization**: Easy workflows for employees to request attendance regularizations.

### 💼 3. Recruitment & Onboarding
*   **Applicant Tracking (ATS)**: Job posting creation, candidate pipeline monitoring, and interview scheduling.
*   **Assessment & Reviews**: Multiple interview rounds, assessment scores, and structured panel feedback.
*   **Structured Onboarding**: Auto-generated checklists to ensure a seamless transition for new hires.

### 💳 4. Payroll & Benefits
*   **Salary Structures**: Define components including base salary, allowances, deductions, and tax configurations.
*   **Payslip Generation**: Automatic monthly calculations with a history log for employees.

### 📊 5. Performance & Asset Management
*   **Performance Reviews**: Establish Key Performance Indicators (KPIs), goal tracking, and peer feedback cycles.
*   **Asset Lifecycle**: Track company hardware/software allocations, depreciations, and maintenance logs.

### 📢 6. Corporate Utilities
*   **Meeting Organizer**: Book meeting rooms, assign minutes of meetings (MoM), and track action items.
*   **Announcements**: Publish company-wide bulletins and official announcements.

---

## 🛠️ Technology Stack

| Component | Technology | Description |
| :--- | :--- | :--- |
| **Backend Framework** | C# / .NET Framework 4.7.2 | Robust, scalable core application logic |
| **Presentation Layer** | ASP.NET MVC 5 | Clean separation of concerns with Model-View-Controller patterns |
| **Database Engine** | Microsoft SQL Server | Relational storage utilizing transaction safety and stored procedures |
| **Frontend Utilities** | Razor, HTML5, CSS3, jQuery | Interactive, responsive UI layouts styled using Tabler Icons |
| **Security** | Forms Authentication / Role-based authorization | Secure session handling and granular menu/module access control |

---

## 📂 Project Architecture & Directory Layout

Zentora follows a standard enterprise-level ASP.NET MVC structure:

```
zentora/
├── zentora.sln                  # Visual Studio Solution File
└── zentoraHRMS/                 # Main Web Application Project
    ├── App_Start/               # Configuration (Routes, Bundles, Filters)
    ├── Controllers/             # Business Logic & Request Handlers
    ├── Models/                  # Data Models & Domain Logic
    ├── Views/                   # Razor templates for frontend pages
    ├── Content/                 # Stylesheets & CSS assets
    ├── Scripts/                 # JavaScript & jQuery components
    ├── Web.config               # Web server and application settings
    └── packages.config          # NuGet package dependencies
```

---

## ⚙️ Local Development Setup

Follow these steps to configure and run Zentora HRMS on your local workstation.

### Prerequisites
*   **Visual Studio** (2019 or newer recommended) with **ASP.NET and web development** workload installed.
*   **SQL Server** (LocalDB, Express, or Developer edition).
*   **SQL Server Management Studio (SSMS)** or Azure Data Studio.

### 1. Database Configuration
1. Open **SQL Server Management Studio (SSMS)** and connect to your database engine instance.
2. Create a new database named `Zentora`.
3. Restore or initialize the project database schema onto the newly created database.

### 2. Project Configuration
1. Open the solution file `zentora.sln` using Visual Studio.
2. In the **Solution Explorer**, expand the `zentoraHRMS` folder.
3. Open `Web.config`.
4. Locate the `<connectionStrings>` block and update the connection string to point to your local SQL Server instance:
   ```xml
   <connectionStrings>
       <add name="Zentora" 
            connectionString="Data Source=YOUR_SERVER_NAME;Initial Catalog=Zentora;Integrated Security=True;Persist Security Info=False;Encrypt=True;TrustServerCertificate=True;" 
            providerName="System.Data.SqlClient" />
   </connectionStrings>
   ```

### 3. Build & Launch
1. Right-click the Solution and select **Restore NuGet Packages** to install all required dependencies.
2. Perform a clean rebuild (`Build -> Rebuild Solution`).
3. Press `F5` or `Ctrl + F5` to launch the application using IIS Express.

---

## 🔒 Security Best Practices

### Gitignore Guidelines
A comprehensive `.gitignore` configuration is pre-configured to prevent sensitive developer data or build artifacts from being committed. This includes:
*   Visual Studio build output (`bin/`, `obj/`, `Debug/`, `Release/`)
*   User-specific setting files (`*.user`, `*.suo`, `.vs/`)
*   NuGet packages (`packages/`)
*   Local SQL Server database files (`*.mdf`, `*.ldf`)
*   Environment-specific config secrets (`*.local.config`, `*.secrets.config`)

### Managing Configuration Secrets
For production deployments, do not store connection credentials directly in the main `Web.config`. Instead, externalize them using `configSource`:
```xml
<!-- In Web.config -->
<connectionStrings configSource="connectionStrings.config" />
```
Create a local `connectionStrings.config` file on the deployment server and add it to your local environment exclusions.

---

## 📄 License & Attribution

This project is configured for internal administrative management. All rights reserved.
