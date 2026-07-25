# Coding Standards & Guidelines 📑

This document establishes the official coding standards, formatting styles, and conventions for Zentora HRMS.

---

## 💻 C# Coding Guidelines

*   **File Layout**: Keep standard namespaces at the top of the file, ordered alphabetically.
*   **Naming Conventions**:
    *   `PascalCase` for public properties, methods, classes, and namespaces:
        ```csharp
        public class EmployeeModel {
            public string FirstName { get; set; }
            public void SaveEmployee() { ... }
        }
        ```
    *   `camelCase` for local variables and parameters:
        ```csharp
        int userId = Convert.ToInt32(Session["UserId"]);
        ```
    *   `_camelCase` with a leading underscore for private member fields:
        ```csharp
        private static string _protectedRoutes;
        ```
*   **Code Documentation**: Write XML summary comments for complex helper functions or database queries to document their behavior.

---

## 🌐 Javascript/jQuery Guidelines

*   **Variables**: Use `var` (or `let`/`const` where appropriate) to prevent global variable leaks.
*   **Ajax Operations**: Always define error callback handlers to handle network failures or unauthorized requests:
    ```javascript
    $.ajax({
        url: '/Staff/GetEmployeeList',
        type: 'GET',
        success: function(response) { ... },
        error: function(xhr, status, error) {
            toastr.error("Failed to load employee list.");
        }
    });
    ```
*   **Selectors**: Use clear ID selectors (`#AddEmpModal`) or custom class targets (`.btn-delete`) instead of complex, brittle DOM path queries.

---

## 📄 HTML & Razor Markup

*   **Indentation**: Maintain clean indentations (2 or 4 spaces) inside nested HTML structures.
*   **Bootstrap Conventions**: Use Bootstrap 5 grid layout elements (`row`, `col-md-x`, `col-12`) for responsive alignments.
*   **Razor Blocks**: Write long code calculations (like string parsing for initials) in explicit Razor code blocks at the beginning of the file or section instead of inline within markup:
    ```html
    @{
        var userName = Session["UserName"]?.ToString() ?? "";
        // ... calculation logic ...
    }
    ```

---

## 🛢️ SQL Formatting

*   **Syntax**: Capitalize SQL keywords (e.g. `SELECT`, `FROM`, `WHERE`, `JOIN`, `AND`, `OR`, `ON`, `UNION`).
*   **Parameters**: Always use parameter placeholders (`@ParamName`) instead of embedding raw values.
*   **Aliases**: When joining tables, use clear short aliases for legibility:
    ```sql
    SELECT 
        e.Id,
        e.EmpCode,
        l.Username
    FROM EmployeeDetails e
    INNER JOIN LoginDetails l ON e.Id = l.EmpId
    WHERE e.IsDeleted = 0
    ```
