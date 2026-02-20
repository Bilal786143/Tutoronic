using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using Tutoronic.Converter;
using Tutoronic.Models;
using Tutoronic.Services.Interface;

namespace Tutoronic.Services.Implementation
{
    public class Teachers : BaseService, ITeachers
    {
        private readonly Model1 _dbContext;
        private readonly TeacherConverter _teacherConverter;
        public Teachers(Model1 db)
        {
            _dbContext = db;
            _teacherConverter = new TeacherConverter();
        }
        public async Task<List<Teacher>> GetAllTeachers()
        {
            return await _dbContext.Teachers.Include(t => t.Comment_reply).ToListAsync();
        }
        public async Task<Teacher> GetTeacherById(int? id)
        {
            return await _dbContext.Teachers.FirstOrDefaultAsync(t => t.Teacher_id == id);
        }
        public async Task<bool> CreateTeacher(Teacher teacher, string teacherImagePath)
        {
            var isTeacherExist = await _dbContext.Teachers.AnyAsync(t => t.teacher_email == teacher.teacher_email);
            if (!isTeacherExist)
            {
                teacher.teacher_pic = teacherImagePath;
                _dbContext.Teachers.Add(teacher);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> UpdateTeacher(Teacher teacher)
        {
            var teacherEntity = await GetTeacherEntityById(teacher.Teacher_id);
            if (teacherEntity == null)
                return false;

            _teacherConverter.UpdateTeacherSuccessfully(teacherEntity, teacher);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Teacher> Login(Teacher teacher)
        {
            return await _dbContext.Teachers.FirstOrDefaultAsync(t => t.teacher_email == teacher.teacher_email && t.teacher_password == teacher.teacher_password);
        }

        public async Task<bool> DeleteTeacher(int id)
        {
            var teacherEntity = await GetTeacherEntityById(id);
            if (teacherEntity == null)
                return false;

            _dbContext.Teachers.Remove(teacherEntity);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public bool DeleteTeacherImage(string requestImageToBeDeleted)
        {
            return DeleteExistingImage(requestImageToBeDeleted);
        }

        private async Task<Teacher> GetTeacherEntityById(int teacherId)
        {
            return await _dbContext.Teachers.FirstOrDefaultAsync(a => a.Teacher_id == teacherId);
        }
    }
}