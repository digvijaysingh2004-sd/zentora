# Project Roadmap 🗺️

This document outlines the milestones, current developments, and future goals for Zentora HRMS.

---

## 📅 Roadmap Overview

```
Phase 1: Foundation (Done)  ──►  Phase 2: Attendance & Leave (Done)  ──►  Phase 3: Recruitment (Done)
                                                                                  │
Phase 6: Mobile & AI (Future) ◄──  Phase 5: Integrations (Planned)    ◄──  Phase 4: Security/RBAC (Active)
```

---

## 🚀 Milestones Detail

### Phase 1: Core Directory & Authentication (Completed)
*   User registration, secure session authentication, and credential storage.
*   Interactive employee directory listing personal and corporate metadata.
*   Basic search and organization structure hierarchy view (Branches, Departments).

### Phase 2: Leave & Attendance Management (Completed)
*   Shift management, check-in/check-out tracking, and location rules configurations.
*   Leave applications workflow with automated approval routing and dynamic balance updates.
*   Attendance regularizations for tracking missing logs.

### Phase 3: Recruitment & Onboarding (Completed)
*   ATS job posting portal, candidate pipelines, and tracking candidate sources.
*   Candidate evaluation, multi-round interview scoring, and onboarding checklist generators.

### Phase 4: Dynamic Permissions & Security Audit (Active)
*   Implement active URL route validation and role-based access checks (`view`, `add`, `edit`, `delete`) globally.
*   Refactor default/hardcoded values (like default login passwords).
*   Add dynamic initials-based fallbacks for missing profile images.

### Phase 5: Hardware & System Integrations (Planned)
*   Integrate biometric card/fingerprint devices with attendance controllers.
*   Email alerts for automated notifications on leave applications and approvals.
*   Dynamic PDF generation for month-end payslips.

### Phase 6: Mobile APIs & Insights Dashboard (Future)
*   Provide secure REST APIs for a mobile companion app.
*   AI-powered dashboard for workforce planning, turnover risk assessment, and performance insights.
