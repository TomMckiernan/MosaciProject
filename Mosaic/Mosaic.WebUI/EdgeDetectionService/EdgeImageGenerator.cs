using System;
using System.Drawing;

namespace EdgeDetectionService
{
    public class EdgeImageGenerator
    {
        public EdgeImage Generate(string masterImage, int threshold)
        {
            var edgeImage = new EdgeImage();
            var sobelGenerator = new SobelGenerator(threshold);
            using (var source = new Bitmap(masterImage))
            {
                edgeImage = sobelGenerator.Generate(source);
            }

            return edgeImage;
        }
    }
}