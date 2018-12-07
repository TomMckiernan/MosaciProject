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
        public string MasterLocation { get; set; }
        public string MosaicLocation { get; set; }
        public ProjectStructure.Types.State State { get; set; }
        public string MasterFileName { get; set; }
        public string TimeOfCreation { get; set; }

        public ProjectCardModel(IMakerClient client, ProjectStructure project)
        {
            ProjectId = project.Id;
            TileImageCount = project.SmallFileIds.Count;
            State = project.Progress;
            MasterLocation = project.MasterLocation;
            MosaicLocation = project.MosaicLocation;
            TimeOfCreation = project.TimeOfCreation;
            if (!String.IsNullOrEmpty(project.LargeFileId))
            {
                var imageFile = client.ReadImageFile(project.LargeFileId);
                MasterFileName = imageFile?.File?.FileName;
            }        
            // Tests for wrong id and whether assigning values are not null or empty
        }
    }
}
