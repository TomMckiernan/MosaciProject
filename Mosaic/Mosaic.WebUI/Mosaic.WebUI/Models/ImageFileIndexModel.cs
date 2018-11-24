using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mosaic.WebUI.Models
{
    public class ImageFileIndexModel
    {
        public List<ImageFileIndexStructure> Files { get; set; }

        public string Error{ get; set; }

        // Returns all files in indexed location that have not been imported
        public void ReadImageFileIndex(IMakerClient client, string indexedLocation, string id)
        {
            var indexedFiles = client.ReadImageFileIndex(indexedLocation);
            var response = client.ReadProject(id);
            if (response == null || response.Project == null)
            {
                Error = "Invalid request project does not exist";
            }
            else if (response.Project.SmallFileIds == null || response.Project.SmallFileIds.Count == 0)
            {
                Files = indexedFiles.Files.ToList();
                Error = indexedFiles.Error;
            }
            else
            {
                Files = indexedFiles.Files.Where(x => !response.Project.SmallFileIds.Contains(x.Id)).ToList();
                Error = indexedFiles.Error;
            }
        }

        public async Task<ImageFileIndexUpdateResponse> UpdateImageFileIndex(IMakerClient client, string indexedLocation)
        {
            var response = await client.UpdateImageFileIndex(indexedLocation);
            return response;
        }
    }
}
