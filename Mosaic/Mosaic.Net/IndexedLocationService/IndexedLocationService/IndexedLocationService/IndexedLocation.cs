using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace IndexedLocationService
{
    public class IndexedLocation
    {
        public ObjectId _id { get; set; }

        public string Location { get; set; }
    }
}
