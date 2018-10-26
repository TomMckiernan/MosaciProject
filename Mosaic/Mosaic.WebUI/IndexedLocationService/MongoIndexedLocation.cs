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
        public IndexedLocationResponse Read(IMongoDatabase db)
        {
            //Returning BsonDocument since protobuf doesn't support ObjectId type
            var collection = db.GetCollection<BsonDocument>("IndexedLocation");
            // In error checking if an error occured this will report back in the respons message

            var bsonResult = collection.Find(x => true).FirstOrDefault();
            var result = new IndexedLocationResponse()
            {
                IndexedLocation = bsonResult.GetValue("Location").ToString()
            };

            var response = new IndexedLocationResponse() { IndexedLocation = result.IndexedLocation };
            return response;
        }

        // Once messaging service in place can replace bool type to request and response
        // Check that only one Indexed location once insert complete
        public IndexedLocationResponse Insert(IndexedLocationRequest request, IMongoDatabase db)
        {
            var collection = db.GetCollection<IndexedLocationRequest>("IndexedLocation");

            if (String.IsNullOrEmpty(request.IndexedLocation))
            {
                return new IndexedLocationResponse() { IndexedLocation = request.IndexedLocation, Error = "Location cannot be empty" };
            }
            var result = collection.ReplaceOne(x => x.IndexedLocation != null, request, new UpdateOptions { IsUpsert = true });

            return new IndexedLocationResponse() { IndexedLocation = request.IndexedLocation };
        }

    }



}
