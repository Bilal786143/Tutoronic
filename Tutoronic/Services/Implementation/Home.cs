using System.Configuration;
using System.Net.Mail;
using Tutoronic.Models;
using Tutoronic.Services.Interface;

namespace Tutoronic.Services.Implementation
{
    public class Home : IHome
    {
        private readonly Model1 _db;
        public Home(Model1 db)
        {
            _db = db;
        }
        public bool BlogDetails(Course_video_comment cvc, Student s)
        {
            cvc.std_fid = s.Student_id;
            _db.Course_video_comment.Add(cvc);
            _db.SaveChanges();
            return true;
        }

        public bool SendMail<T>(T userDetail)
        {
            var typer1 = userDetail.GetType();
            dynamic usdetail = userDetail;
            if (typer1.Name == "Student")
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
            else if (typer1.Name == "Teacher")
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
            else if (typer1.Name == "Admin")
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