using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ImageMosaicService
{
    public static class MosaicTileExtensions
    {
        public static double GetAverage(this List<MosaicTile> mosaicTiles, int threshold)
        {
            if (mosaicTiles.Count == 0)
            {
                return 0;
            }
            var differences = mosaicTiles.Select(x => x.Difference).OrderBy(x => x).ToList();
            var index = Convert.ToInt32(mosaicTiles.Count * ((double)threshold / 100));
            if (index >= differences.Count)
            {
                index = differences.Count - 1;
            }
            var result = differences[index];
            return result;
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

        public static void StoreResizedBitmap(this List<MosaicTile> mosaicTiles, List<Tuple<string, Bitmap>> resizeFiles)
        {
            foreach (var tile in mosaicTiles)
            {
                tile.Bitmap = resizeFiles.Where(x => x.Item1 == tile.Image).Select(y => y.Item2).First();
                if (tile.InQuadrants)
                {
                    tile.TLTile.Bitmap = resizeFiles.Where(x => x.Item1 == tile.Image).Select(y => y.Item2).First();
                    tile.TRTile.Bitmap = resizeFiles.Where(x => x.Item1 == tile.Image).Select(y => y.Item2).First();
                    tile.BLTile.Bitmap = resizeFiles.Where(x => x.Item1 == tile.Image).Select(y => y.Item2).First();
                    tile.BRTile.Bitmap = resizeFiles.Where(x => x.Item1 == tile.Image).Select(y => y.Item2).First();
                }
            }
        }

        public static List<MosaicTile> CalculateEdgeTiles(this List<MosaicTile> tiles, List<PixelCoordinates> coords, int tileWidth, int tileHeight)
        {
            foreach (var co in coords)
            {
                int tileX = co.X / tileWidth;
                int tileY = co.Y / tileHeight;
                var ti = tiles.Where(x => x.X == tileX).Where(y => y.Y == tileY).FirstOrDefault();
                if (ti != null)
                {
                    ti.IsEdge = true;
                }
            }
            return tiles;
        }
    }
}
