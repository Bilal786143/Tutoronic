using System;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Web.Mvc;
using Tutoronic.Models;

namespace Tutoronic.Controllers
{
    public class CheckoutController : Controller
    {
        private Model1 db = new Model1();

        public ActionResult Gotopaypal()
        {
            int id = (int)Session["checkoutcourseid"];
            var c = db.Courses.Find(id);
            return Redirect("https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_xclick&business=ahsanchaudary10@gmail.com&currency=USD&amount=" + Convert.ToDouble(c.course_price.ToString()) + "&item_name=Tutoronic&return=https://localhost:44367/Checkout/OrderBook");
        }

        public ActionResult bookconfirm()
        {
            int id = (int)Session["checkoutcourseid"];
            var c = db.Courses.Find(id);
            return RedirectToAction("blogdetails", "Home", new { id = c.Course_id });
        }

        public ActionResult OrderBook()
        {
            int id = (int)Session["checkoutcourseid"];
            var c = db.Courses.Find(id);
            Order o = new Order();
            var s = (Student)Session["studentloging"];
            if (db.Orders.Where(x => x.Student_fid == s.Student_id && x.OrderDetails.FirstOrDefault().course_fid == c.Course_id).Any())
            {
                return RedirectToAction("blogdetails", "Home", new { id = c.Course_id });
            }
            o.Student_fid = s.Student_id;
            o.Customer_Email = s.student_email;
            o.Customer_Name = s.student_name;
            o.Customer_Phone = s.student_contact;
            o.Order_Date = System.DateTime.Now;
            o.Order_Type = "Sale";
            o.Payment_Method = "Paypal";
            o.Customer_Address = "Pakistan";
            db.Orders.Add(o);
            db.SaveChanges();
            sendmail(c, s);
            OrderDetail od = new OrderDetail();
            od.course_fid = id;
            od.order_fid = o.order_id;
            od.purchase_price = c.course_price;
            od.sale_price = c.course_price;
            db.OrderDetails.Add(od);
            db.SaveChanges();

            //send mail
            //string from = "Your Email Address Here";
            //string to = "Client/Student Email Here";
            //MailMessage mail = new MailMessage(from, to);
            //mail.Subject = "Successfully Registered - Tutoronic";
            //mail.Body = "<h2>Tutoronic</h2><br/>Hi Mr/Mrs <b>" + s.student_name + "</b> <br/>You have Successfully registered on Tutoronic.<br/> Keep Learnig and Enhanced your skills";
            //mail.IsBodyHtml = true;
            //SmtpClient server = new SmtpClient("Mail Server here", 587);
            //server.Credentials = new System.Net.NetworkCredential("Your Email Here", "Password");
            //server.EnableSsl = true;
            //server.Send(mail);
            return RedirectToAction("bookconfirm");

        }

        private void sendmail(Cours c, Student s)
        {
            string from = "Your Email Address Here";
            string to = "Client/Student Email Here";
            MailMessage mail = new MailMessage(from, to);
            mail.Subject = "Successfully Registered - Tutoronic";
            mail.Body = "<h2>Tutoronic</h2><br/>Hi Mr/Mrs <b>" + s.student_name + "</b> <br/>You have Successfully registered on Tutoronic.<br/> Keep Learnig and Enhanced your skills";
            mail.IsBodyHtml = true;
            SmtpClient server = new SmtpClient("Mail Server here", 587);
            server.Credentials = new System.Net.NetworkCredential("Your Email Here", "Password");
            server.EnableSsl = true;
            server.Send(mail);

            //string apiToken = "e8973c9a57ec01562b0d1abe28669d5f1288e96219";
            //string apiSecret = "abdullah1090";
            //string toNumber = "09887";
            //string Masking = "My Site";
            //string MessageText = "How are you?";

            //String api = "https://lifetimesms.com/json?api_token=" + apiToken + "&api_secret=" + apiSecret + "&to=" + toNumber + "&from=" + Masking + "&message=" + MessageText;
            //var httpWebRequest = (HttpWebRequest)WebRequest.Create(api);
            //var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

        }

        public ActionResult index(int id)
        {
            if (Session["studentloging"] != null)
            {
                var c = db.Courses.Find(id);
                var s = (Student)Session["studentloging"];
                if (db.Orders.Where(x => x.Student_fid == s.Student_id && x.OrderDetails.FirstOrDefault().course_fid == c.Course_id).Any())
                {
                    return RedirectToAction("blogdetails", "Home", new { id = c.Course_id });
                }
                else
                {
                    Session["checkoutcourseid"] = id;
                    return View();
                }
            }
            else
            {
                TempData["errormsg"] = " <script> alert(' Please Loging/Signup first. ') </script>";
                return RedirectToAction("register", "Home");
            }
        }
    }
}
