using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace IndexedLocationService
{
    public class IndexedLocation
    {
        MongoClient client;
        IMongoDatabase database;

        public IndexedLocation(string dbName = "MosaicDataBase")
        {
            client = new MongoClient();
            database = client.GetDatabase(dbName);
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
