using System.Web.Mvc;
using Tutoronic.Models;
using Tutoronic.Services.Interface;

namespace Tutoronic.Controllers
{
    public class HomeController : ServerMapPathController
    {
        private readonly IHome _home;
        public HomeController(IHome home)
        {
            _home = home;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Totalenrolment()
        {
            return View();
        }
        public ActionResult about()
        {
            return View();
        }
        public ActionResult Checkout()
        {
            return View();
        }
        public ActionResult about1()
        {
            return View();
        }
        public ActionResult blogclassicgrid()
        {
            return View();
        }
        public ActionResult blogclassicsidebar()
        {
            return View();
        }
        public ActionResult blogdetails(int id, int? vid) //int is a value so we can not give null value so we use question with int for accept null value
        {
            TempData["pid"] = id;
            if (vid != null)
            {
                TempData["vid"] = vid;
            }
            return View();
        }

        [HttpPost]
        public ActionResult blogdetails(Course_video_comment cvc)
        {
            var s = (Student)Session["studentloging"];
            _home.BlogDetails(cvc, s);
            return View();
        }
        public ActionResult bloglistsidebar()
        {
            return View();
        }
        public ActionResult blogstandardsidebar()
        {
            return View();
        }
        public ActionResult contact1()
        {
            return View();
        }
        public ActionResult contact2()
        {
            return View();
        }
        public ActionResult courses()
        {
            return View();
        }
        public ActionResult coursesdetails(int id)
        {
            TempData["pid"] = id;
            return View();
        }
        public ActionResult error404()
        {
            return View();
        }
        public ActionResult events()
        {
            return View();
        }
        public ActionResult eventsdetails()
        {
            return View();
        }
        public ActionResult faq1()
        {
            return View();
        }
        public ActionResult faq2()
        {
            return View();
        }
        public ActionResult forgetpassword()
        {
            return View();
        }
        public ActionResult index2()
        {
            return View();
        }
        public ActionResult login()
        {
            return View();
        }
        public ActionResult membership()
        {
            return View();
        }
        public ActionResult portfolio()
        {
            return View();
        }
        public ActionResult profile()
        {
            return View();
        }
        public ActionResult register()
        {
            return View();
        }
    }
}