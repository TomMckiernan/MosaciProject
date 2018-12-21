using Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Mosaic.WebUI.Models
{
    public class DeleteProjectModel
    {
        public string MasterImageFolder { get; set; }
        public string MosaicImageFolder { get; set; }
        public string Error { get; set; }

        public DeleteProjectModel(string master = "wwwroot\\images\\master\\", string mosaic = "wwwroot\\images\\project\\")
        {
            MasterImageFolder = Path.GetFullPath(master);
            MosaicImageFolder = Path.GetFullPath(mosaic);
        }

        public void DeleteProject(IMakerClient client, string id)
        {
            if (!String.IsNullOrEmpty(id))
            {
                var project = client.ReadProject(id);
                if (project != null && String.IsNullOrEmpty(project.Error))
                {
                    if (!String.IsNullOrEmpty(project.Project.MasterLocation))
                    {
                        DeleteMasterImage(project.Project.MasterLocation);
                    }
                    if (!String.IsNullOrEmpty(project.Project.MosaicLocation))
                    {
                        DeleteMosaicImage(project.Project.MosaicLocation);
                    }
                    var response = client.DeleteProject(id);
                    if (!String.IsNullOrEmpty(response.Error))
                    {
                        Error = response.Error;
                    }
                }
                else
                {
                    Error = "Error in fetching project with id";
                }
            }
            else
            {
                Error = "Id cannot be null or empty";
            }
        }

        public void DeleteMasterImage(string masterLocation)
        {
            var masterFile = Path.GetFileName(masterLocation);
            var masterPath = MasterImageFolder + masterFile;
            if (File.Exists(masterPath))
            {
                File.Delete(masterPath);
            }
            else
            {
                Error = "Master Image does not exist";
            }
        }

        public void DeleteMosaicImage(string mosaicLocation)
        {
            var root = Directory.GetCurrentDirectory();
            var mosaicPath = Path.GetFullPath(root + mosaicLocation);
            if (File.Exists(mosaicPath))
            {
                File.Delete(mosaicPath);
            }
            else
            {
                Error = "Mosaic Image does not exist";
            }
        }
    }
}
