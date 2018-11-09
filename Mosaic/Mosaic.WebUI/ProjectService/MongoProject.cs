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
            var request = new ProjectStructure()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                TimeOfCreation = DateTime.Now.ToString(),
                Progress = ProjectStructure.Types.State.Created
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
                return new ProjectResponse() { Error = "Project with Id cannot be found" };
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

        // Could change the return type
        public ProjectResponse InsertSmallFiles(IMongoDatabase db, ProjectInsertSmallFilesRequest request)
        {
            var collection = db.GetCollection<ProjectStructure>("Project");

            if (String.IsNullOrEmpty(request.Id))
            {
                return new ProjectResponse() { Error = "Id cannot be null or empty" };
            }

            var update = Builders<ProjectStructure>.Update.Set(x => x.SmallFileIds, request.SmallFileIds);
            collection.UpdateOne(x => x.Id.Equals(request.Id), update);

            // update Progress
            return new ProjectResponse();
        }
        
        // Could change the return type
        public ProjectResponse InsertLargeFile(IMongoDatabase db, ProjectInsertLargeFileRequest request)
        {
            var collection = db.GetCollection<ProjectStructure>("Project");

            if (String.IsNullOrEmpty(request.Id))
            {
                return new ProjectResponse() { Error = "Id cannot be null or empty" };
            }

            var update = Builders<ProjectStructure>.Update.Set(x => x.LargeFileId, request.LargeFileId);
            collection.UpdateOne(x => x.Id.Equals(request.Id), update);
            
            // update Progress
            return new ProjectResponse();
        }

        // Could change the return type
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
            return new ProjectResponse();
        }
    }
}
