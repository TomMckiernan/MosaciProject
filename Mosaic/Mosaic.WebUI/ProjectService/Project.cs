using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectService
{
    public class Project
    {
        MongoClient client;
        IMongoDatabase database;

        public Project(string dbName = "MosaicDatabase")
        {
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

        public ProjectResponse InsertSmallFiles(ProjectInsertLargeFileRequest request)
        {
            return new MongoProject().InsertLargeFile(database, request);
        }

        public ProjectResponse DeleteProject(ProjectRequest request)
        {
            return new MongoProject().Delete(database, request);
        }
    }
}
