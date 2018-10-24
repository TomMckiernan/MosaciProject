using Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Mosaic.WebUI.Models
{
    public class IndexedLocationModel
    {
        private readonly string INVALID_PATH = "Enter a valid directory path";

        public string IndexedLocation { get; set; }

        public string Error { get; set; }

        public bool IsIndexedLocationValid => IsPathValid();

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

        private bool IsPathValid()
        {
            try
            {
                Path.GetFullPath(IndexedLocation);
                return true;
            }
            catch (Exception)
            {
                Error = INVALID_PATH;
                return false;
            }

        }
    }
}
