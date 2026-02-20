using System.Collections.Generic;
using System.Threading.Tasks;
using Tutoronic.Models;

namespace Tutoronic.Services.Interface
{
    public interface ITeachers
    {
        Task<List<Teacher>> GetAllTeachers();
        Task<Teacher> GetTeacherById(int? id);
        Task<bool> CreateTeacher(Teacher teacher, string teacherImagePath);
        Task<bool> UpdateTeacher(Teacher teacher);
        Task<Teacher> Login(Teacher teacher);
        Task<bool> DeleteTeacher(int id);
        bool DeleteTeacherImage(string requestImageToBeDeleted);
    }
}
