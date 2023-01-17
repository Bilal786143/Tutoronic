using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Tutoronic.Models;
using Tutoronic.Request;
using Tutoronic.Response;
using Tutoronic.Services.Interface;

namespace Tutoronic.Controllers
{
    public class CoursController : ServerMapPathController
    {
        private Model1 db = new Model1();
        private readonly ICourseService _courseService;

        public CoursController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public async Task<ActionResult> Index()
        {
            return View(await _courseService.GetAllCourses(((Teacher)Session["tch"]).Teacher_id));
        }
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var courseResponse = await _courseService.GetCourseById(id);
            if (courseResponse == null)
                return HttpNotFound();

            return View(courseResponse);
        }
        public ActionResult Create()
        {
            ViewBag.SubCategoryId = _courseService.DropDownList("Subcat_id", "subcat_name", null);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateNewCourseRequest request, HttpPostedFileBase pic)
        {
            var courseImagePath = ServerMapPath(pic);
            if (courseImagePath == ViewBag.message)
            {
                TempData["errormsg"] = "<script> alert('Image Format is not supported')</script>";
                return View("create");
            }
            await _courseService.CreateNewCourse(request, courseImagePath, ((Teacher)Session["tch"]).Teacher_id);
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var courseResponse = await _courseService.GetCourseById(id);
            if (courseResponse == null)
                return HttpNotFound();

            ViewBag.SubCategoryId = _courseService.DropDownList("Subcat_id", "subcat_name", courseResponse.Course.SubCategoryId);
            return View(courseResponse);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(GetCourseByIdResponse request, HttpPostedFileBase pic)
        {
            if (pic != null)
            {
                var imagePath = ServerMapPath(pic);
                if (imagePath == ViewBag.message)
                {
                    TempData["errormsg"] = "<script> alert('Image Format is not supported')</script>";
                    return RedirectToAction("Edit");
                }
                request.Course.CourseImage = imagePath;
            }
            await _courseService.UpdateCourse(request, ((Teacher)Session["tch"]).Teacher_id);
            return RedirectToAction("Index");
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
