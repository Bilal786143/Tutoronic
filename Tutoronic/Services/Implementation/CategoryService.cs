using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Tutoronic.Converter;
using Tutoronic.Models;
using Tutoronic.Response;
using Tutoronic.Services.Interface;
using Tutoronic.ViewModels;

namespace Tutoronic.Services.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly Model1 _dbContext;
        private readonly CategoryConverter _categoryConverter;
        public CategoryService(Model1 dbcontext)
        {
            _dbContext = dbcontext;
            _categoryConverter = new CategoryConverter();
        }

        public async Task<GetAllCategoryResponse> GetAllCategories()
        {
            var categoryList = new GetAllCategoryResponse();
            var categoryEntities = await _dbContext.Categories.ToListAsync();
            categoryList.Categories = _categoryConverter.ConvertCategoryEntitiesToModel(categoryEntities);
            return categoryList;
        }

        public async Task<GetCategoryByIdResponse> GetCategoryById(int? id)
        {
            var cateoryResponse = new GetCategoryByIdResponse();
            var categoryEntity = await GetCategoryEntityById(id);
            if (categoryEntity != null)
                cateoryResponse.Category = _categoryConverter.ConvertCategoryEntityToModel(categoryEntity);

            return cateoryResponse;
        }

        public async Task<bool> CreateNewCategory(Category category, string adminImagePath)
        {
            try
            {
                category.cat_pic = adminImagePath;
                _dbContext.Categories.Add(category);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateCategory(CategoryVM request)
        {
            try
            {
                var categoryEntity = await GetCategoryEntityById(request.CategoryId);
                if (categoryEntity == null)
                    return false;

                _categoryConverter.UpdateCategorySuccessfully(categoryEntity, request);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteCategory(int id)
        {
            var categoryEntity = await GetCategoryEntityById(id);
            if (categoryEntity == null)
                return false;

            var isCategoryExistsInTables = _dbContext.SubCategories.Where(x => x.cat_fid == id).FirstOrDefault();
            if (isCategoryExistsInTables != null)
                return false;
            _dbContext.Categories.Remove(categoryEntity);
            await _dbContext.SaveChangesAsync();
            _categoryConverter.DeleteExistingImage(categoryEntity.cat_pic);
            return true;
        }
        
        private async Task<Category> GetCategoryEntityById(int? id)
        {
            return await _dbContext.Categories.FirstOrDefaultAsync(c => c.Category_id == id);
        }
    }
}