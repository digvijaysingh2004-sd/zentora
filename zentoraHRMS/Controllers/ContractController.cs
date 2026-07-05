using System.Web.Mvc;

namespace zentoraHRMS.Controllers
{
    public class ContractController : Controller
    {
        public ActionResult ContractTypes() { return View(); }
        public ActionResult VendorContracts() { return View(); }
        public ActionResult EmployeeContracts() { return View(); }
        public ActionResult ContractTemplates() { return View(); }
    }
}
