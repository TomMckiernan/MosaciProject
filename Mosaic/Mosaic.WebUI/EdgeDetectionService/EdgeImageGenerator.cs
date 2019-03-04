using System;
using System.Drawing;

namespace EdgeDetectionService
{
    public class EdgeImageGenerator
    {
        public EdgeImage Generate(string masterImage)
        {
            var edgeImage = new EdgeImage();
            var sobelGenerator = new SobelGenerator();
            using (var source = new Bitmap(masterImage))
            {
                edgeImage = sobelGenerator.Generate(source);
            }

            return edgeImage;
        }
    }
}