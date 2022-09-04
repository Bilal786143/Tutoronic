using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Tutoronic.Models;

namespace Tutoronic.Controllers
{
    public class Course_video_commentController : Controller
    {
        private Model1 db = new Model1();

        // GET: Course_video_comment
        public ActionResult Index()
        {
            var course_video_comment = db.Course_video_comment.Include(c => c.Course_Video).Include(c => c.Student);
            return View(course_video_comment.ToList());
        }

        // GET: Course_video_comment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course_video_comment course_video_comment = db.Course_video_comment.Find(id);
            if (course_video_comment == null)
            {
                return HttpNotFound();
            }
            return View(course_video_comment);
        }

        // GET: Course_video_comment/Create
        public ActionResult Create()
        {
            ViewBag.course_vid_fid = new SelectList(db.Course_Video, "Course_vid_id", "video");
            ViewBag.std_fid = new SelectList(db.Students, "Student_id", "student_name");
            return View();
        }

        // POST: Course_video_comment/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Comment_id,course_vid_fid,comment,std_fid")] Course_video_comment course_video_comment)
        {
            if (ModelState.IsValid)
            {
                db.Course_video_comment.Add(course_video_comment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.course_vid_fid = new SelectList(db.Course_Video, "Course_vid_id", "video", course_video_comment.course_vid_fid);
            ViewBag.std_fid = new SelectList(db.Students, "Student_id", "student_name", course_video_comment.std_fid);
            return View(course_video_comment);
        }

        // GET: Course_video_comment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course_video_comment course_video_comment = db.Course_video_comment.Find(id);
            if (course_video_comment == null)
            {
                return HttpNotFound();
            }
            ViewBag.course_vid_fid = new SelectList(db.Course_Video, "Course_vid_id", "video", course_video_comment.course_vid_fid);
            ViewBag.std_fid = new SelectList(db.Students, "Student_id", "student_name", course_video_comment.std_fid);
            return View(course_video_comment);
        }

        // POST: Course_video_comment/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Comment_id,course_vid_fid,comment,std_fid")] Course_video_comment course_video_comment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course_video_comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.course_vid_fid = new SelectList(db.Course_Video, "Course_vid_id", "video", course_video_comment.course_vid_fid);
            ViewBag.std_fid = new SelectList(db.Students, "Student_id", "student_name", course_video_comment.std_fid);
            return View(course_video_comment);
        }

        // GET: Course_video_comment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course_video_comment course_video_comment = db.Course_video_comment.Find(id);
            if (course_video_comment == null)
            {
                return HttpNotFound();
            }
            return View(course_video_comment);
        }

        // POST: Course_video_comment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course_video_comment course_video_comment = db.Course_video_comment.Find(id);
            db.Course_video_comment.Remove(course_video_comment);
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
