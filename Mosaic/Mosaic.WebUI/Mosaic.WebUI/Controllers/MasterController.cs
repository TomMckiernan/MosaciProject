using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mosaic.WebUI.Models;

namespace Mosaic.WebUI.Controllers
{
    public class MasterController : Controller
    {
        IMakerClient client = new MakerClient();

        [HttpPost]
        public ActionResult ReadImageFileIndex(string indexedLocation, string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Project id cannot be null or empty");
            }
            var model = new ImageFileIndexModel();
            model.ReadImageFileIndexMaster(client, indexedLocation);

            if (String.IsNullOrEmpty(model.Error))
            {
                Response.StatusCode = (int)HttpStatusCode.OK;
                return Json(model.Files);
            }

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(model.Error);
        }

        [HttpPost]
        public ActionResult ImportFile(string id, string fileId)
        {
            var model = new MasterFileModel();

            var response = model.InsertMasterFile(client, id, fileId);
            if (String.IsNullOrEmpty(response.Error))
            {
                Response.StatusCode = (int)HttpStatusCode.OK;
                return Json("The update large file id request was valid");
            }
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(response.Error);
        }

        public ActionResult Generate(string Id)
        {
            var model = new IndexedLocationModel(Id);
            model.RequestIndexedLocation(client);

            return View("Generate", model);
        }
    }
}