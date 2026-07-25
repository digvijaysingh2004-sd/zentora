# User Profile Avatars & Initials Fallback System 👤

Zentora HRMS utilizes a standardized avatar fallback system to ensure a polished UI. If an employee or user has not uploaded a profile picture, the system dynamically displays a circular wrapper containing the first letters (initials) of their name.

---

## 🎨 Implementation Locations

1.  **Sidebar/Header Topbar**: [Header.cshtml](file:///C:/digvijayProjects/Projects/zentora/zentoraHRMS/Views/Shared/Header.cshtml)
2.  **Employees Table View**: [Employees.cshtml](file:///C:/digvijayProjects/Projects/zentora/zentoraHRMS/Views/Staff/Employees.cshtml)

---

## ⚙️ How It Works

### 1. In the Employee List ([Employees.cshtml](file:///C:/digvijayProjects/Projects/zentora/zentoraHRMS/Views/Staff/Employees.cshtml))
In the employee list table, names are split into `FirstName` and `LastName`. The fallback computes initials directly using Razor C# syntax:
```html
@if (!string.IsNullOrEmpty(c.ProfileImage))
{
    <img src="@c.ProfileImage" class="rounded-circle border border-2 border-light-subtle shadow-sm" style="width: 38px; height: 38px; object-fit: cover;" onerror="this.onerror=null; this.src='/assets/images/users/avatar-1.jpg';" />
}
else
{
    <div class="rounded-circle bg-primary-subtle text-primary d-flex align-items-center justify-content-center shadow-sm fw-semibold" style="width: 38px; height: 38px; font-size: 11px; min-width: 38px;">
        @(c.FirstName.Length > 0 ? c.FirstName.Substring(0,1) : "")@(c.LastName.Length > 0 ? c.LastName.Substring(0,1) : "")
    </div>
}
```

### 2. In the Header Dropdown ([Header.cshtml](file:///C:/digvijayProjects/Projects/zentora/zentoraHRMS/Views/Shared/Header.cshtml))
For the logged-in user, the full name is stored as a single string inside `Session["UserName"]`. The view uses dynamic string splitting to extract the initials:
```html
@if (Session["ProfileImage"] != null && !string.IsNullOrEmpty(Session["ProfileImage"].ToString()))
{
    <img src="@Session["ProfileImage"]" width="32" height="32" class="rounded-circle d-flex shadow-sm" style="object-fit: cover;" onerror="this.onerror=null; this.src='/assets/images/users/user-2.jpg';" />
}
else
{
    var userName = Session["UserName"]?.ToString() ?? "";
    var initials = "";
    if (!string.IsNullOrEmpty(userName))
    {
        var parts = userName.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length > 0)
        {
            initials += parts[0][0];
            if (parts.Length > 1)
            {
                initials += parts[parts.Length - 1][0];
            }
        }
    }
    initials = initials.ToUpper();

    <div class="rounded-circle bg-primary-subtle text-primary d-inline-flex align-items-center justify-content-center shadow-sm fw-semibold align-middle" style="width: 32px; height: 32px; font-size: 11px; min-width: 32px;">
        @initials
    </div>
}
```

---

## 🎨 Visual Styling Tokens

The fallback initials circle uses standard Bootstrap utility classes for layout and custom coloring:
*   `rounded-circle`: Creates a perfect round border radius.
*   `bg-primary-subtle text-primary`: Applies a modern, premium theme-colored background (light indigo/blue tint) with primary-colored text.
*   `d-inline-flex align-items-center justify-content-center`: Centers the initials text horizontally and vertically inside the circle.
*   `shadow-sm`: Adds a premium, subtle elevation shadow around the circle.
*   `fw-semibold`: Applies a semi-bold font weight to make the initials readable.
