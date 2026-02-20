using System.Web.Hosting;

namespace Tutoronic.Services
{
    public class BaseService
    {
        public bool DeleteExistingImage(string requestImageToBeDeleted)
        {
            if (requestImageToBeDeleted != null)
            {
                var imagePath = requestImageToBeDeleted;
                var path = imagePath.Remove(0, imagePath.IndexOf('/') + 1);
                var completeImagePath = HostingEnvironment.ApplicationPhysicalPath + path;
                System.IO.File.Delete(completeImagePath);
                return true;
            }
            return false;
        }
    }
}