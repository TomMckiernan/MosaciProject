using System;
using System.Collections.Generic;
using System.Drawing;
using AForge.Imaging.Filters;

namespace EdgeDetectionService
{
    public class SobelGenerator
    {
        private int Threshold;

        public SobelGenerator(int threshold = 110)
        {
            Threshold = threshold;
        }

        public EdgeImage Generate(Bitmap source)
        {
            // Make GrayScale Version of image
            Grayscale filter = new Grayscale(0.2125, 0.7154, 0.0721);
            Bitmap grayImage = filter.Apply(source);

            // Apply Sobel Edge Detection to the image
            SobelEdgeDetector sobelFilter = new SobelEdgeDetector();
            Bitmap edgeImage = sobelFilter.Apply(grayImage);

            // Apply Threshold to edge detected image
            // Edges are white pixels with value 255 otherwise black with value 0
            Threshold thresholdFilter = new Threshold(Threshold);
            Bitmap thresholdImage = thresholdFilter.Apply(edgeImage);

            // Render Image
            var newImg = new Bitmap(thresholdImage.Width, thresholdImage.Height);
            if (thresholdImage != null)
            {
                var g = Graphics.FromImage(newImg);

                var srcRect = new Rectangle(0, 0, thresholdImage.Width, thresholdImage.Height);
                var destRect = new Rectangle(0, 0, thresholdImage.Width, thresholdImage.Height);

                g.DrawImage(thresholdImage, destRect, srcRect, GraphicsUnit.Pixel);
            }

            var edgeLocations = new List<PixelCoordinates>();
            // Save location of edges
            for (int x = 0; x < newImg.Width; x++)
            {
                for (int y = 0; y < newImg.Height; y++)
                {
                    if (newImg.GetPixel(x,y).R > Threshold)
                    {
                        edgeLocations.Add(new PixelCoordinates() { X = x, Y = y });
                    }
                }
            }

            // Dispose of the created images
            grayImage.Dispose();
            edgeImage.Dispose();
            thresholdImage.Dispose();
            
            return new EdgeImage() { Image = newImg, Edges = edgeLocations  };

            
        }
    }
}