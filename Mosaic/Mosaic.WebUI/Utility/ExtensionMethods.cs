using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace Utility
{
    public static class ExtensionMethods
    {
        public static IndexedLocationStructure ConvertFromBsonDocument(this IndexedLocationStructure value, BsonDocument doc)
        {
            value.Id = doc.GetValue("_id").ToString();
            value.IndexedLocation = doc.GetValue("IndexedLocation").ToString();
            return value;
        }
    }
}
