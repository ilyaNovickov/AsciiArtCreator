using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using System.Drawing.Text;

namespace AsciiArtCreator.Wpf.Framework.Helpers
{
    internal class FontHelper
    {
        public static IEnumerable<FontFamily> GetFonts()
        {

            char[] charSizes = new char[] { 'i', 'a', 'Z', '%', '#', 'a', 'B', 'l', 'm', ',', '.' };

            bool IsMonospace(FontFamily family)
            {
                foreach (Typeface typeface in family.GetTypefaces())
                {
                    double firstWidth = 0d;

                    foreach (char ch in charSizes)
                    {
                        FormattedText formattedText = new FormattedText(
                            ch.ToString(),
                            CultureInfo.CurrentCulture,
                            FlowDirection.LeftToRight,
                            typeface,
                            10d,
                            Brushes.Black,
                            new NumberSubstitution(),
                            1);
                        if (ch == 'i')  // first char in list
                        {
                            firstWidth = formattedText.Width;
                        }
                        else
                        {
                            if (formattedText.Width != firstWidth)
                                return false;
                        }
                    }
                }

                return true;
            }

            ICollection<FontFamily> fonts = Fonts.SystemFontFamilies;

            List<FontFamily> avaibleFonts = new List<FontFamily>(25)
            {
                new FontFamily(new Uri("pack://application:,,,/"), "./Assests/Fonts/#Anonymous pro"),
                new FontFamily(new Uri("pack://application:,,,/"), "./Assests/Fonts/#Codename Coder Free 4F"),
                new FontFamily(new Uri("pack://application:,,,/"), "./Assests/Fonts/#Cousine"),
                new FontFamily(new Uri("pack://application:,,,/"), "./Assests/Fonts/#Droid Sans Mono Slashed"),
                new FontFamily(new Uri("pack://application:,,,/"), "./Assests/Fonts/#Fira Mono"),
                //             ______
                //             ||||||
                // unmono font \/\/\/
                //new FontFamily(new Uri("pack://application:,,,/"), "./Assests/Fonts/#OpenGost Type A TT"),
                new FontFamily(new Uri("pack://application:,,,/"), "./Assests/Fonts/#Rubik Mono One"),
                new FontFamily(new Uri("pack://application:,,,/"), "./Assests/Fonts/#Vin Mono Pro Light"),
            };

            foreach (FontFamily font in fonts)
            {
                if (!IsMonospace(font))
                    continue;

                avaibleFonts.Add(font);
            }
            
            return avaibleFonts;
        }

        public static async Task<IEnumerable<FontFamily>> GetFontsAsync()
        {
            return await Task.Run(() => { return GetFonts(); });
        }
    }
}
