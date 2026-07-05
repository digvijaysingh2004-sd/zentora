using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using zentoraHRMS.Models;

namespace zentoraHRMS.Controllers
{
    public class RecruitmentController : Controller
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["Zentora"].ConnectionString;

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            base.OnActionExecuting(filterContext);
        }

        #region Job Postings
        public ActionResult JobPostings()
        {
            if (Session["RoleType"] == null || Session["UserId"] == null) return RedirectToAction("Login", "Auth");

            List<JobPostingModel> list = new List<JobPostingModel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT JobId, Title, JobType, Location, NoOfVacancies, ExperienceRequired, Description, Status FROM JobPostings";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new JobPostingModel
                            {
                                JobId = Convert.ToInt32(reader["JobId"]),
                                Title = reader["Title"].ToString(),
                                JobType = reader["JobType"].ToString(),
                                Location = reader["Location"].ToString(),
                                NoOfVacancies = Convert.ToInt32(reader["NoOfVacancies"]),
                                ExperienceRequired = reader["ExperienceRequired"].ToString(),
                                Description = reader["Description"].ToString(),
                                Status = reader["Status"].ToString()
                            });
                        }
                    }
                }
            }
            return View(list);
        }

        [HttpPost]
        public JsonResult SaveJob(JobPostingModel model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = @"INSERT INTO JobPostings (Title, JobType, Location, NoOfVacancies, ExperienceRequired, Description, Status) 
                                     VALUES (@Title, @JobType, @Location, @NoOfVacancies, @ExperienceRequired, @Description, @Status)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Title", model.Title ?? "");
                        cmd.Parameters.AddWithValue("@JobType", model.JobType ?? "Full-Time");
                        cmd.Parameters.AddWithValue("@Location", model.Location ?? "");
                        cmd.Parameters.AddWithValue("@NoOfVacancies", model.NoOfVacancies);
                        cmd.Parameters.AddWithValue("@ExperienceRequired", model.ExperienceRequired ?? "");
                        cmd.Parameters.AddWithValue("@Description", model.Description ?? "");
                        cmd.Parameters.AddWithValue("@Status", model.Status ?? "Active");
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true, message = "Job saved successfully!" });
            }
            catch (Exception ex) { return Json(new { success = false, message = ex.Message }); }
        }

        public JsonResult GetJobById(int id)
        {
            JobPostingModel model = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT JobId, Title, JobType, Location, NoOfVacancies, ExperienceRequired, Description, Status FROM JobPostings WHERE JobId = @JobId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@JobId", id);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            model = new JobPostingModel
                            {
                                JobId = Convert.ToInt32(reader["JobId"]),
                                Title = reader["Title"].ToString(),
                                JobType = reader["JobType"].ToString(),
                                Location = reader["Location"].ToString(),
                                NoOfVacancies = Convert.ToInt32(reader["NoOfVacancies"]),
                                ExperienceRequired = reader["ExperienceRequired"].ToString(),
                                Description = reader["Description"].ToString(),
                                Status = reader["Status"].ToString()
                            };
                        }
                    }
                }
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateJob(JobPostingModel model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = @"UPDATE JobPostings SET Title = @Title, JobType = @JobType, Location = @Location, 
                                     NoOfVacancies = @NoOfVacancies, ExperienceRequired = @ExperienceRequired, 
                                     Description = @Description, Status = @Status WHERE JobId = @JobId";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@JobId", model.JobId);
                        cmd.Parameters.AddWithValue("@Title", model.Title ?? "");
                        cmd.Parameters.AddWithValue("@JobType", model.JobType ?? "Full-Time");
                        cmd.Parameters.AddWithValue("@Location", model.Location ?? "");
                        cmd.Parameters.AddWithValue("@NoOfVacancies", model.NoOfVacancies);
                        cmd.Parameters.AddWithValue("@ExperienceRequired", model.ExperienceRequired ?? "");
                        cmd.Parameters.AddWithValue("@Description", model.Description ?? "");
                        cmd.Parameters.AddWithValue("@Status", model.Status ?? "Active");
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true });
            }
            catch (Exception ex) { return Json(new { success = false, message = ex.Message }); }
        }

        [HttpPost]
        public JsonResult DeleteJob(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM JobPostings WHERE JobId = @JobId";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@JobId", id);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true });
            }
            catch (Exception ex) { return Json(new { success = false, message = ex.Message }); }
        }
        #endregion

        #region Candidates
        public ActionResult Candidates()
        {
            if (Session["RoleType"] == null || Session["UserId"] == null) return RedirectToAction("Login", "Auth");

            List<CandidateModel> list = new List<CandidateModel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"SELECT C.CandidateId, C.JobId, J.Title AS JobTitle, C.FullName, C.Email, C.Phone, C.ResumeUrl, C.ApplicationStatus, C.AppliedDate 
                                 FROM Candidates C INNER JOIN JobPostings J ON C.JobId = J.JobId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new CandidateModel
                            {
                                CandidateId = Convert.ToInt32(reader["CandidateId"]),
                                JobId = Convert.ToInt32(reader["JobId"]),
                                JobTitle = reader["JobTitle"].ToString(),
                                FullName = reader["FullName"].ToString(),
                                Email = reader["Email"].ToString(),
                                Phone = reader["Phone"].ToString(),
                                ResumeUrl = reader["ResumeUrl"].ToString(),
                                ApplicationStatus = reader["ApplicationStatus"].ToString(),
                                AppliedDate = Convert.ToDateTime(reader["AppliedDate"])
                            });
                        }
                    }
                }
            }
            return View(list);
        }

        [HttpPost]
        public JsonResult SaveCandidate(CandidateModel model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = @"INSERT INTO Candidates (JobId, FullName, Email, Phone, ResumeUrl, ApplicationStatus, AppliedDate) 
                                     VALUES (@JobId, @FullName, @Email, @Phone, @ResumeUrl, @ApplicationStatus, GETDATE())";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@JobId", model.JobId);
                        cmd.Parameters.AddWithValue("@FullName", model.FullName ?? "");
                        cmd.Parameters.AddWithValue("@Email", model.Email ?? "");
                        cmd.Parameters.AddWithValue("@Phone", model.Phone ?? "");
                        cmd.Parameters.AddWithValue("@ResumeUrl", model.ResumeUrl ?? "");
                        cmd.Parameters.AddWithValue("@ApplicationStatus", model.ApplicationStatus ?? "Applied");
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true, message = "Candidate saved successfully!" });
            }
            catch (Exception ex) { return Json(new { success = false, message = ex.Message }); }
        }

        public JsonResult GetCandidateById(int id)
        {
            CandidateModel model = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT CandidateId, JobId, FullName, Email, Phone, ResumeUrl, ApplicationStatus, AppliedDate FROM Candidates WHERE CandidateId = @CandidateId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@CandidateId", id);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            model = new CandidateModel
                            {
                                CandidateId = Convert.ToInt32(reader["CandidateId"]),
                                JobId = Convert.ToInt32(reader["JobId"]),
                                FullName = reader["FullName"].ToString(),
                                Email = reader["Email"].ToString(),
                                Phone = reader["Phone"].ToString(),
                                ResumeUrl = reader["ResumeUrl"].ToString(),
                                ApplicationStatus = reader["ApplicationStatus"].ToString(),
                                AppliedDate = Convert.ToDateTime(reader["AppliedDate"])
                            };
                        }
                    }
                }
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateCandidate(CandidateModel model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = @"UPDATE Candidates SET JobId = @JobId, FullName = @FullName, Email = @Email, 
                                     Phone = @Phone, ResumeUrl = @ResumeUrl, ApplicationStatus = @ApplicationStatus WHERE CandidateId = @CandidateId";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@CandidateId", model.CandidateId);
                        cmd.Parameters.AddWithValue("@JobId", model.JobId);
                        cmd.Parameters.AddWithValue("@FullName", model.FullName ?? "");
                        cmd.Parameters.AddWithValue("@Email", model.Email ?? "");
                        cmd.Parameters.AddWithValue("@Phone", model.Phone ?? "");
                        cmd.Parameters.AddWithValue("@ResumeUrl", model.ResumeUrl ?? "");
                        cmd.Parameters.AddWithValue("@ApplicationStatus", model.ApplicationStatus ?? "Applied");
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true });
            }
            catch (Exception ex) { return Json(new { success = false, message = ex.Message }); }
        }

        [HttpPost]
        public JsonResult DeleteCandidate(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Candidates WHERE CandidateId = @CandidateId";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@CandidateId", id);
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
        public JsonResult GetJobsList()
        {
            List<JobPostingModel> list = new List<JobPostingModel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT JobId, Title FROM JobPostings WHERE Status = 'Active'";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new JobPostingModel
                            {
                                JobId = Convert.ToInt32(reader["JobId"]),
                                Title = reader["Title"].ToString()
                            });
                        }
                    }
                }
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        #endregion
            public ActionResult RecruitmentAgencies() { return View(); }
        public ActionResult RecruitmentProcesses() { return View(); }
        public ActionResult InterviewTypes() { return View(); }
        public ActionResult Interviews() { return View(); }
        public ActionResult BackgroundChecks() { return View(); }
        public ActionResult JobCategories() { return View(); }
        public ActionResult JobTypes() { return View(); }
        public ActionResult JobLocations() { return View(); }
        public ActionResult CustomQuestions() { return View(); }
        public ActionResult CandidateSources() { return View(); }
        public ActionResult InterviewRounds() { return View(); }
        public ActionResult InterviewFeedback() { return View(); }
        public ActionResult CandidateAssessments() { return View(); }
        public ActionResult OfferTemplates() { return View(); }
        public ActionResult Offers() { return View(); }
        public ActionResult OnboardingChecklists() { return View(); }
        public ActionResult ChecklistItems() { return View(); }
        public ActionResult CandidateOnboarding() { return View(); }
        public ActionResult Career() { return View(); }
}
}
