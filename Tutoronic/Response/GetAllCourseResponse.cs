using System.Collections.Generic;
using Tutoronic.ViewModels;

namespace Tutoronic.Response
{
    public class GetAllCourseResponse
    {
        public List<CourseVM> Courses{ get; set; }
    }
}