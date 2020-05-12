using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using CoronGame.Models.Abstract;
using CoronGame.Models.Interfaces;

namespace CoronGame.Models
{
    public class Cell : Moveable, IImagable, IFreezable, IAward, IKillable, IJump
    {
        private readonly Color color = Colors.Pink;
        private int cellBounceCount;
        private Image image;

        public Cell(Point point, Size size, int speed, Image image, int award) 
            : base(point, size, speed)
        {
            this.image = image;
            Award = award;
            IsFreeze = false;
            CanKill = true;
            Damage = 0;
            Life = 1;
            IsAlive = true;
        }

        public int JumpHeight => 5;

        public bool IsJumped => cellBounceCount == 5;

        public Image GetImage()
        {
            cellBounceCount--;
            cellBounceCount = cellBounceCount < 0 ? 10 : cellBounceCount;
            return image;
        }

        public int FreezeTime { get; set; }
        public int Time { get; set; }
        public bool IsFreeze { get; set; }
        public int Award { get; }
        public int Damage { get; }
        public int Life { get; }
        public bool IsAlive { get; set; }
        public bool CanKill { get; set; }
    }
}