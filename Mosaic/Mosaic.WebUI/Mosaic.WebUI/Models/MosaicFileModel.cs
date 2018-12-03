using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mosaic.WebUI.Models
{
    public class MosaicFileModel
    {
        public ProjectResponse InsertMosaicFile(IMakerClient client, string id, string mosaicLocation)
        {
            var response = client.InsertMosaicFile(id, mosaicLocation);
            return response;
        }
    }
}
