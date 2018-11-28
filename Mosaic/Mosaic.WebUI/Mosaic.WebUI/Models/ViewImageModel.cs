using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Mosaic.WebUI.Models
{
    public class ViewImageModel
    {
        private readonly string ROOT_IMAGE_FOLDER = "~\\images\\temp\\";
        public string FilePath { get; set; }
        public string Error { get; set; }

        public void CopyImage(string fileToCopy)
        {
            if (File.Exists(fileToCopy))
            {
                var newfile = ROOT_IMAGE_FOLDER + Path.GetFileName(fileToCopy);
                File.Copy(fileToCopy, newfile);
                FilePath = newfile;
            }
            else
            {
                Error = "File to copy does not exist";
            }
        }

        // Deletes the current image that has been copied previous
        public void DeleteImage()
        {
            var di = new DirectoryInfo(ROOT_IMAGE_FOLDER);
            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
        }
    }
}
