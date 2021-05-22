using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;

namespace PrimaryColorByPic.Helpers
{
    public static class ImageHelper
    {
        public static Bitmap ToBitmap(this BitmapImage bitmapImage)
        {
            using MemoryStream outStream = new();

            BitmapEncoder enc = new BmpBitmapEncoder();

            enc.Frames.Add(BitmapFrame.Create(bitmapImage));

            enc.Save(outStream);

            Bitmap bitmap = new(outStream);

            return new Bitmap(bitmap);
        }
    }
}