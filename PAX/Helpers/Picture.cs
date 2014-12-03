using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace PAX.Helpers
{
    /**
     * Upload and resize pictures
     */
    static class Picture
    {
        public static string uploadFolder = "~/Content/Uploads/";

        public static void Delete(string fileName)
        {
            File.Delete(HostingEnvironment.MapPath(uploadFolder) + "/" + fileName);
        }

        public static void Upload(string srcPath, string newFileName)
        {
            var options = "maxwidth=1280&maxheight=1280&format=jpg";
            var destPath = HostingEnvironment.MapPath(Path.Combine(uploadFolder, newFileName)) + ".jpg";

            // Let the image builder add the correct extension based on the output file type
            var imageJob = new ImageResizer.ImageJob(srcPath, destPath, new ImageResizer.Instructions(options));
            imageJob.Build();
        }
    }
}