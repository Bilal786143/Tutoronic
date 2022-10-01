using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using Tutoronic.Models;
using Tutoronic.Services.Interface;

namespace Tutoronic.Services.Implementation
{
    public class Students : IStudents
    {
        private readonly Model1 _db;
        public Students(Model1 db)
        {
            _db = db;
        }
        public async Task<Student> CreateNewStudent(Student student)
        {
            var newStudent = new Student();
            var result = await _db.Students.AnyAsync(x => x.student_email == student.student_email);
            if (!result)
            {
                _db.Students.Add(student);
                _db.SaveChanges();
                newStudent = student;
            }
            return newStudent;
        }
        public async Task<List<Student>> GetAllStudents()
        {
            return await _db.Students.ToListAsync();
        }
        public async Task<Student> GetStudentById(int id)
        {
            return await _db.Students.FindAsync(id);
        }
        public async Task<bool> UpdateStudent(Student student, Student student1)
        {
            student.Student_id = student1.Student_id;
            student.student_password = student1.student_password;
            _db.Entry(student).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteStudent(int id)
        {
            var student = await GetStudentById(id);
            _db.Students.Remove(student);
            _db.SaveChanges();
            return true;
        }
    }
}