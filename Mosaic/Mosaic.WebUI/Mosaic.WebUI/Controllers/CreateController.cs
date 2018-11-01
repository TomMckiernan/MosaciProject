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
        public ActionResult UpdateIndexedLocation(string IndexedLocation)
        {
            var model = new IndexedLocationModel();

            var response = model.UpdateIndexedLocation(client, IndexedLocation);
            if (String.IsNullOrEmpty(response.Error))
            {
                Response.StatusCode = (int)HttpStatusCode.OK;
                return Json("The indexed location request was valid");
            }
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(response.Error);
        }

        [HttpPost]
        public ActionResult ReadImageFileIndex(string IndexedLocation)
        {
            var model = new ImageFileIndexModel();

            var response = model.ReadImageFileIndex(client, IndexedLocation);

            // Instead of return a proto may be appropriate to deserialize into model

            if (String.IsNullOrEmpty(response.Error))
            {
                Response.StatusCode = (int)HttpStatusCode.OK;
                return Json("The read image file index request was valid");
            }

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(response.Error);
        }

        [HttpPost]
        public ActionResult UpdateImageFileIndex(string IndexedLocation)
        {
            var model = new ImageFileIndexModel();

            var response = model.UpdateImageFileIndex(client, IndexedLocation);
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