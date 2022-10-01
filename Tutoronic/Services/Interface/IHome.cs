using Tutoronic.Models;

namespace Tutoronic.Services.Interface
{
    public interface IHome
    {
        bool BlogDetails(Course_video_comment cvc, Student s);
        bool SendMail<T>(T userDetail);
    }
}
