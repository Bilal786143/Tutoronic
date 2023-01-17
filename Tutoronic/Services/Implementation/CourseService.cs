using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Tutoronic.Converter;
using Tutoronic.Models;
using Tutoronic.Request;
using Tutoronic.Response;
using Tutoronic.Services.Interface;
using Tutoronic.ViewModels;

namespace Tutoronic.Services.Implementation
{
    public class CourseService : ICourseService
    {
        private readonly Model1 _dbContext;
        private readonly CourseConverter _courseConverter;

        public CourseService(Model1 dbContext)
        {
            _dbContext = dbContext;
            _courseConverter = new CourseConverter();

        }

        public async Task<GetAllCourseResponse> GetAllCourses(int teacherId)
        {
            var response = new GetAllCourseResponse();
            var courseEntities = await (from course in _dbContext.Courses.Where(x => x.Teacher.Teacher_id == teacherId)

                                        select new CourseVM
                                        {
                                            CourseDescription = course.course_description,
                                            CourseId = course.Course_id,
                                            CourseImage = course.course_pic,
                                            CourseName = course.course_name,
                                            CoursePrice = course.course_price,
                                            TeacherName = course.Teacher.teacher_name,
                                            SubCategoryName = course.SubCategory.subcat_name
                                        }).OrderByDescending(x => x.CourseId).ToListAsync();
            response.Courses = courseEntities;
            return response;
        }

        public async Task<GetCourseByIdResponse> GetCourseById(int? courseId)
        {
            var response = new GetCourseByIdResponse();
            var courseEntity = await GetCourseEntityById(courseId);
            response.Course = _courseConverter.ConvertCourseEntityToViewModel(courseEntity);
            return response;
        }

        public async Task<bool> CreateNewCourse(CreateNewCourseRequest request, string courseImagePath, int? teacherId)
        {
            try
            {
                var course = _courseConverter.RequestToCourseModel(request, courseImagePath, teacherId);
                _dbContext.Courses.Add(course);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateCourse(GetCourseByIdResponse request, int? teacherId)
        {
            try
            {
                var courseEntity = await GetCourseEntityById(request.Course.CourseId);
                if (courseEntity == null ||(courseEntity.teacher_fid!=teacherId))
                    return false;

                _courseConverter.UpdateCourseSuccessfully(courseEntity, request);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private async Task<Cours> GetCourseEntityById(int? courseId)
        {
            return await _dbContext.Courses.FirstOrDefaultAsync(c => c.Course_id == courseId);
        }

        public SelectList DropDownList(string id, string name, int? primaryId)
        {
            var test = new SelectList(_dbContext.SubCategories, "Subcat_id", "subcat_name", primaryId);
            return test;
        }

    }
}