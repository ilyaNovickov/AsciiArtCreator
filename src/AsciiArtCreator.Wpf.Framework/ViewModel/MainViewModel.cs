using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using AsciiArtCreator.Wpf.Framework.Commands;

namespace AsciiArtCreator.Wpf.Framework.ViewModel
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        private string imagePath = null;
        private RelayCommand selectFileCommand;
        private int scale = 100;

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
        public event PropertyChangedEventHandler PropertyChanged;
        public float Scale
        {
            get => scale / 100f;
            set
            {
                scale = (int)(value * 100f);
                OnPropertyChanged("Scale");
            }
        }
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        { 
           PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
