using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using zentoraHRMS.Models;

namespace zentoraHRMS.Controllers
{
    public class HRController : Controller
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["Zentora"].ConnectionString;

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            base.OnActionExecuting(filterContext);
        }

        #region Designations
        public ActionResult Designations()
        {
            if (Session["RoleType"] == null || Session["UserId"] == null) return RedirectToAction("Login", "Auth");

            List<DesignationModel> list = new List<DesignationModel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT DesignationId, DesignationName, Description, IsActive FROM Designations";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new DesignationModel
                            {
                                DesignationId = Convert.ToInt32(reader["DesignationId"]),
                                DesignationName = reader["DesignationName"].ToString(),
                                Description = reader["Description"].ToString(),
                                IsActive = Convert.ToBoolean(reader["IsActive"])
                            });
                        }
                    }
                }
            }
            return View(list);
        }

        [HttpPost]
        public JsonResult SaveDesignation(DesignationModel model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Designations (DesignationName, Description, IsActive) VALUES (@DesignationName, @Description, 1)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@DesignationName", model.DesignationName ?? "");
                        cmd.Parameters.AddWithValue("@Description", model.Description ?? "");
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true, message = "Designation saved successfully!" });
            }
            catch (Exception ex) { return Json(new { success = false, message = ex.Message }); }
        }

        public JsonResult GetDesignationById(int id)
        {
            DesignationModel model = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT DesignationId, DesignationName, Description, IsActive FROM Designations WHERE DesignationId = @DesignationId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@DesignationId", id);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            model = new DesignationModel
                            {
                                DesignationId = Convert.ToInt32(reader["DesignationId"]),
                                DesignationName = reader["DesignationName"].ToString(),
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
        public JsonResult UpdateDesignation(DesignationModel model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Designations SET DesignationName = @DesignationName, Description = @Description WHERE DesignationId = @DesignationId";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@DesignationId", model.DesignationId);
                        cmd.Parameters.AddWithValue("@DesignationName", model.DesignationName ?? "");
                        cmd.Parameters.AddWithValue("@Description", model.Description ?? "");
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true });
            }
            catch (Exception ex) { return Json(new { success = false, message = ex.Message }); }
        }

        [HttpPost]
        public JsonResult DeleteDesignation(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Designations WHERE DesignationId = @DesignationId";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@DesignationId", id);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true });
            }
            catch (Exception ex) { return Json(new { success = false, message = ex.Message }); }
        }
        #endregion

        #region Awards
        public ActionResult Awards()
        {
            if (Session["RoleType"] == null || Session["UserId"] == null) return RedirectToAction("Login", "Auth");

            List<AwardModel> list = new List<AwardModel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"SELECT A.AwardId, A.EmployeeId, E.FirstName + ' ' + E.LastName AS EmployeeName, 
                                 A.AwardType, A.Gift, A.CashPrice, A.AwardDate, A.Description 
                                 FROM Awards A INNER JOIN EmployeeDetails E ON A.EmployeeId = E.Id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new AwardModel
                            {
                                AwardId = Convert.ToInt32(reader["AwardId"]),
                                EmployeeId = Convert.ToInt32(reader["EmployeeId"]),
                                EmployeeName = reader["EmployeeName"].ToString(),
                                AwardType = reader["AwardType"].ToString(),
                                Gift = reader["Gift"].ToString(),
                                CashPrice = Convert.ToDecimal(reader["CashPrice"]),
                                AwardDate = Convert.ToDateTime(reader["AwardDate"]),
                                Description = reader["Description"].ToString()
                            });
                        }
                    }
                }
            }
            return View(list);
        }

        [HttpPost]
        public JsonResult SaveAward(AwardModel model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = @"INSERT INTO Awards (EmployeeId, AwardType, Gift, CashPrice, AwardDate, Description) 
                                     VALUES (@EmployeeId, @AwardType, @Gift, @CashPrice, @AwardDate, @Description)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@EmployeeId", model.EmployeeId);
                        cmd.Parameters.AddWithValue("@AwardType", model.AwardType ?? "");
                        cmd.Parameters.AddWithValue("@Gift", model.Gift ?? "");
                        cmd.Parameters.AddWithValue("@CashPrice", model.CashPrice);
                        cmd.Parameters.AddWithValue("@AwardDate", model.AwardDate);
                        cmd.Parameters.AddWithValue("@Description", model.Description ?? "");
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true, message = "Award saved successfully!" });
            }
            catch (Exception ex) { return Json(new { success = false, message = ex.Message }); }
        }

        public JsonResult GetAwardById(int id)
        {
            AwardModel model = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT AwardId, EmployeeId, AwardType, Gift, CashPrice, AwardDate, Description FROM Awards WHERE AwardId = @AwardId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@AwardId", id);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            model = new AwardModel
                            {
                                AwardId = Convert.ToInt32(reader["AwardId"]),
                                EmployeeId = Convert.ToInt32(reader["EmployeeId"]),
                                AwardType = reader["AwardType"].ToString(),
                                Gift = reader["Gift"].ToString(),
                                CashPrice = Convert.ToDecimal(reader["CashPrice"]),
                                AwardDate = Convert.ToDateTime(reader["AwardDate"]),
                                Description = reader["Description"].ToString()
                            };
                        }
                    }
                }
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateAward(AwardModel model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = @"UPDATE Awards SET EmployeeId = @EmployeeId, AwardType = @AwardType, Gift = @Gift, 
                                     CashPrice = @CashPrice, AwardDate = @AwardDate, Description = @Description WHERE AwardId = @AwardId";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@AwardId", model.AwardId);
                        cmd.Parameters.AddWithValue("@EmployeeId", model.EmployeeId);
                        cmd.Parameters.AddWithValue("@AwardType", model.AwardType ?? "");
                        cmd.Parameters.AddWithValue("@Gift", model.Gift ?? "");
                        cmd.Parameters.AddWithValue("@CashPrice", model.CashPrice);
                        cmd.Parameters.AddWithValue("@AwardDate", model.AwardDate);
                        cmd.Parameters.AddWithValue("@Description", model.Description ?? "");
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true });
            }
            catch (Exception ex) { return Json(new { success = false, message = ex.Message }); }
        }

        [HttpPost]
        public JsonResult DeleteAward(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Awards WHERE AwardId = @AwardId";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@AwardId", id);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true });
            }
            catch (Exception ex) { return Json(new { success = false, message = ex.Message }); }
        }
        #endregion

        #region Award Types
        public ActionResult AwardTypes()
        {
            if (Session["RoleType"] == null || Session["UserId"] == null) return RedirectToAction("Login", "Auth");
            return View();
        }
        #endregion

        #region Promotions
        public ActionResult Promotions()
        {
            if (Session["RoleType"] == null || Session["UserId"] == null) return RedirectToAction("Login", "Auth");

            List<PromotionModel> list = new List<PromotionModel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"SELECT P.PromotionId, P.EmployeeId, E.FirstName + ' ' + E.LastName AS EmployeeName, 
                                 P.DesignationId, D.DesignationName, P.PromotionDate, P.Description 
                                 FROM Promotions P 
                                 INNER JOIN EmployeeDetails E ON P.EmployeeId = E.Id 
                                 INNER JOIN Designations D ON P.DesignationId = D.DesignationId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new PromotionModel
                            {
                                PromotionId = Convert.ToInt32(reader["PromotionId"]),
                                EmployeeId = Convert.ToInt32(reader["EmployeeId"]),
                                EmployeeName = reader["EmployeeName"].ToString(),
                                DesignationId = Convert.ToInt32(reader["DesignationId"]),
                                DesignationName = reader["DesignationName"].ToString(),
                                PromotionDate = Convert.ToDateTime(reader["PromotionDate"]),
                                Description = reader["Description"].ToString()
                            });
                        }
                    }
                }
            }
            return View(list);
        }

        [HttpPost]
        public JsonResult SavePromotion(PromotionModel model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = @"INSERT INTO Promotions (EmployeeId, DesignationId, PromotionDate, Description) 
                                     VALUES (@EmployeeId, @DesignationId, @PromotionDate, @Description)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@EmployeeId", model.EmployeeId);
                        cmd.Parameters.AddWithValue("@DesignationId", model.DesignationId);
                        cmd.Parameters.AddWithValue("@PromotionDate", model.PromotionDate);
                        cmd.Parameters.AddWithValue("@Description", model.Description ?? "");
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true, message = "Promotion saved successfully!" });
            }
            catch (Exception ex) { return Json(new { success = false, message = ex.Message }); }
        }

        public JsonResult GetPromotionById(int id)
        {
            PromotionModel model = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT PromotionId, EmployeeId, DesignationId, PromotionDate, Description FROM Promotions WHERE PromotionId = @PromotionId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@PromotionId", id);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            model = new PromotionModel
                            {
                                PromotionId = Convert.ToInt32(reader["PromotionId"]),
                                EmployeeId = Convert.ToInt32(reader["EmployeeId"]),
                                DesignationId = Convert.ToInt32(reader["DesignationId"]),
                                PromotionDate = Convert.ToDateTime(reader["PromotionDate"]),
                                Description = reader["Description"].ToString()
                            };
                        }
                    }
                }
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdatePromotion(PromotionModel model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = @"UPDATE Promotions SET EmployeeId = @EmployeeId, DesignationId = @DesignationId, 
                                     PromotionDate = @PromotionDate, Description = @Description WHERE PromotionId = @PromotionId";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@PromotionId", model.PromotionId);
                        cmd.Parameters.AddWithValue("@EmployeeId", model.EmployeeId);
                        cmd.Parameters.AddWithValue("@DesignationId", model.DesignationId);
                        cmd.Parameters.AddWithValue("@PromotionDate", model.PromotionDate);
                        cmd.Parameters.AddWithValue("@Description", model.Description ?? "");
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true });
            }
            catch (Exception ex) { return Json(new { success = false, message = ex.Message }); }
        }

        [HttpPost]
        public JsonResult DeletePromotion(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Promotions WHERE PromotionId = @PromotionId";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@PromotionId", id);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true });
            }
            catch (Exception ex) { return Json(new { success = false, message = ex.Message }); }
        }
        #endregion

        #region Announcements
        public ActionResult Announcements()
        {
            if (Session["RoleType"] == null || Session["UserId"] == null) return RedirectToAction("Login", "Auth");

            List<AnnouncementModel> list = new List<AnnouncementModel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT AnnouncementId, Title, Description, StartDate, EndDate, CreatedBy FROM Announcements";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new AnnouncementModel
                            {
                                AnnouncementId = Convert.ToInt32(reader["AnnouncementId"]),
                                Title = reader["Title"].ToString(),
                                Description = reader["Description"].ToString(),
                                StartDate = Convert.ToDateTime(reader["StartDate"]),
                                EndDate = Convert.ToDateTime(reader["EndDate"]),
                                CreatedBy = reader["CreatedBy"].ToString()
                            });
                        }
                    }
                }
            }
            return View(list);
        }

        [HttpPost]
        public JsonResult SaveAnnouncement(AnnouncementModel model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = @"INSERT INTO Announcements (Title, Description, StartDate, EndDate, CreatedBy) 
                                     VALUES (@Title, @Description, @StartDate, @EndDate, @CreatedBy)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Title", model.Title ?? "");
                        cmd.Parameters.AddWithValue("@Description", model.Description ?? "");
                        cmd.Parameters.AddWithValue("@StartDate", model.StartDate);
                        cmd.Parameters.AddWithValue("@EndDate", model.EndDate);
                        cmd.Parameters.AddWithValue("@CreatedBy", Session["UserName"]?.ToString() ?? "System");
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true, message = "Announcement saved successfully!" });
            }
            catch (Exception ex) { return Json(new { success = false, message = ex.Message }); }
        }

        public JsonResult GetAnnouncementById(int id)
        {
            AnnouncementModel model = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT AnnouncementId, Title, Description, StartDate, EndDate, CreatedBy FROM Announcements WHERE AnnouncementId = @AnnouncementId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@AnnouncementId", id);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            model = new AnnouncementModel
                            {
                                AnnouncementId = Convert.ToInt32(reader["AnnouncementId"]),
                                Title = reader["Title"].ToString(),
                                Description = reader["Description"].ToString(),
                                StartDate = Convert.ToDateTime(reader["StartDate"]),
                                EndDate = Convert.ToDateTime(reader["EndDate"]),
                                CreatedBy = reader["CreatedBy"].ToString()
                            };
                        }
                    }
                }
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateAnnouncement(AnnouncementModel model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = @"UPDATE Announcements SET Title = @Title, Description = @Description, 
                                     StartDate = @StartDate, EndDate = @EndDate WHERE AnnouncementId = @AnnouncementId";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@AnnouncementId", model.AnnouncementId);
                        cmd.Parameters.AddWithValue("@Title", model.Title ?? "");
                        cmd.Parameters.AddWithValue("@Description", model.Description ?? "");
                        cmd.Parameters.AddWithValue("@StartDate", model.StartDate);
                        cmd.Parameters.AddWithValue("@EndDate", model.EndDate);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true });
            }
            catch (Exception ex) { return Json(new { success = false, message = ex.Message }); }
        }

        [HttpPost]
        public JsonResult DeleteAnnouncement(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Announcements WHERE AnnouncementId = @AnnouncementId";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@AnnouncementId", id);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true });
            }
            catch (Exception ex) { return Json(new { success = false, message = ex.Message }); }
        }
        #endregion

        #region Assets
        public ActionResult Assets()
        {
            if (Session["RoleType"] == null || Session["UserId"] == null) return RedirectToAction("Login", "Auth");

            List<AssetModel> list = new List<AssetModel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"SELECT A.AssetId, A.AssetName, A.AssetCode, A.EmployeeId, 
                                 ISNULL(E.FirstName + ' ' + E.LastName, 'Unassigned') AS EmployeeName, 
                                 A.PurchaseDate, A.WarrantyDate, A.Status 
                                 FROM Assets A LEFT JOIN EmployeeDetails E ON A.EmployeeId = E.Id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new AssetModel
                            {
                                AssetId = Convert.ToInt32(reader["AssetId"]),
                                AssetName = reader["AssetName"].ToString(),
                                AssetCode = reader["AssetCode"].ToString(),
                                EmployeeId = reader["EmployeeId"] != DBNull.Value ? (int?)Convert.ToInt32(reader["EmployeeId"]) : null,
                                EmployeeName = reader["EmployeeName"].ToString(),
                                PurchaseDate = reader["PurchaseDate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["PurchaseDate"]) : null,
                                WarrantyDate = reader["WarrantyDate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["WarrantyDate"]) : null,
                                Status = reader["Status"].ToString()
                            });
                        }
                    }
                }
            }
            return View(list);
        }

        [HttpPost]
        public JsonResult SaveAsset(AssetModel model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = @"INSERT INTO Assets (AssetName, AssetCode, EmployeeId, PurchaseDate, WarrantyDate, Status) 
                                     VALUES (@AssetName, @AssetCode, @EmployeeId, @PurchaseDate, @WarrantyDate, @Status)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@AssetName", model.AssetName ?? "");
                        cmd.Parameters.AddWithValue("@AssetCode", model.AssetCode ?? "");
                        cmd.Parameters.AddWithValue("@EmployeeId", (object)model.EmployeeId ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@PurchaseDate", (object)model.PurchaseDate ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@WarrantyDate", (object)model.WarrantyDate ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Status", model.Status ?? "Working");
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true, message = "Asset saved successfully!" });
            }
            catch (Exception ex) { return Json(new { success = false, message = ex.Message }); }
        }

        public JsonResult GetAssetById(int id)
        {
            AssetModel model = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT AssetId, AssetName, AssetCode, EmployeeId, PurchaseDate, WarrantyDate, Status FROM Assets WHERE AssetId = @AssetId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@AssetId", id);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            model = new AssetModel
                            {
                                AssetId = Convert.ToInt32(reader["AssetId"]),
                                AssetName = reader["AssetName"].ToString(),
                                AssetCode = reader["AssetCode"].ToString(),
                                EmployeeId = reader["EmployeeId"] != DBNull.Value ? (int?)Convert.ToInt32(reader["EmployeeId"]) : null,
                                PurchaseDate = reader["PurchaseDate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["PurchaseDate"]) : null,
                                WarrantyDate = reader["WarrantyDate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["WarrantyDate"]) : null,
                                Status = reader["Status"].ToString()
                            };
                        }
                    }
                }
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateAsset(AssetModel model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = @"UPDATE Assets SET AssetName = @AssetName, AssetCode = @AssetCode, EmployeeId = @EmployeeId, 
                                     PurchaseDate = @PurchaseDate, WarrantyDate = @WarrantyDate, Status = @Status WHERE AssetId = @AssetId";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@AssetId", model.AssetId);
                        cmd.Parameters.AddWithValue("@AssetName", model.AssetName ?? "");
                        cmd.Parameters.AddWithValue("@AssetCode", model.AssetCode ?? "");
                        cmd.Parameters.AddWithValue("@EmployeeId", (object)model.EmployeeId ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@PurchaseDate", (object)model.PurchaseDate ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@WarrantyDate", (object)model.WarrantyDate ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Status", model.Status ?? "Working");
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true });
            }
            catch (Exception ex) { return Json(new { success = false, message = ex.Message }); }
        }

        [HttpPost]
        public JsonResult DeleteAsset(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Assets WHERE AssetId = @AssetId";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@AssetId", id);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true });
            }
            catch (Exception ex) { return Json(new { success = false, message = ex.Message }); }
        }
        #endregion

        #region Helpers
        [HttpGet]
        public JsonResult GetDesignationsList()
        {
            List<DesignationModel> list = new List<DesignationModel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT DesignationId, DesignationName FROM Designations WHERE IsActive = 1";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new DesignationModel
                            {
                                DesignationId = Convert.ToInt32(reader["DesignationId"]),
                                DesignationName = reader["DesignationName"].ToString()
                            });
                        }
                    }
                }
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        #endregion
            public ActionResult Resignations() { return View(); }
        public ActionResult Terminations() { return View(); }
        public ActionResult Warnings() { return View(); }
        public ActionResult Trips() { return View(); }
        public ActionResult Complaints() { return View(); }
        public ActionResult Transfers() { return View(); }
        public ActionResult Training() { return View(); }
}
}
