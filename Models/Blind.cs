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

        private Image[] sprites;
        
        public Blind(Point point, Size size, int moveSpeed, MoveDirection direction, Image[] sprites) 
            : base(point, size, moveSpeed)
        {
            Direction = direction;
            IsFreeze = false;
            this.sprites = sprites;
            Damage = 0;
            Life = 0;
            IsAlive = true;
            CanKill = false;
            JumpHeight = 0;
            IsJumped = false;
            Point = point;
            Size = size;
            MoveSpeed = moveSpeed;
        }

        public int FreezeTime { get; set; }
        public int Time { get; set; }
        public bool IsFreeze { get; set; }
        
        public Image GetImage()
        {
            var temp = sprites[(int) MoveDirection];
            temp.Height = Size.Height;
            temp.Width = Size.Width;
            
            return temp;
        }

        public int Damage { get; }
        public int Life { get; }
        public bool IsAlive { get; set; }
        public bool CanKill { get; set; }
        public int JumpHeight { get; }
        public bool IsJumped { get; }
    }
}