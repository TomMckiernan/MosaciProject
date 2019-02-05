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

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GenerateMosaic(string id, bool random, int tileWidth, int tileHeight, bool colourBlended)
        {
            // Generate the mosaic passing the project id and whether to randomise tile selection
            var model = new GenerateMosaicModel();
            var response = model.Generate(client, id, random, tileWidth, tileHeight, colourBlended);
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
    }
}