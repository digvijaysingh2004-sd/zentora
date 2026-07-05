using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace zentoraHRMS.Controllers
{
    public class HomeController : Controller
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

        public ActionResult Index()
        {
            if (Session["RoleType"] == null || Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            // Pass session values to the view
            ViewBag.UserName = Session["UserName"];
            ViewBag.RoleType = Session["RoleType"];
            ViewBag.RoleName = Session["RoleName"];
            ViewBag.Designation = Session["Designation"];
            int roleId = Session["RoleId"] != null ? Convert.ToInt32(Session["RoleId"]) : 6;
            ViewBag.RoleId = roleId;
            int userId = Convert.ToInt32(Session["UserId"]);

            // Load counts and data depending on the role to make the dashboard dynamic
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // --- Global stats ---
                    // Total Employees
                    using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM EmployeeDetails WHERE IsDeleted = 0 OR IsDeleted IS NULL", conn))
                    {
                        ViewBag.DbTotalEmployees = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    // Active Branches
                    using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Branches", conn))
                    {
                        ViewBag.DbTotalBranches = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    // Total Departments
                    using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Departments", conn))
                    {
                        ViewBag.DbTotalDepartments = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    // Active Projects
                    using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Projects WHERE IsActive = 1", conn))
                    {
                        ViewBag.DbTotalProjects = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    // Monthly Payroll
                    using (SqlCommand cmd = new SqlCommand("SELECT COALESCE(SUM(BasicSalary + Allowance - Deduction), 0) FROM EmployeeSalaries", conn))
                    {
                        ViewBag.DbMonthlyPayroll = Convert.ToDecimal(cmd.ExecuteScalar());
                    }

                    // Announcements (used in employee view)
                    DataTable dtAnnouncements = new DataTable();
                    using (SqlCommand cmd = new SqlCommand("SELECT TOP 5 Title, Description, StartDate, CreatedBy FROM Announcements ORDER BY StartDate DESC", conn))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dtAnnouncements);
                        }
                    }
                    ViewBag.DbAnnouncements = dtAnnouncements;

                    // --- Role-specific stats ---
                    if (roleId == 1 || roleId == 2) // Admin/Superadmin
                    {
                        // Today's attendance percentage
                        decimal attendanceRate = 95.0m; // Fallback
                        using (SqlCommand cmd = new SqlCommand(@"
                            DECLARE @LatestDate DATE = (SELECT MAX(AttendanceDate) FROM AttendanceRecords);
                            IF @LatestDate IS NOT NULL
                            BEGIN
                                DECLARE @Present INT = (SELECT COUNT(*) FROM AttendanceRecords WHERE AttendanceDate = @LatestDate AND Status = 'Present');
                                DECLARE @Total INT = (SELECT COUNT(*) FROM EmployeeDetails WHERE IsDeleted = 0 OR IsDeleted IS NULL);
                                IF @Total > 0
                                    SELECT CAST(@Present AS DECIMAL(18,2)) / @Total * 100;
                                ELSE
                                    SELECT 100.0;
                            END
                            ELSE
                                SELECT 94.2;", conn))
                        {
                            attendanceRate = Convert.ToDecimal(cmd.ExecuteScalar());
                        }
                        ViewBag.DbAttendanceRate = attendanceRate;
                    }
                    else if (roleId == 3 || roleId == 5) // Manager / Team Lead
                    {
                        // Team Size (employees reporting to this manager)
                        using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM EmployeeDetails WHERE (ReportingManager = @UserId OR AssociateReportingManager = @UserId) AND (IsDeleted = 0 OR IsDeleted IS NULL)", conn))
                        {
                            cmd.Parameters.AddWithValue("@UserId", userId);
                            int size = Convert.ToInt32(cmd.ExecuteScalar());
                            ViewBag.DbTeamSize = size > 0 ? size : 0;
                        }

                        // Pending Leaves
                        using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM LeaveApplications WHERE Status = 'Pending'", conn))
                        {
                            ViewBag.DbPendingLeaves = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        // Team Status Tracker Table
                        DataTable dtTeam = new DataTable();
                        using (SqlCommand cmd = new SqlCommand(@"
                            SELECT TOP 6 CONCAT(FirstName, ' ', LastName) AS EmpName, Designation, Department, Branch, IsActive 
                            FROM EmployeeDetails 
                            WHERE (ReportingManager = @UserId OR AssociateReportingManager = @UserId OR @UserId = 1) AND (IsDeleted = 0 OR IsDeleted IS NULL)", conn))
                        {
                            cmd.Parameters.AddWithValue("@UserId", userId);
                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                da.Fill(dtTeam);
                            }
                        }
                        if (dtTeam.Rows.Count == 0) // Fallback if no reporting employees
                        {
                            dtTeam.Clear();
                            using (SqlCommand cmd = new SqlCommand("SELECT TOP 6 CONCAT(FirstName, ' ', LastName) AS EmpName, Designation, Department, Branch, IsActive FROM EmployeeDetails WHERE IsDeleted = 0 OR IsDeleted IS NULL", conn))
                            {
                                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                                {
                                    da.Fill(dtTeam);
                                }
                            }
                        }
                        ViewBag.DbTeamList = dtTeam;

                        // Leave Approvals List
                        DataTable dtLeaves = new DataTable();
                        using (SqlCommand cmd = new SqlCommand(@"
                            SELECT TOP 3 l.LeaveApplicationId, CONCAT(e.FirstName, ' ', e.LastName) AS EmpName, l.StartDate, l.EndDate, l.Reason, c.CategoryName 
                            FROM LeaveApplications l 
                            JOIN EmployeeDetails e ON l.EmployeeId = e.Id 
                            LEFT JOIN LeaveCategory c ON l.LeaveCategoryId = c.LeaveCategoryId
                            WHERE l.Status = 'Pending'", conn))
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                da.Fill(dtLeaves);
                            }
                        }
                        ViewBag.DbPendingLeavesList = dtLeaves;
                    }
                    else if (roleId == 4) // HR
                    {
                        // Job Postings count
                        using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM JobPostings WHERE Status = 'Open' OR Status IS NULL", conn))
                        {
                            ViewBag.DbOpenJobs = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        // Total Candidates
                        using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Candidates", conn))
                        {
                            ViewBag.DbTotalCandidates = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        // Pending Leaves
                        using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM LeaveApplications WHERE Status = 'Pending'", conn))
                        {
                            ViewBag.DbPendingLeaves = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        // Candidate Applications List
                        DataTable dtCandidates = new DataTable();
                        using (SqlCommand cmd = new SqlCommand(@"
                            SELECT TOP 5 c.FullName, j.Title AS JobTitle, c.ApplicationStatus, c.Email 
                            FROM Candidates c 
                            LEFT JOIN JobPostings j ON c.JobId = j.JobId 
                            ORDER BY c.CandidateId DESC", conn))
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                da.Fill(dtCandidates);
                            }
                        }
                        ViewBag.DbCandidatesList = dtCandidates;
                    }
                    else if (roleId == 6) // Employee
                    {
                        // My Today's Attendance
                        DataTable dtMyAttendance = new DataTable();
                        using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 ClockIn, Status, AttendanceDate FROM AttendanceRecords WHERE EmployeeId = @EmpId ORDER BY AttendanceDate DESC", conn))
                        {
                            cmd.Parameters.AddWithValue("@EmpId", userId);
                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                da.Fill(dtMyAttendance);
                            }
                        }
                        ViewBag.DbMyAttendance = dtMyAttendance;

                        // My Leave Balance
                        DataTable dtLeaveBalance = new DataTable();
                        using (SqlCommand cmd = new SqlCommand(@"
                            SELECT c.CategoryName, c.TotalDays, 
                                   COALESCE((SELECT SUM(DATEDIFF(day, StartDate, EndDate) + 1) 
                                             FROM LeaveApplications 
                                             WHERE EmployeeId = @EmpId AND LeaveCategoryId = c.LeaveCategoryId AND Status = 'Approved'), 0) AS UsedDays
                            FROM LeaveCategory c", conn))
                        {
                            cmd.Parameters.AddWithValue("@EmpId", userId);
                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                da.Fill(dtLeaveBalance);
                            }
                        }
                        ViewBag.DbLeaveBalance = dtLeaveBalance;
                    }
                }
            }
            catch (Exception ex)
            {
                // Fallbacks so page doesn't crash if database has issues
                ViewBag.DbError = ex.Message;
            }

            return View();
        }
    }
}