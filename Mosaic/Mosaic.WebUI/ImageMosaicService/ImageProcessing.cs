﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using Utility;
using System.Drawing.Imaging;

namespace ImageMosaicService
{
    public class ImageProcessing
    {
        private int quality = 6, resizeHeight = 119, resizeWidth = 119;
        private Size tileSize;
        private List<ImageInfo> library;

        public enum Target
        {
            Whole,
            TL,
            TR,
            BL,
            BR
        }

        public ImageProcessing(int tileHeight = 10, int tileWidth = 10)
        {
            tileSize = new Size(tileHeight, tileWidth);
        }

        public Bitmap Resize(string srcFile, int height = 119, int width = 119)
        {
            if (!File.Exists(srcFile))
                return null;

            using (var scrBitmap = Bitmap.FromFile(srcFile))
            {
                var b = new Bitmap(height, width);
                
                using (var g = Graphics.FromImage((Image)b))
                {
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                    g.DrawImage(scrBitmap, 0, 0, height, width);
                    g.Dispose();
                }

                return b;
            }
        }

        public ImageInfo GetAverageColor(Bitmap bmp, string filePath)
        {
            var imageInfo = new ImageInfo(filePath);
            
            int halfX = bmp.Width / 2;
            int halfY = bmp.Width / 2;

            imageInfo.AverageTL = getAverageColor(new Rectangle(0, 0, halfX, halfY), bmp, quality);
            imageInfo.AverageTR = getAverageColor(new Rectangle(halfX, 0, halfX, halfY), bmp, quality);
            imageInfo.AverageBL = getAverageColor(new Rectangle(0, halfY, halfX, halfY), bmp, quality);
            imageInfo.AverageBR = getAverageColor(new Rectangle(halfX, halfY, halfX, halfY), bmp, quality);
            imageInfo.AverageWhole = getAverageColor(new Rectangle(0, 0, bmp.Width, bmp.Width), bmp, quality);

            return imageInfo;
        }

        private Color getAverageColor(Rectangle area, Bitmap bmp, int quality)
        {
            Int64 r = 0, g = 0, b = 0;
            var color = Color.Empty;
            int p = 0;

            var end = new Point(area.X + area.Width, area.Y + area.Height);

            for (int x = area.X = 0; x < end.X; x += quality)
            {
                for (int y = area.Y; y < end.Y; y += quality)
                {
                    color = bmp.GetPixel(x, y);
                    r += color.R;
                    g += color.G;
                    b += color.B;
                    p++;
                }
            }

            return Color.FromArgb(255, (int)(r / p), (int)(g / p), (int)(b / p));
        }

        public Rectangle CreateQuadrantRectangle(int x, int y, int width, int height, Target target)
        {
            var widthHalf = width / 2;
            var heightHalf = height / 2;

            switch (target)
            {
                case Target.Whole:
                    return new Rectangle(x, y, width, height);
                case Target.TL:
                    return new Rectangle(x, y, widthHalf, heightHalf);
                case Target.TR:
                    return new Rectangle(x + widthHalf, y, widthHalf, heightHalf);
                case Target.BL:
                    return new Rectangle(x, y + heightHalf, widthHalf, heightHalf);
                case Target.BR:
                    return new Rectangle(x + widthHalf, y + heightHalf, widthHalf, heightHalf);
                default:
                    return new Rectangle();
            }
        }

        public MosaicTileColour[,] CreateMap(Bitmap img)
        {
            int horizontalTiles = (int)img.Width / tileSize.Width;
            int verticalTiles = (int)img.Height / tileSize.Height;

            var colorMap = new MosaicTileColour[horizontalTiles, verticalTiles];

            int tileWidth = (img.Width - img.Width % horizontalTiles) / horizontalTiles;
            int tileHeight = (img.Height - img.Height % verticalTiles) / verticalTiles;
        
            // Explore option of making this a parallel for loop
            for (int x = 0; x < horizontalTiles; x++)
            {
                for (int y = 0; y < verticalTiles; y++)
                {
                    var xstart = tileWidth * x;
                    var ystart = tileHeight * y;
                    var tileColour = new MosaicTileColour();

                    // These are the opertaions to perform if we want to subdivide tile
                    tileColour.AverageTL = getAverageTileColor(CreateQuadrantRectangle(xstart, ystart, tileWidth, tileHeight, Target.TL), img, quality);
                    tileColour.AverageTR = getAverageTileColor(CreateQuadrantRectangle(xstart, ystart, tileWidth, tileHeight, Target.TR), img, quality);
                    tileColour.AverageBL = getAverageTileColor(CreateQuadrantRectangle(xstart, ystart, tileWidth, tileHeight, Target.BL), img, quality);
                    tileColour.AverageBR = getAverageTileColor(CreateQuadrantRectangle(xstart, ystart, tileWidth, tileHeight, Target.BR), img, quality);
                    tileColour.AverageWhole = getAverageTileColor(CreateQuadrantRectangle(xstart, ystart, tileWidth, tileHeight, Target.Whole), img, quality);
                    colorMap[x, y] = tileColour;
                }
            }
            return colorMap;
        }

        private Color getAverageTileColor(Rectangle area, Bitmap bmp, int quality)
        {
            Int64 r = 0, g = 0, b = 0;
            var color = Color.Empty;
            int pixelCount = 0;
            int xPos, yPos;
            var xEnd = area.X + area.Width;
            var yEnd = area.Y + area.Height;

            for (xPos = area.X; xPos < xEnd; xPos += quality)
            {
                for (yPos = area.Y; yPos < yEnd; yPos += quality)
                {
                    color = bmp.GetPixel(xPos, yPos);
                    r += color.R;
                    g += color.G;
                    b += color.B;
                    pixelCount++;
                }
            }

            return Color.FromArgb(255, (int)(r / pixelCount), (int)(g / pixelCount), (int)(b / pixelCount));
        }

        public Mosaic Render(Bitmap img, MosaicTileColour[,] colorMap, List<ImageInfo> imageInfos, bool random = false, bool 
            colourBlended = false, bool enhanced = false, int enhancedThreshold = 50, bool edgeDetection = false, List<PixelCoordinates> edges = null)
        {
            this.library = imageInfos;
            var newImg = new Bitmap(colorMap.GetLength(0) * tileSize.Width, colorMap.GetLength(1) * tileSize.Height);

            var g = Graphics.FromImage(newImg);
            var b = new SolidBrush(Color.Black);

            g.FillRectangle(b, 0, 0, img.Width, img.Height);

            var info = new ImageInfo[colorMap.GetLength(0), colorMap.GetLength(1)];

            var imageSq = new List<MosaicTile>();

            // Find best image for each tile
            for (int x = 0; x < colorMap.GetLength(0); x++)
            {
                for (int y = 0; y < colorMap.GetLength(1); y++)
                {
                    info[x, y] = new ImageInfo(imageInfos[GetBestImageIndex(colorMap[x, y], x, y, random, Target.Whole)]);
                    // Gets current x, y coords of mosaic images, and stores image to be replaced by
                    imageSq.Add(new MosaicTile(info[x, y], x, y));
                }
            }

            // Set property of tile if tile is considered edge
            if (edgeDetection)
            {
                imageSq = imageSq.CalculateEdgeTiles(edges, tileSize.Width, tileSize.Height);
            }

            // Get the threshold to spilt the tiles at
            var threshold = imageSq.GetAverage(enhancedThreshold);

            //// Recalculate the items in imageSq
            if (enhanced || edgeDetection)
            {
                foreach (var sq in imageSq)
                {
                    if ((enhanced && sq.Difference > threshold) || (edgeDetection && sq.IsEdge))
                    {
                        sq.InQuadrants = true;
                        //info[sq.X, sq.Y].InQuadrants = true;
                        var tileInfo = info[sq.X, sq.Y];
                        tileInfo.InQuadrants = true;
                        tileInfo.TLInfo = imageInfos[GetBestImageIndex(colorMap[sq.X, sq.Y], sq.X, sq.Y, random, Target.TL)];
                        tileInfo.TRInfo = imageInfos[GetBestImageIndex(colorMap[sq.X, sq.Y], sq.X, sq.Y, random, Target.TR)];
                        tileInfo.BLInfo = imageInfos[GetBestImageIndex(colorMap[sq.X, sq.Y], sq.X, sq.Y, random, Target.BL)];
                        tileInfo.BRInfo = imageInfos[GetBestImageIndex(colorMap[sq.X, sq.Y], sq.X, sq.Y, random, Target.BR)];
                        sq.TLTile = new MosaicTile(tileInfo.TLInfo, sq.X, sq.Y);
                        sq.TRTile = new MosaicTile(tileInfo.TRInfo, sq.X, sq.Y);
                        sq.BLTile = new MosaicTile(tileInfo.BLInfo, sq.X, sq.Y);
                        sq.BRTile = new MosaicTile(tileInfo.BRInfo, sq.X, sq.Y);
                    }
                }
            }

            // In parallel resize all of the unique file paths in imageSq
            // Create set for all unique

            //Get unique set of files names within imageSq list including all List within objects
            var selectedFiles = imageSq.GetUniqueImages();
            var resizeFiles = new List<Tuple<string, Bitmap>>();
            Parallel.ForEach(selectedFiles , f =>
            {
                var resizedBmp = Resize(f, tileSize.Height, tileSize.Width);
                if (resizedBmp != null)
                {
                    lock(resizeFiles)
                    {
                        resizeFiles.Add(new Tuple<string, Bitmap>(f, resizedBmp));
                    }
                }
            }); // Parallel.For

            // Store the resize version of the image instead of the original tile image
            imageSq.StoreResizedBitmap(resizeFiles);

            // Render the image to represent each tile
            for (int x = 0; x < colorMap.GetLength(0); x++)
            {
                for (int y = 0; y < colorMap.GetLength(1); y++)
                {
                    if (info[x, y].InQuadrants)
                    {
                        // Get the correct image info from list of image infos
                        RenderTile(g, info[x, y].TLInfo, ref imageSq, x, y, Target.TL);
                        RenderTile(g, info[x, y].TRInfo, ref imageSq, x, y, Target.TR);
                        RenderTile(g, info[x, y].BLInfo, ref imageSq, x, y, Target.BL);
                        RenderTile(g, info[x, y].BRInfo, ref imageSq, x, y, Target.BR);
                    }
                    else
                    {
                        RenderTile(g, info[x, y], ref imageSq, x, y, Target.Whole);
                    }
                }
            }

            if (colourBlended)
            {
                var cm = new ColorMatrix();
                cm.Matrix33 = 0.4f;

                var ia = new ImageAttributes();
                ia.SetColorMatrix(cm);

                g.DrawImage(img,
                    // target
                    new Rectangle(0, 0, newImg.Width, newImg.Height),
                    // source
                    0, 0, img.Width, img.Height,
                    GraphicsUnit.Pixel,
                    ia);
            }
            
            // Dispose of all resized bitmaps used for rendering
            foreach (var bitmap in resizeFiles.Select(x => x.Item2))
            {
                bitmap.Dispose();
            }

            return new Mosaic()
            {
                Image = newImg,
                Tiles = imageSq
            };
        }

        // Pass target into method
        private void RenderTile(Graphics g, ImageInfo info, ref List<MosaicTile> imageSq, int x, int y, Target target)
        {
            Rectangle destRect, srcRect;

            Bitmap source = imageSq.Where(i => i.Image == info.Path).Select(b => b.Bitmap).FirstOrDefault();
            // If file has not been resized create new bitmap and add to list to ensure it is disposed
            if (source == null)
            {
                source = new Bitmap(Image.FromFile(info.Path));
                imageSq.Add(new MosaicTile() { Image = info.Path, Bitmap = source });
            }           
            // Draws stored image for coord x, y for given height and width
            destRect = CreateQuadrantRectangle(x * tileSize.Width, y * tileSize.Height, tileSize.Width, tileSize.Height, target);
            srcRect = new Rectangle(0, 0, source.Width, source.Height);

            g.DrawImage(source, destRect, srcRect, GraphicsUnit.Pixel);         
        }

        // Passes the colour value for the current tile being analysed
        // Uses library which contains the average colour for all of the tile images
        private int GetBestImageIndex(MosaicTileColour tileColor, int x, int y, bool random, Target target)
        {
            double bestPercent = double.MaxValue;
            var bestIndexes = new Dictionary<int, double>();
            bestIndexes.Add(-1, bestPercent);
            const byte offset = 7;
            double difference;

            for (int i = 0; i < library.Count(); i++)
            {
                difference = GetDifferenceForTarget(tileColor, i, target);

                // as well as best diff store the 10th best diff and replace that item when necessary
                if (difference < bestPercent)
                {
                    Point point = new Point();

                    if (library[i].Data.Count > 0 && library[i].Data[0] != null)
                    {
                        point = (Point)library[i].Data[0];
                    }
                    if (point.IsEmpty)
                    {
                        bestIndexes.Add(i, difference);
                    }
                    else if (point.X + offset <= x && point.Y + offset > y && point.Y - offset < y)
                    {
                        bestIndexes.Add(i, difference);
                    }

                    // if length of dictionary is > 10 remove the largest value entry
                    if (bestIndexes.Count() > 10)
                    {
                        var maxValue = bestIndexes.Values.Max();
                        var maxKey = bestIndexes.FirstOrDefault(v => v.Value == maxValue).Key;
                        bestIndexes.Remove(maxKey);
                    }

                    // update best percent to largest value in dictionary
                    bestPercent = bestIndexes.Values.Max();
                }
            }

            // Will remove the inital entry if still present
            if (bestIndexes.ContainsKey(-1))
            {
                bestIndexes.Remove(-1);
            }

            int index;
            if (random)
            {
                // Randomly select one of the best fit indexes 
                index = bestIndexes.ElementAt(new Random().Next(0, bestIndexes.Count - 1)).Key;
            }
            else
            {
                var min = bestIndexes.Values.Min();
                index = bestIndexes.FirstOrDefault(v => v.Value == min).Key;
            }

            library[index].Data.Add(new Point(x, y));
            library[index].Difference = bestIndexes.GetValueOrDefault(index);
            return index;
        }

        private double GetDifferenceForTarget(MosaicTileColour tileColor, int i, Target target)
        {
            if (target == Target.Whole)
            {
                return GetLibraryTileDifference(tileColor, i);
            }
            else
            {
                switch (target)
                {
                    case Target.TL:
                        return tileColor.AverageWhole.GetDifference(library[i].AverageTL);
                    case Target.TR:
                        return tileColor.AverageWhole.GetDifference(library[i].AverageTR);
                    case Target.BL:
                        return tileColor.AverageWhole.GetDifference(library[i].AverageBL);
                    case Target.BR:
                        return tileColor.AverageWhole.GetDifference(library[i].AverageBR);
                    default:
                        return GetLibraryTileDifference(tileColor, i);
                }
            }
        }

        private double GetLibraryTileDifference(MosaicTileColour color, int i)
        {
            var differenceTL = color.AverageTL.GetDifference(library[i].AverageTL);
            var differenceTR = color.AverageTR.GetDifference(library[i].AverageTR);
            var differenceBL = color.AverageBL.GetDifference(library[i].AverageBL);
            var differenceBR = color.AverageBR.GetDifference(library[i].AverageBR);

            return differenceTL + differenceTR + differenceBL + differenceBR;
        }       
    }
}
