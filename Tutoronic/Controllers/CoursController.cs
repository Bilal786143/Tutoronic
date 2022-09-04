using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Tutoronic.Models;

namespace Tutoronic.Controllers
{
    public class CoursController : Controller
    {
        private Model1 db = new Model1();

        // GET: Cours
        public ActionResult Index()
        {
            var courses = db.Courses.Include(c => c.SubCategory).Include(c => c.Teacher);
            return View(courses.ToList());
        }

        // GET: Cours/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cours cours = db.Courses.Find(id);
            if (cours == null)
            {
                return HttpNotFound();
            }
            return View(cours);
        }

        // GET: Cours/Create
        public ActionResult Create()
        {

            ViewBag.Subcat_fid = new SelectList(db.SubCategories, "Subcat_id", "subcat_name");
            ViewBag.teacher_fid = new SelectList(db.Teachers, "Teacher_id", "teacher_name");
            return View();
        }

        // POST: Cours/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Cours cours, HttpPostedFileBase pic)
        {
            Teacher t = (Teacher)Session["tch"];
            cours.teacher_fid = t.Teacher_id;
            string fullpath = Server.MapPath("~/content/pics/" + pic.FileName);
            pic.SaveAs(fullpath);
            cours.course_pic = "~/content/pics/" + pic.FileName;
            cours.approve = false;
            db.Courses.Add(cours);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Cours/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cours cours = db.Courses.Find(id);
            if (cours == null)
            {
                return HttpNotFound();
            }
            ViewBag.Subcat_fid = new SelectList(db.SubCategories, "Subcat_id", "subcat_name", cours.Subcat_fid);
            ViewBag.teacher_fid = new SelectList(db.Teachers, "Teacher_id", "teacher_name", cours.teacher_fid);
            return View(cours);
        }

        // POST: Cours/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Cours cours, HttpPostedFileBase pic)
        {
            Teacher t = (Teacher)Session["tch"];
            cours.teacher_fid = t.Teacher_id;
            if (pic != null)
            {
                string fullpath = Server.MapPath("~/content/pics/" + pic.FileName);
                pic.SaveAs(fullpath);
                cours.course_pic = "~/content/pics/" + pic.FileName;
            }
            db.Entry(cours).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
            //ViewBag.Subcat_fid = new SelectList(db.SubCategories, "Subcat_id", "subcat_name", cours.Subcat_fid);
            //ViewBag.teacher_fid = new SelectList(db.Teachers, "Teacher_id", "teacher_name", cours.teacher_fid);
            //return View(cours);
        }

        // GET: Cours/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cours cours = db.Courses.Find(id);
            if (cours == null)
            {
                return HttpNotFound();
            }
            return View(cours);
        }

        // POST: Cours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            Cours cours = db.Courses.Find(id);
            db.Courses.Remove(cours);
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
