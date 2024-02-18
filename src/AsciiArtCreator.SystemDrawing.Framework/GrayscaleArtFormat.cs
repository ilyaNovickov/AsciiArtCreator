using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing;

namespace AsciiArtCreator.SystemDrawing.Framework
{
    public class GrayscaleArtFormat
    {
        private string name;
        private IEnumerable symbolCollection;
        private Func<Bitmap, IEnumerable,
            GrayscaleAsciiArt.GrayscaleArtOptions, CancellationToken?, IProgress<int>, string> func;

        public GrayscaleArtFormat(string name, IEnumerable collection, Func<Bitmap, IEnumerable,
            GrayscaleAsciiArt.GrayscaleArtOptions, CancellationToken?, IProgress<int>, string> func)
        {
            this.name = name;
            this.func = func;
            this.symbolCollection = collection;
        }

        public string Name => name;
        public IEnumerable SymbolCollection => symbolCollection;
        public Func<Bitmap, IEnumerable,
            GrayscaleAsciiArt.GrayscaleArtOptions, CancellationToken?, IProgress<int>, string> Func => func;
    }
}
