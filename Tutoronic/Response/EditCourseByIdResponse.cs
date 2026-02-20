using Tutoronic.Models;

namespace Tutoronic.Response
{
    public class EditCourseByIdResponse
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
        public string CourseImage { get; set; }
        public decimal CoursePrice { get; set; }
        public string SubCategoryName { get; set; }
        public string TeacherName { get; set; }
        public int SubCategoryId { get; set; }
        public virtual SubCategory SubCategory { get; set; }
    }
}