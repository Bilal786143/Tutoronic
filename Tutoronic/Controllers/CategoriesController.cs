using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Tutoronic.Models;
using Tutoronic.Services.Interface;
using Tutoronic.ViewModels;

namespace Tutoronic.Controllers
{
    public class CategoriesController : ServerMapPathController
    {
        private Model1 db = new Model1();
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<ActionResult> Index()
        {
            return View(await _categoryService.GetAllCategories());
        }
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var categoryResponse = await _categoryService.GetCategoryById(id);
            if (categoryResponse == null)
                return HttpNotFound();

            return View(categoryResponse);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Category category, HttpPostedFileBase pic)
        {
            var imagePath = ServerMapPath(pic);
            if (imagePath == ViewBag.message)
            {
                TempData["errormsg"] = "<script> alert('Image Format is not supported')</script>";
                return View("create");
            }
            await _categoryService.CreateNewCategory(category, imagePath);
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var categoryResponse = await _categoryService.GetCategoryById(id);
            if (categoryResponse == null)
                return HttpNotFound();

            return View(categoryResponse);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CategoryVM category, HttpPostedFileBase pic)
        {
            if (pic != null)
            {
                var imagePath = ServerMapPath(pic);
                if (imagePath == ViewBag.message)
                {
                    TempData["errormsg"] = "<script> alert('Image Format is not supported')</script>";
                    return RedirectToAction("Edit");
                }
                category.CategoryImage = imagePath;
            }
            await _categoryService.UpdateCategory(category);
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var categoryResponse = await _categoryService.GetCategoryById(id);
            if (categoryResponse == null)
                return HttpNotFound();

            return View(categoryResponse);

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var isCategoryDelete = await _categoryService.DeleteCategory(id);
            if (!isCategoryDelete)
            {
                TempData["errormsg"] = "<script> alert('Category Not Deleted Because It Already In Use')</script>";
                return RedirectToAction("Delete");
            }
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
