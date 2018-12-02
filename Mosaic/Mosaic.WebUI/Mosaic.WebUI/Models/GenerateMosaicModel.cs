using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mosaic.WebUI.Models
{
    public class GenerateMosaicModel
    {
        public string ProjectId { get; set; }

        public GenerateMosaicModel(string id)
        {
            ProjectId = id;
        }

        public ImageMosaicResponse Generate(IMakerClient client, string id)
        {
            // Get project
            if (String.IsNullOrEmpty(id))
            {
                return new ImageMosaicResponse() { Error = "Project Id cannot be null or empty" };
            }
            var project = client.ReadProject(id);
            if (!String.IsNullOrEmpty(project.Error))
            {
                return new ImageMosaicResponse() { Error = project.Error };
            }
            else if (project.Project.SmallFileIds.Count == 0 || String.IsNullOrEmpty(project.Project.LargeFileId))
            {
                return new ImageMosaicResponse() { Error = "Master or tile images not specified" };
            }
            //  Get all imagefileindexstructure files for the id
            var tileFilesId = project.Project.SmallFileIds.ToList();
            var tileFiles = client.ReadAllImageFiles(tileFilesId);

            //  Get the image file index structure for the master image
            var masterFileId = project.Project.LargeFileId;
            var masterFile = client.ReadImageFile(masterFileId);
            if (!String.IsNullOrEmpty(tileFiles.Error) || !String.IsNullOrEmpty(masterFile.Error) )
            {
                return new ImageMosaicResponse() { Error = "Master or tile images cannot be read" };
            }

            return client.Generate(tileFiles.Files, masterFile.File);
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
