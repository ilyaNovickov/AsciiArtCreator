using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace AsciiArtCreator.SystemDrawing.Framework
{
    public static class AsciiArtExporter
    {
        public static void Export(this GrayscaleAsciiArt art, string path)
        {
            Bitmap bitmap = new Bitmap(art.Image.Width, art.Image.Height);

            var g = Graphics.FromImage(bitmap);

            var b = art.ImageOptions.Brightness;
            var c = art.ImageOptions.Contrast;
            var t = (1f - c) / 2f;
            var s = art.ImageOptions.Saturation;
            var sr = (1 - s);// * 0.3086f;
            var sg = (1 - s);// * 0.6094f;
            var sb = (1 - s);// * 0.0820f;

            float[][] matrix = {
                   new float[] {c*(sr+s), c*sr,     c*(sr),    0, 0},
                   new float[] {c*sg,     c*(sg+s), c*(sg),    0, 0},
                   new float[] {c*sb,     c*sb,     c*(sb+s),  0, 0},
                   new float[] {0,        0,        0,        1f, 0},
                   new float[] {t+b,      t+b,      t+b,       0, 1}
                   //new float[] {c*(sr+s), 0,     0,    0, 0},
                   //new float[] {0,     c*(sg+s), 0,    0, 0},
                   //new float[] {0,     0,     c*(sb+s),  0, 0},
                   //new float[] {0,        0,        0,        1f, 0},
                   //new float[] {t+b,      t+b,      t+b,       0, 1}
                };

            ImageAttributes attributes = new ImageAttributes();

            attributes.SetColorMatrix(new ColorMatrix(matrix));

            g.DrawImage(art.Image, new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel, attributes);

            g.Dispose();

            bitmap.Save(path);
        }
    }
}
