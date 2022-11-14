using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Tutoronic.Models;

namespace Tutoronic.Controllers
{
    public class CoursController : ServerMapPathController
    {
        private Model1 db = new Model1();
        public ActionResult Index()
        {
            var courses = db.Courses.Include(c => c.SubCategory).Include(c => c.Teacher);
            return View(courses.ToList());
        }
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
        public ActionResult Create()
        {
            ViewBag.Subcat_fid = new SelectList(db.SubCategories, "Subcat_id", "subcat_name");
            ViewBag.teacher_fid = new SelectList(db.Teachers, "Teacher_id", "teacher_name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Cours cours, HttpPostedFileBase pic)
        {
            var imagePath = ServerMapPath(pic);
            if (imagePath == ViewBag.message)
            {
                TempData["errormsg"] = "<script> alert('Image Format is not supported')</script>";
                return View("create");
            }
            Teacher teacher = (Teacher)Session["tch"];
            cours.teacher_fid = teacher.Teacher_id;
            cours.course_pic = imagePath;
            cours.approve = false;
            db.Courses.Add(cours);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Cours cours, HttpPostedFileBase pic)
        {
            if (pic != null)
            {
                var imagePath = ServerMapPath(pic);
                if (imagePath == ViewBag.message)
                {
                    TempData["errormsg"] = "<script> alert('Image Format is not supported')</script>";
                    return RedirectToAction("Edit");
                }
                cours.course_pic = imagePath;
            }
            Teacher teacher = (Teacher)Session["tch"];
            cours.teacher_fid = teacher.Teacher_id;
            db.Entry(cours).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
            //ViewBag.Subcat_fid = new SelectList(db.SubCategories, "Subcat_id", "subcat_name", cours.Subcat_fid);
            //ViewBag.teacher_fid = new SelectList(db.Teachers, "Teacher_id", "teacher_name", cours.teacher_fid);
            //return View(cours);
        }
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
