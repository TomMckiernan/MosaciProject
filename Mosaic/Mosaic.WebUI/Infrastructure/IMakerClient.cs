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

        ImageFileResponse ReadImageFile(string id);

        ImageFileIndexResponse ReadAllImageFiles(IList<string> ids);

        ProjectResponse CreateProject();

        ProjectResponse ReadProject(string id);

        ProjectMultipleResponse ReadAllProjects();

        ProjectResponse InsertSmallFiles(string id, IList<string> fileIds);

        ProjectResponse InsertLargeFile(string id, string fileId);

        ProjectResponse DeleteProject(string id);
    }
}
