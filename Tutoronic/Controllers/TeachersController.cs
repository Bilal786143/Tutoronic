using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Tutoronic.Models;

namespace Tutoronic.Controllers
{
    public class TeachersController : Controller
    {
        private Model1 db = new Model1();

        // GET: Teachers
        public ActionResult Index()
        {
            var teachers = db.Teachers.Include(t => t.Comment_reply);
            return View(teachers.ToList());
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

        // GET: Teachers/Details/5
        public ActionResult Details(int? id)
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

        // GET: Teachers/Create
        public ActionResult Create()
        {
            ViewBag.Teacher_id = new SelectList(db.Comment_reply, "Comment_reply_id", "reply");
            return View();
        }

        // POST: Teachers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Teacher_id,teacher_name,teacher_email,teacher_password,teacher_contact,teacher_pic,teacher_address,teacher_intro,total_enrolment")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                db.Teachers.Add(teacher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Teacher_id = new SelectList(db.Comment_reply, "Comment_reply_id", "reply", teacher.Teacher_id);
            return View(teacher);
        }

        // GET: Teachers/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.Teacher_id = new SelectList(db.Comment_reply, "Comment_reply_id", "reply", teacher.Teacher_id);
            return View(teacher);
        }

        // POST: Teachers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Teacher_id,teacher_name,teacher_email,teacher_password,teacher_contact,teacher_pic,teacher_address,teacher_intro,total_enrolment")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teacher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Teacher_id = new SelectList(db.Comment_reply, "Comment_reply_id", "reply", teacher.Teacher_id);
            return View(teacher);
        }

        // GET: Teachers/Delete/5
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

        // POST: Teachers/Delete/5
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
