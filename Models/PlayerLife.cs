using System.Windows;
using System.Windows.Controls;
using CoronGame.Common;

namespace CoronGame.Models
{
    public class PlayerLife
    {
        public Point Point;
        public Size Size;
        public Image Image;
        public int CountLife;

        public PlayerLife(int countLife)
        {
            Image = ImageParser.Parse(GlobalConstants.LifeImage);
            Point = new Point(0, 636);
            Size = new Size(30, 30);
            CountLife = countLife;
        }
    }
}