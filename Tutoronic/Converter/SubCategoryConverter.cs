using Tutoronic.Models;
using Tutoronic.Request;
using Tutoronic.Services;
using Tutoronic.ViewModels;

namespace Tutoronic.Converter
{
    public class SubCategoryConverter : BaseService
    {
        public SubCategoryVM ConvertSubCategoryEntityToResponseModel(SubCategory subCategoryEntity)
        {
            return new SubCategoryVM()
            {
                Id = subCategoryEntity.Subcat_id,
                SubCategoryName = subCategoryEntity.subcat_name,
                SubCategoryImage = subCategoryEntity.subcat_pic,
                CategoryName = subCategoryEntity.Category.cat_name,
                CategoryId = subCategoryEntity.cat_fid,
            };
        }

        public void UpdateSubCategorySuccessfully(SubCategory subCategoryEntity, SubCategoryVM request)
        {
            subCategoryEntity.subcat_name = request.SubCategoryName;
            //subCategoryEntity.cat_fid = request.;
            //if (request.CategoryImage != null)
            //{
            //    DeleteExistingImage(subCategoryEntity.subcat_pic);
            //    subCategoryEntity.subcat_pic = request.SubCategoryImage;
            //}
        }

        public SubCategory RequestToSubcategoryModel(CreateNewSubCategoryRequest request, string subCategoryImagePath)
        {
            return new SubCategory
            {
                subcat_name = request.SubCategoryName,
                cat_fid = request.CategoryId,
                subcat_pic = subCategoryImagePath,
            };
        }
    }
}