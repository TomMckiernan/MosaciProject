using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace IndexedLocationService
{
    public class IndexedLocation
    {
        MongoClient dbClient;
        MongoClient client;
        IMongoDatabase database;

        public IndexedLocation()
        {
            dbClient = new MongoClient("mongodb://127.0.0.1:27017");
            client = new MongoClient();
            database = client.GetDatabase("MosaicDatabase");
        }

        public IndexedLocationResponse ReadIndexedLocation()
        {
            return new MongoIndexedLocation().Read(database);
        }

        public IndexedLocationResponse UpdateIndexedLocation(IndexedLocationRequest request)
        {
            return new MongoIndexedLocation().Insert(request, database);
        }
    }
}
