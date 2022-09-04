using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Tutoronic.Models;

namespace Tutoronic.Controllers
{
    public class Course_Student_RegistrationController : Controller
    {
        private Model1 db = new Model1();

        // GET: Course_Student_Registration
        public ActionResult Index()
        {
            var course_Student_Registration = db.Course_Student_Registration.Include(c => c.Cours).Include(c => c.Student);
            return View(course_Student_Registration.ToList());
        }

        // GET: Course_Student_Registration/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course_Student_Registration course_Student_Registration = db.Course_Student_Registration.Find(id);
            if (course_Student_Registration == null)
            {
                return HttpNotFound();
            }
            return View(course_Student_Registration);
        }

        // GET: Course_Student_Registration/Create
        public ActionResult Create()
        {
            ViewBag.course_fid = new SelectList(db.Courses, "Course_id", "course_name");
            ViewBag.std_fid = new SelectList(db.Students, "Student_id", "student_name");
            return View();
        }

        // POST: Course_Student_Registration/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Std_reg_id,std_fid,course_fid,course_price")] Course_Student_Registration course_Student_Registration)
        {
            if (ModelState.IsValid)
            {
                db.Course_Student_Registration.Add(course_Student_Registration);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.course_fid = new SelectList(db.Courses, "Course_id", "course_name", course_Student_Registration.course_fid);
            ViewBag.std_fid = new SelectList(db.Students, "Student_id", "student_name", course_Student_Registration.std_fid);
            return View(course_Student_Registration);
        }

        // GET: Course_Student_Registration/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course_Student_Registration course_Student_Registration = db.Course_Student_Registration.Find(id);
            if (course_Student_Registration == null)
            {
                return HttpNotFound();
            }
            ViewBag.course_fid = new SelectList(db.Courses, "Course_id", "course_name", course_Student_Registration.course_fid);
            ViewBag.std_fid = new SelectList(db.Students, "Student_id", "student_name", course_Student_Registration.std_fid);
            return View(course_Student_Registration);
        }

        // POST: Course_Student_Registration/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Std_reg_id,std_fid,course_fid,course_price")] Course_Student_Registration course_Student_Registration)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course_Student_Registration).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.course_fid = new SelectList(db.Courses, "Course_id", "course_name", course_Student_Registration.course_fid);
            ViewBag.std_fid = new SelectList(db.Students, "Student_id", "student_name", course_Student_Registration.std_fid);
            return View(course_Student_Registration);
        }

        // GET: Course_Student_Registration/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course_Student_Registration course_Student_Registration = db.Course_Student_Registration.Find(id);
            if (course_Student_Registration == null)
            {
                return HttpNotFound();
            }
            return View(course_Student_Registration);
        }

        // POST: Course_Student_Registration/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course_Student_Registration course_Student_Registration = db.Course_Student_Registration.Find(id);
            db.Course_Student_Registration.Remove(course_Student_Registration);
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
