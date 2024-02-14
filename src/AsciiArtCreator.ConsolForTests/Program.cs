using AsciiArtCreator.SystemDrawing.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiArtCreator.ConsolForTests
{
    internal class Program
    {
        const string path = @"C:\Users\ACER\Pictures\winxp.png";

        static void Main(string[] args)
        {
            GrayscaleAsciiArt art = new GrayscaleAsciiArt();

            art.SetImage(path);

            Stopwatch sw = new Stopwatch();
            sw.Start();
            art.GetOrCreateAsciiArt();
            sw.Stop();

            Console.WriteLine(sw.ElapsedMilliseconds);
            Console.ReadLine();
        }
    }
}
