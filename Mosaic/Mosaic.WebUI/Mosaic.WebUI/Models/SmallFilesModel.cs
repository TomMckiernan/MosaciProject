using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mosaic.WebUI.Models
{
    public class SmallFilesModel
    {
        public ProjectResponse InsertSmallFiles(IMakerClient client, string id, IList<string> fileIds)
        {
            var response = client.InsertSmallFiles(id, fileIds);
            return response;
        }
    }
}
