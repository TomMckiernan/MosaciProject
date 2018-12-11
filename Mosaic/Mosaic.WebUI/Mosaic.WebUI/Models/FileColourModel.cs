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
                Color.Black, Color.DarkGray, Color.Gray, Color.Silver, Color.White
            };        
        }


        public List<Color> FindClosestColour(List<Color> colours)
        {


            return colours;

            // for each color in colours call method which
            // finds best color fit to list of predefined colors
        }

        private Color best(Color color)
        {
            int r = (int)color.R - color.R,
                g = (int)color.G - color.G,
                b = (int)color.B - color.B;
            //return (r * r + g * g + b * b) <= threshold * threshold;
            return color;
        }
    }
}
