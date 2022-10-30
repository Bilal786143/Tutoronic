using System.Web.Mvc;

namespace Tutoronic.Controllers
{
    public class TeacherController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult students()
        {
            return View();
        }
        public ActionResult logout()
        {
            Session["tch"] = null;
            return RedirectToAction("index", "Home");
        }
        public ActionResult addlisting()
        {
            return View();
        }
        public ActionResult basiccalendar()
        {
            return View();
        }
        public ActionResult allcourses()
        {
            return View();
        }
        public ActionResult courses()
        {
            return View();
        }
        public ActionResult listviewcalendar()
        {
            return View();
        }
        public ActionResult mailbox()
        {
            return View();
        }
        public ActionResult mailboxcompose()
        {
            return View();
        }
        public ActionResult mailboxread()
        {
            return View();
        }
        public ActionResult review()
        {
            return View();
        }
        public ActionResult teacher
            
            ()
        {
            return View();
        }
        public ActionResult userprofile()
        {
            return View();
        }
    }
}