using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq;

namespace ImageFileIndexService
{
    public class MongoImageFileIndex
    {
        public ImageFileIndexResponse Read(IMongoDatabase db, string indexedLocation)
        {
            //Returning BsonDocument since protobuf doesn't support ObjectId type
            var collection = db.GetCollection<ImageFileIndexStructure>("ImageFileIndex");
            // In error checking if an error occured this will report back in the response message

            var response = collection.Find(x => x.FilePath.Contains(indexedLocation)).ToList();
            
            // This will fail initially, needs changing to bson doc structure and then add
            // extension method to ImageFileIndexStructure to allow id to serialize correcty

            var result = new ImageFileIndexResponse();
            result.Files.AddRange(response);
            return result;
        }

        public ImageFileIndexStructureResponse Insert(IMongoDatabase db, ImageFileIndexStructure request)
        {
            var collection = db.GetCollection<ImageFileIndexStructure>("ImageFileIndex");
            // check if this works, may need to change working with objectID
            // add further checks to make sure no part of object is empty
            if (String.IsNullOrEmpty(request.Id))
            {
                return new ImageFileIndexStructureResponse() {  FilePath = request.FilePath, Error = "Location cannot be empty" };
            }
            var result = collection.ReplaceOne(x => x.Id.Equals(request.Id), request, new UpdateOptions { IsUpsert = true });

            return new ImageFileIndexStructureResponse() { FilePath = request.FilePath };
        }
    }
}
