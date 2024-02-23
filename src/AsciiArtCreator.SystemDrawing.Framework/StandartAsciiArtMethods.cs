using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FastBitmapLib;
using AsciiArtCreator.SystemDrawing.Framework.Helpers;

namespace AsciiArtCreator.SystemDrawing.Framework
{
    public static class StandartAsciiArtMethods
    {
        private static readonly Dictionary<int, char> brayallSimbols = new Dictionary<int, char>()
        {
            { 0, '⠄'}, { 1, '⠁'}, { 2, '⠂'}, { 12, '⠃'}, { 3, '⠄'}, { 13, '⠅'},
            { 23, '⠆'},{ 123, '⠇'},{ 4, '⠈'},{ 14, '⠉'},{ 24, '⠊'},{ 124, '⠋'},{ 34, '⠌'},
            { 134, '⠍'},{ 234, '⠎'},{ 1234, '⠏'},{ 5, '⠐'},{ 15, '⠑'},{ 25, '⠒'},{ 125, '⠓'},{ 35, '⠔'},
            { 135, '⠕'},{ 235, '⠖'},{ 1235, '⠗'},{ 45, '⠘'},{ 145, '⠙'},{ 245, '⠚'},{ 1245, '⠛'},{ 345, '⠜'},
{ 1345, '⠝'},{ 2345, '⠞'},{ 12345, '⠟'},{ 6, '⠠'},{ 16, '⠡'},{ 26, '⠢'},{ 126, '⠣'},{ 36, '⠤'},
{ 136, '⠥'},{ 236, '⠦'},{ 1236, '⠧'},{ 46, '⠨'},{ 146, '⠩'},{ 246, '⠪'},{ 1246, '⠫'},{ 346, '⠬'},
{ 1346, '⠭'},{ 2346, '⠮'},{ 12346, '⠯'},{ 56, '⠰'},{ 156, '⠱'},{ 256, '⠲'},{ 1256, '⠳'},{ 356, '⠴'},
{ 1356, '⠵'},{ 2356, '⠶'},{ 12356, '⠷'},{ 456, '⠸'},{ 1456, '⠹'},{ 2456, '⠺'},{ 12456, '⠻'},{ 3456, '⠼'},
{ 13456, '⠽'},{ 23456, '⠾'},{ 123456, '⠿'},{ 7, '⡀'},{ 17, '⡁'},{ 27, '⡂'},{ 127, '⡃'},{ 37, '⡄'},
{ 137, '⡅'},{ 237, '⡆'},{ 1237, '⡇'},{ 47, '⡈'},{ 147, '⡉'},{ 247, '⡊'},{ 1247, '⡋'},{ 347, '⡌'},
{ 1347, '⡍'},{ 2347, '⡎'},{ 12347, '⡏'},{ 57, '⡐'},{ 157, '⡑'},{ 257, '⡒'},{ 1257, '⡓'},{ 357, '⡔'},
{ 1357, '⡕'},{ 2357, '⡖'},{ 12357, '⡗'},{ 457, '⡘'},{ 1457, '⡙'},{ 2457, '⡚'},{ 12457, '⡛'},{ 3457, '⡜'},
{ 13457, '⡝'},{ 23457, '⡞'},{ 123457, '⡟'},{ 67, '⡠'},{ 167, '⡡'},{ 267, '⡢'},{ 1267, '⡣'},{ 367, '⡤'},
{ 1367, '⡥'},{ 2367, '⡦'},{ 12367, '⡧'},{ 467, '⡨'},{ 1467, '⡩'},{ 2467, '⡪'},{ 12467, '⡫'},{ 3467, '⡬'},
{ 13467, '⡭'},{ 23467, '⡮'},{ 123467, '⡯'},{ 567, '⡰'},{ 1567, '⡱'},{ 2567, '⡲'},
{ 12567, '⡳'},{ 3567, '⡴'},{ 13567, '⡵'},{ 23567, '⡶'},{ 123567, '⡷'},{ 4567, '⡸'},{ 14567, '⡹'},{ 24567, '⡺'},
{ 124567, '⡻'},{ 34567, '⡼'},{ 134567, '⡽'},{ 234567, '⡾'},{ 1234567, '⡿'},{ 8, '⢀'},{ 18, '⢁'},{ 28, '⢂'},
{ 128, '⢃'},{ 38, '⢄'},{ 138, '⢅'},{ 238, '⢆'},{ 1238, '⢇'},{ 48, '⢈'},{ 148, '⢉'},{ 248, '⢊'},
{ 1248, '⢋'},{ 348, '⢌'},{ 1348, '⢍'},{ 2348, '⢎'},{ 12348, '⢏'},{ 58, '⢐'},{ 158, '⢑'},{ 258, '⢒'},
{ 1258, '⢓'},{ 358, '⢔'},{ 1358, '⢕'},{ 2358, '⢖'},{ 12358, '⢗'},{ 458, '⢘'},{ 1458, '⢙'},{ 2458, '⢚'},
{ 12458, '⢛'},{ 3458, '⢜'},{ 13458, '⢝'},{ 23458, '⢞'},{ 123458, '⢟'},{ 68, '⢠'},{ 168, '⢡'},{ 268, '⢢'},
{ 1268, '⢣'},{ 368, '⢤'},{ 1368, '⢥'},{ 2368, '⢦'},{ 12368, '⢧'},{ 468, '⢨'},{ 1468, '⢩'},{ 2468, '⢪'},
{ 12468, '⢫'},{ 3468, '⢬'},{ 13468, '⢭'},{ 23468, '⢮'},{ 123468, '⢯'},{ 568, '⢰'},{ 1568, '⢱'},{ 2568, '⢲'},
{ 12568, '⢳'},{ 3568, '⢴'},{ 13568, '⢵'},{ 23568, '⢶'},{ 123568, '⢷'},{ 4568, '⢸'},{ 14568, '⢹'},{ 24568, '⢺'},
{ 124568, '⢻'},{ 34568, '⢼'},{ 134568, '⢽'},{ 234568, '⢾'},{ 1234568, '⢿'},{ 78, '⣀'},{ 178, '⣁'},{ 278, '⣂'},
{ 1278, '⣃'},{ 378, '⣄'},{ 1378, '⣅'},{ 2378, '⣆'},{ 12378, '⣇'},{ 478, '⣈'},{ 1478, '⣉'},{ 2478, '⣊'},
{ 12478, '⣋'},{ 3478, '⣌'},{ 13478, '⣍'},{ 23478, '⣎'},{ 123478, '⣏'},{ 578, '⣐'},{ 1578, '⣑'},{ 2578, '⣒'},
{ 12578, '⣓'},{ 3578, '⣔'},{ 13578, '⣕'},{ 23578, '⣖'},{ 123578, '⣗'},{ 4578, '⣘'},{ 14578, '⣙'},{ 24578, '⣚'},
{ 124578, '⣛'},{ 34578, '⣜'},{ 134578, '⣝'},{ 234578, '⣞'},{ 1234578, '⣟'},{ 678, '⣠'},{ 1678, '⣡'},{ 2678, '⣢'},
{ 12678, '⣣'},{ 3678, '⣤'},{ 13678, '⣥'},{ 23678, '⣦'},{ 123678, '⣧'},{ 4678, '⣨'},{ 14678, '⣩'},{ 24678, '⣪'},
{ 124678, '⣫'},{ 34678, '⣬'},{ 134678, '⣭'},{ 234678, '⣮'},{ 1234678, '⣯'},{ 5678, '⣰'},{ 15678, '⣱'},{ 25678, '⣲'},{ 125678, '⣳'},{ 35678, '⣴'},
{ 135678, '⣵'},{ 235678, '⣶'},{ 1235678, '⣷'},{ 45678, '⣸'},{ 145678, '⣹'},{ 245678, '⣺'},{ 1245678, '⣻'},
{ 345678, '⣼'},{ 1345678, '⣽'},{ 2345678, '⣾'},{ 12345678, '⣿'},
            /*
            {0, "⠄" },
            {1, "⠁" },{2, "⠂" },{12, "⠃" },{3, "⠄" },{13, "⠅" },
            {23, "⠆" },{123, "⠇" },{4, "⠈" },{14, "⠉" },{24, "⠊" },{124, "⠋" },
            {34, "⠌" },{134, "⠍" },{234, "⠎" },{1234, "⠏" },{5, "⠐" },{15, "⠑" },
            {25, "⠒" },{125, "⠓" },{35, "⠔" },{135, "⠕" },{235, "⠖" },{1235, "⠗" },
            {45, "⠘" },{145, "⠙" },{245, "⠚" },{1245, "⠛" },{345, "⠜" },{1345, "⠝" },
            {2345, "⠞" },{12345, "⠟" },{6, "⠠" },{16, "⠡" },{26, "⠢" },{126, "⠣" },
            {36, "⠤" },{136, "⠥" },{236, "⠦" },{1236, "⠧" },{46, "⠨" },{146, "⠩" },
            {246, "⠪" },{1246, "⠫" },{346, "⠬" },{1346, "⠭" },{2346, "⠮" },{12346, "⠯" },
            {56, "⠰" },{156, "⠱" },{256, "⠲" },{1256, "⠳" },{356, "⠴" },{1356, "⠵" },
            {2356, "⠶" },{12356, "⠷" },{456, "⠸" },{1456, "⠹" },{2456, "⠺" },{12456, "⠻" },
            {3456, "⠼" },{13456, "⠽" },{23456, "⠾" },{123456, "⠿" },{7, "⡀" },{17, "⡁" },
            {27, "⡂" },{127, "⡃" },{37, "⡄" },{137, "⡅" },{237, "⡆" },
            {1237, "⡇" },{47, "⡈" },{147, "⡉" },{247, "⡊" },{1247, "⡋" },
            {347, "⡌" },{1347, "⡍" },{2347, "⡎" },{12347, "⡏" },{57, "⡐" },{157, "⡑" },
            {257, "⡒" },{1257, "⡓" },{357, "⡔" },{1357, "⡕" },{2357, "⡖" },{12357, "⡗" },{457, "⡘" },{1457, "⡙" },
            {2457, "⡚" },{12457, "⡛" },{3457, "⡜" },{13457, "⡝" },{23457, "⡞" },{123457, "⡟" },
            {67, "⡠" },{167, "⡡" },{267, "⡢" },{1267, "⡣" },{367, "⡤" },
            {1367, "⡥" },{2367, "⡦" },{12367, "⡧" },{467, "⡨" },{1467, "⡩" },{2467, "⡪" },
            {12467, "⡫" },{3467, "⡬" },{13467, "⡭" },{23467, "⡮" },{123467, "⡯" },{567, "⡰" },
            {1567, "⡱" },{2567, "⡲" },{12567, "⡳" },{3567, "⡴" },{13567, "⡵" },{23567, "⡶" },
            {123567, "⡷" },{4567, "⡸" },{14567, "⡹" },{24567, "⡺" },{124567, "⡻" },{34567, "⡼" },
            {134567, "⡽" },{234567, "⡾" },{1234567, "⡿" },{8, "⢀" },{18, "⢁" },{28, "⢂" },
            {128, "⢃" },{38, "⢄" },{138, "⢅" },{238, "⢆" },{1238, "⢇" },{48, "⢈" },
            {148, "⢉" },{248, "⢊" },{1248, "⢋" },{348, "⢌" },{1348, "⢍" },
            {2348, "⢎" },{12348, "⢏" },{58, "⢐" },{158, "⢑" },{258, "⢒" },
            {1258, "⢓" },{358, "⢔" },{1358, "⢕" },{2358, "⢖" },{12358, "⢗" },{458, "⢘" },
            {1458, "⢙" },{2458, "⢚" },{12458, "⢛" },{3458, "⢜" },{13458, "⢝" },{23458, "⢞" },
            {123458, "⢟" },{68, "⢠" },{168, "⢡" },{268, "⢢" },{1268, "⢣" },{368, "⢤" },
            {1368, "⢥" },{2368, "⢦" },{12368, "⢧" },{468, "⢨" },{1468, "⢩" },{2468, "⢪" },
            {12468, "⢫" },{3468, "⢬" },{13468, "⢭" },{23468, "⢮" },{123468, "⢯" },
            {568, "⢰" },{1568, "⢱" },{2568, "⢲" },{12568, "⢳" },{3568, "⢴" },{13568, "⢵" },
            {23568, "⢶" },{123568, "⢷" },{4568, "⢸" },{14568, "⢹" },{24568, "⢺" },
            {124568, "⢻" },{34568, "⢼" },{134568, "⢽" },{234568, "⢾" },{1234568, "⢿" },
            {78, "⣀" },{178, "⣁" },{278, "⣂" },{1278, "⣃" },{378, "⣄" },
            {1378, "⣅" },{2378, "⣆" },{12378, "⣇" },{478, "⣈" },{1478, "⣉" },
            {2478, "⣊" },{12478, "⣋" },{3478, "⣌" },{13478, "⣍" },{23478, "⣎" },{123478, "⣏" },
            {578, "⣐" },{1578, "⣑" },{2578, "⣒" },{12578, "⣓" },{3578, "⣔" },
            {13578, "⣕" },{23578, "⣖" },{123578, "⣗" },{4578, "⣘" },{14578, "⣙" },
            {24578, "⣚" },{124578, "⣛" },{34578, "⣜" },{134578, "⣝" },{234578, "⣞" },
            {1234578, "⣟" },{678, "⣠" },{1678, "⣡" },{2678, "⣢" },{12678, "⣣" },
            {3678, "⣤" },{13678, "⣥" },{23678, "⣦" },{123678, "⣧" },{4678, "⣨" },
            {14678, "⣩" },{24678, "⣪" },{124678, "⣫" },{34678, "⣬" },{134678, "⣭" },{234678, "⣮" },
            {1234678, "⣯" },{5678, "⣰" },{15678, "⣱" },{25678, "⣲" },{125678, "⣳" },
            {35678, "⣴" },{135678, "⣵" },{235678, "⣶" },{1235678, "⣷" },{45678, "⣸" },{145678, "⣹" },
            {245678, "⣺" },{1245678, "⣻" },{345678, "⣼" },{1345678, "⣽" },{2345678, "⣾" },{12345678, "⣿" }
            */
        };

        private static GrayscaleArtFormat defaultFormat = new GrayscaleArtFormat("Many",
                    new List<char>() { '.', ',', ';', '+', '*', '?', '%', '$', '#', '@' }, BitmapStandartMethod);

        public static GrayscaleArtFormat DefaultFormat => defaultFormat;

        public static GrayscaleArtFormatCollection GetBitmapAsciiFormats()
        {

            GrayscaleArtFormatCollection collection = new GrayscaleArtFormatCollection()
            {
                defaultFormat,
                new GrayscaleArtFormat("Many2",
                    new List<char>() {' ', '.', ',', ';', '+', '*', '?', '%', '$', '#', '@' }, BitmapStandartMethod),
                new GrayscaleArtFormat("Two",
                    new List<char>() { '░', '█' }, BitmapStandartMethod),
                new GrayscaleArtFormat("Four",
                    new List<char>() { '░', '▒', '▓', '█' }, BitmapStandartMethod),
                new GrayscaleArtFormat("Brayall",
                    brayallSimbols, BitmapBrayallMethod)
            };

            return collection;
        }

        public static string BitmapStandartMethod(Bitmap bitmap, IEnumerable symbols,
            GrayscaleAsciiArt.GrayscaleArtOptions options, CancellationToken? token, IProgress<int> progressReporter)
        {
            char ASCIIMapping(int colorValue, IEnumerable<char> collectionInner)
            {
                int indx = ((int)((double)((double)collectionInner.Count() / 256d) * (double)colorValue));
                return collectionInner.ElementAt(indx);
            }

            using (Bitmap resizedBitmap = bitmap.Resize(options.Width, options.Height))
            {
                StringBuilder stringBuilder = new StringBuilder(resizedBitmap.Width);

                using (FastBitmap fastBitmap = new FastBitmap(resizedBitmap))
                {
                    fastBitmap.Lock();

                    for (int y = 0; y < fastBitmap.Height; y++)
                    {
                        for (int x = 0; x < fastBitmap.Width; x++)
                        {
                            if (token?.IsCancellationRequested ?? false)
                                return null;

                            Color sourseColor = fastBitmap.GetPixel(x, y);
                            int grayColor = (sourseColor.R + sourseColor.B + sourseColor.B) / 3;
                            if (options.InverseColors)
                                grayColor = byte.MaxValue - grayColor;
                            //if (options.Monochrome)
                            //    grayColor = grayColor > 128 ? 0 : 255;
                            char str = ASCIIMapping(grayColor, (IEnumerable<char>)symbols);
                            stringBuilder.Append(str);
                        }
                        stringBuilder.AppendLine();
                        progressReporter?.Report((int)(y * 100f / fastBitmap.Height));
                    }
                    return stringBuilder.ToString();
                }
            }
        }

        public static string BitmapBrayallMethod(Bitmap bitmap, IEnumerable symbols,
            GrayscaleAsciiArt.GrayscaleArtOptions options, CancellationToken? token, IProgress<int> progressReporter)
        {
            char GetBrayallSymbol(byte[,] matrix)
            {
                int key = 0;
                int position = 1;
                for (int x = 0; x < matrix.GetLength(0); x++)
                {
                    for (int y = 0; y < matrix.GetLength(1) - 1; y++, position++)
                    {
                        key = matrix[x, y] == byte.MaxValue ? position + (key * 10) : key;
                    }
                }
                key = matrix[0, 3] == byte.MaxValue ? 7 + (key * 10) : key;
                key = matrix[1, 3] == byte.MaxValue ? 8 + (key * 10) : key;

                if (((Dictionary<int, char>)symbols).TryGetValue(key, out char outputValue))
                    return outputValue;
                return '⠄';
            }

            if (options.Width % 2 != 0 || options.Height % 4 != 0)
                return null;



            using (Bitmap resizedBitmap = bitmap.Resize(options.Width, options.Height))
            {
                StringBuilder stringBuilder = new StringBuilder(resizedBitmap.Width);

                using (FastBitmap fastBitmap = new FastBitmap(resizedBitmap))
                {

                    fastBitmap.Lock();
                    for (int y = 0; y < fastBitmap.Height; y += 4)
                    {
                        for (int x = 0; x < fastBitmap.Width; x += 2)
                        {
                            if (token?.IsCancellationRequested ?? false)
                                return null;

                            byte[,] brayallMatrix = new byte[2, 4];
                            for (int extraY = y; extraY < (y + 4); extraY++)
                            {
                                for (int extraX = 0; extraX < (x + 2); extraX++)
                                {
                                    Color sourseColor = fastBitmap.GetPixel(extraX, extraY);
                                    int grayColor = (sourseColor.R + sourseColor.B + sourseColor.B) / 3;
                                    grayColor = grayColor > 128 ? 0 : 255;
                                    if (options.InverseColors)
                                        grayColor = byte.MaxValue - grayColor;
                                    brayallMatrix[extraX % 2, extraY % 4] = (byte)grayColor;
                                }
                            }
                            stringBuilder.Append(GetBrayallSymbol(brayallMatrix));
                        }
                        stringBuilder.AppendLine();
                        progressReporter?.Report((int)(y * 100f / fastBitmap.Height));
                    }
                    return stringBuilder.ToString();
                }
            }
        }
    }
}
