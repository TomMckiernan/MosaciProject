using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Mosaic.WebUI.Models
{
    public class ViewImageModel
    {
        public string CopyPath { get; set; }
        public string FilePath { get; set; }
        public string ImagePath { get; set; }
        public string Error { get; set; }

        public ViewImageModel(string copyPath = "wwwroot\\images\\project\\")
        {            
            CopyPath = Path.GetFullPath(copyPath);
            ImagePath = "\\images\\project\\";
        }

        public void CopyImage(string fileToCopy)
        {
            if (File.Exists(fileToCopy))
            {
                if (Directory.Exists(CopyPath))
                {
                    var newfile = CopyPath + Path.GetFileName(fileToCopy);
                    File.Copy(fileToCopy, newfile);
                    FilePath = newfile;
                    ImagePath = ImagePath + Path.GetFileName(fileToCopy);
                }
                else
                {
                    Error = "Directory to copy to does not exist";
                }

            }
            else
            {
                Error = "File to copy does not exist";
            }
            
        }

        // Deletes the current image that has been copied previous
        public void DeleteImage()
        {
            var di = new DirectoryInfo(CopyPath);
            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
        }
    }
}
