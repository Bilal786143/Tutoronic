using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Tutoronic.Models;

namespace Tutoronic.Controllers
{
    public class AdminsController : Controller
    {
        private Model1 db = new Model1();

        // GET: Admins
        public ActionResult alladmins()
        {
            return View(db.Admins.ToList());
        }
        public ActionResult reports()
        {
            return View(db.Admins.ToList());
        }

        // GET: Admins/Details/5
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

        // GET: Admins/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Admin a, HttpPostedFileBase pic)
        {
            string fullpath = Server.MapPath("~/content/pics/" + pic.FileName);
            pic.SaveAs(fullpath);
            a.admin_pic = "~/content/pics/" + pic.FileName;

            db.Admins.Add(a);
                db.SaveChanges();
                return RedirectToAction("Index");
            
        }

        // GET: Admins/Edit/5
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

        // POST: Admins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Admin admin,HttpPostedFileBase pic)
        {
            if (pic != null)
            {
                string fullpath = Server.MapPath("~/content/pics/" + pic.FileName);
                pic.SaveAs(fullpath);
                admin.admin_pic= "~/content/pics/" + pic.FileName;
            }
            db.Entry(admin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            
        }

        // GET: Admins/Delete/5
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

        // POST: Admins/Delete/5
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
            return RedirectToAction("index","Home");




            
        }


        public ActionResult approve(int id)
        {
            
            var item=db.Courses.Where(x => x.Course_id == id).FirstOrDefault();

            item.approve = true;

            db.Entry(item).State = EntityState.Modified;
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
