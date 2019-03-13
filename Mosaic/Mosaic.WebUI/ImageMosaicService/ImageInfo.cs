using System;
using System.Collections;
using System.Drawing;

namespace ImageMosaicService
{
    public class ImageInfo
    {
        public Color AverageTL { get; set; }
        public Color AverageTR { get; set; }
        public Color AverageBL { get; set; }
        public Color AverageBR { get; set; }
        public Color AverageWhole { get; set; }
        public double Difference { get; set; }
        public string Path { get; set; }
        private ArrayList _data;
        public bool InQuadrants { get; set; }
        public ImageInfo TLInfo { get; set; }
        public ImageInfo TRInfo { get; set; }
        public ImageInfo BLInfo { get; set; }
        public ImageInfo BRInfo { get; set; }

        public ArrayList Data
        {
            get
            {
                if (_data == null)
                    _data = new ArrayList();

                return _data;
            }
            set
            {
                if (_data == null)
                    _data = new ArrayList();

                _data = value;
            }
        }

        public ImageInfo(string filePath)
        {
            this.Path = filePath;
            InQuadrants = false;
        }

        // Copies contents of another instance except quadrant info
        public ImageInfo(ImageInfo other)
        {
            AverageTL = other.AverageTL;
            AverageTR = other.AverageTR;
            AverageBL = other.AverageBL;
            AverageBR = other.AverageBR;
            AverageWhole = other.AverageWhole;
            Difference = other.Difference;
            Path = other.Path;
            _data = other._data;
            InQuadrants = other.InQuadrants;
        }
    }
}
