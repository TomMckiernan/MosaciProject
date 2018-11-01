using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mosaic.WebUI.Models
{
    public class ImageFileIndexModel
    {
        public ImageFileIndexResponse ReadImageFileIndex(IMakerClient client, string indexedLocation)
        {
            return client.ReadImageFileIndex(indexedLocation);
        }

        public async Task<ImageFileIndexUpdateResponse> UpdateImageFileIndex(IMakerClient client, string indexedLocation)
        {
            var response = await client.UpdateImageFileIndex(indexedLocation);

            if (String.IsNullOrEmpty(response.Error))
            {
                return response;
            }

            return response;
        }
    }
}
