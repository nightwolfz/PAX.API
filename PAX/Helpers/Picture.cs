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
        public static string uploadFolder = "~/Uploads/";

        public static void Delete(string fileName)
        {
            File.Delete(HostingEnvironment.MapPath(uploadFolder) + "/" + fileName);
        }

        public static void Upload(string fileName, string newFileName)
        {
            var filePath = HostingEnvironment.MapPath(uploadFolder) + "/" + fileName;

            // Let the image builder add the correct extension based on the output file type
            var imageJob = new ImageResizer.ImageJob(filePath, Path.Combine(uploadFolder, newFileName) + "_x.<ext>",
                           new ImageResizer.Instructions("maxwidth=1280&maxheight=1280&format=jpg"));
            imageJob.Build();
        }
    }
}