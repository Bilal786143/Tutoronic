using System.Threading.Tasks;
using System.Web.Mvc;
using Tutoronic.Request;
using Tutoronic.Response;

namespace Tutoronic.Services.Interface
{
    public interface ISubCategoryService
    {
        Task<GetAllSubCategoryResponse> GetAllSubCategories();
        Task<GetSubCategoryByIdResponse> GetSubCategoryById(int? id);
        Task<EditSubCategoryByIdResponse> EditSubCategoryResponseById(int? id);
        Task<bool> CreateNewSubCategory(CreateNewSubCategoryRequest request, string subCategoryImagePath);
        Task<bool> UpdateSubCategory(EditSubCategoryByIdResponse request);
        Task<bool> DeleteSubCategory(int id);
        SelectList DropDownCategory();
        SelectList DropDownCategory(int id);
    }
}