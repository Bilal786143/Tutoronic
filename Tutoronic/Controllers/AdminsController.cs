using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Tutoronic.Models;

namespace Tutoronic.Controllers
{
    public class AdminsController : ServerMapPathController
    {
        private Model1 db = new Model1();
        public ActionResult alladmins()
        {
            return View(db.Admins.ToList());
        }
        public ActionResult reports()
        {
            return View(db.Admins.ToList());
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Admin a, HttpPostedFileBase pic)
        {
            var imagePath = ServerMapPath(pic);
            if (imagePath == ViewBag.message)
            {
                TempData["errormsg"] = "<script> alert('Image Format is not supported')</script>";
                return View("Create");
            }
            a.admin_pic = imagePath;
            db.Admins.Add(a);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Admin admin, HttpPostedFileBase pic)
        {
            if (pic != null)
            {
                var imagePath = ServerMapPath(pic);
                if (imagePath == ViewBag.message)
                {
                    TempData["errormsg"] = "<script> alert('Image Format is not supported')</script>";
                    return RedirectToAction("Edit");
                }
                admin.admin_pic = imagePath;
            }
            db.Entry(admin).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Admin admin = db.Admins.Find(id);
            db.Admins.Remove(admin);
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
        public ActionResult index()
        {
            return View();
        }
        public ActionResult addlisting()
        {
            return View();
        }
        public ActionResult basiccalendar()
        {
            return View();
        }
        public ActionResult bookmark()
        {
            return View();
        }
        public ActionResult courses()
        {
            return View();
        }
        public ActionResult coursedetail(int id)
        {
            TempData["coursesid"] = id;
            return View();
        }
        public ActionResult logout()
        {
            Session["adm"] = null;
            return RedirectToAction("index", "Home");
        }
        public ActionResult approve(int id)
        {
            var courseEntity = db.Courses.FirstOrDefault(x => x.Course_id == id);
            courseEntity.approve = true;
            db.Entry(courseEntity).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("courses");
        }
        public ActionResult listviewcalendar()
        {
            return View();
        }
        public ActionResult mailbox()
        {
            return View();
        }
        public ActionResult mailboxcompose()
        {
            return View();
        }
        public ActionResult mailboxread()
        {
            return View();
        }
        public ActionResult review()
        {
            return View();
        }
        public ActionResult teacherprofile()
        {
            return View();
        }
        public ActionResult students()
        {
            return View();
        }
        public ActionResult userprofile()
        {
            return View();
        }
    }
}
