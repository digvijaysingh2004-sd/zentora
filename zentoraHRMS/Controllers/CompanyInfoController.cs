using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using zentoraHRMS.Models;
using System.Runtime.Remoting.Messaging;
using System.Data.Common;
using System.Drawing;

namespace zentoraHRMS.Controllers
{
    public class CompanyInfoController : Controller
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["Zentora"].ConnectionString;

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Prevent caching so back button won’t reload old pages
            Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();

            base.OnActionExecuting(filterContext);
        }

        #region Company

        public ActionResult Company()
        {
            if (Session["RoleType"] == null || Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            List<CompanyModel> companyList = new List<CompanyModel>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetCompany", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        CompanyModel company = new CompanyModel
                        {
                            CompanyId = reader["CompanyId"] != DBNull.Value ? Convert.ToInt32(reader["CompanyId"]) : 0,
                            CompanyName = reader["CompanyName"].ToString(),
                            CompanyCode = reader["CompanyCode"].ToString(),
                            RegistrationNumber = reader["RegistrationNumber"].ToString(),
                            CompanyType = reader["CompanyType"].ToString(),
                            IndustryType = reader["IndustryType"].ToString(),
                            TaxId = reader["TaxId"].ToString(),
                            PrimaryEmail = reader["PrimaryEmail"].ToString(),
                            PrimaryPhone = reader["PrimaryPhone"].ToString(),
                            SecondaryPhone = reader["SecondaryPhone"].ToString(),
                            AddressLine1 = reader["AddressLine1"].ToString(),
                            AddressLine2 = reader["AddressLine2"].ToString(),
                            City = reader["City"].ToString(),
                            State = reader["State"].ToString(),
                            Country = reader["Country"].ToString(),
                            ZipCode = reader["ZipCode"].ToString(),
                            WorkingDaysPerWeek = reader["WorkingDaysPerWeek"] != DBNull.Value ? Convert.ToInt32(reader["WorkingDaysPerWeek"]) : 0,
                            PrimaryHRName = reader["PrimaryHRName"].ToString(),
                            PrimaryHREmail = reader["PrimaryHREmail"].ToString(),
                            PrimaryHRPhone = reader["PrimaryHRPhone"].ToString(),
                            DefaultCurrency = reader["DefaultCurrency"].ToString(),
                            DefaultTimeZone = reader["DefaultTimeZone"].ToString(),
                            FinancialYearStart = reader["FinancialYearStart"].ToString(),
                            FinancialYearEnd = reader["FinancialYearEnd"].ToString(),
                            PayrollCycle = reader["PayrollCycle"].ToString(),
                            DefaultLeavePolicy = reader["DefaultLeavePolicy"].ToString(),
                            ShiftTimings = reader["ShiftTimings"].ToString(),
                            LogoUrl = reader["LogoUrl"].ToString(),
                            TagLine = reader["TagLine"].ToString(),
                            BrandingColor = reader["BrandingColor"].ToString(),
                            Website = reader["Website"].ToString(),
                            LinkedInUrl = reader["LinkedInUrl"].ToString(),
                            TwitterUrl = reader["TwitterUrl"].ToString(),
                            FacebookUrl = reader["FacebookUrl"].ToString(),
                            InstagramUrl = reader["InstagramUrl"].ToString(),
                            ParentCompanyId = reader["ParentCompanyId"] != DBNull.Value ? Convert.ToInt32(reader["ParentCompanyId"]) : 0,
                            NumberOfEmployees = reader["NumberOfEmployees"] != DBNull.Value ? Convert.ToInt32(reader["NumberOfEmployees"]) : 0,
                            Certifications = reader["Certifications"].ToString(),
                            InsuranceDetails = reader["InsuranceDetails"].ToString(),
                            Note = reader["Note"].ToString()
                        };

                        companyList.Add(company);
                    }
                    con.Close();
                }
            }

            return View(companyList);
        }

        public ActionResult GetCompany()
        {
            List<CompanyModel> companyList = new List<CompanyModel>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetCompany", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        CompanyModel company = new CompanyModel
                        {
                            CompanyId = reader["CompanyId"] != DBNull.Value ? Convert.ToInt32(reader["CompanyId"]) : 0,
                            CompanyName = reader["CompanyName"].ToString()
                        };

                        companyList.Add(company);
                    }
                    con.Close();
                }
            }
            return Json(companyList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveCompany(CompanyModel model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("InsertCompany", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@CompanyName", model.CompanyName);
                        cmd.Parameters.AddWithValue("@CompanyCode", model.CompanyCode);
                        cmd.Parameters.AddWithValue("@RegistrationNumber", model.RegistrationNumber);
                        cmd.Parameters.AddWithValue("@CompanyType", model.CompanyType);
                        cmd.Parameters.AddWithValue("@IndustryType", model.IndustryType);
                        cmd.Parameters.AddWithValue("@TaxId", model.TaxId);
                        cmd.Parameters.AddWithValue("@PrimaryEmail", model.PrimaryEmail);
                        cmd.Parameters.AddWithValue("@PrimaryPhone", model.PrimaryPhone);
                        cmd.Parameters.AddWithValue("@SecondaryPhone", model.SecondaryPhone);
                        cmd.Parameters.AddWithValue("@AddressLine1", model.AddressLine1);
                        cmd.Parameters.AddWithValue("@AddressLine2", model.AddressLine2);
                        cmd.Parameters.AddWithValue("@City", model.City);
                        cmd.Parameters.AddWithValue("@State", model.State);
                        cmd.Parameters.AddWithValue("@Country", model.Country);
                        cmd.Parameters.AddWithValue("@ZipCode", model.ZipCode);
                        cmd.Parameters.AddWithValue("@WorkingDaysPerWeek", model.WorkingDaysPerWeek);
                        cmd.Parameters.AddWithValue("@PrimaryHRName", model.PrimaryHRName);
                        cmd.Parameters.AddWithValue("@PrimaryHREmail", model.PrimaryHREmail);
                        cmd.Parameters.AddWithValue("@PrimaryHRPhone", model.PrimaryHRPhone);
                        cmd.Parameters.AddWithValue("@DefaultCurrency", model.DefaultCurrency);
                        cmd.Parameters.AddWithValue("@DefaultTimeZone", model.DefaultTimeZone);
                        cmd.Parameters.AddWithValue("@FinancialYearStart", model.FinancialYearStart);
                        cmd.Parameters.AddWithValue("@FinancialYearEnd", model.FinancialYearEnd);
                        cmd.Parameters.AddWithValue("@PayrollCycle", model.PayrollCycle);
                        cmd.Parameters.AddWithValue("@DefaultLeavePolicy", model.DefaultLeavePolicy);
                        cmd.Parameters.AddWithValue("@ShiftTimings", model.ShiftTimings);
                        cmd.Parameters.AddWithValue("@LogoUrl", model.LogoUrl);
                        cmd.Parameters.AddWithValue("@TagLine", model.TagLine);
                        cmd.Parameters.AddWithValue("@BrandingColor", model.BrandingColor);
                        cmd.Parameters.AddWithValue("@Website", model.Website);
                        cmd.Parameters.AddWithValue("@LinkedInUrl", model.LinkedInUrl);
                        cmd.Parameters.AddWithValue("@TwitterUrl", model.TwitterUrl);
                        cmd.Parameters.AddWithValue("@FacebookUrl", model.FacebookUrl);
                        cmd.Parameters.AddWithValue("@InstagramUrl", model.InstagramUrl);
                        cmd.Parameters.AddWithValue("@ParentCompanyId", model.ParentCompanyId);
                        cmd.Parameters.AddWithValue("@NumberOfEmployees", model.NumberOfEmployees);
                        cmd.Parameters.AddWithValue("@Certifications", model.Certifications);
                        cmd.Parameters.AddWithValue("@InsuranceDetails", model.InsuranceDetails);
                        cmd.Parameters.AddWithValue("@Note", model.Note);

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true, message = "Company details saved successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public JsonResult GetCompanyById(int id)
        {
            CompanyModel company = null;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetCompanyById", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CompanyId", id);

                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            company = new CompanyModel
                            {
                                CompanyId = Convert.ToInt32(reader["CompanyId"]),
                                CompanyName = reader["CompanyName"].ToString(),
                                CompanyCode = reader["CompanyCode"].ToString(),
                                RegistrationNumber = reader["RegistrationNumber"].ToString(),
                                CompanyType = reader["CompanyType"].ToString(),
                                IndustryType = reader["IndustryType"].ToString(),
                                TaxId = reader["TaxId"].ToString(),
                                PrimaryEmail = reader["PrimaryEmail"].ToString(),
                                PrimaryPhone = reader["PrimaryPhone"].ToString(),
                                SecondaryPhone = reader["SecondaryPhone"].ToString(),
                                AddressLine1 = reader["AddressLine1"].ToString(),
                                AddressLine2 = reader["AddressLine2"].ToString(),
                                City = reader["City"].ToString(),
                                State = reader["State"].ToString(),
                                Country = reader["Country"].ToString(),
                                ZipCode = reader["ZipCode"].ToString(),
                                WorkingDaysPerWeek = Convert.ToInt32(reader["WorkingDaysPerWeek"]),
                                PrimaryHRName = reader["PrimaryHRName"].ToString(),
                                PrimaryHREmail = reader["PrimaryHREmail"].ToString(),
                                PrimaryHRPhone = reader["PrimaryHRPhone"].ToString(),
                                DefaultCurrency = reader["DefaultCurrency"].ToString(),
                                DefaultTimeZone = reader["DefaultTimeZone"].ToString(),
                                FinancialYearStart = reader["FinancialYearStart"].ToString(),
                                FinancialYearEnd = reader["FinancialYearEnd"].ToString(),
                                PayrollCycle = reader["PayrollCycle"].ToString(),
                                DefaultLeavePolicy = reader["DefaultLeavePolicy"].ToString(),
                                ShiftTimings = reader["ShiftTimings"].ToString(),
                                LogoUrl = reader["LogoUrl"].ToString(),
                                TagLine = reader["TagLine"].ToString(),
                                BrandingColor = reader["BrandingColor"].ToString(),
                                Website = reader["Website"].ToString(),
                                LinkedInUrl = reader["LinkedInUrl"].ToString(),
                                TwitterUrl = reader["TwitterUrl"].ToString(),
                                FacebookUrl = reader["FacebookUrl"].ToString(),
                                InstagramUrl = reader["InstagramUrl"].ToString(),
                                ParentCompanyId = Convert.ToInt32(reader["ParentCompanyId"]),
                                NumberOfEmployees = Convert.ToInt32(reader["NumberOfEmployees"]),
                                Certifications = reader["Certifications"].ToString(),
                                InsuranceDetails = reader["InsuranceDetails"].ToString(),
                                Note = reader["Note"].ToString()
                            };
                        }
                    }
                    con.Close();
                }

                return Json(company, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult UpdateCompany(CompanyModel model)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("UpdateCompany", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CompanyId", model.CompanyId);
                    cmd.Parameters.AddWithValue("@CompanyName", model.CompanyName);
                    cmd.Parameters.AddWithValue("@CompanyCode", model.CompanyCode);
                    cmd.Parameters.AddWithValue("@RegistrationNumber", model.RegistrationNumber);
                    cmd.Parameters.AddWithValue("@CompanyType", model.CompanyType);
                    cmd.Parameters.AddWithValue("@IndustryType", model.IndustryType);
                    cmd.Parameters.AddWithValue("@TaxId", model.TaxId);
                    cmd.Parameters.AddWithValue("@PrimaryEmail", model.PrimaryEmail);
                    cmd.Parameters.AddWithValue("@PrimaryPhone", model.PrimaryPhone);
                    cmd.Parameters.AddWithValue("@SecondaryPhone", model.SecondaryPhone);
                    cmd.Parameters.AddWithValue("@AddressLine1", model.AddressLine1);
                    cmd.Parameters.AddWithValue("@AddressLine2", model.AddressLine2);
                    cmd.Parameters.AddWithValue("@City", model.City);
                    cmd.Parameters.AddWithValue("@State", model.State);
                    cmd.Parameters.AddWithValue("@Country", model.Country);
                    cmd.Parameters.AddWithValue("@ZipCode", model.ZipCode);
                    cmd.Parameters.AddWithValue("@WorkingDaysPerWeek", model.WorkingDaysPerWeek);
                    cmd.Parameters.AddWithValue("@PrimaryHRName", model.PrimaryHRName);
                    cmd.Parameters.AddWithValue("@PrimaryHREmail", model.PrimaryHREmail);
                    cmd.Parameters.AddWithValue("@PrimaryHRPhone", model.PrimaryHRPhone);
                    cmd.Parameters.AddWithValue("@DefaultCurrency", model.DefaultCurrency);
                    cmd.Parameters.AddWithValue("@DefaultTimeZone", model.DefaultTimeZone);
                    cmd.Parameters.AddWithValue("@FinancialYearStart", model.FinancialYearStart);
                    cmd.Parameters.AddWithValue("@FinancialYearEnd", model.FinancialYearEnd);
                    cmd.Parameters.AddWithValue("@PayrollCycle", model.PayrollCycle);
                    cmd.Parameters.AddWithValue("@DefaultLeavePolicy", model.DefaultLeavePolicy);
                    cmd.Parameters.AddWithValue("@ShiftTimings", model.ShiftTimings);
                    cmd.Parameters.AddWithValue("@LogoUrl", model.LogoUrl);
                    cmd.Parameters.AddWithValue("@TagLine", model.TagLine);
                    cmd.Parameters.AddWithValue("@BrandingColor", model.BrandingColor);
                    cmd.Parameters.AddWithValue("@Website", model.Website);
                    cmd.Parameters.AddWithValue("@LinkedInUrl", model.LinkedInUrl);
                    cmd.Parameters.AddWithValue("@TwitterUrl", model.TwitterUrl);
                    cmd.Parameters.AddWithValue("@FacebookUrl", model.FacebookUrl);
                    cmd.Parameters.AddWithValue("@InstagramUrl", model.InstagramUrl);
                    cmd.Parameters.AddWithValue("@ParentCompanyId", model.ParentCompanyId);
                    cmd.Parameters.AddWithValue("@NumberOfEmployees", model.NumberOfEmployees);
                    cmd.Parameters.AddWithValue("@Certifications", model.Certifications);
                    cmd.Parameters.AddWithValue("@InsuranceDetails", model.InsuranceDetails);
                    cmd.Parameters.AddWithValue("@Note", model.Note);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                return Json(new { success = true });
            }
        }

        [HttpPost]
        public JsonResult DeleteCompany(int companyId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand("DeleteCompany", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CompanyId", companyId);

                    con.Open();
                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        return Json(new { success = true });
                    }
                    else
                    {
                        return Json(new { success = false, message = "No company found with this ID." });
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        #endregion Company

        #region Branch

        public ActionResult Branch()
        {
            if (Session["RoleType"] == null || Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            List<BranchModel> branches = new List<BranchModel>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetBranches", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        BranchModel branch = new BranchModel
                        {
                            BranchId = Convert.ToInt32(reader["BranchId"]),
                            BranchName = reader["BranchName"].ToString(),
                            BranchCode = reader["BranchCode"].ToString(),
                            Email = reader["Email"].ToString(),
                            Phone = reader["Phone"].ToString(),
                            AddressLine1 = reader["AddressLine1"].ToString(),
                            City = reader["City"].ToString(),
                            State = reader["State"].ToString(),
                            Country = reader["Country"].ToString(),
                            ZipCode = reader["ZipCode"].ToString(),
                            FaxNumber = reader["FaxNumber"].ToString(),
                            Website = reader["Website"].ToString(),
                            CompanyName = reader["CompanyName"].ToString()  // ✅ use name instead of ID
                        };

                        branches.Add(branch);
                    }
                    con.Close();
                }
            }

            return View(branches);
        }

        public ActionResult GetBranch()
        {
            List<BranchModel> branchsList = new List<BranchModel>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetBranches", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        BranchModel branch = new BranchModel
                        {
                            BranchId = reader["BranchId"] != DBNull.Value ? Convert.ToInt32(reader["BranchId"]) : 0,
                            BranchName = reader["BranchName"].ToString()
                        };

                        branchsList.Add(branch);
                    }
                    con.Close();
                }
            }
            return Json(branchsList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveBranch(BranchModel model)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SaveBranch", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CompanyId", model.CompanyId);
                    cmd.Parameters.AddWithValue("@BranchName", model.BranchName);
                    cmd.Parameters.AddWithValue("@BranchCode", model.BranchCode);
                    cmd.Parameters.AddWithValue("@Email", model.Email);
                    cmd.Parameters.AddWithValue("@Phone", model.Phone);
                    cmd.Parameters.AddWithValue("@AddressLine1", model.AddressLine1);
                    cmd.Parameters.AddWithValue("@AddressLine2", model.AddressLine2);
                    cmd.Parameters.AddWithValue("@City", model.City);
                    cmd.Parameters.AddWithValue("@State", model.State);
                    cmd.Parameters.AddWithValue("@Country", model.Country);
                    cmd.Parameters.AddWithValue("@ZipCode", model.ZipCode);
                    cmd.Parameters.AddWithValue("@FaxNumber", model.FaxNumber);
                    cmd.Parameters.AddWithValue("@Website", model.Website);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            return Json(new { success = true, message = "Branch saved successfully!" });
        }

        public JsonResult GetBranchById(int id)
        {
            BranchModel branch = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetBranchById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BranchId", id);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    branch = new BranchModel
                    {
                        BranchId = Convert.ToInt32(reader["BranchId"]),
                        CompanyId = Convert.ToInt32(reader["CompanyId"]),
                        CompanyName = reader["CompanyName"].ToString(),
                        BranchName = reader["BranchName"].ToString(),
                        BranchCode = reader["BranchCode"].ToString(),
                        Email = reader["Email"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        AddressLine1 = reader["AddressLine1"].ToString(),
                        AddressLine2 = reader["AddressLine2"].ToString(),
                        City = reader["City"].ToString(),
                        State = reader["State"].ToString(),
                        Country = reader["Country"].ToString(),
                        ZipCode = reader["ZipCode"].ToString(),
                        FaxNumber = reader["FaxNumber"].ToString(),
                        Website = reader["Website"].ToString()
                    };
                }
            }
            return Json(branch, JsonRequestBehavior.AllowGet);
        }

        // ✅ Update branch
        [HttpPost]
        public JsonResult UpdateBranch(BranchModel model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("UpdateBranch", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@BranchId", model.BranchId);
                    cmd.Parameters.AddWithValue("@CompanyId", model.CompanyId);
                    cmd.Parameters.AddWithValue("@BranchName", model.BranchName);
                    cmd.Parameters.AddWithValue("@BranchCode", model.BranchCode);
                    cmd.Parameters.AddWithValue("@Email", model.Email);
                    cmd.Parameters.AddWithValue("@Phone", model.Phone);
                    cmd.Parameters.AddWithValue("@AddressLine1", model.AddressLine1);
                    cmd.Parameters.AddWithValue("@AddressLine2", model.AddressLine2);
                    cmd.Parameters.AddWithValue("@City", model.City);
                    cmd.Parameters.AddWithValue("@State", model.State);
                    cmd.Parameters.AddWithValue("@Country", model.Country);
                    cmd.Parameters.AddWithValue("@ZipCode", model.ZipCode);
                    cmd.Parameters.AddWithValue("@FaxNumber", model.FaxNumber);
                    cmd.Parameters.AddWithValue("@Website", model.Website);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteBranch(int branchId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand("DeleteBranch", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BranchId", branchId);

                    con.Open();
                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        return Json(new { success = true });
                    }
                    else
                    {
                        return Json(new { success = false, message = "No Branch found with this ID." });
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        #endregion Branch

        #region Department

        public ActionResult Department()
        {
            if (Session["RoleType"] == null || Session["UserId"] == null || Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            List<DepartmentModel> departments = new List<DepartmentModel>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetDepartments", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        DepartmentModel department = new DepartmentModel
                        {
                            DepartmentId = Convert.ToInt32(reader["DepartmentID"]),
                            DepartmentName = reader["DepartmentName"].ToString(),
                            DepartmentCode = reader["DepartmentCode"].ToString(),
                            BranchName = reader["BranchName"].ToString(),
                            HeadEmail = reader["HeadEmail"].ToString(),
                            HeadPhone = reader["HeadPhone"].ToString(),
                            HeadOfDepartment = reader["HeadOfDepartment"].ToString(),
                            Notes = reader["Notes"].ToString(),
                            NumberOfEmployees = Convert.ToInt32(reader["NumberOfEmployees"])
                        };

                        departments.Add(department);
                    }
                    con.Close();
                }
            }

            return View(departments);
        }

        public ActionResult GetDepartment()
        {
            List<DepartmentModel> departmentList = new List<DepartmentModel>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetDepartments", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        DepartmentModel department = new DepartmentModel
                        {
                            DepartmentId = reader["DepartmentId"] != DBNull.Value ? Convert.ToInt32(reader["DepartmentId"]) : 0,
                            DepartmentName = reader["DepartmentName"].ToString()
                        };

                        departmentList.Add(department);
                    }
                    con.Close();
                }
            }
            return Json(departmentList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveDepartment(DepartmentModel model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand("SaveDepartment", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@DepartmentName", model.DepartmentName);
                    cmd.Parameters.AddWithValue("@BranchId", model.BranchId);
                    cmd.Parameters.AddWithValue("@DepartmentCode", model.DepartmentCode);
                    cmd.Parameters.AddWithValue("@HeadEmail", model.HeadEmail);
                    cmd.Parameters.AddWithValue("@HeadPhone", model.HeadPhone);
                    cmd.Parameters.AddWithValue("@Description", model.Description);
                    cmd.Parameters.AddWithValue("@HeadOfDepartment", model.HeadOfDepartment);
                    cmd.Parameters.AddWithValue("@NumberOfEmployees", model.NumberOfEmployees);
                    cmd.Parameters.AddWithValue("@Notes", model.Notes);
                    cmd.Parameters.AddWithValue("@CreatedBy", Session["UserName"].ToString());

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                return Json(new { success = true, message = "Department saved successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public JsonResult GetDepartmentById(int id)
        {
            DepartmentModel department = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetDepartmentById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DepartmentId", id);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    department = new DepartmentModel
                    {
                        BranchId = Convert.ToInt32(reader["BranchId"]),
                        BranchName = reader["BranchName"].ToString(),
                        DepartmentId = Convert.ToInt32(reader["DepartmentId"]),
                        DepartmentName = reader["DepartmentName"].ToString(),
                        DepartmentCode = reader["DepartmentCode"].ToString(),
                        HeadEmail = reader["HeadEmail"].ToString(),
                        HeadPhone = reader["HeadPhone"].ToString(),
                        HeadOfDepartment = reader["HeadOfDepartment"].ToString(),
                        Description = reader["Description"].ToString(),
                        NumberOfEmployees = Convert.ToInt32(reader["NumberOfEmployees"]),
                        Notes = reader["Notes"].ToString()
                    };
                }
            }
            return Json(department, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateDepartment(DepartmentModel model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand("UpdateDepartment", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@DepartmentId", model.DepartmentId);
                    cmd.Parameters.AddWithValue("@DepartmentName", model.DepartmentName);
                    cmd.Parameters.AddWithValue("@BranchId", model.BranchId);
                    cmd.Parameters.AddWithValue("@DepartmentCode", model.DepartmentCode);
                    cmd.Parameters.AddWithValue("@HeadEmail", model.HeadEmail);
                    cmd.Parameters.AddWithValue("@HeadPhone", model.HeadPhone);
                    cmd.Parameters.AddWithValue("@Description", model.Description);
                    cmd.Parameters.AddWithValue("@HeadOfDepartment", model.HeadOfDepartment);
                    cmd.Parameters.AddWithValue("@NumberOfEmployees", model.NumberOfEmployees);
                    cmd.Parameters.AddWithValue("@Notes", model.Notes);
                    cmd.Parameters.AddWithValue("@UpdatedBy", Session["UserName"].ToString());

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                return Json(new { success = true, message = "Department saved successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteDepartment(int departmentId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand("DeleteDepartment", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DepartmentId", departmentId);

                    con.Open();
                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        return Json(new { success = true });
                    }
                    else
                    {
                        return Json(new { success = false, message = "No Department found with this ID." });
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        #endregion Department

        #region SubDepartment

        public ActionResult SubDepartment()
        {
            if (Session["RoleType"] == null || Session["UserId"] == null || Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            List<SubDepartmentModel> subdepartments = new List<SubDepartmentModel>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetSubDepartments", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        SubDepartmentModel subdepartment = new SubDepartmentModel
                        {
                            SubDepartmentId = Convert.ToInt32(reader["SubDepartmentId"]),
                            SubDepartmentName = reader["SubDepartmentName"].ToString(),
                            SubDepartmentCode = reader["SubDepartmentCode"].ToString(),
                            DepartmentName = reader["DepartmentName"].ToString(),
                            HeadEmail = reader["HeadEmail"].ToString(),
                            HeadPhone = reader["HeadPhone"].ToString(),
                            HeadOfSubDepartment = reader["HeadOfSubDept"].ToString(),
                            Notes = reader["Notes"].ToString(),
                            NumberOfEmployees = Convert.ToInt32(reader["NumberOfEmployees"])
                        };

                        subdepartments.Add(subdepartment);
                    }
                    con.Close();
                }
            }

            return View(subdepartments);
        }

        [HttpPost]
        public ActionResult SaveSubDepartment(SubDepartmentModel model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand("SaveSubDepartment", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@SubDepartmentName", model.SubDepartmentName);
                    cmd.Parameters.AddWithValue("@DepartmentId", model.DepartmentId);
                    cmd.Parameters.AddWithValue("@SubDepartmentCode", model.SubDepartmentCode);
                    cmd.Parameters.AddWithValue("@HeadEmail", model.HeadEmail);
                    cmd.Parameters.AddWithValue("@HeadPhone", model.HeadPhone);
                    cmd.Parameters.AddWithValue("@Description", model.Description);
                    cmd.Parameters.AddWithValue("@HeadOfSubDepartment", model.HeadOfSubDepartment);
                    cmd.Parameters.AddWithValue("@NumberOfEmployees", model.NumberOfEmployees);
                    cmd.Parameters.AddWithValue("@Notes", model.Notes);
                    cmd.Parameters.AddWithValue("@CreatedBy", Session["UserName"].ToString());

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                return Json(new { success = true, message = "SubDepartment saved successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public JsonResult GetSubDepartmentById(int id)
        {
            SubDepartmentModel subdepartment = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetSubDepartmentById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SubDepartmentId", id);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    subdepartment = new SubDepartmentModel
                    {
                        DepartmentId = Convert.ToInt32(reader["DepartmentId"]),
                        DepartmentName = reader["DepartmentName"].ToString(),
                        SubDepartmentId = Convert.ToInt32(reader["SubDepartmentId"]),
                        SubDepartmentName = reader["SubDepartmentName"].ToString(),
                        SubDepartmentCode = reader["SubDepartmentCode"].ToString(),
                        HeadEmail = reader["HeadEmail"].ToString(),
                        HeadPhone = reader["HeadPhone"].ToString(),
                        HeadOfSubDepartment = reader["HeadOfSubDept"].ToString(),
                        Description = reader["Description"].ToString(),
                        NumberOfEmployees = Convert.ToInt32(reader["NumberOfEmployees"]),
                        Notes = reader["Notes"].ToString()
                    };
                }
            }
            return Json(subdepartment, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateSubDepartment(SubDepartmentModel model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand("UpdateSubDepartment", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@SubDepartmentId", model.SubDepartmentId);
                    cmd.Parameters.AddWithValue("@SubDepartmentName", model.SubDepartmentName);
                    cmd.Parameters.AddWithValue("@DepartmentId", model.DepartmentId);
                    cmd.Parameters.AddWithValue("@SubDepartmentCode", model.SubDepartmentCode);
                    cmd.Parameters.AddWithValue("@HeadEmail", model.HeadEmail);
                    cmd.Parameters.AddWithValue("@HeadPhone", model.HeadPhone);
                    cmd.Parameters.AddWithValue("@Description", model.Description);
                    cmd.Parameters.AddWithValue("@HeadOfSubDepartment", model.HeadOfSubDepartment);
                    cmd.Parameters.AddWithValue("@NumberOfEmployees", model.NumberOfEmployees);
                    cmd.Parameters.AddWithValue("@Notes", model.Notes);
                    cmd.Parameters.AddWithValue("@UpdatedBy", Session["UserName"].ToString());

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                return Json(new { success = true, message = "SubDepartment saved successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteSubDepartment(int subdepartmentId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand("DeleteSubDepartment", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SubDepartmentId", subdepartmentId);

                    con.Open();
                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        return Json(new { success = true });
                    }
                    else
                    {
                        return Json(new { success = false, message = "No Department found with this ID." });
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        #endregion SubDepartment

        #region Shift

        public ActionResult Shift()
        {
            if (Session["RoleType"] == null || Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            List<ShiftModel> shifts = new List<ShiftModel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT ShiftId, ShiftName, StartTime, EndTime, IsActive FROM Shifts";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            shifts.Add(new ShiftModel
                            {
                                ShiftId = Convert.ToInt32(reader["ShiftId"]),
                                ShiftName = reader["ShiftName"].ToString(),
                                StartTime = reader["StartTime"].ToString(),
                                EndTime = reader["EndTime"].ToString(),
                                IsActive = Convert.ToBoolean(reader["IsActive"])
                            });
                        }
                    }
                }
            }
            return View(shifts);
        }

        [HttpPost]
        public JsonResult SaveShift(ShiftModel model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Shifts (ShiftName, StartTime, EndTime, IsActive) VALUES (@ShiftName, @StartTime, @EndTime, 1)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ShiftName", model.ShiftName ?? "");
                        cmd.Parameters.AddWithValue("@StartTime", model.StartTime ?? "");
                        cmd.Parameters.AddWithValue("@EndTime", model.EndTime ?? "");
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true, message = "Shift saved successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public JsonResult GetShiftById(int id)
        {
            ShiftModel shift = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT ShiftId, ShiftName, StartTime, EndTime, IsActive FROM Shifts WHERE ShiftId = @ShiftId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ShiftId", id);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            shift = new ShiftModel
                            {
                                ShiftId = Convert.ToInt32(reader["ShiftId"]),
                                ShiftName = reader["ShiftName"].ToString(),
                                StartTime = reader["StartTime"].ToString(),
                                EndTime = reader["EndTime"].ToString(),
                                IsActive = Convert.ToBoolean(reader["IsActive"])
                            };
                        }
                    }
                }
            }
            return Json(shift, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateShift(ShiftModel model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Shifts SET ShiftName = @ShiftName, StartTime = @StartTime, EndTime = @EndTime WHERE ShiftId = @ShiftId";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ShiftId", model.ShiftId);
                        cmd.Parameters.AddWithValue("@ShiftName", model.ShiftName ?? "");
                        cmd.Parameters.AddWithValue("@StartTime", model.StartTime ?? "");
                        cmd.Parameters.AddWithValue("@EndTime", model.EndTime ?? "");
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteShift(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Shifts WHERE ShiftId = @ShiftId";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ShiftId", id);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        #endregion Shift

        #region Roles

        public ActionResult Roles()
        {
            var model = new RolePageViewModel();
            model.Modules = new SidebarViewModel();
            model.RolesList = new List<RoleModel>();

            string connectionString = ConfigurationManager.ConnectionStrings["Zentora"].ConnectionString;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // 1. Fetch the list of existing Roles for the Data Table
                    string roleQuery = "SELECT Id, RoleId, RoleName, RoleDescription, IsActive FROM [Roles] WHERE IsDeleted = 0";
                    using (SqlCommand cmdRoles = new SqlCommand(roleQuery, conn))
                    {
                        using (SqlDataReader reader = cmdRoles.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                model.RolesList.Add(new RoleModel
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    RoleId = reader["RoleId"].ToString(),
                                    RoleName = reader["RoleName"].ToString(),
                                    RoleDescription = reader["RoleDescription"] != DBNull.Value ? reader["RoleDescription"].ToString() : "",
                                    IsActive = Convert.ToBoolean(reader["IsActive"])
                                });
                            }
                        }
                    }

                    // 2. Fetch the Modules for the Permissions Matrix
                    var allParents = new List<ParentModuleVM>();
                    var allChildren = new List<ChildModuleVM>();
                    var allSubChildren = new List<SubChildModuleVM>();

                    using (SqlCommand cmd = new SqlCommand("sp_GetSidebarModules", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                allParents.Add(new ParentModuleVM
                                {
                                    ParentModuleId = Convert.ToInt32(reader["ParentModuleId"]),
                                    ModuleName = reader["ModuleName"].ToString(),
                                    SortOrder = Convert.ToInt32(reader["SortOrder"])
                                });
                            }
                            if (reader.NextResult())
                            {
                                while (reader.Read())
                                {
                                    allChildren.Add(new ChildModuleVM
                                    {
                                        ChildModuleId = Convert.ToInt32(reader["ChildModuleId"]),
                                        ParentModuleId = Convert.ToInt32(reader["ParentModuleId"]),
                                        ModuleName = reader["ModuleName"].ToString(),
                                        SortOrder = Convert.ToInt32(reader["SortOrder"])
                                    });
                                }
                            }
                            if (reader.NextResult())
                            {
                                while (reader.Read())
                                {
                                    allSubChildren.Add(new SubChildModuleVM
                                    {
                                        SubChildModuleId = Convert.ToInt32(reader["SubChildModuleId"]),
                                        ChildModuleId = Convert.ToInt32(reader["ChildModuleId"]),
                                        ModuleName = reader["ModuleName"].ToString(),
                                        SortOrder = Convert.ToInt32(reader["SortOrder"])
                                    });
                                }
                            }
                        }
                    }

                    foreach (var child in allChildren)
                        child.SubChildren = allSubChildren.Where(s => s.ChildModuleId == child.ChildModuleId).ToList();

                    foreach (var parent in allParents)
                        parent.Children = allChildren.Where(c => c.ParentModuleId == parent.ParentModuleId).ToList();

                    model.Modules.ParentModules = allParents;
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error loading roles: " + ex.Message;
            }

            return View(model);
        }

        [HttpPost]
        public JsonResult SaveRole(RoleSaveModel model)
        {
            try
            {
                DataTable tvp = BuildPermissionsTable(model.Permissions);
                int currentUserId = 1; // Hardcoded for now, replace with session ID

                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Zentora"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_InsertRole", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@RoleId", model.RoleId);
                        cmd.Parameters.AddWithValue("@RoleName", model.RoleName);
                        cmd.Parameters.AddWithValue("@RoleDescription", model.RoleDescription ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@CreatedBy", currentUserId);

                        var tvpParam = cmd.Parameters.AddWithValue("@Permissions", tvp);
                        tvpParam.SqlDbType = SqlDbType.Structured;
                        tvpParam.TypeName = "dbo.RolePermissionTVP";

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true, message = "Role saved successfully." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult UpdateRole(RoleSaveModel model)
        {
            try
            {
                DataTable tvp = BuildPermissionsTable(model.Permissions);
                int currentUserId = 1;

                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Zentora"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_UpdateRole", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", model.Id);
                        cmd.Parameters.AddWithValue("@RoleId", model.RoleId);
                        cmd.Parameters.AddWithValue("@RoleName", model.RoleName);
                        cmd.Parameters.AddWithValue("@RoleDescription", model.RoleDescription ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@UpdatedBy", currentUserId);

                        var tvpParam = cmd.Parameters.AddWithValue("@Permissions", tvp);
                        tvpParam.SqlDbType = SqlDbType.Structured;
                        tvpParam.TypeName = "dbo.RolePermissionTVP";

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true, message = "Role updated successfully." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteRole(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Zentora"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_UpdateRoleStatus", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", id);
                        cmd.Parameters.AddWithValue("@IsDeleted", true);
                        cmd.Parameters.AddWithValue("@UpdatedBy", 1); // Current user ID

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public JsonResult GetRoleDetails(int id)
        {
            try
            {
                var roleData = new RoleSaveModel { Permissions = new List<RolePermissionDto>() };

                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Zentora"].ConnectionString))
                {
                    conn.Open();

                    // 1. Get Role Info
                    string roleQuery = "SELECT Id, RoleId, RoleName, RoleDescription FROM [Roles] WHERE Id = @Id";
                    using (SqlCommand cmd = new SqlCommand(roleQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                roleData.Id = Convert.ToInt32(reader["Id"]);
                                roleData.RoleId = reader["RoleId"].ToString();
                                roleData.RoleName = reader["RoleName"].ToString();
                                roleData.RoleDescription = reader["RoleDescription"] != DBNull.Value ? reader["RoleDescription"].ToString() : "";
                            }
                        }
                    }

                    // 2. Get Permissions
                    string permQuery = "SELECT ParentModuleId, ChildModuleId, SubChildModuleId, ActionId FROM [RolePermissions] WHERE RoleId = @Id";
                    using (SqlCommand cmd = new SqlCommand(permQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var dto = new RolePermissionDto
                                {
                                    ActionCode = MapActionIdToCode(Convert.ToInt32(reader["ActionId"]))
                                };

                                if (reader["ParentModuleId"] != DBNull.Value) { dto.ModuleLevel = "p"; dto.ModuleId = Convert.ToInt32(reader["ParentModuleId"]); }
                                else if (reader["ChildModuleId"] != DBNull.Value) { dto.ModuleLevel = "c"; dto.ModuleId = Convert.ToInt32(reader["ChildModuleId"]); }
                                else if (reader["SubChildModuleId"] != DBNull.Value) { dto.ModuleLevel = "s"; dto.ModuleId = Convert.ToInt32(reader["SubChildModuleId"]); }

                                roleData.Permissions.Add(dto);
                            }
                        }
                    }
                }
                return Json(roleData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        // --- Helpers ---
        private DataTable BuildPermissionsTable(List<RolePermissionDto> permissions)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ParentModuleId", typeof(int));
            dt.Columns.Add("ChildModuleId", typeof(int));
            dt.Columns.Add("SubChildModuleId", typeof(int));
            dt.Columns.Add("ActionId", typeof(int));

            if (permissions == null) return dt;

            foreach (var perm in permissions)
            {
                DataRow row = dt.NewRow();
                row["ParentModuleId"] = DBNull.Value;
                row["ChildModuleId"] = DBNull.Value;
                row["SubChildModuleId"] = DBNull.Value;

                if (perm.ModuleLevel == "p") row["ParentModuleId"] = perm.ModuleId;
                else if (perm.ModuleLevel == "c") row["ChildModuleId"] = perm.ModuleId;
                else if (perm.ModuleLevel == "s") row["SubChildModuleId"] = perm.ModuleId;

                row["ActionId"] = MapActionCodeToId(perm.ActionCode);
                dt.Rows.Add(row);
            }
            return dt;
        }

        private int MapActionCodeToId(string code)
        {
            switch (code?.ToLower())
            {
                case "view": return 1;
                case "add": return 2;
                case "edit": return 3;
                case "delete": return 4;
                default: return 1;
            }
        }

        private string MapActionIdToCode(int id)
        {
            switch (id)
            {
                case 1: return "view";
                case 2: return "add";
                case 3: return "edit";
                case 4: return "delete";
                default: return "view";
            }
        }

        #endregion

        #region LeaveCategory

        public ActionResult LeaveCategory()
        {
            if (Session["RoleType"] == null || Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            List<LeaveCategoryModel> leaveCategories = new List<LeaveCategoryModel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT LeaveCategoryId, CategoryName, TotalDays, IsActive FROM LeaveCategory";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            leaveCategories.Add(new LeaveCategoryModel
                            {
                                LeaveCategoryId = Convert.ToInt32(reader["LeaveCategoryId"]),
                                CategoryName = reader["CategoryName"].ToString(),
                                TotalDays = Convert.ToInt32(reader["TotalDays"]),
                                IsActive = Convert.ToBoolean(reader["IsActive"])
                            });
                        }
                    }
                }
            }
            return View(leaveCategories);
        }

        [HttpPost]
        public JsonResult SaveLeaveCategory(LeaveCategoryModel model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO LeaveCategory (CategoryName, TotalDays, IsActive) VALUES (@CategoryName, @TotalDays, 1)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@CategoryName", model.CategoryName ?? "");
                        cmd.Parameters.AddWithValue("@TotalDays", model.TotalDays);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true, message = "Leave Category saved successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public JsonResult GetLeaveCategoryById(int id)
        {
            LeaveCategoryModel model = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT LeaveCategoryId, CategoryName, TotalDays, IsActive FROM LeaveCategory WHERE LeaveCategoryId = @LeaveCategoryId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@LeaveCategoryId", id);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            model = new LeaveCategoryModel
                            {
                                LeaveCategoryId = Convert.ToInt32(reader["LeaveCategoryId"]),
                                CategoryName = reader["CategoryName"].ToString(),
                                TotalDays = Convert.ToInt32(reader["TotalDays"]),
                                IsActive = Convert.ToBoolean(reader["IsActive"])
                            };
                        }
                    }
                }
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateLeaveCategory(LeaveCategoryModel model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "UPDATE LeaveCategory SET CategoryName = @CategoryName, TotalDays = @TotalDays WHERE LeaveCategoryId = @LeaveCategoryId";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@LeaveCategoryId", model.LeaveCategoryId);
                        cmd.Parameters.AddWithValue("@CategoryName", model.CategoryName ?? "");
                        cmd.Parameters.AddWithValue("@TotalDays", model.TotalDays);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteLeaveCategory(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM LeaveCategory WHERE LeaveCategoryId = @LeaveCategoryId";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@LeaveCategoryId", id);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        #endregion LeaveCategory

        #region HolidayCategory

        public ActionResult HolidayCategory()
        {
            if (Session["RoleType"] == null || Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            List<HolidayCategoryModel> list = new List<HolidayCategoryModel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT HolidayCategoryId, CategoryName, IsActive FROM HolidayCategory";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new HolidayCategoryModel
                            {
                                HolidayCategoryId = Convert.ToInt32(reader["HolidayCategoryId"]),
                                CategoryName = reader["CategoryName"].ToString(),
                                IsActive = Convert.ToBoolean(reader["IsActive"])
                            });
                        }
                    }
                }
            }
            return View(list);
        }

        [HttpPost]
        public JsonResult SaveHolidayCategory(HolidayCategoryModel model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO HolidayCategory (CategoryName, IsActive) VALUES (@CategoryName, 1)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@CategoryName", model.CategoryName ?? "");
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true, message = "Holiday Category saved successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public JsonResult GetHolidayCategoryById(int id)
        {
            HolidayCategoryModel model = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT HolidayCategoryId, CategoryName, IsActive FROM HolidayCategory WHERE HolidayCategoryId = @HolidayCategoryId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@HolidayCategoryId", id);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            model = new HolidayCategoryModel
                            {
                                HolidayCategoryId = Convert.ToInt32(reader["HolidayCategoryId"]),
                                CategoryName = reader["CategoryName"].ToString(),
                                IsActive = Convert.ToBoolean(reader["IsActive"])
                            };
                        }
                    }
                }
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateHolidayCategory(HolidayCategoryModel model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "UPDATE HolidayCategory SET CategoryName = @CategoryName WHERE HolidayCategoryId = @HolidayCategoryId";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@HolidayCategoryId", model.HolidayCategoryId);
                        cmd.Parameters.AddWithValue("@CategoryName", model.CategoryName ?? "");
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteHolidayCategory(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM HolidayCategory WHERE HolidayCategoryId = @HolidayCategoryId";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@HolidayCategoryId", id);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        #endregion HolidayCategory

        #region HolidayType

        public ActionResult HolidayType()
        {
            if (Session["RoleType"] == null || Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            List<HolidayTypeModel> list = new List<HolidayTypeModel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT HolidayTypeId, TypeName, IsActive FROM HolidayType";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new HolidayTypeModel
                            {
                                HolidayTypeId = Convert.ToInt32(reader["HolidayTypeId"]),
                                TypeName = reader["TypeName"].ToString(),
                                IsActive = Convert.ToBoolean(reader["IsActive"])
                            });
                        }
                    }
                }
            }
            return View(list);
        }

        [HttpPost]
        public JsonResult SaveHolidayType(HolidayTypeModel model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO HolidayType (TypeName, IsActive) VALUES (@TypeName, 1)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@TypeName", model.TypeName ?? "");
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true, message = "Holiday Type saved successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public JsonResult GetHolidayTypeById(int id)
        {
            HolidayTypeModel model = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT HolidayTypeId, TypeName, IsActive FROM HolidayType WHERE HolidayTypeId = @HolidayTypeId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@HolidayTypeId", id);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            model = new HolidayTypeModel
                            {
                                HolidayTypeId = Convert.ToInt32(reader["HolidayTypeId"]),
                                TypeName = reader["TypeName"].ToString(),
                                IsActive = Convert.ToBoolean(reader["IsActive"])
                            };
                        }
                    }
                }
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateHolidayType(HolidayTypeModel model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "UPDATE HolidayType SET TypeName = @TypeName WHERE HolidayTypeId = @HolidayTypeId";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@HolidayTypeId", model.HolidayTypeId);
                        cmd.Parameters.AddWithValue("@TypeName", model.TypeName ?? "");
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteHolidayType(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM HolidayType WHERE HolidayTypeId = @HolidayTypeId";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@HolidayTypeId", id);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        #endregion HolidayType

        #region Projects

        public ActionResult Projects()
        {
            if (Session["RoleType"] == null || Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            List<ProjectModel> list = new List<ProjectModel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT ProjectId, ProjectName, ProjectCode, Description, StartDate, EndDate, Status, IsActive FROM Projects";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new ProjectModel
                            {
                                ProjectId = Convert.ToInt32(reader["ProjectId"]),
                                ProjectName = reader["ProjectName"].ToString(),
                                ProjectCode = reader["ProjectCode"].ToString(),
                                Description = reader["Description"].ToString(),
                                StartDate = reader["StartDate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["StartDate"]) : null,
                                EndDate = reader["EndDate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["EndDate"]) : null,
                                Status = reader["Status"].ToString(),
                                IsActive = Convert.ToBoolean(reader["IsActive"])
                            });
                        }
                    }
                }
            }
            return View(list);
        }

        [HttpPost]
        public JsonResult SaveProject(ProjectModel model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = @"INSERT INTO Projects (ProjectName, ProjectCode, Description, StartDate, EndDate, Status, IsActive) 
                                     VALUES (@ProjectName, @ProjectCode, @Description, @StartDate, @EndDate, @Status, 1)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ProjectName", model.ProjectName ?? "");
                        cmd.Parameters.AddWithValue("@ProjectCode", model.ProjectCode ?? "");
                        cmd.Parameters.AddWithValue("@Description", model.Description ?? "");
                        cmd.Parameters.AddWithValue("@StartDate", (object)model.StartDate ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@EndDate", (object)model.EndDate ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Status", model.Status ?? "In Progress");
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true, message = "Project saved successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public JsonResult GetProjectById(int id)
        {
            ProjectModel model = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT ProjectId, ProjectName, ProjectCode, Description, StartDate, EndDate, Status, IsActive FROM Projects WHERE ProjectId = @ProjectId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ProjectId", id);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            model = new ProjectModel
                            {
                                ProjectId = Convert.ToInt32(reader["ProjectId"]),
                                ProjectName = reader["ProjectName"].ToString(),
                                ProjectCode = reader["ProjectCode"].ToString(),
                                Description = reader["Description"].ToString(),
                                StartDate = reader["StartDate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["StartDate"]) : null,
                                EndDate = reader["EndDate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["EndDate"]) : null,
                                Status = reader["Status"].ToString(),
                                IsActive = Convert.ToBoolean(reader["IsActive"])
                            };
                        }
                    }
                }
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateProject(ProjectModel model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = @"UPDATE Projects SET ProjectName = @ProjectName, ProjectCode = @ProjectCode, 
                                     Description = @Description, StartDate = @StartDate, EndDate = @EndDate, Status = @Status 
                                     WHERE ProjectId = @ProjectId";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ProjectId", model.ProjectId);
                        cmd.Parameters.AddWithValue("@ProjectName", model.ProjectName ?? "");
                        cmd.Parameters.AddWithValue("@ProjectCode", model.ProjectCode ?? "");
                        cmd.Parameters.AddWithValue("@Description", model.Description ?? "");
                        cmd.Parameters.AddWithValue("@StartDate", (object)model.StartDate ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@EndDate", (object)model.EndDate ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Status", model.Status ?? "In Progress");
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteProject(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Projects WHERE ProjectId = @ProjectId";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ProjectId", id);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        #endregion Projects
    }
}