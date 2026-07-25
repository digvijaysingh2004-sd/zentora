# Master Development Context 🎯

This document outlines the core business context, project objectives, and developer guidelines for building and maintaining Zentora HRMS.

---

## 🚀 Business Vision

Zentora HRMS is designed to serve as an enterprise-grade **Human Resource Management System** that simplifies administrative tasks, secures organizational records, and empowers personnel administration. The platform's success is defined by three pillars:

1.  **Security & Confidentiality**: Protecting sensitive employee information (salaries, address logs, credentials) via a strict, multi-tenant Role-Based Access Control (RBAC) mechanism.
2.  **Performance & Efficiency**: Ensuring fast load times by caching role settings, keeping queries simple, and writing modular frontend views.
3.  **Excellent UX**: Providing clean, responsive user interfaces styled with Bootstrap, Lucide Icons, and dynamic Toastr notifications.

---

## 🛠️ Tech Stack Constraints

Every feature added to this codebase must align with our core technology configuration:
*   **Backend**: C# /.NET Framework 4.7.2.
*   **Web Framework**: ASP.NET MVC 5.
*   **Database**: MS SQL Server using ADO.NET (avoid Entity Framework to maintain direct SQL optimization control).
*   **Frontend**: Razor views (.cshtml), Bootstrap 5 styling elements, and jQuery-based AJAX logic.

---

## 💡 Key Architectural Guidelines

*   **Clean Controllers**: Keep controllers focused on processing incoming arguments and mapping response schemas. Business and data validations should be decoupled or encapsulated cleanly.
*   **Secure Access by Default**: All routes mapping to system modules must verify user actions (`view`, `add`, `edit`, `delete`) using the global filter.
*   **Asynchronous UX**: Implement modifications (create, update, delete) as AJAX calls rather than full-page postbacks to maintain a modern, smooth feel.
