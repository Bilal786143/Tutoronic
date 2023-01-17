using Tutoronic.Models;
using Tutoronic.Request;
using Tutoronic.Response;
using Tutoronic.Services;
using Tutoronic.ViewModels;

namespace Tutoronic.Converter
{
    public class CourseConverter : BaseService
    {
        public CourseVM ConvertCourseEntityToViewModel(Cours courseEntity)
        {
            return new CourseVM()
            {
                CourseId = courseEntity.Course_id,
                CourseDescription = courseEntity.course_description,
                CourseName = courseEntity.course_name,
                CourseImage = courseEntity.course_pic,
                CoursePrice = courseEntity.course_price,
                SubCategoryName = courseEntity.SubCategory.subcat_name,
                TeacherName = courseEntity.Teacher.teacher_name,
                SubCategoryId = courseEntity.Subcat_fid
            };
        }

        public Cours RequestToCourseModel(CreateNewCourseRequest request, string courseImagePath, int? teacherId)
        {
            return new Cours()
            {
                course_name = request.CourseName,
                course_pic = courseImagePath,
                course_price = request.CoursePrice,
                course_description = request.CourseDescription,
                Subcat_fid = request.SubCategoryId,
                teacher_fid = teacherId,
                approve = false
            };
        }

        public void UpdateCourseSuccessfully(Cours courseEntity, GetCourseByIdResponse request)
        {
            courseEntity.Subcat_fid = request.Course.SubCategoryId;
            courseEntity.approve = false;
            courseEntity.course_price = request.Course.CoursePrice;
            courseEntity.course_description = request.Course.CourseDescription;
            courseEntity.course_name = request.Course.CourseName;
            if (request.Course.CourseImage != courseEntity.course_pic)
            {
                DeleteExistingImage(courseEntity.course_pic);
                courseEntity.course_pic = request.Course.CourseImage;
            }
        }
    }
}