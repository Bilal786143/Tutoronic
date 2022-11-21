using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net.Mail;
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
            var result = await _db.Students.AnyAsync(s => s.student_email == student.student_email);
            if (!result)
            {
                _db.Students.Add(student);
                await _db.SaveChangesAsync();
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
        public async Task<Student> LoginStudent(Student student)
        {
            var studentEntity = await _db.Students.FirstOrDefaultAsync(s => s.student_email == student.student_email && s.student_password == student.student_password);
            return studentEntity;
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

        public bool SendMail<T>(T userDetail)
        {
            var type = userDetail.GetType();
            dynamic usdetail = userDetail;
            if (type.Name == "Student")
            {
                var from = ConfigurationManager.AppSettings["Email"];
                string to = usdetail.student_email;
                MailMessage mail = new MailMessage(from, to);
                mail.Subject = "Successfully Registered - Tutoronic";
                mail.Body = "<h2>Tutoronic</h2><br/>Hi Mr/Mrs <b>" + usdetail.student_name + "</b> <br/>You have Successfully registered on Tutoronic.<br/> Keep Learnig and Enhanced your skills";
                mail.IsBodyHtml = true;
                SmtpClient server = new SmtpClient("smtp.gmail.com", 587);
                server.Credentials = new System.Net.NetworkCredential(from, ConfigurationManager.AppSettings["Password"]);
                server.EnableSsl = true;
                server.Send(mail);
                return true;
            }
            else if (type.Name == "Teacher")
            {
                var from = ConfigurationManager.AppSettings["Email"];
                string to = usdetail.student_email;
                MailMessage mail = new MailMessage(from, to);
                mail.Subject = "Successfully Registered - Tutoronic";
                mail.Body = "<h2>Tutoronic</h2><br/>Hi Mr/Mrs <b>" + usdetail.student_name + "</b> <br/>You have Successfully registered on Tutoronic.<br/> Keep Learnig and Enhanced your skills";
                mail.IsBodyHtml = true;
                SmtpClient server = new SmtpClient("smtp.gmail.com", 587);
                server.Credentials = new System.Net.NetworkCredential(from, ConfigurationManager.AppSettings["Password"]);
                server.EnableSsl = true;
                server.Send(mail);
                return true;
            }
            else if (type.Name == "Admin")
            {
                var from = ConfigurationManager.AppSettings["Email"];
                string to = usdetail.student_email;
                MailMessage mail = new MailMessage(from, to);
                mail.Subject = "Successfully Registered - Tutoronic";
                mail.Body = "<h2>Tutoronic</h2><br/>Hi Mr/Mrs <b>" + usdetail.student_name + "</b> <br/>You have Successfully registered on Tutoronic.<br/> Keep Learnig and Enhanced your skills";
                mail.IsBodyHtml = true;
                SmtpClient server = new SmtpClient("smtp.gmail.com", 587);
                server.Credentials = new System.Net.NetworkCredential(from, ConfigurationManager.AppSettings["Password"]);
                server.EnableSsl = true;
                server.Send(mail);
                return true;
            }
            return false;
        }
    }
}