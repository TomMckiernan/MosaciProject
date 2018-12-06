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
    }
}
