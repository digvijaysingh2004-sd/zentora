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
                            SystemAddon BIT NOT NULL DEFAULT 0
                        );
                        
                        -- Seed some initial data
                        INSERT INTO dbo.GoalTypes (GoalTypeName, Description, Status, CreatedBy, CreateDate, SystemAddon) VALUES
                        (N'Technical Goal', N'Goals related to improving technical skills, architecture, and coding quality.', N'Active', N'System', GETDATE(), 1),
                        (N'Behavioral Goal', N'Goals related to communication, leadership, teamwork, and ownership.', N'Active', N'System', GETDATE(), 1),
                        (N'Project Delivery Goal', N'Goals related to project milestones, delivery timelines, and client satisfaction.', N'Active', N'System', GETDATE(), 1);
                    END";

                using (SqlCommand cmd = new SqlCommand(createGoalTypesQuery, con))
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
                                SystemAddon = Convert.ToBoolean(reader["SystemAddon"])
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
                                     VALUES (@GoalTypeName, @Description, @Status, @CreatedBy, GETDATE(), 0)";
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
                                SystemAddon = Convert.ToBoolean(reader["SystemAddon"])
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

        #region Boilerplate Actions
        public ActionResult EmployeeGoals() { return View(); }
        public ActionResult ReviewCycles() { return View(); }
        public ActionResult EmployeeReviews() { return View(); }
        #endregion
    }
}
