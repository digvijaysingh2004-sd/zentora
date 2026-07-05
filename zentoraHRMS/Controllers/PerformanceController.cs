using System.Web.Mvc;

namespace zentoraHRMS.Controllers
{
    public class PerformanceController : Controller
    {
        public ActionResult IndicatorCategories() { return View(); }
        public ActionResult Indicators() { return View(); }
        public ActionResult GoalTypes() { return View(); }
        public ActionResult EmployeeGoals() { return View(); }
        public ActionResult ReviewCycles() { return View(); }
        public ActionResult EmployeeReviews() { return View(); }
    }
}
