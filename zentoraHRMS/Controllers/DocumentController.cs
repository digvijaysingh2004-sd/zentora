using System.Web.Mvc;

namespace zentoraHRMS.Controllers
{
    public class DocumentController : Controller
    {
        public ActionResult DocumentCategories() { return View(); }
        public ActionResult DocumentTypes() { return View(); }
        public ActionResult HRDocuments() { return View(); }
        public ActionResult Acknowledgments() { return View(); }
        public ActionResult DocumentTemplates() { return View(); }
    }
}
