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
            var response = new ImageFileIndexService.ImageFileIndex().ReadAllImageFileIndex(request);
            return response;
        }

        public async Task<ImageFileIndexUpdateResponse> UpdateImageFileIndex(string indexedLocation)
        {
            var request = new ImageFileIndexRequest() { IndexedLocation = indexedLocation };
            var response = await new ImageFileIndexService.ImageFileIndex().AnalyseImageFileIndex(request);
            return response;
        }

        public ImageFileResponse ReadImageFile(string id)
        {
            var request = new ImageFileRequest() { Id = id };
            var response = new ImageFileIndexService.ImageFileIndex().ReadImageFile(request);
            return response;
        }

        public ImageFileIndexResponse ReadAllImageFiles(IList<string> ids)
        {
            var request = new ImageFilesAllRequest();
            request.Ids.AddRange(ids);
            var response = new ImageFileIndexService.ImageFileIndex().ReadAllImageFiles(request);
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

        public ProjectResponse InsertSmallFiles(string id, IList<string> fileIds)
        {
            var request = new ProjectInsertSmallFilesRequest() { Id = id };
            request.SmallFileIds.AddRange(fileIds);
            var response = new ProjectService.Project().InsertSmallFiles(request);
            return response;
        }

        public ProjectResponse InsertLargeFile(string id, string fileId, string masterLocation)
        {
            var request = new ProjectInsertLargeFileRequest() { Id = id, LargeFileId = fileId, Location = masterLocation };
            var response = new ProjectService.Project().InsertLargeFile(request);
            return response;
        }

        public ProjectResponse InsertMosaicFile(string id, string mosaicLocation)
        {
            var request = new ProjectInsertMosaicFileRequest() { Id = id, Location = mosaicLocation };
            var response = new ProjectService.Project().InsertMosaicFile(request);
            return response;
        }

        public ProjectResponse InsertEdgeFile(string id, string edgeLocation)
        {
            var request = new ProjectInsertEdgeFileRequest() { Id = id, Location = edgeLocation };
            var response = new ProjectService.Project().InsertEdgeFile(request);
            return response;
        }

        public ProjectResponse DeleteProject(string id)
        {
            var request = new ProjectRequest() { Id = id };
            var response = new ProjectService.Project().DeleteProject(request);
            return response;
        }

        public ImageMosaicResponse Generate(string id, IList<ImageFileIndexStructure> tiles, ImageFileIndexStructure master, bool random, int width, int height, bool colourBlended, bool enhanced)
        {
            var request = new ImageMosaicRequest() { Id = id, Master = master, Random = random, TileWidth = width, TileHeight = height, ColourBlended = colourBlended, Enhanced = enhanced };
            request.Tiles.AddRange(tiles);
            var response = new ImageMosaicService.ImageMosaic().Generate(request);
            return response;
        }

        public MasterImageColourResponse ReadMasterFileColours(ImageFileIndexStructure file, int height, int width)
        {
            var request = new MasterImageColourRequest() { Master = file };
            var response = new ImageMosaicService.ImageMosaic().GetMasterImageAverageColours(request, height, width);
            return response;
        }

        public EdgeDetectionResponse PreviewEdges(string id, ImageFileIndexStructure master, int threshold)
        {
            var request = new EdgeDetectionRequest() { Id = id, Master = master, Threshold = threshold};
            var response = new EdgeDetectionService.EdgeDetection().PreviewEdges(request);
            return response;
        }

        public EdgeDetectionResponse GetEdgeCoordinates(string id, ImageFileIndexStructure master, int threshold)
        {
            var request = new EdgeDetectionRequest() { Id = id, Master = master, Threshold = threshold };
            var response = new EdgeDetectionService.EdgeDetection().GetEdgeCoordinates(request);
            return response;
        }
    }
}
