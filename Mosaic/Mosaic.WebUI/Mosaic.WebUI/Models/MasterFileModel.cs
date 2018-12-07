using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mosaic.WebUI.Models
{
    public class MasterFileModel
    {
        public ProjectResponse InsertMasterFile(IMakerClient client, string id, string fileId, string masterLocation)
        {
            var response = client.InsertLargeFile(id, fileId, masterLocation);
            return response;
        }
    }
}
