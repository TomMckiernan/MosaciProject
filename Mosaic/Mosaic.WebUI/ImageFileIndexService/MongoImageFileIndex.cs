using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq;
using Google.Protobuf.Collections;

namespace ImageFileIndexService
{
    public class MongoImageFileIndex
    {
        public ImageFileIndexResponse ReadAll(IMongoDatabase db, string indexedLocation)
        {
            var collection = db.GetCollection<ImageFileIndexStructure>("ImageFileIndex");

            if (String.IsNullOrEmpty(indexedLocation))
            {
                return new ImageFileIndexResponse() { Error = "Indexed location cannot be null or empty" };
            }
            var response = collection.Find(x => x.FilePath.Contains(indexedLocation)).ToList();

            var result = new ImageFileIndexResponse();
            result.Files.AddRange(response);
            return result;
        }

        public ImageFileResponse Insert(IMongoDatabase db, ImageFileIndexStructure request)
        {
            var collection = db.GetCollection<ImageFileIndexStructure>("ImageFileIndex");

            // add further checks to make sure no part of object is empty
            if (String.IsNullOrEmpty(request.Id))
            {
                return new ImageFileResponse() {  Error = "Id cannot be null or empty" };
            }
            var result = collection.ReplaceOne(x => x.Id.Equals(request.Id), request, new UpdateOptions { IsUpsert = true });

            return new ImageFileResponse() { File = request };
        }


        public ImageFileResponse ReadImageFile(IMongoDatabase db, string id)
        {
            var collection = db.GetCollection<ImageFileIndexStructure>("ImageFileIndex");

            if (String.IsNullOrEmpty(id))
            {
                return new ImageFileResponse() { Error = "Id cannot be null or empty" };
            }
            var response = collection.Find(x => x.Id == id).FirstOrDefault();
            if (response == null)
            {
                return new ImageFileResponse() { Error = "No image with id can be found" };
            }
            return new ImageFileResponse() { File = response };
        }

        public ImageFileIndexResponse ReadAllImageFiles(IMongoDatabase db, List<string> ids)
        {
            var collection = db.GetCollection<ImageFileIndexStructure>("ImageFileIndex");

            if (ids == null)
            {
                return new ImageFileIndexResponse() { Error = "List of ids cannot be null" };
            }
            var filter = Builders<ImageFileIndexStructure>.Filter.In(p => p.Id, ids);

            var response = collection.Find(filter).ToList();

            var result = new ImageFileIndexResponse();
            result.Files.AddRange(response);
            return result;
        }

        public ImageFileIndexStructureResponse Delete(IMongoDatabase db, string id)
        {
            var collection = db.GetCollection<ImageFileIndexStructure>("ImageFileIndex");

            if (String.IsNullOrEmpty(id))
            {
                return new ImageFileIndexStructureResponse() { Error = "Id cannot be null or empty" };
            }
            var result = collection.DeleteOne(x => x.Id.Equals(id));
            if (result.DeletedCount == 0)
            {
                return new ImageFileIndexStructureResponse() { Error = "File with Id cannot be deleted" };
            }
            return new ImageFileIndexStructureResponse() { };
        }
    }
}
