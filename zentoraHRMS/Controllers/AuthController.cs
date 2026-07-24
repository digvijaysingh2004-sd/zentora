using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace zentoraHRMS.Controllers
{
    public class AuthController : Controller
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["Zentora"].ConnectionString;

        // GET: Auth
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Login(string UserIdentifier, string Password)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("ValidateUser", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserIdentifier", UserIdentifier);
                    cmd.Parameters.AddWithValue("@Password", Password);

                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        // Example: fields returned by ValidateUser proc
                        int userId = Convert.ToInt32(reader["EmpId"]);
                        int roleIntId = Convert.ToInt32(reader["RoleIntId"]);
                        string roleType = reader["RoleType"].ToString();
                        string roleName = reader["RoleName"].ToString();
                        string designation = reader["Designation"].ToString();
                        string fullName = reader["FirstName"].ToString() + ' ' + reader["MiddleName"].ToString() + ' ' + reader["LastName"].ToString();

                        // Store in Session (session-based authentication)
                        Session["UserId"] = userId;
                        Session["UserName"] = fullName;
                        Session["RoleType"] = roleType;
                        Session["RoleName"] = roleName;
                        Session["Designation"] = designation;
                        Session["RoleId"] = roleIntId;

                        string profileImage = "";
                        using (SqlConnection connImg = new SqlConnection(connectionString))
                        {
                            using (SqlCommand cmdImg = new SqlCommand("SELECT ProfileImage FROM EmployeeDetails WHERE Id = @Id", connImg))
                            {
                                cmdImg.Parameters.AddWithValue("@Id", userId);
                                connImg.Open();
                                object imgObj = cmdImg.ExecuteScalar();
                                if (imgObj != null && imgObj != DBNull.Value)
                                {
                                    profileImage = imgObj.ToString();
                                }
                            }
                        }
                        Session["ProfileImage"] = profileImage;

                        // Single redirect URL
                        string redirectUrl = Url.Action("Index", "Home");

                        return Json(new
                        {
                            success = true,
                            message = "Login successful!",
                            redirectUrl = redirectUrl
                        });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Invalid credentials!" });
                    }
                }
            }
        }

        // Logout Action
        [HttpPost]
        public JsonResult Logout()
        {
            // Clear session
            Session.Clear();
            Session.Abandon();

            // Clear authentication cookie
            if (Request.Cookies[".ASPXAUTH"] != null)
            {
                var cookie = new HttpCookie(".ASPXAUTH");
                cookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookie);
            }

            // Disable caching so back button won’t reload dashboard
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();

            return Json(new { success = true });
        }
    }
}