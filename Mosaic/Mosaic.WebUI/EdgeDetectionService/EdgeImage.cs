using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace EdgeDetectionService
{
    public class EdgeImage
    {
        public Image Image { get; set; }
        public List<PixelCoordinates> Edges { get; set; }
    }
}
