using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageFileIndexService
{
    public class ImageFileIndex
    {
        MongoClient client;
        IMongoDatabase database;

        public ImageFileIndex()
        {
            client = new MongoClient();
            database = client.GetDatabase("MosaicDatabase");
        }

        public ImageFileIndexResponse ReadImageFileIndex(string indexedLocation)
        {
            return new MongoImageFileIndex().Read(database, indexedLocation);
        }
    }
}
