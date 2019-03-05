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

        ProjectResponse InsertLargeFile(string id, string fileId, string masterLocation);

        ProjectResponse InsertMosaicFile(string id, string mosaicLocation);

        ProjectResponse InsertEdgeFile(string id, string edgeLocation, List<PixelCoordinates> edges);

        ProjectResponse DeleteProject(string id);

        ImageMosaicResponse Generate(string id, IList<ImageFileIndexStructure> tiles, ImageFileIndexStructure master, bool random, int width, int height, bool colourBlended, bool enhanced);

        MasterImageColourResponse ReadMasterFileColours(ImageFileIndexStructure file, int height, int width);

        EdgeDetectionResponse PreviewEdges(string id, ImageFileIndexStructure master, int threshold);
    }
}
