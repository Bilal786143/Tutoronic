using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Tutoronic.Converter;
using Tutoronic.Models;
using Tutoronic.Request;
using Tutoronic.Response;
using Tutoronic.Services.Interface;
using Tutoronic.ViewModels;

namespace Tutoronic.Services.Implementation
{
    public class SubCategoryService : ISubCategoryService
    {
        private readonly Model1 _dbContext;
        private readonly SubCategoryConverter _subCategoryConverter;

        public SubCategoryService(Model1 dbContext)
        {
            _dbContext = dbContext;
            _subCategoryConverter = new SubCategoryConverter();
        }

        public async Task<GetAllSubCategoryResponse> GetAllSubCategories()
        {
            var subCategoyResponse = new GetAllSubCategoryResponse();
            var subCategoryList = await (from subCategory in _dbContext.SubCategories
                                         join category in _dbContext.Categories
                                         on subCategory.cat_fid equals category.Category_id
                                         select new SubCategoryVM
                                         {
                                             Id = subCategory.Subcat_id,
                                             SubCategoryName = subCategory.subcat_name,
                                             SubCategoryImage = subCategory.subcat_pic,
                                             CategoryName = category.cat_name,
                                         }).ToListAsync();
            subCategoyResponse.SubCategories = subCategoryList;
            return subCategoyResponse;
        }

        public async Task<GetSubCategoryByIdResponse> GetSubCategoryById(int? id)
        {
            var subCateoryResponse = new GetSubCategoryByIdResponse();
            var subCategoryEntity = await GetSubCategoryEntityById(id);
            if (subCategoryEntity != null)
                subCateoryResponse.SubCategory = _subCategoryConverter.ConvertSubCategoryEntityToResponseModel(subCategoryEntity);

            return subCateoryResponse;
        }

        public async Task<EditSubCategoryByIdResponse> EditSubCategoryResponseById(int? id)
        {
            var editSubCateoryResponse = new EditSubCategoryByIdResponse();
            var subCategoryEntity = await GetSubCategoryEntityById(id);
            if (subCategoryEntity != null)
                editSubCateoryResponse = _subCategoryConverter.ConvertEditSubCategoryEntityToResponseModel(subCategoryEntity);

            return editSubCateoryResponse;
        }

        public async Task<bool> CreateNewSubCategory(CreateNewSubCategoryRequest request, string subCategoryImagePath)
        {
            try
            {
                var subCategory = _subCategoryConverter.RequestToSubcategoryModel(request, subCategoryImagePath);
                _dbContext.SubCategories.Add(subCategory);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateSubCategory(EditSubCategoryByIdResponse request)
        {
            try
            {
                var subCategoryEntity = await GetSubCategoryEntityById(request.Id);
                if (subCategoryEntity == null)
                    return false;

                _subCategoryConverter.UpdateSubCategorySuccessfully(subCategoryEntity, request);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteSubCategory(int id)
        {
            try
            {
                var subCateoryEntity = await GetSubCategoryEntityById(id);
                if (subCateoryEntity == null)
                    return false;
                var isSubCategoryExistsInCourse = _dbContext.Courses.Any(x => x.Subcat_fid == id);
                if (isSubCategoryExistsInCourse)
                    return false;

                _dbContext.SubCategories.Remove(subCateoryEntity);
                await _dbContext.SaveChangesAsync();
                _subCategoryConverter.DeleteExistingImage(subCateoryEntity.subcat_pic);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public SelectList DropDownCategory()
        {
            var cate = "Category_id";
            var catename = "cat_name";
            return new SelectList(_dbContext.Categories, cate, catename);
        }

        public SelectList DropDownCategory(int id)
        {
            return new SelectList(_dbContext.Categories, "Category_id", "cat_name", id);
        }

        private async Task<SubCategory> GetSubCategoryEntityById(int? id)
        {
            return await _dbContext.SubCategories.FirstOrDefaultAsync(c => c.Subcat_id == id);
        }
    }
}