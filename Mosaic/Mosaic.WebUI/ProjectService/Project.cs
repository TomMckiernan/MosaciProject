using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using Utility;

namespace ProjectService
{
    public class Project
    {
        MongoClient client;
        IMongoDatabase database;

        public Project(string dbName = "MosaicDatabase")
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(ProjectStructure)))
            {
                BsonClassMap.RegisterClassMap<ProjectStructure>(cm =>
                {
                    // deals with it being a read only property
                    cm.AutoMap();
                    cm.MapProperty(m => m.SmallFileIds);
                });
            }

            client = new MongoClient();
            database = client.GetDatabase(dbName);
        }

        public ProjectResponse CreateProject()
        {
            return new MongoProject().Create(database);
        }

        public ProjectResponse ReadProject(ProjectRequest request)
        {
            return new MongoProject().Read(database, request);
        }

        public ProjectMultipleResponse ReadAllProjects()
        {
            return new MongoProject().ReadAll(database);
        }

        public ProjectResponse InsertSmallFiles(ProjectInsertSmallFilesRequest request)
        {
            return new MongoProject().InsertSmallFiles(database, request);
        }

        public ProjectResponse InsertLargeFile(ProjectInsertLargeFileRequest request)
        {
            return new MongoProject().InsertLargeFile(database, request);
        }

        public ProjectResponse InsertMosaicFile(ProjectInsertMosaicFileRequest request)
        {
            return new MongoProject().InsertMosaicFile(database, request);
        }

        public ProjectResponse DeleteProject(ProjectRequest request)
        {
            return new MongoProject().Delete(database, request);
        }

        public ProjectResponse InsertEdgeFile(ProjectInsertEdgeFileRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
