using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiArtCreator.SystemDrawing.Framework.Helpers
{
    public static class BitmapExpansMethods
    {
        public static Bitmap Resize(this Bitmap bitmap, int width, int height)
        {
            return new Bitmap(bitmap, width, height);
        }
    }
}
