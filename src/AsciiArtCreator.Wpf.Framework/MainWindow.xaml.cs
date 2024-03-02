using AsciiArtCreator.Wpf.Framework.Helpers;
using AsciiArtCreator.Wpf.Framework.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AsciiArtCreator.Wpf.Framework
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel viewModel = null;

        private ObservableCollection<FontFamily> fonts = null;

        public MainWindow()
        {
            InitializeComponent();
            viewModel = new MainViewModel();
            DataContext = viewModel;
        }

        private double a => (viewModel.MinScale * viewModel.MaxScale - Math.Pow(1d, 2d)) / (viewModel.MinScale - 2d * 1d + viewModel.MaxScale);
        private double b => Math.Pow((1d - viewModel.MinScale), 2d) / (viewModel.MinScale - 2d * 1d + viewModel.MaxScale);
        private double c => 2d * Math.Log((viewModel.MaxScale - 1d) / (1d - viewModel.MinScale));

        public ObservableCollection<FontFamily> Fonts => fonts;

        private async void GetFontsAsync()
        {
            IEnumerable<FontFamily> avaibleFonts = await Task.Run(() => FontHelper.GetFonts());

            fonts = new ObservableCollection<FontFamily>(avaibleFonts);

            fontComboBox.ItemsSource = Fonts;

            fontComboBox.SelectedIndex = Fonts.IndexOf(new FontFamily("Consolas"));
        }

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (viewModel == null)
                return;
            
            viewModel.Scale = (float)(a + b * Math.Exp(c * slider.Value));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            switch (((Button)sender).Content.ToString())
            {
                case "2%":
                    viewModel.Scale = .02f;
                    break;
                case "50%":
                    viewModel.Scale = .5f;
                    break;
                
                case "500%":
                    viewModel.Scale = 5.0f;
                    break;
                case "1000%":
                    viewModel.Scale = 10.0f;
                    break;
                default:
                case "100%":
                    viewModel.Scale = 1.0f;
                    break;
            }

            slider.Value = Math.Log((viewModel.Scale - a) / b) / c; 
        }

        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            this.richTextBox.Copy();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetFontsAsync();
        }

        private void intUpDown_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            

            FormattedText formattedText = new FormattedText(
                            "A",
                            CultureInfo.CurrentCulture,
                            FlowDirection.LeftToRight,
                            ((FontFamily)fontComboBox.SelectedItem).GetTypefaces().First(),
                            intUpDown.Value.Value,
                            Brushes.Black,
                            new NumberSubstitution(),
                            1);

            var a = formattedText.Width * viewModel.AsciiArtData.Width;
            var b = formattedText.Height * viewModel.AsciiArtData.Height;

        }
    }
}
