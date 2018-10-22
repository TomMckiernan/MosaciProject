using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mosaic.WebUI.Models
{
    public class IndexedLocationModel
    {
        public string IndexedLocation { get; set; }

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
          
        }
    }
}
