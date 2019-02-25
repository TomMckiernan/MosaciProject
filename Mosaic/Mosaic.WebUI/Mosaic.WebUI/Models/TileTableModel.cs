using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Utility;

namespace Mosaic.WebUI.Models
{
    public class TileTableModel
    {
        public string Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string AverageColour { get; set; }
        public string StandardColour{ get; set; }
        public string LastWriteTime { get; set; }

        public TileTableModel(ImageFileIndexStructure imageFile)
        {
            Id = imageFile.Id;
            FileName = imageFile.FileName;
            FilePath = imageFile.FilePath;
            LastWriteTime = imageFile.LastWriteTime;
            if (imageFile.Data != null)
            {
                var colour = Color.FromArgb(imageFile.Data.AverageWhole);
                AverageColour = colour.ToHex();
                StandardColour = new FileColourModel().FindClosestColour(colour).ToHex();
            }
        }
    }
}
