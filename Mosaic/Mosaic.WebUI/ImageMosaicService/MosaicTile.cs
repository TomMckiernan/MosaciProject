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
    }
}
