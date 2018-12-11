﻿using Infrastructure;
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
        public Dictionary<string, int> TileImageColours { get; set; }
        public string JsonTileImageColours { get; set; }
        public string JsonTileImageHexColours { get; set; }
        public List<string> MasterImageColours { get; set; }

        public GenerateMosaicModel(string id)
        {
        }

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
                ReadTileColours(client, project);
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

        public void ReadTileColours(IMakerClient client, ProjectResponse project)
        {
            var smallFiles = client.ReadAllImageFiles(project.Project.SmallFileIds);
            // This a shortcut version of this method
            // At the moment is takes into account the four quadrant averages of the file
            // rather than just one average which represents the whole tile.

            var fileColours = smallFiles.Files.Select(x => Color.FromArgb(x.Data.AverageWhole)).ToList();

            var fileColorModel = new FileColourModel().FindClosestColour(fileColours);

            var tileHexValues = fileColorModel.Select(x => x.ToHex());
            // Call FileColorModel and convert the list of colours to the closest 
            // pre defined colour

            Dictionary<string, int> colours = new Dictionary<string, int>();
            foreach (var value in tileHexValues)
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

            TileImageColours = colours;
            JsonTileImageColours = JsonConvert.SerializeObject(colours, Formatting.Indented);
            JsonTileImageHexColours = JsonConvert.SerializeObject(colours.Keys, Formatting.Indented);
            
            //Structure for mosaic generator model
            //- properties needed
            //- Master image
            //- Tile image count
            //- Average colour for the tile files
            //- Master image average colour analysis
            //  - i.e the average colour for each tile in the image

            //  Get all imagefileindexstructure files for the id
            //  Get the image file index structure for the master image
            //  Therefore need request to get the current project structure
        }
    }
}
