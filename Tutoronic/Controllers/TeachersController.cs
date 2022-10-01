using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Tutoronic.Models;
using Tutoronic.Services.Interface;

namespace Tutoronic.Controllers
{
    public class TeachersController : Controller
    {
        private Model1 db = new Model1();
        private readonly ITeachers _teacher;
        public TeachersController(ITeachers teacher)
        {
            _teacher = teacher;
        }
        public async Task<ActionResult> Index()
        {
            return View(await _teacher.GetAllTeachers());
        }
        public ActionResult profile_update(Teacher teacher)
        {
            Teacher t = (Teacher)Session["tch"];
            teacher.teacher_pic = t.teacher_pic;
            teacher.Teacher_id = t.Teacher_id;
            if (teacher.teacher_password == null)
            {
                teacher.teacher_password = t.teacher_password;
            }
            db.Entry(teacher).State = EntityState.Modified;
            db.SaveChanges();
            Session["tch"] = teacher;
            return RedirectToAction("teacherprofile", "Teacher");
        }

        public ActionResult Details(int id)
        {
            var teacher = _teacher.GetTeacherById(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }
        public ActionResult Create()
        {
            ViewBag.Teacher_id = new SelectList(db.Comment_reply, "Comment_reply_id", "reply");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                await _teacher.CreateTeacher(teacher);
                return RedirectToAction("Index");
            }
            ViewBag.Teacher_id = new SelectList(db.Comment_reply, "Comment_reply_id", "reply", teacher.Teacher_id);
            return View(teacher);
        }

        public ActionResult Edit(int id)
        {

            var teacher = _teacher.GetTeacherById(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            ViewBag.Teacher_id = new SelectList(db.Comment_reply, "Comment_reply_id", "reply", teacher.Id);
            return View(teacher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                _teacher.UpdateTeacher(teacher);
                return RedirectToAction("Index");
            }
            ViewBag.Teacher_id = new SelectList(db.Comment_reply, "Comment_reply_id", "reply", teacher.Teacher_id);
            return View(teacher);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Teacher teacher = db.Teachers.Find(id);
            db.Teachers.Remove(teacher);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
