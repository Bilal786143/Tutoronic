using Tutoronic.Models;

namespace Tutoronic.Converter
{
    public class TeacherConverter
    {
        public void UpdateTeacherSuccessfully(Teacher teacherEntity, Teacher request)
        {
            teacherEntity.teacher_name = request.teacher_name;
            teacherEntity.teacher_contact = request.teacher_contact;
            teacherEntity.teacher_password = request.teacher_password;
            teacherEntity.teacher_address = request.teacher_address;
            teacherEntity.teacher_intro = request.teacher_intro;
        }
    }
}