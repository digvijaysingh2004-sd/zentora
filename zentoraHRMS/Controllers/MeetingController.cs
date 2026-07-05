using System.Web.Mvc;

namespace zentoraHRMS.Controllers
{
    public class MeetingController : Controller
    {
        public ActionResult MeetingTypes() { return View(); }
        public ActionResult MeetingRooms() { return View(); }
        public ActionResult Meetings() { return View(); }
        public ActionResult MeetingAttendees() { return View(); }
        public ActionResult MeetingMinutes() { return View(); }
        public ActionResult ActionItems() { return View(); }
    }
}
