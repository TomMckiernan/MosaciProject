using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ImageMosaicService
{
    public class MosaicTile
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Image { get; set; }
        public double Difference { get; set; }
        public Bitmap Bitmap { get; set; }
        public bool InQuadrants{ get; set; }
        public MosaicTile TLMosiacTile { get; set; }
        public MosaicTile TRMosiacTile { get; set; }
        public MosaicTile BLMosiacTile { get; set; }
        public MosaicTile BRMosiacTile { get; set; }

        public MosaicTile()
        {

        }
        public MosaicTile(ImageInfo info, int x, int y)
        {
            X = x;
            Y = y;
            Image = info.Path;
            Difference = info.Difference;
        }
    }
}
