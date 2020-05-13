using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using CoronGame.Models.Abstract;
using CoronGame.Models.Common;
using CoronGame.Models.Interfaces;

namespace CoronGame.Models
{
    public class Cell : Moveable, IImagable, IFreezable, IAward, IKillable, IJump
    {
        private readonly Color color = Colors.Pink;
        private int cellBounceCount;
        private Image[] aliveSprites;

        public Cell(Point point, Size size, int speed, Image[] aliveSprites, int award) 
            : base(point, size, speed)
        {
            this.aliveSprites = aliveSprites;
            Award = new Award(point, award, color);
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
            
            var temp = aliveSprites[(int) MoveDirection];
            temp.Height = Size.Height;
            temp.Width = Size.Width;
            
            return temp;
        }

        public int FreezeTime { get; set; }
        public int Time { get; set; }
        public bool IsFreeze { get; set; }
        public Award Award { get; }
        public int Damage { get; }
        public int Life { get; }
        public bool IsAlive { get; set; }
        public bool CanKill { get; set; }
    }
}