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
        private readonly string INVALID_PATH_STRUCTURE = "Indexed location must have directory path structure";
        private readonly string INVALID_PATH_LENGTH = "Indexed location is required";

        public string IndexedLocation { get; set; }

        public string Error { get; set; }

        public bool IsIndexedLocationValid => IsPathValid(IndexedLocation);

        public IndexedLocationModel()
        {

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
            var response = client.UpdateIndexedLocation(indexedLocation);
            return response;
        }

        public bool IsPathValid(string location)
        {
            return Directory.Exists(location);
        }
    }
}
