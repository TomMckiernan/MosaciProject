using System;

namespace Infrastructure
{
    public class MakerClient : IMakerClient
    {
        public IndexedLocationResponse UpdateIndexedLocation(string indexedLocation)
        {
            return new IndexedLocationResponse { IndexedLocation = "" };
        }
    }
}
