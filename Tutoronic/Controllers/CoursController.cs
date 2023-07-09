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
            ViewBag.SubCategoryId = _courseService.DropDownList(null);
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

            var courseResponse = await _courseService.EditCourseResponseById(id);
            if (courseResponse == null)
                return HttpNotFound();

            ViewBag.SubCategoryId = _courseService.DropDownList(courseResponse.SubCategoryId);
            return View(courseResponse);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditCourseByIdResponse request, HttpPostedFileBase pic)
        {
            if (pic != null)
            {
                var courseImagePath = ServerMapPath(pic);
                if (courseImagePath == ViewBag.message)
                {
                    TempData["errormsg"] = "<script> alert('Image Format is not supported')</script>";
                    return RedirectToAction("Edit");
                }
                request.CourseImage = courseImagePath;
            }
            await _courseService.UpdateCourse(request, ((Teacher)Session["tch"]).Teacher_id);
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var courseResponse = await _courseService.GetCourseById(id);
            if (courseResponse == null)
                return HttpNotFound();

            return View(courseResponse);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var isCourseDelete = await _courseService.DeleteCourse(id);
            if (!isCourseDelete)
            {
                TempData["errormsg"] = "<script> alert('Category Not Deleted Because It Already In Use')</script>";
                return RedirectToAction("Delete");
            }
            return RedirectToAction("Index");
        }
    }
}