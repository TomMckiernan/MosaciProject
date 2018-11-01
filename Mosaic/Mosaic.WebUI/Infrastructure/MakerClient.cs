using System;
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
    }
}
