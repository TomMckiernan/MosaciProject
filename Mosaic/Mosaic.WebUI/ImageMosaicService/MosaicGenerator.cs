using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Drawing;

namespace ImageMosaic
{
    public class MosaicGenerator
    {
        public Mosaic Generate(string imageToMash, string srcImageDirectory)
        {
            var imageProcessing = new ImageProcessing();
            var imageInfos = new List<ImageInfo>();
            var mosaic = new Mosaic();

            var di = new DirectoryInfo(srcImageDirectory);
            var files = di.GetFiles("*.jpg", SearchOption.AllDirectories).ToList();

            Parallel.ForEach(files, f =>
            {
                using (var inputBmp = imageProcessing.Resize(f.FullName))
                {
                    var info = imageProcessing.GetAverageColor(inputBmp, f.FullName);
                    
                    if(info != null)
                        imageInfos.Add(info);
                }
            });

            using (var source = new Bitmap(imageToMash))
            {
                var colorMap = imageProcessing.CreateMap(source);
                mosaic = imageProcessing.Render(source, colorMap, imageInfos);
            }

            return mosaic;
        }
    }
}
