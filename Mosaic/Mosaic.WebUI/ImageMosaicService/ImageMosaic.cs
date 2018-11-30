﻿using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageMosaicService
{
    public class ImageMosaic
    {
        MongoClient client;
        IMongoDatabase database;

        public ImageMosaicResponse Generate(ImageMosaicRequest request)
        {
            var tilesPath = request.Tiles.Select(x => x.FilePath).ToList();
            var mosaicGenerator = new MosaicGenerator();
            var mosaic = mosaicGenerator.Generate(request.Master.FilePath, tilesPath);
            var location = string.Format("..\\..\\..\\..\\..\\..\\test\\{0}.jpg", request.Master.Id);
            mosaic.Image.Save(location);
            return new ImageMosaicResponse() { Location = location };
        }
    }
}
