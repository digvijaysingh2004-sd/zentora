# Request & API Flow 🔄

Zentora HRMS features two kinds of request/response flows: **Synchronous View Rendering** and **Asynchronous AJAX mutations**.

---

## 🗺️ Architectural Endpoints Flow

```
   ┌────────────────────────────────────────────────────────┐
   │                    Client Browser                      │
   └───────────┬────────────────────────────────┬───────────┘
               │                                │
               │ GET View (HTML)                │ POST Mutation (AJAX/JSON)
               ▼                                ▼
   ┌───────────────────┐            ┌───────────────────┐
   │  Global Filter    │            │  Global Filter    │
   │  (Auth Check)     │            │  (Auth Check)     │
   └───────────┬───────┘            └───────────┬───────┘
               │                                │
               ▼                                ▼
   ┌───────────────────┐            ┌───────────────────┐
   │  Permission Filter│            │  Permission Filter│
   │  (Route Check)    │            │  (Action Check)   │
   └───────────┬───────┘            └───────────┬───────┘
               │                                │
               ▼                                ▼
   ┌───────────────────┐            ┌───────────────────┐
   │ Controller Action │            │ Controller Action │
   │ (Render View)     │            │ (Perform Mutation)│
   └───────────┬───────┘            └───────────┬───────┘
               │                                │
               ▼ Return HTML                    ▼ Return JSON
   ┌───────────────────┐            ┌───────────────────┐
   │ Browser Renders   │            │ jQuery Handlers   │
   │ Full Page Markup  │            │ (Toastr Success)  │
   └───────────────────┘            └───────────────────┘
```

---

## 1. Page Requests (Synchronous GET)
Standard page requests return complete HTML documents generated on the server using Razor syntax.

### Example Flow: Accessing `/Staff/Employees`
1.  **Request**: Browser issues `GET /Staff/Employees`.
2.  **Filter**: `PermissionActionFilter` verifies the session. It loads protected routes and checks if the user has `view` permission mapped for `/Staff/Employees`.
3.  **Controller**: `StaffController.Employees()` is called. It queries raw SQL for employee profiles and populates a view list:
    ```csharp
    public ActionResult Employees() {
        // ... (fetches database entries)
        return View(list);
    }
    ```
4.  **Response**: The system renders [Employees.cshtml](file:///C:/digvijayProjects/Projects/zentora/zentoraHRMS/Views/Staff/Employees.cshtml) integrated inside [\_Layout.cshtml](file:///C:/digvijayProjects/Projects/zentora/zentoraHRMS/Views/Shared/_Layout.cshtml) and transmits HTML back to the browser.

---

## 2. API Mutations (Asynchronous POST)
Operations (like saving, updating, or deleting database entries) are performed using jQuery AJAX endpoints that return JSON structures.

### Standard Response Payload
All JSON actions return a standard object format for consistent client processing:
```json
{
  "success": true,
  "message": "Operation executed successfully."
}
```

### Example Mutation Flow: Deleting an Employee
1.  **Trigger**: User clicks delete, executing javascript:
    ```javascript
    function DeleteEmp(id) {
        $.ajax({
            url: '/Staff/DeleteEmployee',
            type: 'POST',
            data: { id: id },
            success: function(res) {
                if (res.success) {
                    toastr.success(res.message);
                    location.reload();
                } else {
                    toastr.error(res.message);
                }
            }
        });
    }
    ```
2.  **Request**: `POST /Staff/DeleteEmployee` with payload `id=10`.
3.  **Authorization**: `PermissionActionFilter` intercepts and resolves the action `DeleteEmployee` to its parent route `/Staff/Employees`. It checks if the user has the `delete` permission. If not, it blocks execution and sends a `JsonResult` directly:
    ```json
    { "success": false, "message": "Access Denied: You do not have permission to perform this action." }
    ```
4.  **Execution**: If authorized, `StaffController.DeleteEmployee(id)` soft-deletes the database record:
    ```csharp
    [HttpPost]
    public JsonResult DeleteEmployee(int id) {
        // SQL Mutation (UPDATE EmployeeDetails SET IsDeleted = 1 WHERE Id = @Id)
        return Json(new { success = true, message = "Employee deleted successfully!" });
    }
    ```
