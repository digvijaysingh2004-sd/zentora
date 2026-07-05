using System.Web.Mvc;

namespace zentoraHRMS.Controllers
{
    public class TrainingController : Controller
    {
        public ActionResult TrainingTypes() { return View(); }
        public ActionResult TrainingPrograms() { return View(); }
        public ActionResult TrainingSessions() { return View(); }
        public ActionResult EmployeeTrainings() { return View(); }
    }
}
