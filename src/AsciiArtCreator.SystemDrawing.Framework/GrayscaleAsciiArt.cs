using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiArtCreator.SystemDrawing.Framework
{
    public class GrayscaleAsciiArt
    {
        public class GrayscaleArtOptions
        {
            public GrayscaleArtOptions() 
            {
                width = 500;
                height = 500;
                minHeight = 4;
                minWidth = 2;
                InverseColors = false;
            }

            private int width;
            private int height;
            private int minWidth;
            private int minHeight;

            public int MinWidth
            {
                get => minWidth;
                set
                {
                    if (value <= 0)
                        throw new Exception("Minimum value can't be below zero");
                    minWidth = value;
                }
            }

            public int MinHeight
            {
                get => minHeight;
                set
                {
                    if (value <= 0)
                        throw new Exception("Minimum value can't be below zero");
                    minHeight = value;
                }
            }

            public int Width
            {
                get => width;
                set
                {
                    if (value < MinWidth)
                        throw new Exception("Value can't be below than minimum value");
                    width = value;
                }
            }

            public int Height
            {
                get => height;
                set
                {
                    if (value < MinWidth)
                        throw new Exception("Value can't be below than minimum value");
                    height = value;
                }
            }

            public bool InverseColors { get; set; }
        }

        private Image image;
        private string art;
        private GrayscaleArtOptions options = new GrayscaleArtOptions();

        public GrayscaleAsciiArt()
        {

        }

        public GrayscaleAsciiArt(Image image)
        {
            this.Image = image;
        }

        public Image Image
        {
            get => image;
            set
            {
                image = value;
                art = null;
                ImageChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public GrayscaleArtOptions ImageOptions
        {
            get => options;
            set => options = value;
        }

        public event EventHandler ImageChanged;

        public string GetAsciiArt()
        {
            return art;
        }

        public void GetOrCreateAsciiArt()
        {

        }
    }
}
