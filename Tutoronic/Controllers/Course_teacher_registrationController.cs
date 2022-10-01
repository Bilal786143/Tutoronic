using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Tutoronic.Models;

namespace Tutoronic.Controllers
{
    public class Course_teacher_registrationController : Controller
    {
        private Model1 db = new Model1();
        public ActionResult Index()
        {
            var course_teacher_registration = db.Course_teacher_registration.Include(c => c.Cours).Include(c => c.Teacher);
            return View(course_teacher_registration.ToList());
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course_teacher_registration course_teacher_registration = db.Course_teacher_registration.Find(id);
            if (course_teacher_registration == null)
            {
                return HttpNotFound();
            }
            return View(course_teacher_registration);
        }
        public ActionResult Create()
        {
            ViewBag.course_fid = new SelectList(db.Courses, "Course_id", "course_name");
            ViewBag.teacher_fid = new SelectList(db.Teachers, "Teacher_id", "teacher_name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Course_tch_reg_id,teacher_fid,course_fid,Status,course_price")] Course_teacher_registration course_teacher_registration)
        {
            if (ModelState.IsValid)
            {
                db.Course_teacher_registration.Add(course_teacher_registration);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.course_fid = new SelectList(db.Courses, "Course_id", "course_name", course_teacher_registration.course_fid);
            ViewBag.teacher_fid = new SelectList(db.Teachers, "Teacher_id", "teacher_name", course_teacher_registration.teacher_fid);
            return View(course_teacher_registration);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course_teacher_registration course_teacher_registration = db.Course_teacher_registration.Find(id);
            if (course_teacher_registration == null)
            {
                return HttpNotFound();
            }
            ViewBag.course_fid = new SelectList(db.Courses, "Course_id", "course_name", course_teacher_registration.course_fid);
            ViewBag.teacher_fid = new SelectList(db.Teachers, "Teacher_id", "teacher_name", course_teacher_registration.teacher_fid);
            return View(course_teacher_registration);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Course_tch_reg_id,teacher_fid,course_fid,Status,course_price")] Course_teacher_registration course_teacher_registration)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course_teacher_registration).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.course_fid = new SelectList(db.Courses, "Course_id", "course_name", course_teacher_registration.course_fid);
            ViewBag.teacher_fid = new SelectList(db.Teachers, "Teacher_id", "teacher_name", course_teacher_registration.teacher_fid);
            return View(course_teacher_registration);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course_teacher_registration course_teacher_registration = db.Course_teacher_registration.Find(id);
            if (course_teacher_registration == null)
            {
                return HttpNotFound();
            }
            return View(course_teacher_registration);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course_teacher_registration course_teacher_registration = db.Course_teacher_registration.Find(id);
            db.Course_teacher_registration.Remove(course_teacher_registration);
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
