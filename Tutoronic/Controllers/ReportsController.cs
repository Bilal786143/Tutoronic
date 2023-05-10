using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Tutoronic.Models;

namespace Tutoronic.Controllers
{
    public class ReportsController : Controller
    {
        Model1 db = new Model1();
        public ActionResult coursesalereport()
        {
            var t = (Teacher)Session["tch"];
            if (t != null)
            {
                var od = db.OrderDetails.Where(x => x.Cours.teacher_fid == t.Teacher_id).ToList();
                return View(od);
            }
            else { return RedirectToAction("login", "Home"); }
        }
        public ActionResult tchrstudent()
        {
            return View(studentemailslist());
        }
        public List<Student> studentemailslist()
        {
            var teacher = (Teacher)Session["tch"];
            List<Student> students = db.OrderDetails.Where(x => x.Cours.teacher_fid == teacher.Teacher_id).Select(x => x.Order.Student).ToList();
            return students;
        }
    }
}