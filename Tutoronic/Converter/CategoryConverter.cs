using System.Collections.Generic;
using Tutoronic.Models;
using Tutoronic.Services;
using Tutoronic.ViewModels;

namespace Tutoronic.Converter
{
    public class CategoryConverter : BaseService
    {
        public List<CategoryVM> ConvertCategoryEntitiesToResponseModel(List<Category> categoryEntities)
        {
            var categoriesList = new List<CategoryVM>();
            foreach (var categoryEntity in categoryEntities)
            {
                categoriesList.Add(new CategoryVM()
                {
                    CategoryId = categoryEntity.Category_id,
                    CategoryName = categoryEntity.cat_name,
                    CategoryImage = categoryEntity.cat_pic
                });
            }
            return categoriesList;
        }

        public CategoryVM ConvertCategoryEntityToResponseModel(Category categoryEntity)
        {
            return new CategoryVM()
            {
                CategoryId = categoryEntity.Category_id,
                CategoryName = categoryEntity.cat_name,
                CategoryImage = categoryEntity.cat_pic
            };
        }

        public void UpdateCategorySuccessfully(Category categoryEntity, CategoryVM request)
        {
            categoryEntity.cat_name = request.CategoryName;
            if (request.CategoryImage != null)
            {
                DeleteExistingImage(categoryEntity.cat_pic);
                categoryEntity.cat_pic = request.CategoryImage;
            }
        }
    }
}
