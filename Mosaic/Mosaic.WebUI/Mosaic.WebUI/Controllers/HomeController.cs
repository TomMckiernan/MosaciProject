using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Mosaic.WebUI.Models;

namespace Mosaic.WebUI.Controllers
{
    public class HomeController : Controller
    {
        IMakerClient client = new MakerClient();

        public IActionResult Index()
        {
            var model = new ProjectModel();
            model.ReadAllProjects(client);
            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create()
        {
            ViewData["Message"] = "Begin the creation of your Mosaic Image";

            var response = new ProjectModel().CreateProject(client);
            if (String.IsNullOrEmpty(response.Error))
            {
                Response.StatusCode = (int)HttpStatusCode.OK;
                return Json(response);
            }
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(response.Error);
        }

        public IActionResult SelectProject(string Id)
        {
            var model = new IndexedLocationModel(Id);
            model.RequestIndexedLocation(client);

            var progress = client.ReadProject(Id).Project.Progress;
            if (progress == ProjectStructure.Types.State.Smalladded|| progress == ProjectStructure.Types.State.Completed)
            {
                return new TileController().Generate(Id);
            }
            else if (progress == ProjectStructure.Types.State.Largeadded)
            {
                return new MasterController().ImportTiles(Id);
            }
            else
            {
                return ImportMaster(Id);
            }
        }

        public IActionResult ImportMaster(string Id)
        {
            var model = new IndexedLocationModel(Id);
            model.RequestIndexedLocation(client);

            return View("ImportMaster", model);
        }

        [HttpPost]
        public IActionResult Delete(string id)
        {
            var model = new DeleteProjectModel();
            model.DeleteProject(client, id);
            if (String.IsNullOrEmpty(model.Error))
            {
                Response.StatusCode = (int)HttpStatusCode.OK;
                return Json("Delete project request was valid");
            }
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(model.Error);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
