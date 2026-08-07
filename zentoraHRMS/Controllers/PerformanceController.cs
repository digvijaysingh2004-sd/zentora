using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using zentoraHRMS.Models;

namespace zentoraHRMS.Controllers
{
    public class PerformanceController : Controller
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["Zentora"].ConnectionString;

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            Response.Cache.SetNoStore();

            EnsureTablesExist();

            base.OnActionExecuting(filterContext);
        }

        private void EnsureTablesExist()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // 0. Run Migration: Convert SystemAddon from BIT to DATETIME if it exists as BIT
                // This must run before table checks to avoid parser implicit conversion compile errors.
                string migrateGoalTypes = @"
                    IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'GoalTypes' AND COLUMN_NAME = 'SystemAddon' AND DATA_TYPE = 'bit')
                    BEGIN
                        DECLARE @ConstraintNameGT NVARCHAR(255);
                        SELECT @ConstraintNameGT = dc.name 
                        FROM sys.default_constraints dc
                        JOIN sys.columns c ON dc.parent_column_id = c.column_id AND dc.parent_object_id = c.object_id
                        WHERE c.name = 'SystemAddon' AND OBJECT_NAME(dc.parent_object_id) = 'GoalTypes';
                        
                        IF @ConstraintNameGT IS NOT NULL
                            EXEC('ALTER TABLE dbo.GoalTypes DROP CONSTRAINT ' + @ConstraintNameGT);
                            
                        ALTER TABLE dbo.GoalTypes DROP COLUMN SystemAddon;
                        ALTER TABLE dbo.GoalTypes ADD SystemAddon DATETIME NULL DEFAULT GETDATE();
                    END";
                using (SqlCommand cmd = new SqlCommand(migrateGoalTypes, con))
                {
                    cmd.ExecuteNonQuery();
                }

                string migrateEmployeeGoals = @"
                    IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'EmployeeGoals' AND COLUMN_NAME = 'SystemAddon' AND DATA_TYPE = 'bit')
                    BEGIN
                        DECLARE @ConstraintNameEG NVARCHAR(255);
                        SELECT @ConstraintNameEG = dc.name 
                        FROM sys.default_constraints dc
                        JOIN sys.columns c ON dc.parent_column_id = c.column_id AND dc.parent_object_id = c.object_id
                        WHERE c.name = 'SystemAddon' AND OBJECT_NAME(dc.parent_object_id) = 'EmployeeGoals';
                        
                        IF @ConstraintNameEG IS NOT NULL
                            EXEC('ALTER TABLE dbo.EmployeeGoals DROP CONSTRAINT ' + @ConstraintNameEG);
                            
                        ALTER TABLE dbo.EmployeeGoals DROP COLUMN SystemAddon;
                        ALTER TABLE dbo.EmployeeGoals ADD SystemAddon DATETIME NULL DEFAULT GETDATE();
                    END";
                using (SqlCommand cmd = new SqlCommand(migrateEmployeeGoals, con))
                {
                    cmd.ExecuteNonQuery();
                }

                // 1. Create IndicatorCategories Table if not exists
                string createCategoriesQuery = @"
                    IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'IndicatorCategories')
                    BEGIN
                        CREATE TABLE dbo.IndicatorCategories (
                            IndicatorCategoryId INT IDENTITY(1,1) PRIMARY KEY,
                            CategoryName NVARCHAR(100) NOT NULL,
                            Description NVARCHAR(MAX) NULL,
                            IsActive BIT NOT NULL DEFAULT 1,
                            CreatedAt DATETIME NULL DEFAULT GETDATE(),
                            UpdatedAt DATETIME NULL,
                            CreatedBy NVARCHAR(100) NULL,
                            UpdatedBy NVARCHAR(100) NULL
                        );
                        
                        -- Seed some initial data
                        INSERT INTO dbo.IndicatorCategories (CategoryName, Description, IsActive, CreatedBy) VALUES
                        (N'Technical Skills', N'Indicators related to programming, technical competence, and system knowledge.', 1, N'System'),
                        (N'Soft Skills', N'Indicators related to communication, teamwork, and interpersonal skills.', 1, N'System'),
                        (N'Leadership & Management', N'Indicators related to project planning, team leading, and decision making.', 1, N'System');
                    END";

                using (SqlCommand cmd = new SqlCommand(createCategoriesQuery, con))
                {
                    cmd.ExecuteNonQuery();
                }

                // 2. Create Indicators Table if not exists
                string createIndicatorsQuery = @"
                    IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Indicators')
                    BEGIN
                        CREATE TABLE dbo.Indicators (
                            IndicatorId INT IDENTITY(1,1) PRIMARY KEY,
                            IndicatorCategoryId INT NOT NULL,
                            IndicatorName NVARCHAR(100) NOT NULL,
                            Description NVARCHAR(MAX) NULL,
                            IsActive BIT NOT NULL DEFAULT 1,
                            MeasurementUnit NVARCHAR(100) NULL,
                            TargetValue NVARCHAR(100) NULL,
                            CreatedAt DATETIME NULL DEFAULT GETDATE(),
                            UpdatedAt DATETIME NULL,
                            CreatedBy NVARCHAR(100) NULL,
                            UpdatedBy NVARCHAR(100) NULL,
                            CONSTRAINT FK_Indicators_IndicatorCategories FOREIGN KEY (IndicatorCategoryId) 
                                REFERENCES dbo.IndicatorCategories (IndicatorCategoryId) ON DELETE CASCADE
                        );
                        
                        -- Seed some initial data
                        INSERT INTO dbo.Indicators (IndicatorCategoryId, IndicatorName, Description, IsActive, MeasurementUnit, TargetValue, CreatedBy) VALUES
                        (1, N'Coding Quality', N'Adherence to programming standards and code review practices.', 1, N'Percentage', N'95%', N'System'),
                        (1, N'Problem Solving', N'Ability to analyze issues and implement efficient technical solutions.', 1, N'Score', N'4.5/5', N'System'),
                        (2, N'Communication', N'Effective verbal and written communication with peers and clients.', 1, N'Score', N'4.0/5', N'System'),
                        (2, N'Team Collaboration', N'Supporting other team members and contributing to team goals.', 1, N'Score', N'4.5/5', N'System'),
                        (3, N'Project Planning', N'Estimation accuracy and task coordination.', 1, N'Percentage', N'90%', N'System');
                    END
                    ELSE
                    BEGIN
                        -- Migration: add columns if they exist on the table but not yet in database
                        IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Indicators' AND COLUMN_NAME = 'MeasurementUnit')
                        BEGIN
                            ALTER TABLE dbo.Indicators ADD MeasurementUnit NVARCHAR(100) NULL;
                        END
                        IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Indicators' AND COLUMN_NAME = 'TargetValue')
                        BEGIN
                            ALTER TABLE dbo.Indicators ADD TargetValue NVARCHAR(100) NULL;
                        END
                    END";

                using (SqlCommand cmd = new SqlCommand(createIndicatorsQuery, con))
                {
                    cmd.ExecuteNonQuery();
                }

                // 3. Create GoalTypes Table if not exists
                string createGoalTypesQuery = @"
                    IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'GoalTypes')
                    BEGIN
                        CREATE TABLE dbo.GoalTypes (
                            GoalTypeId INT IDENTITY(1,1) PRIMARY KEY,
                            GoalTypeName NVARCHAR(100) NOT NULL,
                            Description NVARCHAR(MAX) NULL,
                            Status NVARCHAR(50) NULL DEFAULT 'Active',
                            CreatedBy NVARCHAR(100) NULL,
                            CreateDate DATETIME NULL DEFAULT GETDATE(),
                            UpdatedBy NVARCHAR(100) NULL,
                            UpdateDate DATETIME NULL,
                            SystemAddon DATETIME NULL DEFAULT GETDATE()
                        );
                        
                        -- Seed some initial data
                        INSERT INTO dbo.GoalTypes (GoalTypeName, Description, Status, CreatedBy, CreateDate, SystemAddon) VALUES
                        (N'Technical Goal', N'Goals related to improving technical skills, architecture, and coding quality.', N'Active', N'System', GETDATE(), GETDATE()),
                        (N'Behavioral Goal', N'Goals related to communication, leadership, teamwork, and ownership.', N'Active', N'System', GETDATE(), GETDATE()),
                        (N'Project Delivery Goal', N'Goals related to project milestones, delivery timelines, and client satisfaction.', N'Active', N'System', GETDATE(), GETDATE());
                    END";

                using (SqlCommand cmd = new SqlCommand(createGoalTypesQuery, con))
                {
                    cmd.ExecuteNonQuery();
                }

                // 4. Create EmployeeGoals Table if not exists
                string createEmployeeGoalsQuery = @"
                    IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'EmployeeGoals')
                    BEGIN
                        CREATE TABLE dbo.EmployeeGoals (
                            EmployeeGoalId INT IDENTITY(1,1) PRIMARY KEY,
                            EmployeeId INT NOT NULL,
                            GoalTypeId INT NOT NULL,
                            GoalTitle NVARCHAR(255) NOT NULL,
                            Description NVARCHAR(MAX) NULL,
                            StartDate DATETIME NULL,
                            EndDate DATETIME NULL,
                            Target NVARCHAR(255) NULL,
                            Progress INT NOT NULL DEFAULT 0,
                            Status NVARCHAR(50) NULL DEFAULT 'Active',
                            CreateDate DATETIME NULL DEFAULT GETDATE(),
                            CreateBy NVARCHAR(100) NULL,
                            UpdateDate DATETIME NULL,
                            UpdateBy NVARCHAR(100) NULL,
                            SystemAddon DATETIME NULL DEFAULT GETDATE(),
                            CONSTRAINT FK_EmployeeGoals_EmployeeDetails FOREIGN KEY (EmployeeId) REFERENCES dbo.EmployeeDetails(Id) ON DELETE CASCADE,
                            CONSTRAINT FK_EmployeeGoals_GoalTypes FOREIGN KEY (GoalTypeId) REFERENCES dbo.GoalTypes(GoalTypeId) ON DELETE CASCADE
                        );
                    END";

                using (SqlCommand cmd = new SqlCommand(createEmployeeGoalsQuery, con))
                {
                    cmd.ExecuteNonQuery();
                }

                // 5. Create ReviewCycles Table if not exists
                string dropOldReviewCyclesQuery = @"
                    IF EXISTS (SELECT * FROM sys.tables WHERE name = 'ReviewCycles')
                    BEGIN
                        -- If the table exists but uses the old schema (like containing StartDate or CycleName columns), drop it so it can be re-created
                        IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ReviewCycles' AND (COLUMN_NAME = 'StartDate' OR COLUMN_NAME = 'CycleName'))
                        BEGIN
                            DROP TABLE dbo.ReviewCycles;
                        END
                    END";

                using (SqlCommand cmd = new SqlCommand(dropOldReviewCyclesQuery, con))
                {
                    cmd.ExecuteNonQuery();
                }

                string createReviewCyclesQuery = @"
                    IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'ReviewCycles')
                    BEGIN
                        CREATE TABLE dbo.ReviewCycles (
                            ReviewCycleId INT IDENTITY(1,1) PRIMARY KEY,
                            ReviewCycleName NVARCHAR(150) NOT NULL,
                            Description NVARCHAR(MAX) NULL,
                            Frequency NVARCHAR(50) NULL,
                            Status NVARCHAR(50) NOT NULL DEFAULT 'Active',
                            CreateDate DATETIME NULL DEFAULT GETDATE(),
                            CreateBy INT NULL,
                            UpdateDate DATETIME NULL,
                            UpdateBy INT NULL,
                            SystemAddon DATETIME NULL DEFAULT GETDATE()
                        );
                        
                        -- Seed some initial data
                        INSERT INTO dbo.ReviewCycles (ReviewCycleName, Description, Frequency, Status, CreateBy, CreateDate, SystemAddon) VALUES
                        (N'Annual Performance Review 2026', N'Company-wide annual appraisal process for the year 2026.', N'Annual', N'Active', 1, GETDATE(), GETDATE()),
                        (N'Mid-Year Performance Review 2026', N'Mid-year performance feedback and goal adjustments.', N'Semi-Annual', N'Active', 1, GETDATE(), GETDATE());
                    END";

                using (SqlCommand cmd = new SqlCommand(createReviewCyclesQuery, con))
                {
                    cmd.ExecuteNonQuery();
                }

                // 6. Migrate Indicators Table to support RoleId
                string migrateIndicatorsQuery = @"
                    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Indicators' AND COLUMN_NAME = 'RoleId')
                    BEGIN
                        ALTER TABLE dbo.Indicators ADD RoleId INT NULL;
                        ALTER TABLE dbo.Indicators ADD CONSTRAINT FK_Indicators_Roles FOREIGN KEY (RoleId) REFERENCES dbo.Roles(Id) ON DELETE SET NULL;
                    END";
                using (SqlCommand cmd = new SqlCommand(migrateIndicatorsQuery, con))
                {
                    cmd.ExecuteNonQuery();
                }

                // 7. Create EmployeeReviews Table if not exists
                string createReviewsQuery = @"
                    IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'EmployeeReviews')
                    BEGIN
                        CREATE TABLE dbo.EmployeeReviews (
                            ReviewId INT IDENTITY(1,1) PRIMARY KEY,
                            EmployeeId INT NOT NULL,
                            ReviewerId INT NOT NULL,
                            ReviewCycleId INT NOT NULL,
                            ReviewDate DATETIME NOT NULL,
                            Rating DECIMAL(3,2) NULL,
                            Status NVARCHAR(50) NOT NULL DEFAULT 'Scheduled',
                            OverallComments NVARCHAR(MAX) NULL,
                            CreatedAt DATETIME NULL DEFAULT GETDATE(),
                            CreatedBy NVARCHAR(100) NULL,
                            UpdatedAt DATETIME NULL,
                            UpdatedBy NVARCHAR(100) NULL,
                            CONSTRAINT FK_EmployeeReviews_EmployeeDetails FOREIGN KEY (EmployeeId) REFERENCES dbo.EmployeeDetails(Id),
                            CONSTRAINT FK_EmployeeReviews_ReviewerDetails FOREIGN KEY (ReviewerId) REFERENCES dbo.EmployeeDetails(Id),
                            CONSTRAINT FK_EmployeeReviews_ReviewCycles FOREIGN KEY (ReviewCycleId) REFERENCES dbo.ReviewCycles(ReviewCycleId)
                        );
                    END";
                using (SqlCommand cmd = new SqlCommand(createReviewsQuery, con))
                {
                    cmd.ExecuteNonQuery();
                }

                // 8. Create EmployeeReviewRatings Table if not exists
                string createRatingsQuery = @"
                    IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'EmployeeReviewRatings')
                    BEGIN
                        CREATE TABLE dbo.EmployeeReviewRatings (
                            ReviewRatingId INT IDENTITY(1,1) PRIMARY KEY,
                            ReviewId INT NOT NULL,
                            IndicatorId INT NOT NULL,
                            RatingValue INT NOT NULL,
                            Comments NVARCHAR(MAX) NULL,
                            CONSTRAINT FK_EmployeeReviewRatings_EmployeeReviews FOREIGN KEY (ReviewId) REFERENCES dbo.EmployeeReviews(ReviewId) ON DELETE CASCADE,
                            CONSTRAINT FK_EmployeeReviewRatings_Indicators FOREIGN KEY (IndicatorId) REFERENCES dbo.Indicators(IndicatorId) ON DELETE CASCADE
                        );
                    END";
                using (SqlCommand cmd = new SqlCommand(createRatingsQuery, con))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        #region Indicator Categories
        public ActionResult IndicatorCategories()
        {
            if (Session["RoleType"] == null || Session["UserId"] == null) 
                return RedirectToAction("Login", "Auth");

            List<IndicatorCategoryModel> list = new List<IndicatorCategoryModel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT IndicatorCategoryId, CategoryName, Description, IsActive, CreatedAt, UpdatedAt, CreatedBy, UpdatedBy FROM IndicatorCategories ORDER BY IndicatorCategoryId DESC";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new IndicatorCategoryModel
                            {
                                IndicatorCategoryId = Convert.ToInt32(reader["IndicatorCategoryId"]),
                                CategoryName = reader["CategoryName"].ToString(),
                                Description = reader["Description"].ToString(),
                                IsActive = Convert.ToBoolean(reader["IsActive"]),
                                CreatedAt = reader["CreatedAt"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["CreatedAt"]) : null,
                                UpdatedAt = reader["UpdatedAt"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["UpdatedAt"]) : null,
                                CreatedBy = reader["CreatedBy"].ToString(),
                                UpdatedBy = reader["UpdatedBy"].ToString()
                            });
                        }
                    }
                }
            }
            return View(list);
        }

        [HttpPost]
        public JsonResult SaveIndicatorCategory(IndicatorCategoryModel model)
        {
            try
            {
                string creator = Session["UserName"]?.ToString() ?? "System";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = @"INSERT INTO IndicatorCategories (CategoryName, Description, IsActive, CreatedAt, CreatedBy) 
                                     VALUES (@CategoryName, @Description, @IsActive, GETDATE(), @CreatedBy)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@CategoryName", model.CategoryName ?? "");
                        cmd.Parameters.AddWithValue("@Description", model.Description ?? "");
                        cmd.Parameters.AddWithValue("@IsActive", model.IsActive);
                        cmd.Parameters.AddWithValue("@CreatedBy", creator);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true, message = "Indicator Category saved successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public JsonResult GetIndicatorCategoryById(int id)
        {
            IndicatorCategoryModel model = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT IndicatorCategoryId, CategoryName, Description, IsActive FROM IndicatorCategories WHERE IndicatorCategoryId = @IndicatorCategoryId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@IndicatorCategoryId", id);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            model = new IndicatorCategoryModel
                            {
                                IndicatorCategoryId = Convert.ToInt32(reader["IndicatorCategoryId"]),
                                CategoryName = reader["CategoryName"].ToString(),
                                Description = reader["Description"].ToString(),
                                IsActive = Convert.ToBoolean(reader["IsActive"])
                            };
                        }
                    }
                }
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateIndicatorCategory(IndicatorCategoryModel model)
        {
            try
            {
                string updater = Session["UserName"]?.ToString() ?? "System";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = @"UPDATE IndicatorCategories 
                                     SET CategoryName = @CategoryName, Description = @Description, IsActive = @IsActive, 
                                         UpdatedAt = GETDATE(), UpdatedBy = @UpdatedBy 
                                     WHERE IndicatorCategoryId = @IndicatorCategoryId";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@IndicatorCategoryId", model.IndicatorCategoryId);
                        cmd.Parameters.AddWithValue("@CategoryName", model.CategoryName ?? "");
                        cmd.Parameters.AddWithValue("@Description", model.Description ?? "");
                        cmd.Parameters.AddWithValue("@IsActive", model.IsActive);
                        cmd.Parameters.AddWithValue("@UpdatedBy", updater);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true, message = "Indicator Category updated successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteIndicatorCategory(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM IndicatorCategories WHERE IndicatorCategoryId = @IndicatorCategoryId";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@IndicatorCategoryId", id);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true, message = "Indicator Category deleted successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        #endregion

        #region Indicators
        public ActionResult Indicators()
        {
            if (Session["RoleType"] == null || Session["UserId"] == null) 
                return RedirectToAction("Login", "Auth");

            string currentRole = Session["RoleName"]?.ToString() ?? "";
            if (!currentRole.Equals("Superadmin", StringComparison.OrdinalIgnoreCase) && 
                !currentRole.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                return RedirectToAction("Index", "Home");
            }

            List<IndicatorModel> list = new List<IndicatorModel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"SELECT I.IndicatorId, I.IndicatorCategoryId, C.CategoryName, I.IndicatorName, I.Description, 
                                        I.IsActive, I.MeasurementUnit, I.TargetValue, I.CreatedAt, I.UpdatedAt, I.CreatedBy, I.UpdatedBy,
                                        I.RoleId, R.RoleName 
                                 FROM Indicators I
                                 INNER JOIN IndicatorCategories C ON I.IndicatorCategoryId = C.IndicatorCategoryId
                                 LEFT JOIN Roles R ON I.RoleId = R.Id
                                 ORDER BY I.IndicatorId DESC";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new IndicatorModel
                            {
                                IndicatorId = Convert.ToInt32(reader["IndicatorId"]),
                                IndicatorCategoryId = Convert.ToInt32(reader["IndicatorCategoryId"]),
                                CategoryName = reader["CategoryName"].ToString(),
                                IndicatorName = reader["IndicatorName"].ToString(),
                                Description = reader["Description"].ToString(),
                                IsActive = Convert.ToBoolean(reader["IsActive"]),
                                MeasurementUnit = reader["MeasurementUnit"].ToString(),
                                TargetValue = reader["TargetValue"].ToString(),
                                CreatedAt = reader["CreatedAt"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["CreatedAt"]) : null,
                                UpdatedAt = reader["UpdatedAt"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["UpdatedAt"]) : null,
                                CreatedBy = reader["CreatedBy"].ToString(),
                                UpdatedBy = reader["UpdatedBy"].ToString(),
                                RoleId = reader["RoleId"] != DBNull.Value ? (int?)Convert.ToInt32(reader["RoleId"]) : null,
                                RoleName = reader["RoleName"] != DBNull.Value ? reader["RoleName"].ToString() : "All Roles"
                            });
                        }
                    }
                }
            }
            return View(list);
        }

        [HttpPost]
        public JsonResult SaveIndicator(IndicatorModel model)
        {
            try
            {
                if (Session["RoleType"] == null || Session["UserId"] == null) 
                    return Json(new { success = false, message = "Unauthorized access" });

                string currentRole = Session["RoleName"]?.ToString() ?? "";
                if (!currentRole.Equals("Superadmin", StringComparison.OrdinalIgnoreCase) && 
                    !currentRole.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                {
                    return Json(new { success = false, message = "Access Denied. Only Administrators can manage indicators." });
                }

                string creator = Session["UserName"]?.ToString() ?? "System";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = @"INSERT INTO Indicators (IndicatorCategoryId, IndicatorName, Description, IsActive, MeasurementUnit, TargetValue, CreatedAt, CreatedBy, RoleId) 
                                     VALUES (@IndicatorCategoryId, @IndicatorName, @Description, @IsActive, @MeasurementUnit, @TargetValue, GETDATE(), @CreatedBy, @RoleId)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@IndicatorCategoryId", model.IndicatorCategoryId);
                        cmd.Parameters.AddWithValue("@IndicatorName", model.IndicatorName ?? "");
                        cmd.Parameters.AddWithValue("@Description", model.Description ?? "");
                        cmd.Parameters.AddWithValue("@IsActive", model.IsActive);
                        cmd.Parameters.AddWithValue("@MeasurementUnit", model.MeasurementUnit ?? "");
                        cmd.Parameters.AddWithValue("@TargetValue", model.TargetValue ?? "");
                        cmd.Parameters.AddWithValue("@CreatedBy", creator);
                        cmd.Parameters.AddWithValue("@RoleId", (object)model.RoleId ?? DBNull.Value);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true, message = "Indicator saved successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public JsonResult GetIndicatorById(int id)
        {
            IndicatorModel model = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT IndicatorId, IndicatorCategoryId, IndicatorName, Description, IsActive, MeasurementUnit, TargetValue, RoleId FROM Indicators WHERE IndicatorId = @IndicatorId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@IndicatorId", id);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            model = new IndicatorModel
                            {
                                IndicatorId = Convert.ToInt32(reader["IndicatorId"]),
                                IndicatorCategoryId = Convert.ToInt32(reader["IndicatorCategoryId"]),
                                IndicatorName = reader["IndicatorName"].ToString(),
                                Description = reader["Description"].ToString(),
                                IsActive = Convert.ToBoolean(reader["IsActive"]),
                                MeasurementUnit = reader["MeasurementUnit"].ToString(),
                                TargetValue = reader["TargetValue"].ToString(),
                                RoleId = reader["RoleId"] != DBNull.Value ? (int?)Convert.ToInt32(reader["RoleId"]) : null
                            };
                        }
                    }
                }
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateIndicator(IndicatorModel model)
        {
            try
            {
                if (Session["RoleType"] == null || Session["UserId"] == null) 
                    return Json(new { success = false, message = "Unauthorized access" });

                string currentRole = Session["RoleName"]?.ToString() ?? "";
                if (!currentRole.Equals("Superadmin", StringComparison.OrdinalIgnoreCase) && 
                    !currentRole.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                {
                    return Json(new { success = false, message = "Access Denied. Only Administrators can manage indicators." });
                }

                string updater = Session["UserName"]?.ToString() ?? "System";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = @"UPDATE Indicators 
                                     SET IndicatorCategoryId = @IndicatorCategoryId, IndicatorName = @IndicatorName, 
                                         Description = @Description, IsActive = @IsActive, 
                                         MeasurementUnit = @MeasurementUnit, TargetValue = @TargetValue,
                                         UpdatedAt = GETDATE(), UpdatedBy = @UpdatedBy, RoleId = @RoleId
                                     WHERE IndicatorId = @IndicatorId";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@IndicatorId", model.IndicatorId);
                        cmd.Parameters.AddWithValue("@IndicatorCategoryId", model.IndicatorCategoryId);
                        cmd.Parameters.AddWithValue("@IndicatorName", model.IndicatorName ?? "");
                        cmd.Parameters.AddWithValue("@Description", model.Description ?? "");
                        cmd.Parameters.AddWithValue("@IsActive", model.IsActive);
                        cmd.Parameters.AddWithValue("@MeasurementUnit", model.MeasurementUnit ?? "");
                        cmd.Parameters.AddWithValue("@TargetValue", model.TargetValue ?? "");
                        cmd.Parameters.AddWithValue("@UpdatedBy", updater);
                        cmd.Parameters.AddWithValue("@RoleId", (object)model.RoleId ?? DBNull.Value);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true, message = "Indicator updated successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteIndicator(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Indicators WHERE IndicatorId = @IndicatorId";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@IndicatorId", id);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true, message = "Indicator deleted successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public JsonResult GetIndicatorCategoriesList()
        {
            List<IndicatorCategoryModel> list = new List<IndicatorCategoryModel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT IndicatorCategoryId, CategoryName FROM IndicatorCategories WHERE IsActive = 1 ORDER BY CategoryName";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new IndicatorCategoryModel
                            {
                                IndicatorCategoryId = Convert.ToInt32(reader["IndicatorCategoryId"]),
                                CategoryName = reader["CategoryName"].ToString()
                            });
                        }
                    }
                }
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetRolesList()
        {
            List<object> list = new List<object>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT Id, RoleName FROM Roles WHERE IsActive = 1 AND IsDeleted = 0 ORDER BY RoleName";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new
                            {
                                RoleId = Convert.ToInt32(reader["Id"]),
                                RoleName = reader["RoleName"].ToString()
                            });
                        }
                    }
                }
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Goal Types
        public ActionResult GoalTypes()
        {
            if (Session["RoleType"] == null || Session["UserId"] == null) 
                return RedirectToAction("Login", "Auth");

            List<GoalTypeModel> list = new List<GoalTypeModel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT GoalTypeId, GoalTypeName, Description, Status, CreatedBy, CreateDate, UpdatedBy, UpdateDate, SystemAddon FROM GoalTypes ORDER BY GoalTypeId DESC";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new GoalTypeModel
                            {
                                GoalTypeId = Convert.ToInt32(reader["GoalTypeId"]),
                                GoalTypeName = reader["GoalTypeName"].ToString(),
                                Description = reader["Description"].ToString(),
                                Status = reader["Status"].ToString(),
                                CreatedBy = reader["CreatedBy"].ToString(),
                                CreateDate = reader["CreateDate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["CreateDate"]) : null,
                                UpdatedBy = reader["UpdatedBy"].ToString(),
                                UpdateDate = reader["UpdateDate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["UpdateDate"]) : null,
                                SystemAddon = reader["SystemAddon"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["SystemAddon"]) : null
                            });
                        }
                    }
                }
            }
            return View(list);
        }

        [HttpPost]
        public JsonResult SaveGoalType(GoalTypeModel model)
        {
            try
            {
                string creator = Session["UserName"]?.ToString() ?? "System";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = @"INSERT INTO GoalTypes (GoalTypeName, Description, Status, CreatedBy, CreateDate, SystemAddon) 
                                     VALUES (@GoalTypeName, @Description, @Status, @CreatedBy, GETDATE(), GETDATE())";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@GoalTypeName", model.GoalTypeName ?? "");
                        cmd.Parameters.AddWithValue("@Description", model.Description ?? "");
                        cmd.Parameters.AddWithValue("@Status", model.Status ?? "Active");
                        cmd.Parameters.AddWithValue("@CreatedBy", creator);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true, message = "Goal Type saved successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public JsonResult GetGoalTypeById(int id)
        {
            GoalTypeModel model = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT GoalTypeId, GoalTypeName, Description, Status, SystemAddon FROM GoalTypes WHERE GoalTypeId = @GoalTypeId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@GoalTypeId", id);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            model = new GoalTypeModel
                            {
                                GoalTypeId = Convert.ToInt32(reader["GoalTypeId"]),
                                GoalTypeName = reader["GoalTypeName"].ToString(),
                                Description = reader["Description"].ToString(),
                                Status = reader["Status"].ToString(),
                                SystemAddon = reader["SystemAddon"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["SystemAddon"]) : null
                            };
                        }
                    }
                }
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateGoalType(GoalTypeModel model)
        {
            try
            {
                string updater = Session["UserName"]?.ToString() ?? "System";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = @"UPDATE GoalTypes 
                                     SET GoalTypeName = @GoalTypeName, Description = @Description, Status = @Status, 
                                         UpdatedBy = @UpdatedBy, UpdateDate = GETDATE() 
                                     WHERE GoalTypeId = @GoalTypeId";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@GoalTypeId", model.GoalTypeId);
                        cmd.Parameters.AddWithValue("@GoalTypeName", model.GoalTypeName ?? "");
                        cmd.Parameters.AddWithValue("@Description", model.Description ?? "");
                        cmd.Parameters.AddWithValue("@Status", model.Status ?? "Active");
                        cmd.Parameters.AddWithValue("@UpdatedBy", updater);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true, message = "Goal Type updated successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteGoalType(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM GoalTypes WHERE GoalTypeId = @GoalTypeId";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@GoalTypeId", id);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true, message = "Goal Type deleted successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        #endregion

        #region Employee Goals
        public ActionResult EmployeeGoals()
        {
            if (Session["RoleType"] == null || Session["UserId"] == null) 
                return RedirectToAction("Login", "Auth");

            List<EmployeeGoalModel> list = new List<EmployeeGoalModel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT eg.EmployeeGoalId, eg.EmployeeId, 
                           (emp.FirstName + ' ' + ISNULL(emp.LastName, '')) AS EmployeeName, 
                           emp.ProfileImage, emp.Email,
                           eg.GoalTypeId, gt.GoalTypeName, eg.GoalTitle, eg.Description, 
                           eg.StartDate, eg.EndDate, eg.Target, eg.Progress, eg.Status, 
                           eg.CreateDate, eg.CreateBy, eg.UpdateDate, eg.UpdateBy, eg.SystemAddon 
                    FROM EmployeeGoals eg
                    INNER JOIN EmployeeDetails emp ON eg.EmployeeId = emp.Id
                    INNER JOIN GoalTypes gt ON eg.GoalTypeId = gt.GoalTypeId
                    ORDER BY eg.EmployeeGoalId DESC";
                
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new EmployeeGoalModel
                            {
                                EmployeeGoalId = Convert.ToInt32(reader["EmployeeGoalId"]),
                                EmployeeId = Convert.ToInt32(reader["EmployeeId"]),
                                EmployeeName = reader["EmployeeName"].ToString(),
                                ProfileImage = reader["ProfileImage"] != DBNull.Value ? reader["ProfileImage"].ToString() : "",
                                Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : "",
                                GoalTypeId = Convert.ToInt32(reader["GoalTypeId"]),
                                GoalTypeName = reader["GoalTypeName"].ToString(),
                                GoalTitle = reader["GoalTitle"].ToString(),
                                Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : "",
                                StartDate = reader["StartDate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["StartDate"]) : null,
                                EndDate = reader["EndDate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["EndDate"]) : null,
                                Target = reader["Target"] != DBNull.Value ? reader["Target"].ToString() : "",
                                Progress = reader["Progress"] != DBNull.Value ? Convert.ToInt32(reader["Progress"]) : 0,
                                Status = reader["Status"] != DBNull.Value ? reader["Status"].ToString() : "Active",
                                CreateBy = reader["CreateBy"] != DBNull.Value ? reader["CreateBy"].ToString() : "",
                                CreateDate = reader["CreateDate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["CreateDate"]) : null,
                                UpdateBy = reader["UpdateBy"] != DBNull.Value ? reader["UpdateBy"].ToString() : "",
                                UpdateDate = reader["UpdateDate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["UpdateDate"]) : null,
                                SystemAddon = reader["SystemAddon"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["SystemAddon"]) : null
                            });
                        }
                    }
                }
            }
            return View(list);
        }

        [HttpPost]
        public JsonResult SaveEmployeeGoal(EmployeeGoalModel model)
        {
            try
            {
                if (Session["RoleType"] == null || Session["UserId"] == null) 
                    return Json(new { success = false, message = "Unauthorized access" });

                string creator = Session["UserName"]?.ToString() ?? "System";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = @"INSERT INTO EmployeeGoals 
                                     (EmployeeId, GoalTypeId, GoalTitle, Description, StartDate, EndDate, Target, Progress, Status, CreateBy, CreateDate, SystemAddon) 
                                     VALUES 
                                     (@EmployeeId, @GoalTypeId, @GoalTitle, @Description, @StartDate, @EndDate, @Target, @Progress, @Status, @CreateBy, GETDATE(), GETDATE())";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@EmployeeId", model.EmployeeId);
                        cmd.Parameters.AddWithValue("@GoalTypeId", model.GoalTypeId);
                        cmd.Parameters.AddWithValue("@GoalTitle", model.GoalTitle ?? "");
                        cmd.Parameters.AddWithValue("@Description", model.Description ?? "");
                        cmd.Parameters.AddWithValue("@StartDate", model.StartDate.HasValue ? (object)model.StartDate.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@EndDate", model.EndDate.HasValue ? (object)model.EndDate.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@Target", model.Target ?? "");
                        cmd.Parameters.AddWithValue("@Progress", model.Progress);
                        cmd.Parameters.AddWithValue("@Status", model.Status ?? "Active");
                        cmd.Parameters.AddWithValue("@CreateBy", creator);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true, message = "Employee Goal saved successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public JsonResult GetEmployeeGoalById(int id)
        {
            EmployeeGoalModel model = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT eg.EmployeeGoalId, eg.EmployeeId, 
                           (emp.FirstName + ' ' + ISNULL(emp.LastName, '')) AS EmployeeName, 
                           emp.ProfileImage, emp.Email,
                           eg.GoalTypeId, gt.GoalTypeName, eg.GoalTitle, eg.Description, 
                           eg.StartDate, eg.EndDate, eg.Target, eg.Progress, eg.Status, 
                           eg.SystemAddon 
                    FROM EmployeeGoals eg
                    INNER JOIN EmployeeDetails emp ON eg.EmployeeId = emp.Id
                    INNER JOIN GoalTypes gt ON eg.GoalTypeId = gt.GoalTypeId
                    WHERE eg.EmployeeGoalId = @EmployeeGoalId";
                
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@EmployeeGoalId", id);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            model = new EmployeeGoalModel
                            {
                                EmployeeGoalId = Convert.ToInt32(reader["EmployeeGoalId"]),
                                EmployeeId = Convert.ToInt32(reader["EmployeeId"]),
                                EmployeeName = reader["EmployeeName"].ToString(),
                                ProfileImage = reader["ProfileImage"] != DBNull.Value ? reader["ProfileImage"].ToString() : "",
                                Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : "",
                                GoalTypeId = Convert.ToInt32(reader["GoalTypeId"]),
                                GoalTypeName = reader["GoalTypeName"].ToString(),
                                GoalTitle = reader["GoalTitle"].ToString(),
                                Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : "",
                                StartDate = reader["StartDate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["StartDate"]) : null,
                                EndDate = reader["EndDate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["EndDate"]) : null,
                                Target = reader["Target"] != DBNull.Value ? reader["Target"].ToString() : "",
                                Progress = reader["Progress"] != DBNull.Value ? Convert.ToInt32(reader["Progress"]) : 0,
                                Status = reader["Status"] != DBNull.Value ? reader["Status"].ToString() : "Active",
                                SystemAddon = reader["SystemAddon"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["SystemAddon"]) : null
                            };
                        }
                    }
                }
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateEmployeeGoal(EmployeeGoalModel model)
        {
            try
            {
                if (Session["RoleType"] == null || Session["UserId"] == null) 
                    return Json(new { success = false, message = "Unauthorized access" });

                string updater = Session["UserName"]?.ToString() ?? "System";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = @"UPDATE EmployeeGoals 
                                     SET EmployeeId = @EmployeeId, GoalTypeId = @GoalTypeId, GoalTitle = @GoalTitle, 
                                         Description = @Description, StartDate = @StartDate, EndDate = @EndDate, 
                                         Target = @Target, Progress = @Progress, Status = @Status, 
                                         UpdateBy = @UpdateBy, UpdateDate = GETDATE() 
                                     WHERE EmployeeGoalId = @EmployeeGoalId";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@EmployeeGoalId", model.EmployeeGoalId);
                        cmd.Parameters.AddWithValue("@EmployeeId", model.EmployeeId);
                        cmd.Parameters.AddWithValue("@GoalTypeId", model.GoalTypeId);
                        cmd.Parameters.AddWithValue("@GoalTitle", model.GoalTitle ?? "");
                        cmd.Parameters.AddWithValue("@Description", model.Description ?? "");
                        cmd.Parameters.AddWithValue("@StartDate", model.StartDate.HasValue ? (object)model.StartDate.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@EndDate", model.EndDate.HasValue ? (object)model.EndDate.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@Target", model.Target ?? "");
                        cmd.Parameters.AddWithValue("@Progress", model.Progress);
                        cmd.Parameters.AddWithValue("@Status", model.Status ?? "Active");
                        cmd.Parameters.AddWithValue("@UpdateBy", updater);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true, message = "Employee Goal updated successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteEmployeeGoal(int id)
        {
            try
            {
                if (Session["RoleType"] == null || Session["UserId"] == null) 
                    return Json(new { success = false, message = "Unauthorized access" });

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM EmployeeGoals WHERE EmployeeGoalId = @EmployeeGoalId";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@EmployeeGoalId", id);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true, message = "Employee Goal deleted successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public JsonResult GetActiveGoalTypes()
        {
            List<GoalTypeModel> list = new List<GoalTypeModel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT GoalTypeId, GoalTypeName FROM GoalTypes WHERE Status = 'Active'";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new GoalTypeModel
                            {
                                GoalTypeId = Convert.ToInt32(reader["GoalTypeId"]),
                                GoalTypeName = reader["GoalTypeName"].ToString()
                            });
                        }
                    }
                }
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetActiveEmployees()
        {
            List<EmployeeModel> list = new List<EmployeeModel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT Id, FirstName, LastName, Username, ProfileImage FROM EmployeeDetails WHERE IsDeleted = 0 AND IsActive = 1";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new EmployeeModel
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"] != DBNull.Value ? reader["LastName"].ToString() : "",
                                ProfileImage = reader["ProfileImage"] != DBNull.Value ? reader["ProfileImage"].ToString() : ""
                            });
                        }
                    }
                }
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Review Cycles
        public ActionResult ReviewCycles()
        {
            if (Session["RoleType"] == null || Session["UserId"] == null) 
                return RedirectToAction("Login", "Auth");

            List<ReviewCycleModel> list = new List<ReviewCycleModel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT ReviewCycleId, ReviewCycleName, Description, Frequency, Status, 
                           CreateDate, CreateBy, UpdateDate, UpdateBy, SystemAddon 
                    FROM ReviewCycles 
                    ORDER BY ReviewCycleId DESC";
                
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new ReviewCycleModel
                            {
                                ReviewCycleId = Convert.ToInt32(reader["ReviewCycleId"]),
                                ReviewCycleName = reader["ReviewCycleName"].ToString(),
                                Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : "",
                                Frequency = reader["Frequency"] != DBNull.Value ? reader["Frequency"].ToString() : "",
                                Status = reader["Status"] != DBNull.Value ? reader["Status"].ToString() : "Active",
                                CreateBy = reader["CreateBy"] != DBNull.Value ? (int?)Convert.ToInt32(reader["CreateBy"]) : null,
                                CreateDate = reader["CreateDate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["CreateDate"]) : null,
                                UpdateBy = reader["UpdateBy"] != DBNull.Value ? (int?)Convert.ToInt32(reader["UpdateBy"]) : null,
                                UpdateDate = reader["UpdateDate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["UpdateDate"]) : null,
                                SystemAddon = reader["SystemAddon"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["SystemAddon"]) : null
                            });
                        }
                    }
                }
            }
            return View(list);
        }

        [HttpGet]
        public JsonResult GetReviewCycle(int id)
        {
            try
            {
                if (Session["RoleType"] == null || Session["UserId"] == null) 
                    return Json(new { success = false, message = "Unauthorized access" }, JsonRequestBehavior.AllowGet);

                ReviewCycleModel model = null;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = @"
                        SELECT ReviewCycleId, ReviewCycleName, Description, Frequency, Status, 
                               CreateDate, CreateBy, UpdateDate, UpdateBy, SystemAddon 
                        FROM ReviewCycles 
                        WHERE ReviewCycleId = @ReviewCycleId";
                    
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ReviewCycleId", id);
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                model = new ReviewCycleModel
                                {
                                    ReviewCycleId = Convert.ToInt32(reader["ReviewCycleId"]),
                                    ReviewCycleName = reader["ReviewCycleName"].ToString(),
                                    Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : "",
                                    Frequency = reader["Frequency"] != DBNull.Value ? reader["Frequency"].ToString() : "",
                                    Status = reader["Status"] != DBNull.Value ? reader["Status"].ToString() : "Active",
                                    CreateBy = reader["CreateBy"] != DBNull.Value ? (int?)Convert.ToInt32(reader["CreateBy"]) : null,
                                    CreateDate = reader["CreateDate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["CreateDate"]) : null,
                                    UpdateBy = reader["UpdateBy"] != DBNull.Value ? (int?)Convert.ToInt32(reader["UpdateBy"]) : null,
                                    UpdateDate = reader["UpdateDate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["UpdateDate"]) : null,
                                    SystemAddon = reader["SystemAddon"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["SystemAddon"]) : null
                                };
                            }
                        }
                    }
                }

                if (model != null)
                {
                    return Json(new { success = true, data = model }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { success = false, message = "Review Cycle not found" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult SaveReviewCycle(ReviewCycleModel model)
        {
            try
            {
                if (Session["RoleType"] == null || Session["UserId"] == null) 
                    return Json(new { success = false, message = "Unauthorized access" });

                int updaterId = Convert.ToInt32(Session["UserId"]);

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    if (model.ReviewCycleId == 0)
                    {
                        string query = @"
                            INSERT INTO ReviewCycles (ReviewCycleName, Description, Frequency, Status, CreateBy, CreateDate, SystemAddon) 
                            VALUES (@ReviewCycleName, @Description, @Frequency, @Status, @CreateBy, GETDATE(), GETDATE())";
                        
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@ReviewCycleName", model.ReviewCycleName ?? "");
                            cmd.Parameters.AddWithValue("@Description", model.Description ?? "");
                            cmd.Parameters.AddWithValue("@Frequency", model.Frequency ?? "One-Time");
                            cmd.Parameters.AddWithValue("@Status", model.Status ?? "Active");
                            cmd.Parameters.AddWithValue("@CreateBy", updaterId);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        string query = @"
                            UPDATE ReviewCycles 
                            SET ReviewCycleName = @ReviewCycleName, Description = @Description, 
                                Frequency = @Frequency, Status = @Status, 
                                UpdateBy = @UpdateBy, UpdateDate = GETDATE() 
                            WHERE ReviewCycleId = @ReviewCycleId";
                        
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@ReviewCycleId", model.ReviewCycleId);
                            cmd.Parameters.AddWithValue("@ReviewCycleName", model.ReviewCycleName ?? "");
                            cmd.Parameters.AddWithValue("@Description", model.Description ?? "");
                            cmd.Parameters.AddWithValue("@Frequency", model.Frequency ?? "One-Time");
                            cmd.Parameters.AddWithValue("@Status", model.Status ?? "Active");
                            cmd.Parameters.AddWithValue("@UpdateBy", updaterId);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                return Json(new { success = true, message = "Review Cycle saved successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteReviewCycle(int id)
        {
            try
            {
                if (Session["RoleType"] == null || Session["UserId"] == null) 
                    return Json(new { success = false, message = "Unauthorized access" });

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM ReviewCycles WHERE ReviewCycleId = @ReviewCycleId";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ReviewCycleId", id);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true, message = "Review Cycle deleted successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        #endregion

        public ActionResult EmployeeReviews()
        {
            if (Session["RoleType"] == null || Session["UserId"] == null) 
                return RedirectToAction("Login", "Auth");

            string currentRole = Session["RoleName"]?.ToString() ?? "";
            ViewBag.IsAdmin = currentRole.Equals("Superadmin", StringComparison.OrdinalIgnoreCase) || 
                              currentRole.Equals("Admin", StringComparison.OrdinalIgnoreCase);

            List<EmployeeReviewModel> list = new List<EmployeeReviewModel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT r.ReviewId, r.EmployeeId, r.ReviewerId, r.ReviewCycleId, r.ReviewDate, r.Rating, r.Status, r.OverallComments,
                           emp.FirstName AS EmpFirst, emp.LastName AS EmpLast, emp.Email AS EmpEmail, emp.ProfileImage AS EmpImg, emp.RoleType AS EmpRoleId,
                           rev.FirstName AS RevFirst, rev.LastName AS RevLast, rev.Email AS RevEmail, rev.ProfileImage AS RevImg,
                           c.ReviewCycleName
                    FROM EmployeeReviews r
                    INNER JOIN EmployeeDetails emp ON r.EmployeeId = emp.Id
                    INNER JOIN EmployeeDetails rev ON r.ReviewerId = rev.Id
                    INNER JOIN ReviewCycles c ON r.ReviewCycleId = c.ReviewCycleId
                    ORDER BY r.ReviewId DESC";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new EmployeeReviewModel
                            {
                                ReviewId = Convert.ToInt32(reader["ReviewId"]),
                                EmployeeId = Convert.ToInt32(reader["EmployeeId"]),
                                EmployeeName = reader["EmpFirst"].ToString() + " " + reader["EmpLast"].ToString(),
                                EmployeeEmail = reader["EmpEmail"].ToString(),
                                EmployeeImage = reader["EmpImg"] != DBNull.Value ? reader["EmpImg"].ToString() : "",
                                ReviewerId = Convert.ToInt32(reader["ReviewerId"]),
                                ReviewerName = reader["RevFirst"].ToString() + " " + reader["RevLast"].ToString(),
                                ReviewerEmail = reader["RevEmail"].ToString(),
                                ReviewerImage = reader["RevImg"] != DBNull.Value ? reader["RevImg"].ToString() : "",
                                ReviewCycleId = Convert.ToInt32(reader["ReviewCycleId"]),
                                ReviewCycleName = reader["ReviewCycleName"].ToString(),
                                ReviewDate = Convert.ToDateTime(reader["ReviewDate"]),
                                Rating = reader["Rating"] != DBNull.Value ? (decimal?)Convert.ToDecimal(reader["Rating"]) : null,
                                Status = reader["Status"].ToString(),
                                OverallComments = reader["OverallComments"] != DBNull.Value ? reader["OverallComments"].ToString() : "",
                                EmployeeRoleId = reader["EmpRoleId"] != DBNull.Value ? (int?)Convert.ToInt32(reader["EmpRoleId"]) : null
                            });
                        }
                    }
                }
            }

            return View(list);
        }

        [HttpGet]
        public JsonResult GetEmployeeReviews()
        {
            try
            {
                if (Session["RoleType"] == null || Session["UserId"] == null)
                    return Json(new { success = false, message = "Unauthorized access" }, JsonRequestBehavior.AllowGet);

                List<EmployeeReviewModel> list = new List<EmployeeReviewModel>();
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = @"
                        SELECT r.ReviewId, r.EmployeeId, r.ReviewerId, r.ReviewCycleId, r.ReviewDate, r.Rating, r.Status, r.OverallComments,
                               emp.FirstName AS EmpFirst, emp.LastName AS EmpLast, emp.Email AS EmpEmail, emp.ProfileImage AS EmpImg, emp.RoleType AS EmpRoleId,
                               rev.FirstName AS RevFirst, rev.LastName AS RevLast, rev.Email AS RevEmail, rev.ProfileImage AS RevImg,
                               c.ReviewCycleName
                        FROM EmployeeReviews r
                        INNER JOIN EmployeeDetails emp ON r.EmployeeId = emp.Id
                        INNER JOIN EmployeeDetails rev ON r.ReviewerId = rev.Id
                        INNER JOIN ReviewCycles c ON r.ReviewCycleId = c.ReviewCycleId
                        ORDER BY r.ReviewId DESC";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new EmployeeReviewModel
                                {
                                    ReviewId = Convert.ToInt32(reader["ReviewId"]),
                                    EmployeeId = Convert.ToInt32(reader["EmployeeId"]),
                                    EmployeeName = reader["EmpFirst"].ToString() + " " + reader["EmpLast"].ToString(),
                                    EmployeeEmail = reader["EmpEmail"].ToString(),
                                    EmployeeImage = reader["EmpImg"] != DBNull.Value ? reader["EmpImg"].ToString() : "",
                                    ReviewerId = Convert.ToInt32(reader["ReviewerId"]),
                                    ReviewerName = reader["RevFirst"].ToString() + " " + reader["RevLast"].ToString(),
                                    ReviewerEmail = reader["RevEmail"].ToString(),
                                    ReviewerImage = reader["RevImg"] != DBNull.Value ? reader["RevImg"].ToString() : "",
                                    ReviewCycleId = Convert.ToInt32(reader["ReviewCycleId"]),
                                    ReviewCycleName = reader["ReviewCycleName"].ToString(),
                                    ReviewDate = Convert.ToDateTime(reader["ReviewDate"]),
                                    Rating = reader["Rating"] != DBNull.Value ? (decimal?)Convert.ToDecimal(reader["Rating"]) : null,
                                    Status = reader["Status"].ToString(),
                                    OverallComments = reader["OverallComments"] != DBNull.Value ? reader["OverallComments"].ToString() : "",
                                    EmployeeRoleId = reader["EmpRoleId"] != DBNull.Value ? (int?)Convert.ToInt32(reader["EmpRoleId"]) : null
                                });
                            }
                        }
                    }
                }
                return Json(new { success = true, data = list }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult SaveEmployeeReview(EmployeeReviewModel model)
        {
            try
            {
                if (Session["RoleType"] == null || Session["UserId"] == null)
                    return Json(new { success = false, message = "Unauthorized access" });

                string currentRole = Session["RoleName"]?.ToString() ?? "";
                if (!currentRole.Equals("Superadmin", StringComparison.OrdinalIgnoreCase) && 
                    !currentRole.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                {
                    return Json(new { success = false, message = "Access Denied. Only Administrators can schedule reviews." });
                }

                string creator = Session["UserName"]?.ToString() ?? "System";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    if (model.ReviewId == 0)
                    {
                        string query = @"
                            INSERT INTO EmployeeReviews (EmployeeId, ReviewerId, ReviewCycleId, ReviewDate, Status, CreatedAt, CreatedBy)
                            VALUES (@EmployeeId, @ReviewerId, @ReviewCycleId, @ReviewDate, 'Scheduled', GETDATE(), @CreatedBy)";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@EmployeeId", model.EmployeeId);
                            cmd.Parameters.AddWithValue("@ReviewerId", model.ReviewerId);
                            cmd.Parameters.AddWithValue("@ReviewCycleId", model.ReviewCycleId);
                            cmd.Parameters.AddWithValue("@ReviewDate", model.ReviewDate);
                            cmd.Parameters.AddWithValue("@CreatedBy", creator);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        string query = @"
                            UPDATE EmployeeReviews
                            SET EmployeeId = @EmployeeId, ReviewerId = @ReviewerId, ReviewCycleId = @ReviewCycleId, 
                                ReviewDate = @ReviewDate, UpdatedAt = GETDATE(), UpdatedBy = @UpdatedBy
                            WHERE ReviewId = @ReviewId";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@ReviewId", model.ReviewId);
                            cmd.Parameters.AddWithValue("@EmployeeId", model.EmployeeId);
                            cmd.Parameters.AddWithValue("@ReviewerId", model.ReviewerId);
                            cmd.Parameters.AddWithValue("@ReviewCycleId", model.ReviewCycleId);
                            cmd.Parameters.AddWithValue("@ReviewDate", model.ReviewDate);
                            cmd.Parameters.AddWithValue("@UpdatedBy", creator);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                return Json(new { success = true, message = "Review scheduled successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public JsonResult GetEmployeeReviewById(int id)
        {
            try
            {
                if (Session["RoleType"] == null || Session["UserId"] == null)
                    return Json(new { success = false, message = "Unauthorized access" }, JsonRequestBehavior.AllowGet);

                EmployeeReviewModel model = null;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = @"
                        SELECT ReviewId, EmployeeId, ReviewerId, ReviewCycleId, ReviewDate, Rating, Status, OverallComments
                        FROM EmployeeReviews
                        WHERE ReviewId = @ReviewId";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ReviewId", id);
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                model = new EmployeeReviewModel
                                {
                                    ReviewId = Convert.ToInt32(reader["ReviewId"]),
                                    EmployeeId = Convert.ToInt32(reader["EmployeeId"]),
                                    ReviewerId = Convert.ToInt32(reader["ReviewerId"]),
                                    ReviewCycleId = Convert.ToInt32(reader["ReviewCycleId"]),
                                    ReviewDate = Convert.ToDateTime(reader["ReviewDate"]),
                                    Rating = reader["Rating"] != DBNull.Value ? (decimal?)Convert.ToDecimal(reader["Rating"]) : null,
                                    Status = reader["Status"].ToString(),
                                    OverallComments = reader["OverallComments"] != DBNull.Value ? reader["OverallComments"].ToString() : ""
                                };
                            }
                        }
                    }
                }

                if (model != null)
                    return Json(new { success = true, data = model }, JsonRequestBehavior.AllowGet);

                return Json(new { success = false, message = "Review not found" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult GetIndicatorsForEmployee(int employeeId)
        {
            try
            {
                if (Session["RoleType"] == null || Session["UserId"] == null)
                    return Json(new { success = false, message = "Unauthorized access" }, JsonRequestBehavior.AllowGet);

                int? employeeRoleId = null;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT RoleType FROM EmployeeDetails WHERE Id = @Id", con))
                    {
                        cmd.Parameters.AddWithValue("@Id", employeeId);
                        object res = cmd.ExecuteScalar();
                        if (res != null && res != DBNull.Value)
                        {
                            employeeRoleId = Convert.ToInt32(res);
                        }
                    }
                }

                List<IndicatorModel> indicators = new List<IndicatorModel>();
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = @"
                        SELECT I.IndicatorId, I.IndicatorCategoryId, C.CategoryName, I.IndicatorName, I.Description, 
                               I.MeasurementUnit, I.TargetValue
                        FROM Indicators I
                        INNER JOIN IndicatorCategories C ON I.IndicatorCategoryId = C.IndicatorCategoryId
                        WHERE I.IsActive = 1 AND (I.RoleId IS NULL OR I.RoleId = @RoleId)
                        ORDER BY C.CategoryName, I.IndicatorName";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@RoleId", (object)employeeRoleId ?? DBNull.Value);
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                indicators.Add(new IndicatorModel
                                {
                                    IndicatorId = Convert.ToInt32(reader["IndicatorId"]),
                                    IndicatorCategoryId = Convert.ToInt32(reader["IndicatorCategoryId"]),
                                    CategoryName = reader["CategoryName"].ToString(),
                                    IndicatorName = reader["IndicatorName"].ToString(),
                                    Description = reader["Description"].ToString(),
                                    MeasurementUnit = reader["MeasurementUnit"] != DBNull.Value ? reader["MeasurementUnit"].ToString() : "",
                                    TargetValue = reader["TargetValue"] != DBNull.Value ? reader["TargetValue"].ToString() : ""
                                });
                            }
                        }
                    }
                }

                return Json(new { success = true, data = indicators }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult SubmitEmployeeReview(ReviewSubmissionModel model)
        {
            try
            {
                if (Session["RoleType"] == null || Session["UserId"] == null)
                    return Json(new { success = false, message = "Unauthorized access" });

                if (model.Ratings == null || model.Ratings.Count == 0)
                    return Json(new { success = false, message = "Please rate at least one performance indicator." });

                double totalRating = 0;
                foreach (var r in model.Ratings)
                {
                    totalRating += r.RatingValue;
                }
                decimal averageRating = (decimal)(totalRating / model.Ratings.Count);
                string updater = Session["UserName"]?.ToString() ?? "System";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    using (SqlTransaction transaction = con.BeginTransaction())
                    {
                        try
                        {
                            string deleteRatingsQuery = "DELETE FROM EmployeeReviewRatings WHERE ReviewId = @ReviewId";
                            using (SqlCommand cmd = new SqlCommand(deleteRatingsQuery, con, transaction))
                            {
                                cmd.Parameters.AddWithValue("@ReviewId", model.ReviewId);
                                cmd.ExecuteNonQuery();
                            }

                            string insertRatingQuery = @"
                                INSERT INTO EmployeeReviewRatings (ReviewId, IndicatorId, RatingValue, Comments)
                                VALUES (@ReviewId, @IndicatorId, @RatingValue, @Comments)";
                            foreach (var rating in model.Ratings)
                            {
                                using (SqlCommand cmd = new SqlCommand(insertRatingQuery, con, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@ReviewId", model.ReviewId);
                                    cmd.Parameters.AddWithValue("@IndicatorId", rating.IndicatorId);
                                    cmd.Parameters.AddWithValue("@RatingValue", rating.RatingValue);
                                    cmd.Parameters.AddWithValue("@Comments", rating.Comments ?? "");
                                    cmd.ExecuteNonQuery();
                                }
                            }

                            string updateReviewQuery = @"
                                UPDATE EmployeeReviews
                                SET Rating = @Rating, Status = 'Completed', OverallComments = @OverallComments,
                                    UpdatedAt = GETDATE(), UpdatedBy = @UpdatedBy
                                WHERE ReviewId = @ReviewId";
                            using (SqlCommand cmd = new SqlCommand(updateReviewQuery, con, transaction))
                            {
                                cmd.Parameters.AddWithValue("@ReviewId", model.ReviewId);
                                cmd.Parameters.AddWithValue("@Rating", averageRating);
                                cmd.Parameters.AddWithValue("@OverallComments", model.OverallComments ?? "");
                                cmd.Parameters.AddWithValue("@UpdatedBy", updater);
                                cmd.ExecuteNonQuery();
                            }

                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw ex;
                        }
                    }
                }

                return Json(new { success = true, message = "Performance review submitted successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public JsonResult GetConductedReviewDetails(int reviewId)
        {
            try
            {
                if (Session["RoleType"] == null || Session["UserId"] == null)
                    return Json(new { success = false, message = "Unauthorized access" }, JsonRequestBehavior.AllowGet);

                EmployeeReviewModel review = null;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = @"
                        SELECT r.ReviewId, r.EmployeeId, r.ReviewerId, r.ReviewCycleId, r.ReviewDate, r.Rating, r.Status, r.OverallComments,
                               emp.FirstName AS EmpFirst, emp.LastName AS EmpLast, emp.Email AS EmpEmail, emp.ProfileImage AS EmpImg,
                               rev.FirstName AS RevFirst, rev.LastName AS RevLast, rev.Email AS RevEmail, rev.ProfileImage AS RevImg,
                               c.ReviewCycleName
                        FROM EmployeeReviews r
                        INNER JOIN EmployeeDetails emp ON r.EmployeeId = emp.Id
                        INNER JOIN EmployeeDetails rev ON r.ReviewerId = rev.Id
                        INNER JOIN ReviewCycles c ON r.ReviewCycleId = c.ReviewCycleId
                        WHERE r.ReviewId = @ReviewId";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ReviewId", reviewId);
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                review = new EmployeeReviewModel
                                {
                                    ReviewId = Convert.ToInt32(reader["ReviewId"]),
                                    EmployeeId = Convert.ToInt32(reader["EmployeeId"]),
                                    EmployeeName = reader["EmpFirst"].ToString() + " " + reader["EmpLast"].ToString(),
                                    EmployeeEmail = reader["EmpEmail"].ToString(),
                                    EmployeeImage = reader["EmpImg"] != DBNull.Value ? reader["EmpImg"].ToString() : "",
                                    ReviewerId = Convert.ToInt32(reader["ReviewerId"]),
                                    ReviewerName = reader["RevFirst"].ToString() + " " + reader["RevLast"].ToString(),
                                    ReviewerEmail = reader["RevEmail"].ToString(),
                                    ReviewerImage = reader["RevImg"] != DBNull.Value ? reader["RevImg"].ToString() : "",
                                    ReviewCycleId = Convert.ToInt32(reader["ReviewCycleId"]),
                                    ReviewCycleName = reader["ReviewCycleName"].ToString(),
                                    ReviewDate = Convert.ToDateTime(reader["ReviewDate"]),
                                    Rating = reader["Rating"] != DBNull.Value ? (decimal?)Convert.ToDecimal(reader["Rating"]) : null,
                                    Status = reader["Status"].ToString(),
                                    OverallComments = reader["OverallComments"] != DBNull.Value ? reader["OverallComments"].ToString() : ""
                                };
                            }
                        }
                    }
                }

                if (review == null)
                    return Json(new { success = false, message = "Review not found" }, JsonRequestBehavior.AllowGet);

                List<EmployeeReviewRatingModel> ratings = new List<EmployeeReviewRatingModel>();
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = @"
                        SELECT r.ReviewRatingId, r.ReviewId, r.IndicatorId, r.RatingValue, r.Comments,
                               i.IndicatorName, c.CategoryName, i.MeasurementUnit, i.TargetValue, i.Description
                        FROM EmployeeReviewRatings r
                        INNER JOIN Indicators i ON r.IndicatorId = i.IndicatorId
                        INNER JOIN IndicatorCategories c ON i.IndicatorCategoryId = c.IndicatorCategoryId
                        WHERE r.ReviewId = @ReviewId
                        ORDER BY c.CategoryName, i.IndicatorName";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ReviewId", reviewId);
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ratings.Add(new EmployeeReviewRatingModel
                                {
                                    ReviewRatingId = Convert.ToInt32(reader["ReviewRatingId"]),
                                    ReviewId = Convert.ToInt32(reader["ReviewId"]),
                                    IndicatorId = Convert.ToInt32(reader["IndicatorId"]),
                                    RatingValue = Convert.ToInt32(reader["RatingValue"]),
                                    Comments = reader["Comments"].ToString(),
                                    IndicatorName = reader["IndicatorName"].ToString(),
                                    IndicatorCategoryName = reader["CategoryName"].ToString(),
                                    MeasurementUnit = reader["MeasurementUnit"] != DBNull.Value ? reader["MeasurementUnit"].ToString() : "",
                                    TargetValue = reader["TargetValue"] != DBNull.Value ? reader["TargetValue"].ToString() : "",
                                    Description = reader["Description"].ToString()
                                });
                            }
                        }
                    }
                }

                return Json(new { success = true, review = review, ratings = ratings }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult DeleteEmployeeReview(int id)
        {
            try
            {
                if (Session["RoleType"] == null || Session["UserId"] == null)
                    return Json(new { success = false, message = "Unauthorized access" });

                string currentRole = Session["RoleName"]?.ToString() ?? "";
                if (!currentRole.Equals("Superadmin", StringComparison.OrdinalIgnoreCase) && 
                    !currentRole.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                {
                    return Json(new { success = false, message = "Access Denied. Only Administrators can delete reviews." });
                }

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM EmployeeReviews WHERE ReviewId = @ReviewId";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ReviewId", id);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true, message = "Review deleted successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult ResetEmployeeReview(int id)
        {
            try
            {
                if (Session["RoleType"] == null || Session["UserId"] == null)
                    return Json(new { success = false, message = "Unauthorized access" });

                string currentRole = Session["RoleName"]?.ToString() ?? "";
                if (!currentRole.Equals("Superadmin", StringComparison.OrdinalIgnoreCase) && 
                    !currentRole.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                {
                    return Json(new { success = false, message = "Access Denied. Only Administrators can reset reviews." });
                }

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    using (SqlTransaction transaction = con.BeginTransaction())
                    {
                        try
                        {
                            using (SqlCommand cmd = new SqlCommand("DELETE FROM EmployeeReviewRatings WHERE ReviewId = @ReviewId", con, transaction))
                            {
                                cmd.Parameters.AddWithValue("@ReviewId", id);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = new SqlCommand("UPDATE EmployeeReviews SET Status = 'Scheduled', Rating = NULL, OverallComments = NULL, UpdatedAt = GETDATE(), UpdatedBy = @UpdatedBy WHERE ReviewId = @ReviewId", con, transaction))
                            {
                                cmd.Parameters.AddWithValue("@ReviewId", id);
                                cmd.Parameters.AddWithValue("@UpdatedBy", Session["UserName"]?.ToString() ?? "System");
                                cmd.ExecuteNonQuery();
                            }

                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw ex;
                        }
                    }
                }
                return Json(new { success = true, message = "Review reset successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public JsonResult GetActiveReviewCycles()
        {
            try
            {
                List<object> list = new List<object>();
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "SELECT ReviewCycleId, ReviewCycleName FROM ReviewCycles WHERE Status = 'Active' ORDER BY ReviewCycleName";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new
                                {
                                    ReviewCycleId = Convert.ToInt32(reader["ReviewCycleId"]),
                                    ReviewCycleName = reader["ReviewCycleName"].ToString()
                                });
                            }
                        }
                    }
                }
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
