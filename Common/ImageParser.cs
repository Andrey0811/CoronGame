using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

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