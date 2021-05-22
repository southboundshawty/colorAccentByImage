using System;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using PrimaryColorByPic.Core;
using PrimaryColorByPic.Helpers;

namespace PrimaryColorByPic.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private ICommand _loadImageCommand;
        private Brush _mostUsedColor;

        private Brush _nearestColor;

        private BitmapImage _testImage;

        public Brush MostUsedColor
        {
            get => _mostUsedColor;
            set
            {
                _mostUsedColor = value;
                OnPropertyChanged();
            }
        }

        public Brush NearestColor
        {
            get => _nearestColor;
            set
            {
                _nearestColor = value;
                OnPropertyChanged();
            }
        }

        public BitmapImage TestImage
        {
            get => _testImage;
            set
            {
                _testImage = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoadImageCommand => _loadImageCommand ??= new RelayCommand(LoadImage);

        private async void LoadImage(object commandParameter)
        {
            OpenFileDialog openFileDialog = new()
                {CheckPathExists = true, Filter = "Файлы изображений|*.jpg;*.png;"};

            openFileDialog.ShowDialog();

            string path = openFileDialog.FileName;

            if (string.IsNullOrWhiteSpace(path))
                return;

            TestImage = new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute));

            Color mostUsedColor = default; 
            Color nearestColor = default;

            await Task.Factory.StartNew(() =>
            {
                mostUsedColor = Palette.GetMostUsedColor(TestImage.ToBitmap());
                nearestColor = Palette.GetNearestColor(mostUsedColor);
            });

            MostUsedColor =
                new SolidColorBrush(mostUsedColor);
            NearestColor =
                new SolidColorBrush(nearestColor);
        }
    }
}