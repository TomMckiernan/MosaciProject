using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Drawing;

namespace ImageMosaicService
{
    public class MosaicGenerator
    {
        public Mosaic Generate(string masterImage, List<ImageFileIndexStructure> tileImages, bool random = false, int tileWidth = 10, int tileHeight = 10, 
            bool colourBlended = false, bool enhanced = false, int enhancedThreshold = 50, bool edgeDetection = false, List<PixelCoordinates> edges = null)
        {
            var imageProcessing = new ImageProcessing(tileWidth, tileHeight);
            var imageInfos = new List<ImageInfo>();
            var mosaic = new Mosaic();

            Parallel.ForEach(tileImages, f =>
            {
                var info = new ImageInfo(f.FilePath);
                info.AverageBL = Color.FromArgb(f.Data.AverageBL);
                info.AverageBR = Color.FromArgb(f.Data.AverageBR);
                info.AverageTL = Color.FromArgb(f.Data.AverageTL);
                info.AverageTR = Color.FromArgb(f.Data.AverageTR);
                   
                if(info != null)
                {
                    lock (imageInfos)
                    {
                        imageInfos.Add(info);
                    }
                }
            });

            using (var source = new Bitmap(masterImage))
            {
                var colorMap = imageProcessing.CreateMap(source);
                mosaic = imageProcessing.Render(source, colorMap, imageInfos, random, colourBlended, enhanced, enhancedThreshold,edgeDetection, edges);
            }

            return mosaic;
        }
    }
}
