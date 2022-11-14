using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Tutoronic.Models;

namespace Tutoronic.Controllers
{
    public class SubCategoriesController : ServerMapPathController
    {
        private Model1 db = new Model1();
        public ActionResult Index()
        {
            var subCategories = db.SubCategories.Include(s => s.Category);
            return View(subCategories.ToList());
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategory subCategory = db.SubCategories.Find(id);
            if (subCategory == null)
            {
                return HttpNotFound();
            }
            return View(subCategory);
        }
        public ActionResult Create()
        {
            ViewBag.cat_fid = new SelectList(db.Categories, "Category_id", "cat_name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SubCategory subCategory, HttpPostedFileBase pic)
        {
            var imagePath = ServerMapPath(pic);
            if (imagePath == ViewBag.message)
            {
                TempData["errormsg"] = "<script> alert('Image Format is not supported')</script>";
                return View("Create");
            }
            subCategory.subcat_pic = imagePath;
            db.SubCategories.Add(subCategory);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            SubCategory subCategory = db.SubCategories.Find(id);
            if (subCategory == null)
                return HttpNotFound();

            ViewBag.cat_fid = new SelectList(db.Categories, "Category_id", "cat_name", subCategory.cat_fid);
            return View(subCategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Subcat_id,subcat_name,subcat_pic,cat_fid")] SubCategory subCategory, HttpPostedFileBase pic)
        {
            if (pic != null)
            {
                var imagePath = ServerMapPath(pic);
                if (imagePath == ViewBag.message)
                {
                    TempData["errormsg"] = "<script> alert('Image Format is not supported')</script>";
                    return View("Edit");
                }
                subCategory.subcat_pic = imagePath;
            }
            db.Entry(subCategory).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategory subCategory = db.SubCategories.Find(id);
            if (subCategory == null)
            {
                return HttpNotFound();
            }
            return View(subCategory);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SubCategory subCategory = db.SubCategories.Find(id);
            db.SubCategories.Remove(subCategory);
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
