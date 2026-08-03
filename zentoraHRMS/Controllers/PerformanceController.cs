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

            List<IndicatorModel> list = new List<IndicatorModel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"SELECT I.IndicatorId, I.IndicatorCategoryId, C.CategoryName, I.IndicatorName, I.Description, 
                                        I.IsActive, I.MeasurementUnit, I.TargetValue, I.CreatedAt, I.UpdatedAt, I.CreatedBy, I.UpdatedBy 
                                 FROM Indicators I
                                 INNER JOIN IndicatorCategories C ON I.IndicatorCategoryId = C.IndicatorCategoryId
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
                                UpdatedBy = reader["UpdatedBy"].ToString()
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
                string creator = Session["UserName"]?.ToString() ?? "System";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = @"INSERT INTO Indicators (IndicatorCategoryId, IndicatorName, Description, IsActive, MeasurementUnit, TargetValue, CreatedAt, CreatedBy) 
                                     VALUES (@IndicatorCategoryId, @IndicatorName, @Description, @IsActive, @MeasurementUnit, @TargetValue, GETDATE(), @CreatedBy)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@IndicatorCategoryId", model.IndicatorCategoryId);
                        cmd.Parameters.AddWithValue("@IndicatorName", model.IndicatorName ?? "");
                        cmd.Parameters.AddWithValue("@Description", model.Description ?? "");
                        cmd.Parameters.AddWithValue("@IsActive", model.IsActive);
                        cmd.Parameters.AddWithValue("@MeasurementUnit", model.MeasurementUnit ?? "");
                        cmd.Parameters.AddWithValue("@TargetValue", model.TargetValue ?? "");
                        cmd.Parameters.AddWithValue("@CreatedBy", creator);
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
                string query = "SELECT IndicatorId, IndicatorCategoryId, IndicatorName, Description, IsActive, MeasurementUnit, TargetValue FROM Indicators WHERE IndicatorId = @IndicatorId";
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
                                TargetValue = reader["TargetValue"].ToString()
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
                string updater = Session["UserName"]?.ToString() ?? "System";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = @"UPDATE Indicators 
                                     SET IndicatorCategoryId = @IndicatorCategoryId, IndicatorName = @IndicatorName, 
                                         Description = @Description, IsActive = @IsActive, 
                                         MeasurementUnit = @MeasurementUnit, TargetValue = @TargetValue,
                                         UpdatedAt = GETDATE(), UpdatedBy = @UpdatedBy 
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

        #region Boilerplate Actions
        public ActionResult ReviewCycles() { return View(); }
        public ActionResult EmployeeReviews() { return View(); }
        #endregion
    }
}
