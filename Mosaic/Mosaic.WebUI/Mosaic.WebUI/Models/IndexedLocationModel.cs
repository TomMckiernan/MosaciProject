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

        public IndexedLocationModel(string projectId = "Default")
        {
            ProjectId = projectId;
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
