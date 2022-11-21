using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Tutoronic.Models;
using Tutoronic.Services.Interface;
using Tutoronic.ViewModels;

namespace Tutoronic.Controllers
{
    public class AdminsController : ServerMapPathController
    {
        private readonly IAdminService _adminService;
        public AdminsController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        public async Task<ActionResult> alladmins()
        {
            return View(await _adminService.GetAllAdmins());
        }
        public async Task<ActionResult> reports()
        {
            return View(await _adminService.GetAllAdmins());
        }
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var admin = await _adminService.GetAdminById(id);
            if (admin == null)
                return HttpNotFound();

            return View(admin);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Admin admin, HttpPostedFileBase pic)
        {
            var imagePath = ServerMapPath(pic);
            if (imagePath == ViewBag.message)
            {
                TempData["errormsg"] = "<script> alert('Image Format is not supported')</script>";
                return View("Create");
            }
            await _adminService.CreateNewAdmin(admin, imagePath);
            return RedirectToAction("AllAdmins");
        }

        [HttpPost]
        public async Task<ActionResult> Register(Admin admin, HttpPostedFileBase pic)
        {
            Session.Remove("adm");
            var imagePath = ServerMapPath(pic);
            if (imagePath == ViewBag.message)
            {
                TempData["errormsg"] = "<script> alert('Image Format is not supported')</script>";
                return RedirectToAction("Register", "Home");
            }
            var isAdminExist = await _adminService.CreateNewAdmin(admin, imagePath);

            if (!isAdminExist)
            {
                TempData["errormsg"] = "<script> alert('This Email is already Registered. Please enter new Email.')</script>";
                return RedirectToAction("Register", "Home");
            }
            Session["adm"] = admin;
            //_home.SendMail(admin);
            return RedirectToAction("index", "Admins");
        }

        public async Task<ActionResult> Login(Admin admin)
        {
            Session.Remove("adm");
            var adminEntity = await _adminService.Login(admin);
            if (adminEntity == null)
            {
                ViewBag.message = "The Email you have entered is not registered yet. Please Register Your Account Here";
                return View("login", "Home");
            }
            Session["adm"] = adminEntity;
            return RedirectToAction("index", "Admins");
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var admin = await _adminService.GetAdminById(id);
            if (admin == null)
                return HttpNotFound();

            return View(admin);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(AdminVM admin, HttpPostedFileBase pic)
        {
            if (pic != null)
            {
                var imagePath = ServerMapPath(pic);
                if (imagePath == ViewBag.message)
                {
                    TempData["errormsg"] = "<script> alert('Image Format is not supported')</script>";
                    return RedirectToAction("Edit");
                }
                admin.AdminPic = imagePath;
            }
            await _adminService.UpdateAdmin(admin);
            return RedirectToAction("AllAdmins");
        }
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var admin = await _adminService.GetAdminById(id);
            if (admin == null)
                return HttpNotFound();

            return View(admin);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _adminService.DeleteAdmin(id);
            return RedirectToAction("AllAdmins");
        }
        public ActionResult index()
        {
            return View();
        }
        public ActionResult addlisting()
        {
            return View();
        }
        public ActionResult basiccalendar()
        {
            return View();
        }
        public ActionResult bookmark()
        {
            return View();
        }
        public ActionResult courses()
        {
            return View();
        }
        public ActionResult coursedetail(int id)
        {
            TempData["coursesid"] = id;
            return View();
        }
        public ActionResult Logout()
        {
            Session.Remove("adm");
            return RedirectToAction("index", "Home");
        }
        public async Task<ActionResult> Approve(int id)
        {
            await _adminService.AdminApproveCourse(id, true);
            return RedirectToAction("courses");
        }

        public async Task<ActionResult> DisApprove(int id)
        {
            await _adminService.AdminApproveCourse(id, false);
            return RedirectToAction("courses");
        }
        public ActionResult listviewcalendar()
        {
            return View();
        }
        public ActionResult mailbox()
        {
            return View();
        }
        public ActionResult mailboxcompose()
        {
            return View();
        }
        public ActionResult mailboxread()
        {
            return View();
        }
        public ActionResult review()
        {
            return View();
        }
        public ActionResult teacherprofile()
        {
            return View();
        }
        public ActionResult students()
        {
            return View();
        }
        public ActionResult userprofile()
        {
            return View();
        }
    }
}
