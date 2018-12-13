using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Drawing;
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

        public static string ToHex(this Color c)
        {
            return "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }

        public static List<Color> ToList(this Color[,] colors)
        {
            List<Color> list = new List<Color>();

            foreach (var color in colors)
            {
                list.Add(color);
            }
            return list;
        }
    }
}
