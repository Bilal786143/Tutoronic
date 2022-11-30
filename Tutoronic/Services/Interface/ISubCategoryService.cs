using System.Threading.Tasks;
using System.Web.Mvc;
using Tutoronic.Request;
using Tutoronic.Response;
using Tutoronic.ViewModels;

namespace Tutoronic.Services.Interface
{
    public interface ISubCategoryService
    {
        Task<GetAllSubCategoryResponse> GetAllSubCategories();
        Task<GetSubCategoryByIdResponse> GetSubCategoryById(int? id);
        Task<bool> CreateNewSubCategory(CreateNewSubCategoryRequest request, string subCategoryImagePath);
        Task<bool> UpdateSubCategory(SubCategoryVM request);
        Task<bool> DeleteSubCategory(int id);
        SelectList DropDownCategory();
        SelectList DropDownCategory(int id);
    }
}