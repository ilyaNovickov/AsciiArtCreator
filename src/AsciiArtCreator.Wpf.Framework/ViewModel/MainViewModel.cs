using System;
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

namespace AsciiArtCreator.Wpf.Framework.ViewModel
{
    internal class MainViewModel : INotifyPropertyChanged
    {
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
                OnPropertyChanged("ImagePath");
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
                return;
            }));
        }

        public RelayCommand SaveCommand
        {
            get => saveCommand ?? (saveCommand = new RelayCommand((_) =>
            {
                return;
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

                OutputArt = await asciiArt.GetOrCreateAsciiArtAsync();

                asciiArt.Dispose();

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

        //public RelayCommand CopyCommand
        //{
        //    get => saveCommand ?? (saveCommand = new RelayCommand((_) =>
        //    {
        //        return;
        //    }));
        //}

        //public ActionCommand<float> ScaleChangeCommand
        //{
        //    get => scaleChangeCommand ?? (scaleChangeCommand = new ActionCommand<float>((value) =>
        //    {
        //        Scale = value;
        //    }));
        //}

        //public RelayCommand SelectFontCommand
        //{
        //    get => selectFontCommand ?? (selectFontCommand = new RelayCommand((_) =>
        //    {
        //        Microsoft.Win32.Fo openFileDialog = new Microsoft.Win32.OpenFileDialog();

        //        openFileDialog.Filter = "Image files (*.png, *.jpeg, *.jpg, *.bmp)|*.png;*.jpeg;*.jpg;*.bmp";

        //        bool? ok = openFileDialog.ShowDialog();

        //        if (ok.HasValue && ok.Value)
        //        {
        //            ImagePath = openFileDialog.FileName;
        //        }
        //    }));
        //}

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



        public void OnPropertyChanged([CallerMemberName] string prop = "")
        { 
           PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
