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
        public string EdgeLocation { get; set; }
        public string JSMasterLocation { get { return GetJSMasterLocation(); } }
        public string JSMosaicLocation { get { return GetJSMosaicLocation(); } }
        public string JSEdgeLocation { get { return GetJSEdgeLocation(); } }
        public ProjectStructure.Types.State State { get; set; }
        public GenerateMosaicColoursModel ColoursModel { get; set; }
        public Tuple<string, ProjectStructure.Types.State> PartialModel {get; set;}

        // To bool values are for the purpose of testing
        public void ReadProjectData(IMakerClient client, string projectId, bool readColours = true, int height = 10, int width = 10)
        {
            var project = ProjectErrorCheck(client, projectId);
            if (String.IsNullOrEmpty(project.Error))
            {
                ProjectId = project.Project.Id;
                TileImageCount = project.Project.SmallFileIds.Count;
                State = project.Project.Progress;
                MasterLocation = project.Project.MasterLocation;
                MosaicLocation = project.Project.MosaicLocation;
                EdgeLocation = project.Project.EdgeLocation;
                PartialModel = new Tuple<string, ProjectStructure.Types.State>(ProjectId, State);
                if (readColours)
                {
                    ColoursModel = GenerateColoursModel(client, project, height, width);
                }
            }
        }

        public ImageMosaicResponse Generate(IMakerClient client, string id, bool random = false, int tileWidth = 10, int tileHeight = 10, bool colourBlended = false, bool enhanced = false)
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

            return client.Generate(id, tileFiles.Files, masterFile.File, random, tileWidth, tileHeight, colourBlended, enhanced);
        }

        public EdgeDetectionResponse PreviewEdges(IMakerClient client, string id, int threshold)
        {
            // Get project
            var project = ProjectErrorCheck(client, id);
            if (!String.IsNullOrEmpty(project.Error))
            {
                return new EdgeDetectionResponse() { Error = project.Error };
            }
            if (threshold < 1 || threshold > 255)
            {
                return new EdgeDetectionResponse() { Error = "Threshold must be in valid range" };
            }

            //  Get the image file index structure for the master image
            var masterFileId = project.Project.LargeFileId;
            var masterFile = client.ReadImageFile(masterFileId);
            if (!String.IsNullOrEmpty(masterFile.Error))
            {
                return new EdgeDetectionResponse() { Error = "Master image cannot be read" };
            }

            return client.PreviewEdges(id, masterFile.File, threshold);
        }

        public GenerateMosaicColoursModel GenerateColoursModel(IMakerClient client, ProjectResponse project, int height, int width)
        {
            var result = new GenerateMosaicColoursModel(client, project, height, width);
            return result;
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

        private string GetJSEdgeLocation()
        {
            return EdgeLocation.Replace(@"\", @"\\");
        }
    }
}
