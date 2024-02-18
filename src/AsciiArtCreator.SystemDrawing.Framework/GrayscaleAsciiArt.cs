using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Drawing.Imaging;

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

        private Image image = null;
        private string art = null;
        private GrayscaleArtFormat format = StandartAsciiArtMethods.DefaultFormat;
        private GrayscaleArtOptions options = new GrayscaleArtOptions();

        public GrayscaleAsciiArt()
        {

        }

        public GrayscaleAsciiArt(Image image)
        {
            this.Image = image;
        }

        public GrayscaleAsciiArt(string path)
        {
            if (!File.Exists(path))
                throw new Exception("Такого файла не существует");

            this.Image = Image.FromFile(path);
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

        public GrayscaleArtFormat Format
        {
            get => format;
            set
            {
                format = value;
                art = null;
            }
        }

        public GrayscaleArtOptions ImageOptions
        {
            get => options;
            set => options = value;
        }

        public event EventHandler ImageChanged;

        public bool SetImage(string path)
        {
            if (!File.Exists(path))
                return false;

            try
            {
                Image = Image.FromFile(path);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        public string GetAsciiArt()
        {
            return art;
        }

        public string GetOrCreateAsciiArt()
        {
            return format.Func.Invoke((Bitmap)Image, format.SymbolCollection, options, null, null);
        }

        public async Task<string> GetOrCreateAsciiArtAsync(CancellationToken? token = null)
        {
            return await Task<string>.Run(() =>
            {
                return format.Func.Invoke((Bitmap)Image, format.SymbolCollection, options, null, null);
            });
        }

        public async Task<string> GetOrCreateAsciiArtAsync(IProgress<int> progress, CancellationToken? token = null)
        {
            return await Task<string>.Run(() =>
            {
                return format.Func.Invoke((Bitmap)Image, format.SymbolCollection, options, token, progress);
            });
        }

        //public void GetOrCreateAsciiArt()
        //{
        //    Bitmap image = new Bitmap(this.image.Width, this.image.Height);

        //    var g = Graphics.FromImage(image);

        //    var b = 0;
        //    var c = 1f;
        //    var t = (1f - c) / 2f;
        //    var s = 0;
        //    var sr = (1 - s) * 0.3086f;
        //    var sg = (1 - s) * 0.6094f;
        //    var sb = (1 - s) * 0.0820f;

        //    float[][] matrix = {
        //       new float[] {c*(sr+s), c*sr,     c*(sr),    0, 0},
        //       new float[] {c*sg,     c*(sg+s), c*(sg),    0, 0},
        //       new float[] {c*sb,     c*sb,     c*(sb+s),  0, 0},
        //       new float[] {0,        0,        0,        1f, 0},
        //       new float[] {t+b,      t+b,      t+b,       0, 1}
        //       //new float[] {c*(sr+s), 0,     0,    0, 0},
        //       //new float[] {0,     c*(sg+s), 0,    0, 0},
        //       //new float[] {0,     0,     c*(sb+s),  0, 0},
        //       //new float[] {0,        0,        0,        1f, 0},
        //       //new float[] {t+b,      t+b,      t+b,       0, 1}
        //    };


        //    ImageAttributes attributes = new ImageAttributes();

        //    attributes.SetColorMatrix(new ColorMatrix(matrix));

        //    g.DrawImage(this.image, new Rectangle(0,0,image.Width,image.Height), 
        //        0, 0, this.image.Width, this.image.Height, GraphicsUnit.Pixel, attributes);

        //    g.Dispose();

        //    image.Save("test.png");

        //}
    }
}
