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
        public ActionResult ReadImageFileIndex(string indexedLocation, string id)
        {
            var model = new ImageFileIndexModel();

            model.ReadImageFileIndex(client, indexedLocation, id);

            if (String.IsNullOrEmpty(model.Error))
            {
                Response.StatusCode = (int)HttpStatusCode.OK;
                return Json(model.Files);
            }

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(model.Error);
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

        [HttpPost]
        public ActionResult ImportFiles(string id, IEnumerable<string> fileIds)
        {
            var model = new SmallFilesModel();

            var response = model.InsertSmallFiles(client, id, fileIds.ToList());
            if (String.IsNullOrEmpty(response.Error))
            {
                Response.StatusCode = (int)HttpStatusCode.OK;
                return Json("The update small file ids request was valid");
            }
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(response.Error);
        }

        public ActionResult ImportMaster(string Id)
        {
            var model = new IndexedLocationModel(Id);
            model.RequestIndexedLocation(client);

            return View("ImportMaster", model);
        }
    }
}