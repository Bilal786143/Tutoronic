using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Tutoronic.Models;
using Tutoronic.Services.Interface;

namespace Tutoronic.Controllers
{
    public class TeachersController : ServerMapPathController
    {
        private readonly ITeachers _teacherService;
        public TeachersController(ITeachers teacher)
        {
            _teacherService = teacher;
        }
        public async Task<ActionResult> Index()
        {
            return View(await _teacherService.GetAllTeachers());
        }
        public async Task<ActionResult> profileupdate(Teacher teacher)
        {
            Teacher t = (Teacher)Session["tch"];
            teacher.teacher_pic = t.teacher_pic;
            teacher.Teacher_id = t.Teacher_id;
            if (teacher.teacher_password == null)
            {
                teacher.teacher_password = t.teacher_password;
            }
            await _teacherService.UpdateTeacher(teacher);
            Session["tch"] = teacher;
            return RedirectToAction("teacherprofile", "teacher");
        }
        public ActionResult Details(int id)
        {
            var teacher = _teacherService.GetTeacherById(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }
        //public ActionResult Create()
        //{
        //    ViewBag.Teacher_id = new SelectList(db.Comment_reply, "Comment_reply_id", "reply");
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create(Teacher teacher)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // await _teacher.CreateTeacher(teacher);
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.Teacher_id = new SelectList(db.Comment_reply, "Comment_reply_id", "reply", teacher.Teacher_id);
        //    return View(teacher);
        //}
        //public ActionResult Edit(int id)
        //{

        //    var teacher = _teacher.GetTeacherById(id);
        //    if (teacher == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.Teacher_id = new SelectList(db.Comment_reply, "Comment_reply_id", "reply", teacher.Id);
        //    return View(teacher);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(Teacher teacher)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _teacher.UpdateTeacher(teacher);
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.Teacher_id = new SelectList(db.Comment_reply, "Comment_reply_id", "reply", teacher.Teacher_id);
        //    return View(teacher);
        //}
        //public async Task<ActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        //    var teacher = await _teacher.GetTeacherById(id);
        //    if (teacher == null)
        //        return HttpNotFound();

        //    return View(teacher);
        //}

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(int id)
        //{
        //    var isTeacherDelete = await _teacher.DeleteTeacher(id);
        //    if (isTeacherDelete)
        //        return RedirectToAction("Index");
        //    return RedirectToAction("Index");
        //}

        [HttpPost]
        public async Task<ActionResult> TeacherRegister(Teacher teacher, HttpPostedFileBase pic)
        {
            Session.Remove("tch");
            var teacherImagePath = ServerMapPath(pic);
            if (teacherImagePath == ViewBag.message)
            {
                TempData["errormsg"] = "<script> alert('Image Format is not supported')</script>";
                return RedirectToAction("Register", "Home");
            }
            var isTeacherCreated = await _teacherService.CreateTeacher(teacher, teacherImagePath);
            if (!isTeacherCreated)
            {
                TempData["errormsg"] = "<script> alert('This Email is already Registered. Please enter new Email.')</script>";
                if (!teacherImagePath.Contains("blank-profile-picture-973460_640.png"))
                    _teacherService.DeleteTeacherImage(teacherImagePath);

                return RedirectToAction("Register", "Home");
            }
            Session["tch"] = teacher;
            return RedirectToAction("index", "teacher");
        }

        [HttpPost]
        public async Task<ActionResult> TeacherLogin(Teacher teacher)
        {
            var teacherEntity = await _teacherService.Login(teacher);
            if (teacherEntity == null)
            {
                TempData["errormsg"] = "<script> alert('The Email you have entered is not registered yet. Please Register Your Account Here')</script>";
                return RedirectToAction("login", "Home");
            }
            Session.Remove("tch");
            Session["tch"] = teacherEntity;
            return RedirectToAction("index", "Teacher");

        }

    }
}
