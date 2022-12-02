using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Tutoronic.Request;
using Tutoronic.Response;
using Tutoronic.Services.Interface;

namespace Tutoronic.Controllers
{
    public class SubCategoriesController : ServerMapPathController
    {
        private readonly ISubCategoryService _subCategoryService;
        
        public SubCategoriesController(ISubCategoryService subCategoryService)
        {
            _subCategoryService = subCategoryService;
        }

        public async Task<ActionResult> Index()
        {
            return View(await _subCategoryService.GetAllSubCategories());
        }
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var subCategoryResponse = await _subCategoryService.GetSubCategoryById(id);
            if (subCategoryResponse == null)
                return HttpNotFound();

            return View(subCategoryResponse);
        }
        public ActionResult Create()
        {
            ViewBag.CategoryId = _subCategoryService.DropDownCategory();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateNewSubCategoryRequest request, HttpPostedFileBase pic)
        {
            var subCategoryImagePath = ServerMapPath(pic);
            if (subCategoryImagePath == ViewBag.message)
            {
                TempData["errormsg"] = "<script> alert('Image Format is not supported')</script>";
                return View("Create");
            }
            await _subCategoryService.CreateNewSubCategory(request, subCategoryImagePath);
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var subCategory = await _subCategoryService.GetSubCategoryById(id);
            if (subCategory == null)
                return HttpNotFound();

            ViewBag.CategoryId = _subCategoryService.DropDownCategory(subCategory.SubCategory.CategoryId);
            return View(subCategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(GetSubCategoryByIdResponse subCategory, HttpPostedFileBase pic)
        {
            if (pic != null)
            {
                var imagePath = ServerMapPath(pic);
                if (imagePath == ViewBag.message)
                {
                    TempData["errormsg"] = "<script> alert('Image Format is not supported')</script>";
                    return View("Edit");
                }
                //subCategory.subcat_pic = imagePath;
            }
            //db.Entry(subCategory).State = EntityState.Modified;
            //db.SaveChanges();
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var subCategory = await _subCategoryService.GetSubCategoryById(id);
            if (subCategory == null)
                return HttpNotFound();

            return View(subCategory);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var isSubCategoryDeleted = await _subCategoryService.DeleteSubCategory(id);
            if (!isSubCategoryDeleted)
            {
                TempData["errormsg"] = "<script> alert('SubCategory Not Deleted Because It Already In Use')</script>";
                return RedirectToAction("Delete");
            }
            return RedirectToAction("Index");
        }
    }
}
