using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mosaic.WebUI.Models
{
    public class ProjectModel
    {
        public ProjectResponse CreateProject(IMakerClient client)
        {
            var response = client.CreateProject();
            return response;
        }

        public ProjectMultipleResponse ReadAllProjects(IMakerClient client)
        {
            var response = client.ReadAllProjects();
            return response;
        }
    }
}
