using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Mosaic.WebUI.Controllers
{
    public class CreateController : Controller
    {
        // POST: Create2/Create
        [HttpPost]
        public ActionResult UpdateIndexedLocation(string IndexedLocation)
        {

                //If using MVC5
                return new StatusCodeResult(200);

        }

    }
}