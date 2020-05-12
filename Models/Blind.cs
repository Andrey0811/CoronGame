using System.Windows;
using System.Windows.Controls;
using CoronGame.Models.Abstract;
using CoronGame.Models.Common;
using CoronGame.Models.Enums;
using CoronGame.Models.Interfaces;

namespace CoronGame.Models
{
    public class Blind : Moveable, IFreezable, IImagable, IKillable, IJump
    {
        public MoveDirection Direction { get; }

        private Image image;
        
        public Blind(Point point, Size size, int moveSpeed, MoveDirection direction, Image image) 
            : base(point, size, moveSpeed)
        {
            Direction = direction;
            IsFreeze = false;
            this.image = image;
            Damage = 0;
            Life = 0;
            IsAlive = true;
            CanKill = false;
            JumpHeight = 0;
            IsJumped = false;
        }

        public int FreezeTime { get; set; }
        public int Time { get; set; }
        public bool IsFreeze { get; set; }
        
        public Image GetImage()
        {
            return image;
        }

        public int Damage { get; }
        public int Life { get; }
        public bool IsAlive { get; set; }
        public bool CanKill { get; set; }
        public int JumpHeight { get; }
        public bool IsJumped { get; }
    }
}