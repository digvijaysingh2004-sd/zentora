using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using zentoraHRMS.Models;

namespace zentoraHRMS.Controllers
{
    public class AttendanceController : Controller
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["Zentora"].ConnectionString;

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            base.OnActionExecuting(filterContext);
        }

        #region Attendance Records
        public ActionResult AttendanceRecords()
        {
            if (Session["RoleType"] == null || Session["UserId"] == null) return RedirectToAction("Login", "Auth");

            List<AttendanceRecordModel> list = new List<AttendanceRecordModel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"SELECT AR.AttendanceId, AR.EmployeeId, E.FirstName + ' ' + E.LastName AS EmployeeName, 
                                 AR.AttendanceDate, AR.ClockIn, AR.ClockOut, AR.Status 
                                 FROM AttendanceRecords AR 
                                 INNER JOIN EmployeeDetails E ON AR.EmployeeId = E.Id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new AttendanceRecordModel
                            {
                                AttendanceId = Convert.ToInt32(reader["AttendanceId"]),
                                EmployeeId = Convert.ToInt32(reader["EmployeeId"]),
                                EmployeeName = reader["EmployeeName"].ToString(),
                                AttendanceDate = Convert.ToDateTime(reader["AttendanceDate"]),
                                ClockIn = (TimeSpan)reader["ClockIn"],
                                ClockOut = reader["ClockOut"] != DBNull.Value ? (TimeSpan?)reader["ClockOut"] : null,
                                Status = reader["Status"].ToString()
                            });
                        }
                    }
                }
            }
            return View(list);
        }

        [HttpPost]
        public JsonResult SaveAttendance(int employeeId, string status, string clockInStr, string clockOutStr, string dateStr)
        {
            try
            {
                DateTime attendanceDate = DateTime.Parse(dateStr);
                TimeSpan clockIn = TimeSpan.Parse(clockInStr);
                TimeSpan? clockOut = null;
                if (!string.IsNullOrEmpty(clockOutStr))
                {
                    clockOut = TimeSpan.Parse(clockOutStr);
                }

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = @"INSERT INTO AttendanceRecords (EmployeeId, AttendanceDate, ClockIn, ClockOut, Status) 
                                     VALUES (@EmployeeId, @AttendanceDate, @ClockIn, @ClockOut, @Status)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                        cmd.Parameters.AddWithValue("@AttendanceDate", attendanceDate);
                        cmd.Parameters.AddWithValue("@ClockIn", clockIn);
                        cmd.Parameters.AddWithValue("@ClockOut", (object)clockOut ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Status", status ?? "Present");
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true, message = "Attendance log saved successfully!" });
            }
            catch (Exception ex) { return Json(new { success = false, message = ex.Message }); }
        }

        public JsonResult GetAttendanceById(int id)
        {
            AttendanceRecordModel model = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT AttendanceId, EmployeeId, AttendanceDate, ClockIn, ClockOut, Status FROM AttendanceRecords WHERE AttendanceId = @AttendanceId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@AttendanceId", id);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            model = new AttendanceRecordModel
                            {
                                AttendanceId = Convert.ToInt32(reader["AttendanceId"]),
                                EmployeeId = Convert.ToInt32(reader["EmployeeId"]),
                                AttendanceDate = Convert.ToDateTime(reader["AttendanceDate"]),
                                ClockIn = (TimeSpan)reader["ClockIn"],
                                ClockOut = reader["ClockOut"] != DBNull.Value ? (TimeSpan?)reader["ClockOut"] : null,
                                Status = reader["Status"].ToString()
                            };
                        }
                    }
                }
            }

            // Map to string format for simple JSON rendering of timespans and dates
            var result = new
            {
                AttendanceId = model.AttendanceId,
                EmployeeId = model.EmployeeId,
                AttendanceDate = model.AttendanceDate.ToString("yyyy-MM-dd"),
                ClockIn = model.ClockIn.ToString(@"hh\:mm"),
                ClockOut = model.ClockOut.HasValue ? model.ClockOut.Value.ToString(@"hh\:mm") : "",
                Status = model.Status
            };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateAttendance(int attendanceId, int employeeId, string status, string clockInStr, string clockOutStr, string dateStr)
        {
            try
            {
                DateTime attendanceDate = DateTime.Parse(dateStr);
                TimeSpan clockIn = TimeSpan.Parse(clockInStr);
                TimeSpan? clockOut = null;
                if (!string.IsNullOrEmpty(clockOutStr))
                {
                    clockOut = TimeSpan.Parse(clockOutStr);
                }

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = @"UPDATE AttendanceRecords SET EmployeeId = @EmployeeId, AttendanceDate = @AttendanceDate, 
                                     ClockIn = @ClockIn, ClockOut = @ClockOut, Status = @Status WHERE AttendanceId = @AttendanceId";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@AttendanceId", attendanceId);
                        cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                        cmd.Parameters.AddWithValue("@AttendanceDate", attendanceDate);
                        cmd.Parameters.AddWithValue("@ClockIn", clockIn);
                        cmd.Parameters.AddWithValue("@ClockOut", (object)clockOut ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Status", status ?? "Present");
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true });
            }
            catch (Exception ex) { return Json(new { success = false, message = ex.Message }); }
        }

        [HttpPost]
        public JsonResult DeleteAttendance(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM AttendanceRecords WHERE AttendanceId = @AttendanceId";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@AttendanceId", id);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true });
            }
            catch (Exception ex) { return Json(new { success = false, message = ex.Message }); }
        }
        #endregion
            public ActionResult AttendancePolicies() { return View(); }
        public ActionResult AttendanceRegularizations() { return View(); }
}
}
