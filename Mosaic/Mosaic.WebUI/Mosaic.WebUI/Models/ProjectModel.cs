using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mosaic.WebUI.Models
{
    public class ProjectModel
    {
        public IList<ProjectStructure> Projects{ get; set; }

        public string Error { get; set; }

        public ProjectResponse CreateProject(IMakerClient client)
        {
            var response = client.CreateProject();
            return response;
        }

        public void ReadAllProjects(IMakerClient client)
        {
            var response = client.ReadAllProjects();
            var projectCards = response.Projects.Select(x => new ProjectCardModel(client, x)).ToList();
            Projects = response.Projects.ToList();
            Error = response.Error;
        }
    }
}
