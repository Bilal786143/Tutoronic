using System.Data;
using System.Linq;
using System.Web.Mvc;
using Tutoronic.Models;

namespace Tutoronic.Controllers
{
    public class SearchController : Controller
    {
        Model1 db = new Model1();
        public ActionResult index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult index(string c)
        {
            return View(db.Courses.Where(x=>x.course_name.Contains(c) || x.course_description.Contains(c)).ToList());
        }
    }
}
