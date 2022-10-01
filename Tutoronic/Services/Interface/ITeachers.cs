using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutoronic.Models;

namespace Tutoronic.Services.Interface
{
    public interface ITeachers
    {
        Task<List<Teacher>> GetAllTeachers();
        Task<Teacher> GetTeacherById(int id);
        Task<Teacher> CreateTeacher(Teacher teacher);
        Task<bool> UpdateTeacher(Teacher teacher);
    }
}
