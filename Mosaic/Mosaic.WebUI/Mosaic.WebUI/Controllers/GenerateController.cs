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
        public ActionResult GenerateMosaic(string id)
        {
            var model = new GenerateMosaicModel(id);
            var response = model.Generate(client, id);
            if (String.IsNullOrEmpty(response.Error))
            {
                var image = new ViewImageModel(id);
                image.CopyImage(response.Location);
                Response.StatusCode = (int)HttpStatusCode.OK;
                return Json(image.FilePath);
            }
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(response.Error);
        }
    }
}