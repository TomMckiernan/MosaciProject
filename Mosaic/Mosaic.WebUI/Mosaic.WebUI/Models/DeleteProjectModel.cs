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
        public string EdgeImageFolder { get; set; }
        public string Error { get; set; }

        public DeleteProjectModel(string master = "wwwroot\\images\\master\\", string mosaic = "wwwroot\\images\\project\\", string edge = "wwwroot\\images\\edges\\")
        {
            MasterImageFolder = Path.GetFullPath(master);
            MosaicImageFolder = Path.GetFullPath(mosaic);
            EdgeImageFolder = Path.GetFullPath(edge);
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
                    if (!String.IsNullOrEmpty(project.Project.EdgeLocation))
                    {
                        DeleteEdgeImage(project.Project.EdgeLocation);
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
            var mosaicFile = Path.GetFileName(mosaicLocation);
            var mosaicPath = MosaicImageFolder + mosaicFile;
            if (File.Exists(mosaicPath))
            {
                File.Delete(mosaicPath);
            }
            else
            {
                Error = "Mosaic Image does not exist";
            }
        }

        private void DeleteEdgeImage(string edgeLocation)
        {
            var edgeFile = Path.GetFileName(edgeLocation);
            var edgePath = EdgeImageFolder + edgeFile;
            if (File.Exists(edgePath))
            {
                File.Delete(edgePath);
            }
            else
            {
                Error = "Edge Image does not exist";
            }
        }
    }
}
