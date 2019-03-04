using MongoDB.Bson;
using MongoDB.Bson.Serialization;
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

        public static double GetDifference(this Color color, Color otherColor)
        {
            int r, g, b;
            double difference;

            r = otherColor.R;
            g = otherColor.G;
            b = otherColor.B;

            r = Math.Abs(color.R - r);
            g = Math.Abs(color.G - g);
            b = Math.Abs(color.B - b);

            difference = r + g + b;
            difference /= 3 * 255;
            return difference;
        }

        public static PixelCoordinates toPixelCoordinate(this BsonValue value)
        {
            var coord = BsonSerializer.Deserialize<PixelCoordinates>(value.ToJson());
            return coord;
        }
    }
}
