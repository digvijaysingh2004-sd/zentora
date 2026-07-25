# Sprints Log 🏃‍♂️

This document tracks active development tasks, timelines, and milestones achieved in recent sprint cycles.

---

## 🟢 Current Sprint: Sprint 6 (Security & UI Polish)

**Goal**: Implement strict URL permission checks, clean up default inputs, and improve profile picture fallback aesthetics.

### 📋 Task Board
*   [x] **Task 1**: Remove hardcoded default password `"123456"` from the add employee form.
*   [x] **Task 2**: Add initials-based dynamic avatar fallbacks for logged-in users in the header.
*   [x] **Task 3**: Implement active route permission blocking inside the global MVC filter.
*   [x] **Task 4**: Support JSON response formats for AJAX requests blocked by permissions.
*   [x] **Task 5**: Display warning notifications on dashboard redirections using Toastr.

---

## ↩️ Past Sprints History

### Sprint 5 (UI Refactoring & Breadcrumbs)
*   **Accomplishments**:
    *   Integrated dynamic breadcrumbs and unified page headers in the layout.
    *   Refactored the dashboard charts to represent real-time attendance numbers.
    *   Added Choices.js support for select dropdown elements on modal forms.

### Sprint 4 (Payroll & Payslip Setup)
*   **Accomplishments**:
    *   Set up salary components and allowances calculation routines.
    *   Configured monthly payroll runs and monthly payslips history views.

### Sprints 1-3 (Core Platform Setup)
*   **Accomplishments**:
    *   Configured standard controllers (Staff, HR, Attendance).
    *   Setup database schema on SQL Server.
    *   Wrote core authentication logic.
