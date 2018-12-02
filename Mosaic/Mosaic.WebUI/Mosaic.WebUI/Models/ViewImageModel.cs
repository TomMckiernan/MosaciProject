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
        public string Error { get; set; }

        public ViewImageModel(string id, string copyPath = "~\\images\\tiles\\")
        {            
            CopyPath = copyPath + id + "\\";
        }

        public void CopyImage(string fileToCopy)
        {
            if (File.Exists(fileToCopy))
            {
                if (!Directory.Exists(CopyPath))
                {
                    Directory.CreateDirectory(CopyPath);
                }
                var newfile = CopyPath + Path.GetFileName(fileToCopy);
                // Check if file with that id exist
                // else create one
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
            var di = new DirectoryInfo(CopyPath);
            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
        }
    }
}
