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
    public class CreateController : Controller
    {
        IMakerClient client = new MakerClient();

        [HttpPost]
        public ActionResult UpdateIndexedLocation(string indexedLocation)
        {
            var model = new IndexedLocationModel();

            var response = model.UpdateIndexedLocation(client, indexedLocation);
            if (String.IsNullOrEmpty(response.Error))
            {
                Response.StatusCode = (int)HttpStatusCode.OK;
                return Json("The indexed location request was valid");
            }
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(response.Error);
        }

        [HttpPost]
        public ActionResult ReadImageFileIndex(string indexedLocation)
        {
            var model = new ImageFileIndexModel();

            var response = model.ReadImageFileIndex(client, indexedLocation);

            if (String.IsNullOrEmpty(response.Error))
            {
                Response.StatusCode = (int)HttpStatusCode.OK;
                return Json(response.Files);
            }

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(response.Error);
        }

        [HttpPost]
        public ActionResult UpdateImageFileIndex(string indexedLocation)
        {
            var model = new ImageFileIndexModel();

            var response = model.UpdateImageFileIndex(client, indexedLocation);
            if (String.IsNullOrEmpty(response.Result.Error))
            {
                Response.StatusCode = (int)HttpStatusCode.OK;
                return Json("The update image file index request was valid");
            }
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(response.Result.Error);
        }
    }
}