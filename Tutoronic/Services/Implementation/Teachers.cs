using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using Tutoronic.Models;
using Tutoronic.Services.Interface;

namespace Tutoronic.Services.Implementation
{
    public class Teachers: ITeachers
    {
        private readonly Model1 _db;
        public Teachers(Model1 db)
        {
            _db = db;
        }
        public async Task<List<Teacher>> GetAllTeachers()
        {
            return await _db.Teachers.Include(t => t.Comment_reply).ToListAsync();
        }
        public async Task<Teacher> GetTeacherById(int id)
        {
            return await _db.Teachers.FindAsync(id);
        }
        public async Task<Teacher> CreateTeacher(Teacher teacher)
        {
            var newTeacher = new Teacher();
            var result = await _db.Teachers.AnyAsync(x => x.teacher_email == teacher.teacher_email);
            if (!result)
            {
                _db.Teachers.Add(teacher);
                await _db.SaveChangesAsync();
                newTeacher = teacher;
            }
            return newTeacher;
        }
        public async Task<bool> UpdateTeacher(Teacher teacher)
        {
            _db.Entry(teacher).State = EntityState.Modified;
            await _db.SaveChangesAsync(); 
            return true;
        }
    }
}