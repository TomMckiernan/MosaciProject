using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Mosaic.WebUI.Models;

namespace Mosaic.WebUI.Controllers
{
    public class GenerateController : Controller
    {
        IMakerClient client = new MakerClient();
        string EDGE_IMAGE_LOCATION = "wwwroot\\images\\edges\\";

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GenerateMosaic(string id, bool random, int tileWidth, int tileHeight, bool colourBlended, bool enhanced)
        {
            // Generate the mosaic passing the project id and whether to randomise tile selection
            var model = new GenerateMosaicModel();
            var response = model.Generate(client, id, random, tileWidth, tileHeight, colourBlended, enhanced);
            if (String.IsNullOrEmpty(response.Error))
            {
                // copy generated image to root directory to allow it display
                var image = new ViewImageModel();
                image.CopyImage(response.Location);
                // update project status and store location
                var insertResponse = new MosaicFileModel().InsertMosaicFile(client, id, image.ImagePath);
                if (String.IsNullOrEmpty(insertResponse.Error))
                {
                    Response.StatusCode = (int)HttpStatusCode.OK;
                    return Json(image.ImagePath);
                }
            }
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(response.Error);
        }

        [HttpPost]
        public ActionResult UpdateColourAnalysis(string Id, int height, int width)
        {
            // Generate the mosaic model so enable update to the colour analysis of library
            var model = new GenerateMosaicModel();
            model.ReadProjectData(client, Id, true, height, width);
            if (model.ColoursModel != null)
            {
                Response.StatusCode = (int)HttpStatusCode.OK;
                return Json(model.ColoursModel);
            }
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json("Error in generating colour model");
        }


        [HttpPost]
        public ActionResult PreviewEdges(string id, int threshold)
        {
            var model = new GenerateMosaicModel();
            var response = model.PreviewEdges(client, id, threshold);
            if (String.IsNullOrEmpty(response.Error))
            {
                // copy generated image to root directory to allow it display
                var image = new ViewImageModel(EDGE_IMAGE_LOCATION);
                image.CopyImage(response.Location);
                // update project status and store location
                var insertResponse = new EdgeFileModel().InsertEdgeFile(client, id, image.ImagePath, response.Edges.ToList());
                if (String.IsNullOrEmpty(insertResponse.Error))
                {
                    Response.StatusCode = (int)HttpStatusCode.OK;
                    return Json(image.ImagePath);
                }
            }
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(response.Error);
        }
    }
}