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

            List<FontFamily> avaibleFonts = new List<FontFamily>(5);

            foreach (FontFamily font in fonts)
            {
                if (!IsMonospace(font))
                    continue;

                avaibleFonts.Add(font);
            }

            return avaibleFonts;
        }
    }
}
