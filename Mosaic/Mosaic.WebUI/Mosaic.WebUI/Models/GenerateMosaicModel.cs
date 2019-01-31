using Infrastructure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Utility;
using Newtonsoft;
using Newtonsoft.Json;

namespace Mosaic.WebUI.Models
{
    public class GenerateMosaicModel
    {
        public string ProjectId { get; set; }
        public int TileImageCount { get; set; }
        public string MasterLocation { get; set; }
        public string MosaicLocation { get; set; }
        public string JSMosaicLocation { get { return GetJSMosaicLocation(); } }
        public string JSMasterLocation { get { return GetJSMasterLocation(); } }
        public ProjectStructure.Types.State State { get; set; }
        public GenerateMosaicColoursModel ColoursModel { get; set; }
        public Tuple<string, ProjectStructure.Types.State> PartialModel {get; set;}

        // To bool values are for the purpose of testing
        public void ReadProjectData(IMakerClient client, string projectId, bool readColours = true)
        {
            var project = ProjectErrorCheck(client, projectId);
            if (String.IsNullOrEmpty(project.Error))
            {
                ProjectId = project.Project.Id;
                TileImageCount = project.Project.SmallFileIds.Count;
                State = project.Project.Progress;
                MasterLocation = project.Project.MasterLocation;
                MosaicLocation = project.Project.MosaicLocation;
                PartialModel = new Tuple<string, ProjectStructure.Types.State>(ProjectId, State);
                if (readColours)
                {
                    ColoursModel = new GenerateMosaicColoursModel(client, project);
                }
            }
        }

        public ImageMosaicResponse Generate(IMakerClient client, string id, bool random = false, int tileWidth = 10, int tileHeight = 10)
        {
            // Get project
            var project = ProjectErrorCheck(client, id);
            if (!String.IsNullOrEmpty(project.Error))
            {
                return new ImageMosaicResponse() { Error = project.Error };
            }

            //  Get all imagefileindexstructure files for the id
            var tileFilesId = project.Project.SmallFileIds.ToList();
            var tileFiles = client.ReadAllImageFiles(tileFilesId);

            //  Get the image file index structure for the master image
            var masterFileId = project.Project.LargeFileId;
            var masterFile = client.ReadImageFile(masterFileId);
            if (!String.IsNullOrEmpty(tileFiles.Error) || !String.IsNullOrEmpty(masterFile.Error))
            {
                return new ImageMosaicResponse() { Error = "Master or tile images cannot be read" };
            }

            return client.Generate(id, tileFiles.Files, masterFile.File, random, tileWidth, tileHeight);
        }

        //Return project response
        private ProjectResponse ProjectErrorCheck(IMakerClient client, string id)
        {
            // Get project
            if (String.IsNullOrEmpty(id))
            {
                return new ProjectResponse() { Error = "Project Id cannot be null or empty" };
            }
            var project = client.ReadProject(id);
            if (!String.IsNullOrEmpty(project.Error))
            {
                return project;
            }
            else if (project.Project.SmallFileIds.Count == 0 || String.IsNullOrEmpty(project.Project.LargeFileId))
            {
                project.Error = "Master or tile images not specified";
            }
            return project;
        }

        private string GetJSMasterLocation()
        {
            return MasterLocation.Replace(@"\", @"\\");
        }

        private string GetJSMosaicLocation()
        {
            return MosaicLocation.Replace(@"\", @"\\");
        }
    }
}
