using System.Threading.Tasks;
using Tutoronic.Models;
using Tutoronic.Response;
using Tutoronic.ViewModels;

namespace Tutoronic.Services.Interface
{
    public interface ICategoryService
    {
        Task<GetAllCategoryResponse> GetAllCategories();
        Task<GetCategoryByIdResponse> GetCategoryById(int? id);
        Task<bool> CreateNewCategory(Category category, string adminImagePath);
        Task<bool> UpdateCategory(CategoryVM request);
        Task<bool> DeleteCategory(int id);
    }
}