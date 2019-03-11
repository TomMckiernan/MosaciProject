using System;
using System.Collections.Generic;
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
    }
}
