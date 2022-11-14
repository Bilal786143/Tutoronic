using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tutoronic.Models;
using Tutoronic.Services.Interface;

namespace Tutoronic.Controllers
{
    public class HomeController : ServerMapPathController
    {
        Model1 db = new Model1();
        private readonly IHome _home;
        private readonly IStudents _students;
        public HomeController(IHome home, IStudents students)
        {
            _home = home;
            _students = students;
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

        [HttpPost]
        public ActionResult tchlogin(Teacher t)
        {
            Session.Remove("tch");
            var teacherEntity = db.Teachers.Where(x => x.teacher_email == t.teacher_email & x.teacher_password == t.teacher_password).FirstOrDefault();
            if (teacherEntity != null)
            {
                Session["tch"] = teacherEntity;
                return RedirectToAction("index", "Teacher");
            }
            else if (teacherEntity == null)
            {
                ViewBag.message = "The Email you have entered is not registered yet. Please Register Your Account Here";
                return View("login");
            }
            else
            {
                ViewBag.message = "Email or Password is incorrect";
                return View("login");
            }
        }
        public ActionResult admlogin(Admin a)
        {
            Session.Remove("adm");
            var adminEntity = db.Admins.Where(x => x.admin_email == a.admin_email & x.admin_password == a.admin_password).FirstOrDefault();
            if (adminEntity != null)
            {
                Session["adm"] = adminEntity;
                return RedirectToAction("index", "Admins");
            }
            else if (adminEntity == null)
            {
                ViewBag.message = "The Email you have entered is not registered yet. Please Register Your Account Here";
                return View("login");
            }
            else
            {
                ViewBag.message = "Email or Password is incorrect";
                return View("login");
            }
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

        [HttpPost]
        public ActionResult admregister(Admin admin, HttpPostedFileBase pic)
        {
            Session.Remove("adm");
            var imagePath = ServerMapPath(pic);
            if (imagePath == ViewBag.message)
            {
                TempData["errormsg"] = "<script> alert('Image Format is not supported')</script>";
                return View("register");
            }
            var result = db.Admins.Where(x => x.admin_email == admin.admin_email).Count();
            if (result == 1)
            {
                ViewBag.message = "This Email is already Registered. Please enter new Email.";
                return View("register");
            }
            admin.admin_pic = imagePath;
            db.Admins.Add(admin);
            db.SaveChanges();
            Session["adm"] = admin;
            _home.SendMail(admin);
            return RedirectToAction("index", "Admins");
        }

        [HttpPost]
        public ActionResult tchregister(Teacher teacher, HttpPostedFileBase pic)
        {
            Session.Remove("tch");
            var imagePath = ServerMapPath(pic);
            if (imagePath == ViewBag.message)
            {
                TempData["errormsg"] = "<script> alert('Image Format is not supported')</script>";
                return View("Create");
            }
            var result = db.Teachers.Where(x => x.teacher_email == teacher.teacher_email).Count();
            if (result == 1)
            {
                ViewBag.message = "This Email is already Registered. Please enter new Email.";
                return View("register");
            }
            teacher.teacher_pic = imagePath;
            db.Teachers.Add(teacher);
            db.SaveChanges();
            Session["tch"] = teacher;
            return RedirectToAction("index", "teacher");
        }
    }
}