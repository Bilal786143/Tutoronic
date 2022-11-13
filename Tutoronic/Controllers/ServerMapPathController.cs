using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Tutoronic.Models;

namespace Tutoronic.Controllers
{
    public class ServerMapPathController : Controller
    {
        // GET: ServerMapPath
        public string ServerMapPath(HttpPostedFileBase pic)
        {
            var picPath = "";
            if (pic != null)
            {
                if (!IsImageFormatExist(pic.FileName))
                {
                    ViewBag.message = "Image Format is not supported";
                    return ViewBag.message;
                }
                //for live website return only picPath
                picPath = "~/content/pics/" + Guid.NewGuid() + pic.FileName;
                string fullPath = Server.MapPath(picPath);
                pic.SaveAs(fullPath);
            }
            else
            {
                picPath = "~/content/pics/blank-profile-picture-973460_640.png";
            }
            return picPath;
        }

        public string ServerMapPathVideo(HttpPostedFileBase vid)
        {
            //for live website return only picPath
            var videoPath = "~/content/videos/" + Guid.NewGuid() + vid.FileName;
            var fullPath = Server.MapPath(videoPath);
            vid.SaveAs(fullPath);
            return videoPath;
        }   

        public bool IsImageFormatExist(string imageName)
        {
            var imageExtension = Path.GetExtension(imageName).ToUpper();
            var cleanformat = imageExtension.Replace(".", string.Empty);
            return Enum.TryParse(cleanformat, out ImageFormat imageFormat);
        }
        public bool IsVideoFormatExist(string videoName)
        {
            var imageExtension = Path.GetExtension(videoName).ToUpper();
            var cleanformat = imageExtension.Replace(".", string.Empty);
            return Enum.TryParse(cleanformat, out VideoFormat videoFormat);
        }

    }
}