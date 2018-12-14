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
        public ProjectStructure.Types.State State { get; set; }
        public string JsonTileImageColours { get; set; }
        public string JsonTileImageHexColours { get; set; }
        public string JsonMasterImageColours { get; set; }
        public string JsonMasterImageHexColours { get; set; }

        public void ReadProjectData(IMakerClient client, string projectId)
        {
            var project = ProjectErrorCheck(client, projectId);
            if (String.IsNullOrEmpty(project.Error))
            {
                ProjectId = project.Project.Id;
                TileImageCount = project.Project.SmallFileIds.Count;
                State = project.Project.Progress;
                MasterLocation = project.Project.MasterLocation;
                MosaicLocation = project.Project.MosaicLocation;
                if (!String.IsNullOrEmpty(project.Project.LargeFileId))
                {
                    ReadMasterColours(client, project);
                }
                if (project.Project.SmallFileIds != null && project.Project.SmallFileIds.Count != 0)
                {
                    ReadTileColours(client, project);
                }
            }
        }

        public ImageMosaicResponse Generate(IMakerClient client, string id, bool random = false)
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

            return client.Generate(id, tileFiles.Files, masterFile.File, random);
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

        private void ReadMasterColours(IMakerClient client, ProjectResponse project)
        {
            // Convert the ARGB values from master file into Color objects
            var master = client.ReadImageFile(project.Project.LargeFileId);
            var masterARGB = client.ReadMasterFileColours(master.File);
            var masterColours = masterARGB.AverageTileARGB.Select(x => Color.FromArgb(x)).ToList();

            // Find the closest standard Color object for each color in master file colours
            var masterClosestColours = new FileColourModel().FindClosestColour(masterColours);
            var masterClosestColoursHex = masterClosestColours.Select(x => x.ToHex()).ToList();

            var masterFileDictionary = ConvertColourListToDictionary(masterClosestColoursHex);

            JsonMasterImageColours = JsonConvert.SerializeObject(masterFileDictionary, Formatting.Indented);
            JsonMasterImageHexColours = JsonConvert.SerializeObject(masterFileDictionary.Keys, Formatting.Indented);
        }

        public void ReadTileColours(IMakerClient client, ProjectResponse project)
        {
            // At the moment is takes into account the four quadrant averages of the file
            // rather than just one average which represents the whole tile.
            // Convert the ARGB values stored in project into Color objects
            var tiles = client.ReadAllImageFiles(project.Project.SmallFileIds);
            var tilesColours = tiles.Files.Select(x => Color.FromArgb(x.Data.AverageWhole)).ToList();

            // Find the closest standard Color object for each color in tile files colours
            var tilesClosestColours = new FileColourModel().FindClosestColour(tilesColours);
            var tilesFilesClosestColoursHex = tilesClosestColours.Select(x => x.ToHex()).ToList();

            var tilesDictionary = ConvertColourListToDictionary(tilesFilesClosestColoursHex);

            JsonTileImageColours = JsonConvert.SerializeObject(tilesDictionary, Formatting.Indented);
            JsonTileImageHexColours = JsonConvert.SerializeObject(tilesDictionary.Keys, Formatting.Indented);
        }

        private Dictionary<string, int> ConvertColourListToDictionary(IList<string> list)
        {
            Dictionary<string, int> colours = new Dictionary<string, int>();
            foreach (var value in list)
            {
                if (!colours.ContainsKey(value))
                {
                    colours.Add(value, 1);
                }
                else
                {
                    colours[value]++;
                }
            }
            return colours;
        }
    }
}
