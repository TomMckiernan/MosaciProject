using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mosaic.WebUI.Models
{
    public class GenerateMosaicModel
    {
        public string Generate(IMakerClient client, string id)
        {
            // Get project
            var project = client.ReadProject(id);

            //  Get all imagefileindexstructure files for the id
            var tileFilesId = project.Project.SmallFileIds.ToList();
            client.ReadAllImageFiles(tileFilesId);

            //  Get the image file index structure for the master image
            var masterFileId = project.Project.LargeFileId;
            client.ReadImageFile(masterFileId);


            return "";
        }

        //Structure for mosaic generator model
        //- properties needed
        //- Master image
        //- Tile image count
        //- Average colour for the tile files
        //- Master image average colour analysis
        //  - i.e the average colour for each tile in the image


        //  Get all imagefileindexstructure files for the id
        //  Get the image file index structure for the master image
        //  Therefore need request to get the current project structure
    }
}
