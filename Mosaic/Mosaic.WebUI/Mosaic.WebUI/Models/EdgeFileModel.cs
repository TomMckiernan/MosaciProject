﻿using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mosaic.WebUI.Models
{
    public class EdgeFileModel
    {
        public ProjectResponse InsertEdgeFile(IMakerClient client, string id, string edgeLocation)
        {
            var response = client.InsertEdgeFile(id, edgeLocation);
            return response;
        }
    }
}
