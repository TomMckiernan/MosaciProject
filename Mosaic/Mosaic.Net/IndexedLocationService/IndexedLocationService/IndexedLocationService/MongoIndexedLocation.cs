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
        public IndexedLocation Read(bool request, IMongoClient client)
        {
            var db = client.GetDatabase("MosaicDatabase");
            var collection = db.GetCollection<IndexedLocation>("IndexedLocation");

            var response = collection.Find(x => x.Location != null).FirstOrDefault();
            return response;
        }

        // Once messaging service in place can replace bool type to request and response
        // Check that only one Indexed location once insert complete
        public ReplaceOneResult Insert(IndexedLocation request, IMongoClient client)
        {
            var db = client.GetDatabase("MosaicDatabase");
            var collection = db.GetCollection<IndexedLocation>("IndexedLocation");

            collection.InsertOne(request, new InsertOneOptions());

            var result = collection.ReplaceOne(x => x.Location != null, request, new UpdateOptions { IsUpsert = true });
            //Replace this with a insert response type
            return result;
        }

    }



}
