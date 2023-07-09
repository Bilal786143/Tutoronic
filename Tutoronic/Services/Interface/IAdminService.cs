using System.Threading.Tasks;
using Tutoronic.Models;
using Tutoronic.Response;
using Tutoronic.ViewModels;

namespace Tutoronic.Services.Interface
{
    public interface IAdminService
    {
        Task<GetAllAdminsResponse> GetAllAdmins();
        Task<GetAdminByIdResponse> GetAdminById(int? id);
        Task<bool> CreateNewAdmin(Admin admin, string adminImagePath);
        Task<Admin> Login(Admin admin);
        Task<bool> UpdateAdmin(AdminVM admin);
        Task<bool> DeleteAdmin(int id);
        Task<bool> AdminApproveCourse(int id, bool isApprove);
    }
}
