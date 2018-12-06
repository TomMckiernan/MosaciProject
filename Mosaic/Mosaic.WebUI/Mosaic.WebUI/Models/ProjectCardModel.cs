using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mosaic.WebUI.Models
{
    public class ProjectCardModel
    {
        public string ProjectId { get; set; }
        public int TileImageCount { get; set; }
        public string MosaicLocation { get; set; }
        public ProjectStructure.Types.State State { get; set; }
        public string MasterFileName { get; set; }

        public ProjectCardModel(IMakerClient client, ProjectStructure project, ImageFileResponse imageFile)
        {
            ProjectId = project.Id;
            TileImageCount = project.SmallFileIds.Count;
            MosaicLocation = project.MosaicLocation;
            State = project.Progress;
            MasterFileName = imageFile.File.FileName;
            // Tests for wrong id and whether assigning values are not null or empty
        }
    }
}
