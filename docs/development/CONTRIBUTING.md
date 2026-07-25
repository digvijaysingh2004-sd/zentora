# Contributing Guidelines 🤝

Thank you for contributing to Zentora HRMS! To maintain code stability and clean deployment pipelines, please follow these workflows when submitting updates.

---

## 🍴 Git Workflow

### 1. Branch Naming Convention
Create a branch from `main` using the appropriate prefix:
*   `feature/your-feature-name` (e.g., `feature/payslip-pdf`)
*   `bugfix/your-fix-name` (e.g., `bugfix/unauthorized-api-bypass`)
*   `docs/doc-updates` (e.g., `docs/permissions-documentation`)

### 2. Commit Message Guidelines
Write descriptive commit messages explaining the rationale behind the change:
*   `feat: add active route blocking in PermissionActionFilter`
*   `fix: remove default password from add employee form`

---

## 🛠️ Local Development Cycle

1.  Connect to your local SQL Server database using SSMS.
2.  Open `Web.config` and configure the connection string to point to your database.
3.  Open the solution `zentora.sln` using Visual Studio.
4.  Restore NuGet packages, perform a rebuild, and launch the site (`F5` or `Ctrl + F5`).
5.  Perform your modifications and test actions inside the browser.

---

## 📋 Pull Request (PR) Checklist

Before submitting a PR, make sure your branch satisfies the following checklist:
*   [ ] The codebase compiles cleanly with no errors.
*   [ ] Modified `.cshtml` files are verified inside the browser to ensure CSS and alignment match.
*   [ ] Database scripts or migrations (if tables or schemas are modified) are recorded.
*   [ ] Sensitive configurations (like database passwords or local variables) are NOT committed.
*   [ ] Coding standards are followed (consistent casings and naming styles).
