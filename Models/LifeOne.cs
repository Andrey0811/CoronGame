using System.Windows;
using System.Windows.Controls;
using CoronGame.Models.Interfaces;

namespace CoronGame.Models
{
    public class LifeOne : IKillable, IImagable, IGameObject, IJump
    {
        public int Damage { get; }
        public int Life { get; }
        public bool IsAlive { get; set; }
        public bool CanKill { get; set; }
        public Size Size { get; }
        public Point Point { get; set; }
        private Image image;

        public LifeOne(Point point, Size size, Image image)
        {
            Damage = 0;
            Life = 1;
            CanKill = true;
            Point = point;
            Size = size;
            this.image = image;
            IsJumped = false;
            JumpHeight = 0;
        }

        public Image GetImage()
        {
            var temp = image;
            temp.Height = Size.Height;
            temp.Width = Size.Width;
            return temp;
        }

        public int JumpHeight { get; }
        public bool IsJumped { get; }
    }
}