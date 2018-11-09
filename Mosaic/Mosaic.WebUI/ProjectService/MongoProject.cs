using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectService
{
    public class MongoProject
    {
        public ProjectResponse Create(IMongoDatabase db)
        {
            var id = ObjectId.GenerateNewId().ToString();
            var request = new ProjectStructure()
            {
                Id = id
                // Progress cahnge type to enum
            };
            var collection = db.GetCollection<ProjectStructure>("Project");
            collection.InsertOne(request);
            return new ProjectResponse() { Project = request };

        }

        public ProjectResponse Read(IMongoDatabase db, ProjectRequest request)
        {
            var collection = db.GetCollection<ProjectStructure>("Project");

            if (String.IsNullOrEmpty(request.Id))
            {
                return new ProjectResponse() { Error = "Id cannot be null or empty" };
            }
            var response = collection.Find(x => x.Id.Equals(request.Id)).FirstOrDefault();
            if (response == null)
            {
                return new ProjectResponse() { Error = "Project with Id does not exist" };
            }
            return new ProjectResponse() { Project = response };
        }

        public ProjectMultipleResponse ReadAll(IMongoDatabase db)
        {
            var collection = db.GetCollection<ProjectStructure>("Project");
       
            var response = collection.Find(x => true).ToList();

            var result = new ProjectMultipleResponse();
            result.Projects.AddRange(response);
            return result;
        }

        public ProjectResponse InsertSmallFiles(IMongoDatabase db, ProjectInsertSmallFilesRequest request)
        {
            throw new NotImplementedException();
        }

        public ProjectResponse InsertLargeFile(IMongoDatabase db, ProjectInsertLargeFileRequest request)
        {
            throw new NotImplementedException();
        }

        public ProjectResponse Delete(IMongoDatabase db, ProjectRequest request)
        {
            var collection = db.GetCollection<ProjectStructure>("Project");

            if (String.IsNullOrEmpty(request.Id))
            {
                return new ProjectResponse() { Error = "Id cannot be null or empty" };
            }
            var result = collection.DeleteOne(x => x.Id.Equals(request.Id));
            if (result.DeletedCount == 0)
            {
                return new ProjectResponse() { Error = "Project with Id cannot be deleted" };
            }
            return new ProjectResponse() { };
        }
    }
}
