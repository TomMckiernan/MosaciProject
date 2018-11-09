using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class MakerClient : IMakerClient
    {
        // Change so that each service is created on start and new service not created for each request
        public IndexedLocationResponse ReadIndexedLocation()
        {
            var request = new IndexedLocationRequest() {};
            var response = new IndexedLocationService.IndexedLocation().ReadIndexedLocation();
            return response;
        }

        public IndexedLocationResponse UpdateIndexedLocation(string indexedLocation)
        {
            var request = new IndexedLocationRequest() { IndexedLocation = indexedLocation };
            var response = new IndexedLocationService.IndexedLocation().UpdateIndexedLocation(request);
            return response;
        }

        public ImageFileIndexResponse ReadImageFileIndex(string indexedLocation)
        {
            var request = new ImageFileIndexRequest() { IndexedLocation = indexedLocation };
            var response = new ImageFileIndexService.ImageFileIndex().ReadImageFileIndex(request);
            return response;
        }

        public async Task<ImageFileIndexUpdateResponse> UpdateImageFileIndex(string indexedLocation)
        {
            var request = new ImageFileIndexRequest() { IndexedLocation = indexedLocation };
            var response = await new ImageFileIndexService.ImageFileIndex().AnalyseImageFileIndex(request);
            return response;
        }

        public ProjectResponse CreateProject()
        {
            var response = new ProjectService.Project().CreateProject();
            return response;
        }

        public ProjectResponse ReadProject(string id)
        {
            var request = new ProjectRequest() { Id = id };
            var response = new ProjectService.Project().ReadProject(request);
            return response;
        }

        public ProjectMultipleResponse ReadAllProjects()
        {
            var response = new ProjectService.Project().ReadAllProjects();
            return response;
        }

        public ProjectResponse InsertSamllFiles(string id, IList<string> fileIds)
        {
            var request = new ProjectInsertSmallFilesRequest() { Id = id };
            request.SmallFileIds.AddRange(fileIds);
            var response = new ProjectService.Project().InsertSmallFiles(request);
            return response;
        }

        public ProjectResponse InsertLargeFile(string id, string fileId)
        {
            var request = new ProjectInsertLargeFileRequest() { Id = id, LargeFileId = fileId };
            var response = new ProjectService.Project().InsertLargeFile(request);
            return response;
        }

        public ProjectResponse DeleteProject(string id)
        {
            var request = new ProjectRequest() { Id = id };
            var response = new ProjectService.Project().DeleteProject(request);
            return response;
        }
    }
}
