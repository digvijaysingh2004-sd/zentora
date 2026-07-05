using System.Web.Mvc;

namespace zentoraHRMS.Controllers
{
    public class LandingPageController : Controller
    {
        public ActionResult LandingPage() { return View(); }
        public ActionResult CustomPages() { return View(); }
        public ActionResult ContactInquiries() { return View(); }
        public ActionResult Newsletter() { return View(); }
    }
}
