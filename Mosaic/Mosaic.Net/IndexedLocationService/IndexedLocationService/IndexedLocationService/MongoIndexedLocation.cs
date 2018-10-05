using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;

namespace IndexedLocationService
{
    public class MongoIndexedLocation
    {
        // Once messaging service in place can replace bool type to request and response
        public IndexedLocation Create(bool request, IMongoClient client)
        {
            var db = client.GetDatabase("MosaicDatabase");
            var collection = db.GetCollection<IndexedLocation>("IndexedLocation");

            var response = collection.Find(x => x._id != null).FirstOrDefault();
            return response;
        }

        // Once messaging service in place can replace bool type to request and response
        // Check that only one Indexed location once insert complete
        public IndexedLocation Insert(bool request, IMongoClient client)
        {
            var db = client.GetDatabase("MosaicDatabase");
            var collection = db.GetCollection<IndexedLocation>("IndexedLocation");

            var response = collection.Find(x => x._id != null).First();
            return response;
        }

    }



}
