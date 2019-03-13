using Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Utility;

namespace Mosaic.WebUI.Models
{
    public class GenerateMosaicColoursModel
    {
        public Dictionary<string, int> TileImageColorDictionary { get; set; }
        public string JsonTileImageColours { get; set; }
        public string JsonTileImageHexColours { get; set; }
        public Dictionary<string, int> MasterImageColorDictionary { get; set; }
        public string JsonMasterImageColours { get; set; }
        public string JsonMasterImageHexColours { get; set; }
        public double LibrarySuitability { get; set; }

        // Empty constructor for testing purposes
        public GenerateMosaicColoursModel()
        {
        }

        public GenerateMosaicColoursModel(IMakerClient client, ProjectResponse project, int height, int width)
        {
            ReadMasterColours(client, project, height, width);
            ReadTileColours(client, project);
            LibrarySuitability = CalulateLibrarySuitability();
        }

        public void ReadMasterColours(IMakerClient client, ProjectResponse project, int height, int width)
        {
            // Convert the ARGB values from master file into Color objects
            var master = client.ReadImageFile(project.Project.LargeFileId);
            var masterARGB = client.ReadMasterFileColours(master.File, height, width);
            var masterColours = masterARGB.AverageTileARGB.Select(x => Color.FromArgb(x)).ToList();

            // Find the closest standard Color object for each color in master file colours
            var masterClosestColours = new FileColourModel().FindClosestColour(masterColours);
            var masterClosestColoursHex = masterClosestColours.Select(x => x.ToHex()).ToList();

            var masterFileDictionary = ConvertColourListToDictionary(masterClosestColoursHex);

            MasterImageColorDictionary = masterFileDictionary;
            JsonMasterImageColours = JsonConvert.SerializeObject(masterFileDictionary, Formatting.Indented);
            JsonMasterImageHexColours = JsonConvert.SerializeObject(masterFileDictionary.Keys, Formatting.Indented);
        }

        public void ReadTileColours(IMakerClient client, ProjectResponse project)
        {
            // At the moment is takes into account the four quadrant averages of the file
            // rather than just one average which represents the whole tile.
            // Convert the ARGB values stored in project into Color objects
            var tiles = client.ReadAllImageFiles(project.Project.SmallFileIds);
            var tilesColours = tiles.Files.Select(x => Color.FromArgb(x.Data.AverageWhole)).ToList();

            // Find the closest standard Color object for each color in tile files colours
            var tilesClosestColours = new FileColourModel().FindClosestColour(tilesColours);
            var tilesFilesClosestColoursHex = tilesClosestColours.Select(x => x.ToHex()).ToList();

            var tilesDictionary = ConvertColourListToDictionary(tilesFilesClosestColoursHex);

            TileImageColorDictionary = tilesDictionary;
            JsonTileImageColours = JsonConvert.SerializeObject(tilesDictionary, Formatting.Indented);
            JsonTileImageHexColours = JsonConvert.SerializeObject(tilesDictionary.Keys, Formatting.Indented);
        }

        private Dictionary<string, int> ConvertColourListToDictionary(IList<string> list)
        {
            Dictionary<string, int> colours = new Dictionary<string, int>();
            foreach (var value in list)
            {
                if (!colours.ContainsKey(value))
                {
                    colours.Add(value, 1);
                }
                else
                {
                    colours[value]++;
                }
            }
            return colours;
        }

        public double CalulateLibrarySuitability()
        {
            // Compare the master colour library with the tile colour library
            // For every colour in the master image
            double count = 0;
            foreach (var item in MasterImageColorDictionary)
            {
                if (TileImageColorDictionary.ContainsKey(item.Key))
                {
                    count++;
                }
            }

            if (MasterImageColorDictionary.Keys.Count == 0)
            {
                return 0;
            }
            return (count / MasterImageColorDictionary.Keys.Count) * 100;
            // check with the tile images if there is a standard colour which 
            // matches the standard colour in the master image
        }
    }
}

