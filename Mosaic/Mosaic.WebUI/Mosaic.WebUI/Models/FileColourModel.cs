using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Utility;

namespace Mosaic.WebUI.Models
{
    public class FileColourModel
    {
        public List<Color> PreDefinedColours { get; set; }

        public FileColourModel()
        {
            PreDefinedColours = new List<Color>()
            {
                Color.Red, Color.OrangeRed, Color.Orange, Color.Gold, Color.Yellow, Color.YellowGreen,
                Color.Green, Color.Teal, Color.Blue, Color.BlueViolet, Color.Violet, Color.MediumVioletRed,
                Color.Black, Color.DarkGray, Color.Silver, Color.White
            };        
        }


        public List<Color> FindClosestColour(List<Color> colours)
        {
            var bestFitColours = colours.Select(x => GetBestColour(x)).ToList();
            return bestFitColours;
        }

        public Color FindClosestColour(Color colour)
        {
            var bestFitColours = GetBestColour(colour);
            return bestFitColours;
        }

        private Color GetBestColour(Color color)
        {
            double difference;
            double bestDifference = double.MaxValue;
            Color bestColor;


            for (int i = 0; i < PreDefinedColours.Count(); i++)
            {
                difference = color.GetDifference(PreDefinedColours[i]);
                if (difference < bestDifference)
                {
                    bestDifference = difference;
                    bestColor = PreDefinedColours[i];
                }
            }

            return bestColor;
        }
    }
}
