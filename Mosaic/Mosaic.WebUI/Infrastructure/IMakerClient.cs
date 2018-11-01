using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
