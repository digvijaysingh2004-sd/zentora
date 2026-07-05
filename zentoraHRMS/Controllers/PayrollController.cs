using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using zentoraHRMS.Models;

namespace zentoraHRMS.Controllers
{
    public class PayrollController : Controller
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["Zentora"].ConnectionString;

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            base.OnActionExecuting(filterContext);
        }

        #region Salary Components
        public ActionResult SalaryComponents()
        {
            if (Session["RoleType"] == null || Session["UserId"] == null) return RedirectToAction("Login", "Auth");

            List<SalaryComponentModel> list = new List<SalaryComponentModel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT ComponentId, ComponentName, ComponentType, Description FROM SalaryComponents";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new SalaryComponentModel
                            {
                                ComponentId = Convert.ToInt32(reader["ComponentId"]),
                                ComponentName = reader["ComponentName"].ToString(),
                                ComponentType = reader["ComponentType"].ToString(),
                                Description = reader["Description"].ToString()
                            });
                        }
                    }
                }
            }
            return View(list);
        }

        [HttpPost]
        public JsonResult SaveComponent(SalaryComponentModel model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO SalaryComponents (ComponentName, ComponentType, Description) VALUES (@ComponentName, @ComponentType, @Description)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ComponentName", model.ComponentName ?? "");
                        cmd.Parameters.AddWithValue("@ComponentType", model.ComponentType ?? "Earning");
                        cmd.Parameters.AddWithValue("@Description", model.Description ?? "");
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true, message = "Salary component saved successfully!" });
            }
            catch (Exception ex) { return Json(new { success = false, message = ex.Message }); }
        }

        public JsonResult GetComponentById(int id)
        {
            SalaryComponentModel model = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT ComponentId, ComponentName, ComponentType, Description FROM SalaryComponents WHERE ComponentId = @ComponentId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ComponentId", id);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            model = new SalaryComponentModel
                            {
                                ComponentId = Convert.ToInt32(reader["ComponentId"]),
                                ComponentName = reader["ComponentName"].ToString(),
                                ComponentType = reader["ComponentType"].ToString(),
                                Description = reader["Description"].ToString()
                            };
                        }
                    }
                }
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateComponent(SalaryComponentModel model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "UPDATE SalaryComponents SET ComponentName = @ComponentName, ComponentType = @ComponentType, Description = @Description WHERE ComponentId = @ComponentId";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ComponentId", model.ComponentId);
                        cmd.Parameters.AddWithValue("@ComponentName", model.ComponentName ?? "");
                        cmd.Parameters.AddWithValue("@ComponentType", model.ComponentType ?? "Earning");
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
        public JsonResult DeleteComponent(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM SalaryComponents WHERE ComponentId = @ComponentId";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ComponentId", id);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true });
            }
            catch (Exception ex) { return Json(new { success = false, message = ex.Message }); }
        }
        #endregion

        #region Employee Salaries
        public ActionResult EmployeeSalaries()
        {
            if (Session["RoleType"] == null || Session["UserId"] == null) return RedirectToAction("Login", "Auth");

            List<EmployeeSalaryModel> list = new List<EmployeeSalaryModel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"SELECT S.SalaryId, S.EmployeeId, E.FirstName + ' ' + E.LastName AS EmployeeName, 
                                 S.BasicSalary, S.Allowance, S.Deduction 
                                 FROM EmployeeSalaries S INNER JOIN EmployeeDetails E ON S.EmployeeId = E.Id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new EmployeeSalaryModel
                            {
                                SalaryId = Convert.ToInt32(reader["SalaryId"]),
                                EmployeeId = Convert.ToInt32(reader["EmployeeId"]),
                                EmployeeName = reader["EmployeeName"].ToString(),
                                BasicSalary = Convert.ToDecimal(reader["BasicSalary"]),
                                Allowance = Convert.ToDecimal(reader["Allowance"]),
                                Deduction = Convert.ToDecimal(reader["Deduction"])
                            });
                        }
                    }
                }
            }
            return View(list);
        }

        [HttpPost]
        public JsonResult SaveSalary(EmployeeSalaryModel model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO EmployeeSalaries (EmployeeId, BasicSalary, Allowance, Deduction) VALUES (@EmployeeId, @BasicSalary, @Allowance, @Deduction)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@EmployeeId", model.EmployeeId);
                        cmd.Parameters.AddWithValue("@BasicSalary", model.BasicSalary);
                        cmd.Parameters.AddWithValue("@Allowance", model.Allowance);
                        cmd.Parameters.AddWithValue("@Deduction", model.Deduction);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true, message = "Employee salary setup saved!" });
            }
            catch (Exception ex) { return Json(new { success = false, message = ex.Message }); }
        }

        public JsonResult GetSalaryById(int id)
        {
            EmployeeSalaryModel model = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT SalaryId, EmployeeId, BasicSalary, Allowance, Deduction FROM EmployeeSalaries WHERE SalaryId = @SalaryId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@SalaryId", id);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            model = new EmployeeSalaryModel
                            {
                                SalaryId = Convert.ToInt32(reader["SalaryId"]),
                                EmployeeId = Convert.ToInt32(reader["EmployeeId"]),
                                BasicSalary = Convert.ToDecimal(reader["BasicSalary"]),
                                Allowance = Convert.ToDecimal(reader["Allowance"]),
                                Deduction = Convert.ToDecimal(reader["Deduction"])
                            };
                        }
                    }
                }
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateSalary(EmployeeSalaryModel model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "UPDATE EmployeeSalaries SET EmployeeId = @EmployeeId, BasicSalary = @BasicSalary, Allowance = @Allowance, Deduction = @Deduction WHERE SalaryId = @SalaryId";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@SalaryId", model.SalaryId);
                        cmd.Parameters.AddWithValue("@EmployeeId", model.EmployeeId);
                        cmd.Parameters.AddWithValue("@BasicSalary", model.BasicSalary);
                        cmd.Parameters.AddWithValue("@Allowance", model.Allowance);
                        cmd.Parameters.AddWithValue("@Deduction", model.Deduction);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true });
            }
            catch (Exception ex) { return Json(new { success = false, message = ex.Message }); }
        }

        [HttpPost]
        public JsonResult DeleteSalary(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM EmployeeSalaries WHERE SalaryId = @SalaryId";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@SalaryId", id);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true });
            }
            catch (Exception ex) { return Json(new { success = false, message = ex.Message }); }
        }
        #endregion

        #region Payroll Runs
        public ActionResult PayrollRuns()
        {
            if (Session["RoleType"] == null || Session["UserId"] == null) return RedirectToAction("Login", "Auth");

            List<PayrollRunModel> list = new List<PayrollRunModel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT PayrollRunId, MonthYear, ProcessedDate, Status FROM PayrollRuns";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new PayrollRunModel
                            {
                                PayrollRunId = Convert.ToInt32(reader["PayrollRunId"]),
                                MonthYear = reader["MonthYear"].ToString(),
                                ProcessedDate = Convert.ToDateTime(reader["ProcessedDate"]),
                                Status = reader["Status"].ToString()
                            });
                        }
                    }
                }
            }
            return View(list);
        }

        [HttpPost]
        public JsonResult SavePayrollRun(PayrollRunModel model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO PayrollRuns (MonthYear, ProcessedDate, Status) VALUES (@MonthYear, GETDATE(), @Status)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@MonthYear", model.MonthYear ?? "");
                        cmd.Parameters.AddWithValue("@Status", model.Status ?? "Paid");
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true, message = "Payroll processed successfully!" });
            }
            catch (Exception ex) { return Json(new { success = false, message = ex.Message }); }
        }

        [HttpPost]
        public JsonResult DeletePayrollRun(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM PayrollRuns WHERE PayrollRunId = @PayrollRunId";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@PayrollRunId", id);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true });
            }
            catch (Exception ex) { return Json(new { success = false, message = ex.Message }); }
        }
        #endregion

        #region Payslips
        public ActionResult Payslips()
        {
            if (Session["RoleType"] == null || Session["UserId"] == null) return RedirectToAction("Login", "Auth");

            List<EmployeeSalaryModel> list = new List<EmployeeSalaryModel>();
            // Using employee salary listings to simulate generation of individual payslips
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"SELECT S.SalaryId, S.EmployeeId, E.FirstName + ' ' + E.LastName AS EmployeeName, 
                                 S.BasicSalary, S.Allowance, S.Deduction 
                                 FROM EmployeeSalaries S INNER JOIN EmployeeDetails E ON S.EmployeeId = E.Id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new EmployeeSalaryModel
                            {
                                SalaryId = Convert.ToInt32(reader["SalaryId"]),
                                EmployeeId = Convert.ToInt32(reader["EmployeeId"]),
                                EmployeeName = reader["EmployeeName"].ToString(),
                                BasicSalary = Convert.ToDecimal(reader["BasicSalary"]),
                                Allowance = Convert.ToDecimal(reader["Allowance"]),
                                Deduction = Convert.ToDecimal(reader["Deduction"])
                            });
                        }
                    }
                }
            }
            return View(list);
        }
        #endregion
    }
}
