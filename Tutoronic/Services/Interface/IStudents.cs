using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using Tutoronic.Models;

namespace Tutoronic.Services.Interface
{
    public interface IStudents
    {
        Task<List<Student>> GetAllStudents();
        Task<Student> CreateNewStudent(Student student);
        Task<Student> GetStudentById(int id);
        Task<bool> UpdateStudent(Student student, Student student1);
        Task<bool> DeleteStudent(int id);
    }
}
