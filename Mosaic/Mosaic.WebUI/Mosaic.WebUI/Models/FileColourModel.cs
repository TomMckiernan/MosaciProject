using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Mosaic.WebUI.Models
{
    public class FileColourModel
    {
        public List<Color> PreDefinedColours { get; set; }

        //'Red', 'Orange Red', 'Orange', 'Orange Yellow', 'Yellow', 
        //'Yellow Green', 'Green', 'Blue Green', 'Blue', 'Blue Violet', 'Violet', 
        //'Red Violet', 'Black', 'Dark Gray', 'Gray', 'Silver', 'White']

        public FileColourModel()
        {
            PreDefinedColours = new List<Color>()
            {
                Color.Red, Color.OrangeRed, Color.Orange, Color.Gold, Color.Yellow, Color.YellowGreen,
                Color.Green, Color.Aqua, Color.Blue, Color.BlueViolet, Color.Violet, Color.MediumVioletRed,
                Color.Black, Color.DarkGray, Color.Silver, Color.White
            };        
        }


        public List<Color> FindClosestColour(List<Color> colours)
        {
            var bestFitColours = colours.Select(x => GetBestColour(x)).ToList();
            return bestFitColours;
        }

        private Color GetBestColour(Color color)
        {
            double difference;
            double bestDifference = double.MaxValue;
            Color bestColor;


            for (int i = 0; i < PreDefinedColours.Count(); i++)
            {
                difference = GetDifference(color, PreDefinedColours[i]);
                if (difference < bestDifference)
                {
                    bestDifference = difference;
                    bestColor = PreDefinedColours[i];
                }
            }

            return bestColor;
        }

        private double GetDifference(Color colour, Color preDefinedColour)
        {
            double difference;

            var r = Math.Abs(colour.R - preDefinedColour.R);
            var g = Math.Abs(colour.G - preDefinedColour.G);
            var b = Math.Abs(colour.B - preDefinedColour.B);

            difference = r + g + b;
            difference /= 3 * 255;
            return difference;
        }
    }
}
