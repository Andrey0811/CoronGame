using System.Windows;
using System.Windows.Controls;
using CoronGame.Common;
using CoronGame.Models;

namespace CoronGame.Factories
{
    public class LifeBuilder
    {
        public static LifeOne CreatLife(Point point) =>
            new LifeOne(point, new Size(20, 20), ImageParser.Parse(GlobalConstants.LifeImage));
    }
}