using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace ImageMosaicService
{
    public class ImageProcessing
    {
        private int quality = 6, resizeHeight = 119, resizeWidth = 119;
        private Size tileSize;
        private List<ImageInfo> library;

        public ImageProcessing(int tileHeight = 10, int tileWidth = 10)
        {
            tileSize = new Size(tileHeight, tileWidth);
        }

        public Bitmap Resize(string srcFile)
        {
            if (!File.Exists(srcFile))
                return null;

            using (var scrBitmap = Bitmap.FromFile(srcFile))
            {
                var b = new Bitmap(resizeHeight, resizeWidth);
                
                using (var g = Graphics.FromImage((Image)b))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.DrawImage(scrBitmap, 0, 0, resizeHeight, resizeWidth);
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
            imageInfo.AverageTR = getAverageColor(new Rectangle(halfX, 0, bmp.Width - halfX, halfY), bmp, quality);
            imageInfo.AverageBL = getAverageColor(new Rectangle(0, halfY, halfX, bmp.Height - halfY), bmp, quality);
            imageInfo.AverageBR = getAverageColor(new Rectangle(halfX, halfY, bmp.Width - halfX, bmp.Height - halfY), bmp, quality);
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

        public Color[,] CreateMap(Bitmap img)
        {
            int horizontalTiles = (int)img.Width / tileSize.Width;
            int verticalTiles = (int)img.Height / tileSize.Height;

            var colorMap = new Color[horizontalTiles, verticalTiles];

            int tileWidth = (img.Width - img.Width % horizontalTiles) / horizontalTiles;
            int tileHeight = (img.Height - img.Height % verticalTiles) / verticalTiles;

            Int64 r, g, b;
            int pixelCount;
            Color color;

            int xPos, yPos;

            for (int x = 0; x < horizontalTiles; x++)
            {
                for (int y = 0; y < verticalTiles; y++)
                {
                    r = 0;
                    g = 0;
                    b = 0;
                    pixelCount = 0;

                    for (xPos = tileWidth * x; xPos < x * tileWidth + tileWidth; xPos++)
                    {
                        for (yPos = tileHeight * y; yPos < y * tileHeight + tileHeight; yPos++)
                        {
                            color = img.GetPixel(xPos, yPos);
                            r += color.R; g += color.G; b += color.B;
                            pixelCount++;
                        }
                    }
                    colorMap[x, y] = Color.FromArgb(255, (int)r / pixelCount, (int)g / pixelCount, (int)b / pixelCount);
                }

            }
            return colorMap;
        }

        public Mosaic Render(Bitmap img, Color[,] colorMap, List<ImageInfo> imageInfos, bool random = false)
        {
            this.library = imageInfos;
            var newImg = new Bitmap(colorMap.GetLength(0) * tileSize.Width, colorMap.GetLength(1) * tileSize.Height);

            var g = Graphics.FromImage(newImg);
            var b = new SolidBrush(Color.Black);

            g.FillRectangle(b, 0, 0, img.Width, img.Height);

            ImageInfo info;
            Rectangle destRect, srcRect;

            var imageSq = new List<MosaicTile>();

            // Getting stuck in an extremely large loop here - bottleneck
            // For dog test example it is a 120 * 160 loop
            for (int x = 0; x < colorMap.GetLength(0); x++)
            {
                for (int y = 0; y < colorMap.GetLength(1); y++)
                {
                    if (random)
                    {
                        info = imageInfos[GetBestImageIndexRandom(colorMap[x, y], x, y)];
                    }
                    else
                    {
                        info = imageInfos[GetBestImageIndex(colorMap[x, y], x, y)];
                    }
                    using (Image source = Image.FromFile(info.Path))
                    {
                        // Gets current x, y coords of mosaic images, and stores image to be replaced by
                        imageSq.Add(new MosaicTile()
                        {
                            X = x,
                            Y = y,
                            Image = info.Path
                        });

                        // Draws stored image for coord x, y for given height and width
                        destRect = new Rectangle(x * tileSize.Width, y * tileSize.Height, tileSize.Width, tileSize.Height);
                        srcRect = new Rectangle(0, 0, source.Width, source.Height);

                        g.DrawImage(source, destRect, srcRect, GraphicsUnit.Pixel);
                    }
                }
            }

            return new Mosaic()
            {
                Image = newImg,
                Tiles = imageSq
            };
        }

        private int GetBestImageIndexRandom(Color color, int x, int y)
        {
            double bestPercent = double.MaxValue;
            var bestIndexes = new Dictionary<int, double>();
            bestIndexes.Add(-1, bestPercent);
            const byte offset = 7;
            double difference;

            for (int i = 0; i < library.Count(); i++)
            {
                difference = GetLibraryTileDifference(color, i);

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

            // Randomly select one of the best fit indexes 
            var randomIndex = bestIndexes.ElementAt(new Random().Next(0, bestIndexes.Count -1)).Key;

            library[randomIndex].Data.Add(new Point(x, y));
            return randomIndex;
        }
    
        // Passes the colour value for the current tile being analysed
        // Uses library which contains the average colour for all of the tile images
        private int GetBestImageIndex(Color color, int x, int y)
        {
            double bestPercent = double.MaxValue;
            int bestIndex = 0;
            const byte offset = 7;
            double difference;

            for (int i = 0; i < library.Count(); i++)
            {
                if (library[i] != null)
                {
                    difference = GetLibraryTileDifference(color, i);

                    if (difference < bestPercent)
                    {
                        Point point = new Point();

                        if (library[i].Data.Count > 0 && library[i].Data[0] != null)
                        {
                            point = (Point)library[i].Data[0];
                        }
                        if (point.IsEmpty)
                        {
                            bestPercent = difference;
                            bestIndex = i;
                        }
                        else if (point.X + offset <= x && point.Y + offset > y && point.Y - offset < y)
                        {
                            bestPercent = difference;
                            bestIndex = i;
                        }
                    }
                }               
            }

            library[bestIndex].Data.Add(new Point(x, y));
            return bestIndex;
        }

        private double GetLibraryTileDifference(Color color, int i)
        {
            int r, g, b;
            Color[] passColor;
            double difference;

            passColor = new Color[4];
            passColor[0] = library[i].AverageTL;
            passColor[1] = library[i].AverageTR;
            passColor[2] = library[i].AverageBL;
            passColor[3] = library[i].AverageBR;

            r = passColor[0].R + passColor[1].R + passColor[2].R + passColor[3].R;
            g = passColor[0].G + passColor[1].G + passColor[2].G + passColor[3].G;
            b = passColor[0].B + passColor[1].B + passColor[2].B + passColor[3].B;

            r = Math.Abs(color.R - (r / 4));
            g = Math.Abs(color.G - (g / 4));
            b = Math.Abs(color.B - (b / 4));

            difference = r + g + b;
            difference /= 3 * 255;
            return difference;
        }
    }
}
