using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Tutoronic.Models;
using Tutoronic.Services.Interface;

namespace Tutoronic.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudents _students;
        public StudentsController(IStudents students)
        {
            _students = students;
        }
        public async Task<ActionResult> Index()
        {
            return View(await _students.GetAllStudents());
        }

        public async Task<ActionResult> Details(int id)
        {
            var student = await _students.GetStudentById(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Student student)
        {
            var newStudent = await _students.CreateNewStudent(student);
            if (newStudent != null)
                return RedirectToAction("Index");
            return View(student);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var student = await _students.GetStudentById(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Student student, HttpPostedFileBase pic)
        {
            Student s = (Student)Session["studentloging"];
            if (pic == null)
            {
                student.student_pic = s.student_pic;
            }
            else
            {
                string fullpath = Server.MapPath("~/content/pics/" + pic.FileName);
                pic.SaveAs(fullpath);
                student.student_pic = "~/content/pics/" + pic.FileName;
            }
            await _students.UpdateStudent(student, s);
            Session["studentloging"] = student;
            return RedirectToAction("profile", "Home");
        }
        public async Task<ActionResult> Delete(int id)
        {
            var student = await _students.GetStudentById(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _students.DeleteStudent(id);
            return RedirectToAction("Index");
        }
    }
}
