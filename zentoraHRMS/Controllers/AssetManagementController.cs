using System.Web.Mvc;

namespace zentoraHRMS.Controllers
{
    public class AssetManagementController : Controller
    {
        public ActionResult AssetTypes() { return View(); }
        public ActionResult Assets() { return View(); }
        public ActionResult Dashboard() { return View(); }
        public ActionResult Depreciation() { return View(); }
    }
}
