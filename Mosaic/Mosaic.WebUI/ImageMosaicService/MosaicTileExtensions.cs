using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageMosaicService
{
    public static class MosaicTileExtensions
    {
        public static double GetAverage(this List<MosaicTile> mosaicTiles)
        {
            double count = 0;
            foreach (var dif in mosaicTiles)
            {
                count += dif.Difference;
            }
            
            return (mosaicTiles.Count == 0) ? count : count / mosaicTiles.Count;
        }

        public static List<String> GetUniqueImages(this List<MosaicTile> mosaicTiles)
        {
            var uniqueFiles = new HashSet<string>();
            foreach (var tile in mosaicTiles)
            {
                uniqueFiles.Add(tile.Image);
                if (tile.InQuadrants)
                {
                    uniqueFiles.Add(tile.TLTile.Image);
                    uniqueFiles.Add(tile.TRTile.Image);
                    uniqueFiles.Add(tile.BLTile.Image);
                    uniqueFiles.Add(tile.BRTile.Image);
                }
            }
            return uniqueFiles.ToList();
        }
    }
}
