using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public interface IMakerClient
    {
        IndexedLocationResponse UpdateIndexedLocation(string indexedLocation);

        IndexedLocationResponse ReadIndexedLocation();

        ImageFileIndexResponse ReadImageFileIndex(string indexedLocation);
        
        Task<ImageFileIndexUpdateResponse> UpdateImageFileIndex(string indexedLocation);

    }
}
