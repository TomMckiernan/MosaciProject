using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ImageFileIndexService
{
    class MongoImageFileIndex
    {
        public ImageFileIndexResponse Read(IMongoDatabase db, string indexedLocation)
        {
            //Returning BsonDocument since protobuf doesn't support ObjectId type
            var collection = db.GetCollection<ImageFileIndexStructure>("IndexedLocation");
            // In error checking if an error occured this will report back in the response message

            var response = collection.Find(x => x.FilePath.Contains(indexedLocation)).ToList();
            //if (bsonResult == null)
            //{
            //    return new IndexedLocationResponse() { Error = "No indexed location in database" };
            //}
            //var result = new IndexedLocationStructure().ConvertFromBsonDocument(bsonResult);

            //var response = new IndexedLocationResponse()
            //{
            //    IndexedLocation = result.IndexedLocation
            //};

            var result = new ImageFileIndexResponse();
            result.Files = response;

            return response;
        }

        //public ImageFileIndexResponse Insert(ImageFileIndexRequest request, IMongoDatabase db)
        //{
        //    var collection = db.GetCollection<IndexedLocationRequest>("IndexedLocation");

        //    if (String.IsNullOrEmpty(request.IndexedLocation))
        //    {
        //        return new IndexedLocationResponse() { IndexedLocation = request.IndexedLocation, Error = "Location cannot be empty" };
        //    }
        //    var result = collection.ReplaceOne(x => x.IndexedLocation != null, request, new UpdateOptions { IsUpsert = true });

        //    return new IndexedLocationResponse() { IndexedLocation = request.IndexedLocation };
        //}
    }
}
