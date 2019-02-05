using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace ImageMosaicService
{
    public class ImageMosaic
    {
        public ImageMosaicResponse Generate(ImageMosaicRequest request)
        {
            var tilesPath = request.Tiles.Select(x => x.FilePath).ToList();
            var mosaicGenerator = new MosaicGenerator();
            var mosaic = mosaicGenerator.Generate(request.Master.FilePath, request.Tiles.ToList(), request.Random, request.TileWidth, request.TileHeight, request.ColourBlended);
            var location = string.Format("C:\\Users\\Tom_m\\OneDrive\\Pictures\\MosaicImageTests\\{0}.jpg", request.Id);
            mosaic.Image.Save(location);
            mosaic.Image.Dispose();
            return new ImageMosaicResponse() { Location = location };
        }

        public MasterImageColourResponse GetMasterImageAverageColours(MasterImageColourRequest request)
        {
            var imageProcessing = new ImageProcessing();
            Color[,] colorMap;
            using (var source = new Bitmap(request.Master.FilePath))
            {
                colorMap = imageProcessing.CreateMap(source);
            }

            var colorList = colorMap.ToList();
            var hexColorList = colorList.Select(x => x.ToArgb());
            var response = new MasterImageColourResponse() { };
            response.AverageTileARGB.AddRange(hexColorList);

            return response;
        }
    }
}
