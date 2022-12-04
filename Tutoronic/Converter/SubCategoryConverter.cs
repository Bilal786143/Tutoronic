using Tutoronic.Models;
using Tutoronic.Request;
using Tutoronic.Response;
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

        public EditSubCategoryByIdResponse ConvertEditSubCategoryEntityToResponseModel(SubCategory subCategoryEntity)
        {
            return new EditSubCategoryByIdResponse()
            {
                Id = subCategoryEntity.Subcat_id,
                Name = subCategoryEntity.subcat_name,
                Image = subCategoryEntity.subcat_pic,
                CategoryId = subCategoryEntity.cat_fid,
            };
        }

        public void UpdateSubCategorySuccessfully(SubCategory subCategoryEntity, EditSubCategoryByIdResponse request)
        {
            subCategoryEntity.subcat_name = request.Name;
            subCategoryEntity.cat_fid = request.CategoryId;
            if (request.Image != subCategoryEntity.subcat_pic)
            {
                DeleteExistingImage(subCategoryEntity.subcat_pic);
                subCategoryEntity.subcat_pic = request.Image;
            }
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