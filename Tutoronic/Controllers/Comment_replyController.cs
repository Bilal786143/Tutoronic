using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Tutoronic.Models;

namespace Tutoronic.Controllers
{
    public class Comment_replyController : Controller
    {
        private Model1 db = new Model1();

        // GET: Comment_reply
        public ActionResult Index()
        {
            var comment_reply = db.Comment_reply.Include(c => c.Course_video_comment).Include(c => c.Teacher);
            return View(comment_reply.ToList());
        }

        // GET: Comment_reply/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment_reply comment_reply = db.Comment_reply.Find(id);
            if (comment_reply == null)
            {
                return HttpNotFound();
            }
            return View(comment_reply);
        }

        // GET: Comment_reply/Create
        public ActionResult Create()
        {
            ViewBag.C_V_C_fid = new SelectList(db.Course_video_comment, "Comment_id", "comment");
            ViewBag.Comment_reply_id = new SelectList(db.Teachers, "Teacher_id", "teacher_name");
            return View();
        }

        // POST: Comment_reply/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Comment_reply_id,C_V_C_fid,teacher_fid,reply")] Comment_reply comment_reply)
        {
            if (ModelState.IsValid)
            {
                db.Comment_reply.Add(comment_reply);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.C_V_C_fid = new SelectList(db.Course_video_comment, "Comment_id", "comment", comment_reply.C_V_C_fid);
            ViewBag.Comment_reply_id = new SelectList(db.Teachers, "Teacher_id", "teacher_name", comment_reply.Comment_reply_id);
            return View(comment_reply);
        }

        // GET: Comment_reply/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment_reply comment_reply = db.Comment_reply.Find(id);
            if (comment_reply == null)
            {
                return HttpNotFound();
            }
            ViewBag.C_V_C_fid = new SelectList(db.Course_video_comment, "Comment_id", "comment", comment_reply.C_V_C_fid);
            ViewBag.Comment_reply_id = new SelectList(db.Teachers, "Teacher_id", "teacher_name", comment_reply.Comment_reply_id);
            return View(comment_reply);
        }

        // POST: Comment_reply/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Comment_reply_id,C_V_C_fid,teacher_fid,reply")] Comment_reply comment_reply)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment_reply).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.C_V_C_fid = new SelectList(db.Course_video_comment, "Comment_id", "comment", comment_reply.C_V_C_fid);
            ViewBag.Comment_reply_id = new SelectList(db.Teachers, "Teacher_id", "teacher_name", comment_reply.Comment_reply_id);
            return View(comment_reply);
        }

        // GET: Comment_reply/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment_reply comment_reply = db.Comment_reply.Find(id);
            if (comment_reply == null)
            {
                return HttpNotFound();
            }
            return View(comment_reply);
        }

        // POST: Comment_reply/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment_reply comment_reply = db.Comment_reply.Find(id);
            db.Comment_reply.Remove(comment_reply);
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
