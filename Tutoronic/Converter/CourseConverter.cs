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
        public EditCourseByIdResponse ConvertEditCourseEntityToResponseModel(Cours course)
        {
            return new EditCourseByIdResponse()
            {
                CourseId = course.Course_id,
                CourseName = course.course_name,
                CourseImage = course.course_pic,
                CourseDescription = course.course_description,
                CoursePrice = course.course_price,
                SubCategoryName = course.SubCategory.subcat_name,
                TeacherName = course.Teacher.teacher_name,
                SubCategoryId = course.Subcat_fid,
            };
        }

        public void UpdateCourseSuccessfully(Cours courseEntity, EditCourseByIdResponse request)
        {
            courseEntity.Subcat_fid = request.SubCategoryId;
            courseEntity.approve = false;
            courseEntity.course_price = request.CoursePrice;
            courseEntity.course_description = request.CourseDescription;
            courseEntity.course_name = request.CourseName;
            if (request.CourseImage != courseEntity.course_pic)
            {
                DeleteExistingImage(courseEntity.course_pic);
                courseEntity.course_pic = request.CourseImage;
            }
        }
    }
}