﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using AsciiArtCreator.Wpf.Framework.Commands;
using AsciiArtCreator.SystemDrawing.Framework;
using AsciiArtCreator.Wpf.Framework.Helpers;
using System.Collections.ObjectModel;
using System.Windows.Media.Media3D;
using static AsciiArtCreator.Wpf.Framework.ViewModel.MainViewModel;
using System.Runtime.InteropServices;
using System.Windows.Media.Imaging;
using System.Globalization;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;

namespace AsciiArtCreator.Wpf.Framework.ViewModel
{
    

    public class MainViewModel : INotifyPropertyChanged
    {
        public class ArtData : INotifyPropertyChanged
        {
            private int width = 500;
            private int height = 500;
            private bool saveProportions = false;
            private float proportionValue = 1f;
            private int minWidth = 2;
            private int minHeight = 4;
            private int maxWidth = 5000;
            private int maxHeight = 5000;

            public float ProportionValueWH
            {
                get => proportionValue;
                set
                {
                    if (value <= 0)
                        proportionValue = 1f;
                    else
                        proportionValue = value;
                    OnPropertyChanged("ProportionValueWH");
                }
            }

            public int Width
            {
                get => width;
                set
                {
                    if (value < MinWidth || value > MaxWidth)
                        //throw new Exception("Значение должно быть в интервале от минимального до максимального");
                        return;

                    if (SaveProportions && height != (int)Math.Round(value / ProportionValueWH))
                    {
                        int newHeight = (int)Math.Round(value / ProportionValueWH);
                        
                        if (newHeight < MinHeight || newHeight > MaxHeight)
                            return;
                        width = value;
                        Height = newHeight;
                    }
                    else
                        width = value;
                    OnPropertyChanged("Width");
                }
            }

            public int Height
            {
                get => height;
                set
                {
                    if (value < MinHeight || value > MaxHeight)
                        //throw new Exception("Значение должно быть в интервале от минимального до максимального");
                        return;

                    if (SaveProportions && width != (int)Math.Round(value * ProportionValueWH))
                    {
                        int newWidth = (int)Math.Round(value * ProportionValueWH);
                        
                        if (newWidth < MinWidth || newWidth > MaxWidth)
                            return;
                        height = value;
                        Width = newWidth;
                    }
                    else   
                        height = value;

                    OnPropertyChanged("Height");
                }
            }

            public bool SaveProportions
            {
                get => proportionValue > 0 ? saveProportions : false;
                set
                {
                    saveProportions = value;
                    UpdateSize();
                    OnPropertyChanged("SaveProportions");
                }
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
                    OnPropertyChanged("MinWidth");
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
                    OnPropertyChanged("MinHeight");
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
                    OnPropertyChanged("MaxWidth");
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
                    OnPropertyChanged("MaxHeight"); ;
                }
            }

            public void UpdateSize()
            {
                if (!saveProportions)
                    return;

                Width = Width;
            }

            public event PropertyChangedEventHandler PropertyChanged;

            public void OnPropertyChanged([CallerMemberName] string prop = "")
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
            }
        }

        private string imagePath = null;
        private RelayCommand selectFileCommand;
        private RelayCommand saveCommand;
        private RelayCommand exportCommand;
        private RelayCommand getArtCommand;
        private RelayCommand stopCommand;
        //private RelayCommand copyCommand;
        //private ActionCommand<float> scaleChangeCommand;
        //private RelayCommand selectFontCommand;
        private ObservableCollection<GrayscaleArtFormat> artFormats = 
            new ObservableCollection<GrayscaleArtFormat>(StandartAsciiArtMethods.GetBitmapAsciiFormats());
        private float minScale = 0.02f;
        private float maxScale = 10.0f;
        private float scale = 1f;
        private ArtData artData = new ArtData();

        public ArtData AsciiArtData
        {
            get => artData;
            //set
            //{
            //    artData = value;
            //    OnPropertyChanged("AsciiArtData");
            //}
        }

        private string art = "";//"{\\rtf1\\ansi\\ansicpg1252\\uc1\\htmautsp\\deff2{\\fonttbl{\\f0\\fcharset0 Times New Roman;}{\\f2\\fcharset0 Segoe UI;}}{\\colortbl\\red0\\green0\\blue0;\\red255\\green255\\blue255;}\\loch\\hich\\dbch\\pard\\plain\\ltrpar\\itap0{\\lang1033\\fs18\\f2\\cf0 \\cf0\\ql{\\f2 {\\ltrch This is the }{\\b\\ltrch RichTextBox}\\li0\\ri0\\sa0\\sb0\\fi0\\ql\\par}}}";

        public ObservableCollection<GrayscaleArtFormat> ArtFormats
        {
            get => artFormats;
        }

        public string ImagePath
        {
            get => imagePath;
            set
            {
                if (!File.Exists(value))
                    return;

                imagePath = value;
                GetImageSize(value);
                OnPropertyChanged("ImagePath");
            }
        }

        private void GetImageSize(string path)
        {
            using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                BitmapFrame bitmapFrame = BitmapFrame.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.None);

                artData.ProportionValueWH = (float)bitmapFrame.PixelWidth / (float)bitmapFrame.PixelHeight;
            }

            
        }

        public RelayCommand SelectFileCommand
        {
            get => selectFileCommand ?? (selectFileCommand = new RelayCommand((_) =>
            {
                Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();

                openFileDialog.Filter = "Image files (*.png, *.jpeg, *.jpg, *.bmp)|*.png;*.jpeg;*.jpg;*.bmp";

                bool? ok = openFileDialog.ShowDialog();

                if (ok.HasValue && ok.Value)
                {
                    ImagePath = openFileDialog.FileName;
                }
            }));
        }

        public RelayCommand ExportCommand
        {
            get => exportCommand ?? (exportCommand = new RelayCommand((_) =>
            {
                Image myImage = new Image();
                FormattedText text = new FormattedText(OutputArt,
                        new CultureInfo("en-us"),
                        FlowDirection.LeftToRight,
                        new Typeface(new FontFamily("Consolas"), FontStyles.Normal, FontWeights.Normal, new FontStretch()),
                        12d,
                        Brushes.Black);

                DrawingVisual drawingVisual = new DrawingVisual();
                DrawingContext drawingContext = drawingVisual.RenderOpen();
                drawingContext.DrawText(text, new Point(2, 2));
                drawingContext.Close();

                RenderTargetBitmap bmp = new RenderTargetBitmap(6000, 6000, 120, 96, PixelFormats.Pbgra32);
                bmp.Render(drawingVisual);

                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bmp));

                // 6) save image to file
                using (var imageFile = new FileStream("text.png", FileMode.Create, FileAccess.Write))
                {
                    encoder.Save(imageFile);
                    imageFile.Flush();
                    imageFile.Close();
                }
            }));
        }

        public RelayCommand SaveCommand
        {
            get => saveCommand ?? (saveCommand = new RelayCommand((_) =>
            {
                if (OutputArt == null)
                    return;

                Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();

                saveFileDialog.Filter = "Text file (*.txt)|*.txt";

                bool? res = saveFileDialog.ShowDialog();

                if (res.HasValue && res.Value)
                {
                    using (FileStream fileStream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                    {
                        StreamWriter streamWriter = new StreamWriter(fileStream);

                        streamWriter.Write(OutputArt);

                        streamWriter.Close();
                    }
                }
            }));
        }

        public RelayCommand GetArtCommand
        {
            get => getArtCommand ?? (getArtCommand = new RelayCommand(async (_) =>
            {
                if (ImagePath == null)
                    return;

                

                GrayscaleAsciiArt asciiArt = new GrayscaleAsciiArt(ImagePath);

                asciiArt.Format = (GrayscaleArtFormat)_;

                asciiArt.ImageOptions.Height = artData.Height;
                asciiArt.ImageOptions.Width = artData.Width;

                string str = await asciiArt.GetOrCreateAsciiArtAsync();

                asciiArt.Dispose();

                GetDocSize(in str);

                OutputArt = str;

                return;
            }));
        }

        public RelayCommand StopCommand
        {
            get => stopCommand ?? (stopCommand = new RelayCommand((_) =>
            {
                return;
            }));
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public float MinScale
        {
            get => minScale;
            set
            {
                if (value <= 0 && value > maxScale)
                    throw new Exception("Минимальное значение не может быть ниже 0 или больше максимального");
                minScale = value;
                OnPropertyChanged("MinScale");
            }
        }

        public float MaxScale
        {
            get => maxScale;
            set
            {
                if (value <= 0 && value < minScale)
                    throw new Exception("Минимальное значение не может быть ниже 0 или меньше минимального");
                maxScale = value;
                OnPropertyChanged("MaxScale");
            }
        }

        public float Scale
        {
            get => scale;
            set
            {
                if (value < minScale && value > maxScale)
                    throw new Exception("Маштаб должен быть в интервале от Min до Max");
                scale = value;
                OnPropertyChanged("Scale");
            }
        }

        public string OutputArt
        {
            get => art;
            set
            {
                art = value;
                OnPropertyChanged("OutputArt");
            }
        }

        private double docWidth = 500d;
        private double docHeight = 500d;

        public double DocWidth
        {
            get => docWidth;
            set
            {
                docWidth = value;
                OnPropertyChanged("DocWidth");
            }
        }

        public double DocHeight
        {
            get => docHeight;
            set
            {
                docHeight = value;
                OnPropertyChanged("DocHeight");
            }
        }

        private void GetDocSize(in string str)
        {
            FontFamily fontFamily = new FontFamily("Consolas");

            FormattedText formattedText = new FormattedText(
                            str,
                            CultureInfo.CurrentCulture,
                            FlowDirection.LeftToRight,
                            fontFamily.GetTypefaces().First(),
                            12d,
                            Brushes.Black,
                            new NumberSubstitution(),
                            1.25d);

            DocWidth = formattedText.Width + 30d;//* artData.Width + 5d;
            DocHeight = formattedText.Height + 30d;//* artData.Height + 5d;
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        { 
           PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
