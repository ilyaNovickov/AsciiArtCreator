using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Drawing.Imaging;
using System.Diagnostics;

namespace AsciiArtCreator.SystemDrawing.Framework
{
    public class GrayscaleAsciiArt : IDisposable
    {
        public class GrayscaleArtOptions
        {
            public GrayscaleArtOptions() 
            {
                width = 500;
                height = 500;
                maxHeight = 5000;
                maxWidth = 5000;
                minHeight = 4;
                minWidth = 2;
                InverseColors = false;
            }

            private int width;
            private int height;
            private int minWidth;
            private int minHeight;
            private int maxWidth;
            private int maxHeight;
            private float brightness;
            private float contrast;
            private float saturation;

            public float Brightness
            {
                get => brightness;
                set
                {
                    if (value < -1f || value > 1f)
                        return;

                    brightness = value;
                }
            }
            public float Contrast
            {
                get => contrast;
                set => contrast = value;
            }
            public float Saturation
            {
                get => saturation;
                set => saturation = value;
            }

            public int MinWidth
            {
                get => minWidth;
                set
                {
                    if (value <= 0 && value > maxWidth)
                        throw new Exception("Минимальное значение не может быть меньше 0 " +
                            "или больше максимального значения");
                    minWidth = value;
                }
            }

            public int MinHeight
            {
                get => minHeight;
                set
                {
                    if (value <= 0 && value > maxHeight)
                        throw new Exception("Минимальное значение не может быть меньше 0 " +
                            "или больше максимального значения");
                    minHeight = value;
                }
            }

            public int MaxWidth
            {
                get => maxWidth;
                set
                {
                    if (value <= 0 && value < minWidth)
                        throw new Exception("Максимальное значение не может быть меньше 0 или минимального значения");
                    maxWidth = value;
                }
            }

            public int MaxHeight
            {
                get => maxHeight;
                set
                {
                    if (value <= 0 && value < minHeight)
                        throw new Exception("Максимальное значение не может быть меньше 0 или минимального значения");
                    maxHeight = value;
                }
            }

            public int Width
            {
                get => width;
                set
                {
                    if (value < MinWidth || value > MaxWidth)
                        throw new Exception("Значение должно быть в интервале от минимального до максимального");
                    width = value;
                }
            }

            public int Height
            {
                get => height;
                set
                {
                    if (value < MinHeight || value > MaxHeight)
                        throw new Exception("Значение должно быть в интервале от минимального до максимального");

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

            //options.Width = Image.Width;
            //options.Height = Image.Height;
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
            string str = await Task.Run(() =>
            {
                return format.Func.Invoke((Bitmap)Image, format.SymbolCollection, options, token, null);
            });

            return str;
        }

        public async Task<string> GetOrCreateAsciiArtAsync(IProgress<int> progress, CancellationToken? token = null)
        {
            string str = await Task.Run(() =>
            {
                return format.Func.Invoke((Bitmap)Image, format.SymbolCollection, options, token, progress);
            });

            return str;
        }

        public void Dispose()
        {
            Image?.Dispose();
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
