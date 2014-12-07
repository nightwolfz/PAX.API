using System.IO;
using System.Web.Hosting;
using ImageResizer;

namespace PAX.Helpers
{
    /**
     * Manipulate images
     */
    static class Picture
    {
        public static string uploadFolder = "~/Content/Uploads/";

        public static void Delete(string fileName)
        {
            File.Delete(fileName);
        }

        public static void Resize(string srcPath, string newFileName)
        {
            var options = "maxwidth=1280&maxheight=1280&format=jpg";
            var destPath = HostingEnvironment.MapPath(Path.Combine(uploadFolder, newFileName)) + ".jpg";

            // Let the image builder add the correct extension based on the output file type
            var imageJob = new ImageJob(srcPath, destPath, new Instructions(options));
            imageJob.Build();

            // Delete Original
            Delete(srcPath);
        }
    }
}