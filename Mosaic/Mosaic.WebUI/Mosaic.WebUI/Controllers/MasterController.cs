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
        string MASTER_IMAGE_LOCATION = "wwwroot\\images\\master\\";

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
        public ActionResult ImportFile(string id, string fileId, string filePath)
        {
            // copy master image to root directory to allow it display
            // set the name of the local master image to the project id
            var viewImage = ViewImage(filePath, id);

            // update project status and store master file id
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

        [HttpPost]
        public ActionResult ViewImage(string filepath, string fileName)
        {
            var model = new ViewImageModel(MASTER_IMAGE_LOCATION);
            model.CopyImage(filepath, fileName);
            if (String.IsNullOrEmpty(model.Error))
            {
                Response.StatusCode = (int)HttpStatusCode.OK;
                return Json(model.FilePath);
            }
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(model.Error);
        }

        public ActionResult ImportTiles(string Id)
        {
            var model = new IndexedLocationModel(Id);
            model.RequestIndexedLocation(client);

            return View("ImportTiles", model);
        }
    }
}