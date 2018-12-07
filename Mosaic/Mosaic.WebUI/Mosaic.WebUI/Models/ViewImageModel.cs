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
        }

        public void CopyImage(string fileToCopy, string fileName = "")
        {
            //Need to check if already exists i.e if want to generate again
            if (File.Exists(fileToCopy))
            {
                if (Directory.Exists(CopyPath))
                {
                    var newFileName = String.IsNullOrEmpty(fileName) ? Path.GetFileName(fileToCopy) : fileName + Path.GetExtension(fileToCopy);
                    var newfile = CopyPath + newFileName;
                    // Copies files to location and will override if already exists
                    File.Copy(fileToCopy, newfile, true);
                    FilePath = newfile;
                    ImagePath = CopyPath.Substring(CopyPath.IndexOf("\\images\\"));
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

        // Deletes file by passing the relative path of a image
        public void DeleteImage(string fileToDelete)
        {
            var location = Path.GetFullPath(fileToDelete);
            File.Delete(location);
        }
    }
}
