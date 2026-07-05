using System;
using System.Web.Mvc;

namespace zentoraHRMS.Controllers
{
    public class CalendarController : Controller
    {
        public ActionResult Index()
        {
            if (Session["RoleType"] == null || Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            return View();
        }
    }
}
