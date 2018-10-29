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
            //If using MVC5
            if (String.IsNullOrEmpty(IndexedLocation))
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("The indexed location cannot be null or empty");
            }

            var model = new IndexedLocationModel();
            if (!model.IsPathValid(IndexedLocation))
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("The indexed location is not a valid directory");
            }

            // add test for the null check
            var response = model.UpdateIndexedLocation(client, IndexedLocation);
            if (String.IsNullOrEmpty(response.Error))
            {
                Response.StatusCode = (int)HttpStatusCode.OK;
                return Json("The indexed location request was valid");
            }
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json("The indexed location request was not valid");
        }

    }
}