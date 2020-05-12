using System;
using System.Drawing;
using System.Windows.Media.Imaging;
using Image = System.Windows.Controls.Image;

namespace CoronGame.Common
{
    public static class ImageParser
    {
        public static Image Parse(string imagePath)
        {
            var image = new Image();
            var uri = new Uri(imagePath, UriKind.RelativeOrAbsolute);
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = uri;
            bitmap.EndInit();
            image.Source = bitmap;
            return image;
        }
    }
}