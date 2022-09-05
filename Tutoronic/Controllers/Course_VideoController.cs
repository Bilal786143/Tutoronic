using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Tutoronic.Models;

namespace Tutoronic.Controllers
{
    public class Course_VideoController : Controller
    {
        private Model1 db = new Model1();

        // GET: Course_Video
        public ActionResult Index(int id)
        {
            TempData["course_id"] = id;
            var course_Video = db.Course_Video.Include(c => c.Cours).Include(c => c.Teacher);
            return View(course_Video.ToList());
        }

        // GET: Course_Video/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course_Video course_Video = db.Course_Video.Find(id);
            if (course_Video == null)
            {
                return HttpNotFound();
            }
            return View(course_Video);
        }

        // GET: Course_Video/Create
        public ActionResult Create()
        {
            ViewBag.course_fid = new SelectList(db.Courses, "Course_id", "course_name");
            ViewBag.teacher_fid = new SelectList(db.Teachers, "Teacher_id", "teacher_name");
            return View();
        }

        // POST: Course_Video/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Course_Video course_Video, HttpPostedFileBase vid)
        {
            Teacher t = new Teacher();
            t = (Teacher)Session["tch"];
            int id = (int)TempData.Peek("course_id");
            course_Video.course_fid = id;
            course_Video.teacher_fid = t.Teacher_id;
            string fullpath = Server.MapPath("~/content/videos/" + vid.FileName);
            vid.SaveAs(fullpath);
            course_Video.video = "~/content/videos/" + vid.FileName;
            db.Course_Video.Add(course_Video);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = id });
        }

        // GET: Course_Video/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course_Video course_Video = db.Course_Video.Find(id);
            if (course_Video == null)
            {
                return HttpNotFound();
            }
            ViewBag.course_fid = new SelectList(db.Courses, "Course_id", "course_name", course_Video.course_fid);
            ViewBag.teacher_fid = new SelectList(db.Teachers, "Teacher_id", "teacher_name", course_Video.teacher_fid);
            return View(course_Video);
        }

        // POST: Course_Video/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Course_Video course_Video, HttpPostedFileBase vid)
        {
            var data = db.Course_Video.Find(course_Video.Course_vid_id);
            data.video_description = course_Video.video_description;
            data.video_title = course_Video.video_title;
            var id = data.course_fid;
            if (vid != null)
            {
                string fullpath = Server.MapPath("~/content/videos/" + vid.FileName);
                vid.SaveAs(fullpath);
                course_Video.video = "~/content/videos/" + vid.FileName;
            }
            db.Entry(data).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", new { id = data.course_fid });
        }

        // GET: Course_Video/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course_Video course_Video = db.Course_Video.Find(id);
            if (course_Video == null)
            {
                return HttpNotFound();
            }
            return View(course_Video);
        }

        // POST: Course_Video/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course_Video course_Video = db.Course_Video.Find(id);
            db.Course_Video.Remove(course_Video);
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
