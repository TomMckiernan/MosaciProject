﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
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
                Progress = ProjectStructure.Types.State.Created,
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

            response = UpdateSmallImageIds(db, response);
            return new ProjectResponse() { Project = response };
        }

        public ProjectMultipleResponse ReadAll(IMongoDatabase db)
        {
            var collection = db.GetCollection<ProjectStructure>("Project");
    
            var response = collection.Find(x => true).ToList();

            var responseUpdated = response.Select(x => UpdateSmallImageIds(db, x));

            var result = new ProjectMultipleResponse();
            result.Projects.AddRange(responseUpdated);
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
            if (request.SmallFileIds == null || request.SmallFileIds.Count == 0)
            {
                return new ProjectResponse() { Error = "File ids cannot be null or empty" };
            }

            var project = collection.Find(x => x.Id.Equals(request.Id)).FirstOrDefault();
            project.SmallFileIds.AddRange(request.SmallFileIds);
            project.Progress = ProjectStructure.Types.State.Smalladded;

            //Sees modified and original as the same so doesn't replace
            var update = Builders<ProjectStructure>.Update.Set(x => x.SmallFileIds, request.SmallFileIds)
                .Set(x => x.Progress, project.Progress);
            var updateResponse = collection.UpdateOne(x => x.Id.Equals(request.Id), update);
            var response = new ProjectStructure() { Id = request.Id };

            response.SmallFileIds.AddRange(request.SmallFileIds);
            return new ProjectResponse() { Project = response };
        }
        
        // Could change the return type
        public ProjectResponse InsertLargeFile(IMongoDatabase db, ProjectInsertLargeFileRequest request)
        {
            var collection = db.GetCollection<ProjectStructure>("Project");

            if (String.IsNullOrEmpty(request.Id) || String.IsNullOrEmpty(request.LargeFileId))
            {
                return new ProjectResponse() { Error = "Id cannot be null or empty" };
            }

            var update = Builders<ProjectStructure>.Update.Set(x => x.LargeFileId, request.LargeFileId)
                .Set(x => x.Progress, ProjectStructure.Types.State.Largeadded);
            collection.UpdateOne(x => x.Id.Equals(request.Id), update);
            
            return new ProjectResponse() { Project = new ProjectStructure() { Id = request.Id, LargeFileId = request.LargeFileId } };
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
            return new ProjectResponse() { Project = new ProjectStructure() { Id = request.Id } };
        }

        private IEnumerable<string> ReadSmallFieldIds(IMongoDatabase db, string id)
        {
            var collection = db.GetCollection<BsonDocument>("Project");

            var fields = Builders<BsonDocument>.Projection.Include(p => p["SmallFileIds"]);
            var response = collection.Find(x => x["_id"].Equals(id)).Project(fields).FirstOrDefault();
            if (response == null)
            {
                return new List<string>();
            }
            var smallFileIds = response["SmallFileIds"].AsBsonArray.ToList();
            var stringList = smallFileIds.Select(i => i.ToString());
            return stringList;
        }

        private ProjectStructure UpdateSmallImageIds(IMongoDatabase db, ProjectStructure response)
        {
            var smallFileIds = ReadSmallFieldIds(db, response.Id);
            if (smallFileIds != null && smallFileIds.Count() > 0)
            {
                response.SmallFileIds.AddRange(smallFileIds);
            }
            return response;
        }
    }
}