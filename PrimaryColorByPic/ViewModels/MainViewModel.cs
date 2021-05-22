using Microsoft.Win32;

using PrimaryColorByPic.Core;
using PrimaryColorByPic.Helpers;

using System;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PrimaryColorByPic.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private System.Windows.Media.Brush _mostUsedColor;

        public System.Windows.Media.Brush MostUsedColor
        {
            get => _mostUsedColor;
            set
            {
                _mostUsedColor = value;
                OnPropertyChanged();
            }
        }

        private System.Windows.Media.Brush _nearestColor;

        public System.Windows.Media.Brush NearestColor
        {
            get => _nearestColor;
            set
            {
                _nearestColor = value;
                OnPropertyChanged();
            }
        }

        private BitmapImage _testImage;

        public BitmapImage TestImage
        {
            get => _testImage;
            set
            {
                _testImage = value;
                OnPropertyChanged();
            }
        }

        private ICommand _loadImageCommand;
        public ICommand LoadImageCommand => _loadImageCommand ??= new RelayCommand(LoadImage);

        private void LoadImage(object commandParameter)
        {
            OpenFileDialog openFileDialog = new()
            { CheckPathExists = true, Filter = "Файлы изображений|*.jpg;*.png;" };

            openFileDialog.ShowDialog();

            string path = openFileDialog.FileName;

            if (string.IsNullOrWhiteSpace(path))
                return;

            TestImage = new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute));

            Color mostUsedColor = Palette.GetMostUsedColor(TestImage.ToBitmap());
            Color nearestColor = Palette.GetNearestColor(mostUsedColor);

            MostUsedColor = new SolidColorBrush(System.Windows.Media.Color.FromArgb(mostUsedColor.A, mostUsedColor.R, mostUsedColor.G, mostUsedColor.B));
            NearestColor = new SolidColorBrush(System.Windows.Media.Color.FromArgb(nearestColor.A, nearestColor.R, nearestColor.G, nearestColor.B));
        }
    }
}
