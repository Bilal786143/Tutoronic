﻿using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tutoronic.Models;
using Tutoronic.Services.Interface;

namespace Tutoronic.Controllers
{
    public class HomeController : Controller
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
        public ActionResult stdlogin(Student s)
        {
            Student res = db.Students.FirstOrDefault(x => x.student_email == s.student_email & x.student_password == s.student_password);
            if (res != null)
            {
                Session["studentloging"] = res;
                return RedirectToAction("index");
            }
            else if (res == null)
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

        [HttpPost]
        public ActionResult tchlogin(Teacher t)
        {
            Teacher res = db.Teachers.Where(x => x.teacher_email == t.teacher_email & x.teacher_password == t.teacher_password).FirstOrDefault();
            if (res != null)
            {
                Session["tch"] = res;
                return RedirectToAction("index", "Teacher");
            }
            else if (res == null)
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
            Admin res = db.Admins.Where(x => x.admin_email == a.admin_email & x.admin_password == a.admin_password).FirstOrDefault();
            if (res != null)
            {
                Session["adm"] = res;
                return RedirectToAction("index", "Admins");
            }
            else if (res == null)
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
        public ActionResult stdregister(Student s, HttpPostedFileBase pic)
        {
            if (pic == null)
            {
                s.student_pic = "~/content/pics/blank-profile-picture-973460_640.png";
            }
            else
            {
                string fullpath = Server.MapPath("~/content/pics/" + pic.FileName);
                pic.SaveAs(fullpath);
                s.student_pic = "~/content/pics/" + pic.FileName;
            }
            var newStudent = _students.CreateNewStudent(s);
            if (newStudent == null)
            {
                ViewBag.message = "This Email is already Registered. Please enter new Email.";
                return View("register");
            }
            else
            {
                Session["studentloging"] = s;
                _home.SendMail(s);
                return RedirectToAction("index");
            }
        }

        [HttpPost]
        public ActionResult admregister(Admin a, HttpPostedFileBase pic)
        {
            int result = db.Admins.Where(x => x.admin_email == a.admin_email).Count();
            if (result == 1)
            {
                ViewBag.message = "This Email is already Registered. Please enter new Email.";
                return View("register");
            }
            else
            {
                if (pic == null)
                {
                    a.admin_pic = "~/content/pics/blank-profile-picture-973460_640.png";
                }
                else
                {
                    string fullpath = Server.MapPath("~/content/pics/" + pic.FileName);
                    pic.SaveAs(fullpath);
                    a.admin_pic = "~/content/pics/" + pic.FileName;
                }
                db.Admins.Add(a);
                db.SaveChanges();
                Session["adm"] = a;
                return RedirectToAction("index", "Admins");
            }
        }

        [HttpPost]
        public ActionResult tchregister(Teacher t, HttpPostedFileBase pic)
        {
            int result = db.Teachers.Where(x => x.teacher_email == t.teacher_email).Count();
            if (result == 1)
            {
                ViewBag.message = "This Email is already Registered. Please enter new Email.";
                return View("register");
            }
            else
            {
                if (pic == null)
                {
                    t.teacher_pic = "~/content/pics/blank-profile-picture-973460_640.png";
                }
                else
                {
                    string fullpath = Server.MapPath("~/content/pics/" + pic.FileName);
                    pic.SaveAs(fullpath);
                    t.teacher_pic = "~/content/pics/" + pic.FileName;
                }
                db.Teachers.Add(t);
                db.SaveChanges();
                Session["tch"] = t;
                return RedirectToAction("index", "teacher");
            }
        }
    }
}