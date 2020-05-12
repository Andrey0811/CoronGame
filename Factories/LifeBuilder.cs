using System.Windows;
using System.Windows.Controls;
using CoronGame.Common;
using CoronGame.Models;

namespace CoronGame.Factories
{
    public class LifeBuilder
    {
        private readonly Image image = ImageParser.Parse(GlobalConstants.LifeImage);

        public LifeOne CreatLife(Point point) =>
            new LifeOne(point, new Size(20, 20), image);
    }
}