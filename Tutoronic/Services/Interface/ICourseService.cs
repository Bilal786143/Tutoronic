using System.Threading.Tasks;
using Tutoronic.Request;
using Tutoronic.Response;

namespace Tutoronic.Services.Interface
{
    public interface ICourseService : IBaseDropDownList
    {
        Task<GetAllCourseResponse> GetAllCourses(int teacherId);
        Task<GetCourseByIdResponse> GetCourseById(int? courseId);
        Task<EditCourseByIdResponse> EditCourseResponseById(int? id);
        Task<bool> CreateNewCourse(CreateNewCourseRequest request, string courseImagePath, int? teacherId);
        Task<bool> UpdateCourse(EditCourseByIdResponse request, int? teacherId);
        Task<bool> DeleteCourse(int? id);
    }
}
