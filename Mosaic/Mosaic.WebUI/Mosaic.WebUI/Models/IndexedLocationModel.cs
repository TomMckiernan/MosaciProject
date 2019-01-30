using Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Mosaic.WebUI.Models
{
    public class IndexedLocationModel
    {
        public string IndexedLocation { get; set; }

        public string Error { get; set; }

        public bool IsIndexedLocationValid => IsPathValid(IndexedLocation);

        public string ProjectId {get; private set;}

        public Tuple<string, ProjectStructure.Types.State> PartialModel { get; set; }

        public IndexedLocationModel(IMakerClient client, string projectId = "Default")
        {
            var project = ProjectErrorCheck(client, projectId);
            if (String.IsNullOrEmpty(project.Error))
            {
                ProjectId = projectId;
                PartialModel = new Tuple<string, ProjectStructure.Types.State>(ProjectId, project.Project.Progress);
            }
        }

        private ProjectResponse ProjectErrorCheck(IMakerClient client, string id)
        {
            // Get project
            if (String.IsNullOrEmpty(id))
            {
                return new ProjectResponse() { Error = "Project Id cannot be null or empty" };
            }
            var project = client.ReadProject(id);
            return project;
        }

        public void RequestIndexedLocation(IMakerClient client)
        {
            var response = client.ReadIndexedLocation();
            if (string.IsNullOrEmpty(response.Error))
            {
                IndexedLocation = response.IndexedLocation;
            }
            Error = response.Error;
        }

        public IndexedLocationResponse UpdateIndexedLocation(IMakerClient client, string indexedLocation)
        {
            var response = new IndexedLocationResponse();
            if (String.IsNullOrEmpty(indexedLocation))
            {
                response.Error = "The indexed location cannot be null or empty";
                return response;
            }

            if (!IsPathValid(indexedLocation))
            {
                response.Error = "The indexed location is not a valid directory";
                return response;
            }

            response = client.UpdateIndexedLocation(indexedLocation);
            return response;
        }

        public bool IsPathValid(string location)
        {
            return Directory.Exists(location);
        }
    }
}
