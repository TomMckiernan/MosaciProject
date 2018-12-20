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
        public string Error { get; set; }

        public void DeleteProject(IMakerClient client, string id)
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
            }
            else
            {
                Error = "Error in fetching project with id";
            }
        }

        public void DeleteMasterImage(string masterLocation)
        {
            if (File.Exists(masterLocation))
            {
                File.Delete(masterLocation);
            }
            else
            {
                Error = "Master Image does not exist";
            }
        }

        public void DeleteMosaicImage(string mosaicLocation)
        {
            if (File.Exists(mosaicLocation))
            {
                File.Delete(mosaicLocation);
            }
            else
            {
                Error = "Mosaic Image does not exist";
            }
        }
    }
}
