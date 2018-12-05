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
        public Mosaic Generate(string masterImage, List<ImageFileIndexStructure> tileImages, bool random = false)
        {
            var imageProcessing = new ImageProcessing();
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
                    imageInfos.Add(info);
                }
            });

            using (var source = new Bitmap(masterImage))
            {
                var colorMap = imageProcessing.CreateMap(source);
                mosaic = imageProcessing.Render(source, colorMap, imageInfos, random);
            }

            return mosaic;
        }
    }
}
