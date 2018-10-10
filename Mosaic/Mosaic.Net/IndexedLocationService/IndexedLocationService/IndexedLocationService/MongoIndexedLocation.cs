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
        public IndexedLocationResponse Read(bool request, IMongoDatabase db)
        {
            var collection = db.GetCollection<IndexedLocation>("IndexedLocation");
            // In error checking if an error occured this will report back in the respons message
            var result = collection.Find(x => x.Location != null).FirstOrDefault();

            var response = new IndexedLocationResponse() { Location = result.Location };
            return response;
        }

        // Once messaging service in place can replace bool type to request and response
        // Check that only one Indexed location once insert complete
        public IndexedLocationResponse Insert(IndexedLocation request, IMongoDatabase db)
        {
            var collection = db.GetCollection<IndexedLocation>("IndexedLocation");

            collection.InsertOne(request, new InsertOneOptions());

            var result = collection.ReplaceOne(x => x.Location != null, request, new UpdateOptions { IsUpsert = true });
            //Replace this with a insert response type
            var response = new IndexedLocationResponse() { Location = request.Location };
            return response;
        }

    }



}
