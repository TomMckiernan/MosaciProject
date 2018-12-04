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
        public Mosaic Generate(string masterImage, List<string> tileImages, bool random = false)
        {
            var imageProcessing = new ImageProcessing();
            var imageInfos = new List<ImageInfo>();
            var mosaic = new Mosaic();

            var files = tileImages.Select(x => new FileInfo(x)).ToList();

            Parallel.ForEach(files, f =>
            {
                using (var inputBmp = imageProcessing.Resize(f.FullName))
                {
                    var info = imageProcessing.GetAverageColor(inputBmp, f.FullName);
                    
                    if(info != null)
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
