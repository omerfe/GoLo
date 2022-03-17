using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Managers
{
    public static class FileManager
    {
        public static string GetUniqueNameAndSavePhotoToDisk(this IFormFile pictureFile, IWebHostEnvironment webHostEnvironment, string folderToSaveTo)
        {
            string uniqueFileName = default;

            if (pictureFile is not null)
            {
                uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(pictureFile.FileName);

                string savingPath = Path.Combine(webHostEnvironment.WebRootPath, "img", folderToSaveTo, uniqueFileName);
                using (var fileStream = new FileStream(savingPath, FileMode.Create))
                {
                    pictureFile.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        public static void RemoveImageFromDisk(string imageName, IWebHostEnvironment webHostEnvironment, string folderToDeleteFrom)
        {
            if (!string.IsNullOrEmpty(imageName))
            {
                string filePath = Path.Combine(webHostEnvironment.WebRootPath, "img", folderToDeleteFrom, imageName);
                System.IO.File.Delete(filePath);
            }
        }
    }
}
