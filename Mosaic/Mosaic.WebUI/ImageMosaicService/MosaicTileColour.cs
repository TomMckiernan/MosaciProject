using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ImageMosaicService
{
    public class MosaicTileColour
    {
        public Color AverageBL { get; set; }
        public Color AverageBR { get; set; }
        public Color AverageTL { get; set; }
        public Color AverageTR { get; set; }
        public Color AverageWhole { get; set; }
    }
}
