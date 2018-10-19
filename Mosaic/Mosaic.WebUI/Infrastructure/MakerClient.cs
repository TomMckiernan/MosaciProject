using System;

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
    }
}
