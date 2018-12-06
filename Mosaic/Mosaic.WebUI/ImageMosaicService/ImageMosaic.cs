using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageMosaicService
{
    public class ImageMosaic
    {
        public ImageMosaicResponse Generate(ImageMosaicRequest request)
        {
            var tilesPath = request.Tiles.Select(x => x.FilePath).ToList();
            var mosaicGenerator = new MosaicGenerator();
            var mosaic = mosaicGenerator.Generate(request.Master.FilePath, request.Tiles.ToList(), request.Random);
            var location = string.Format("C:\\Users\\Tom_m\\OneDrive\\Pictures\\MosaicImageTests\\{0}.jpg", request.Id);
            mosaic.Image.Save(location);
            return new ImageMosaicResponse() { Location = location };
        }
    }
}
