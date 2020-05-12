using System.Windows;
using System.Windows.Controls;
using CoronGame.Models.Abstract;
using CoronGame.Models.Common;
using CoronGame.Models.Enums;
using CoronGame.Models.Interfaces;

namespace CoronGame.Models
{
    public class Bullet : Moveable, IImagable, IFreezable, IKillable, IJump
    {
        public MoveDirection Direction { get; }
        
        private Image[] sprites;
        
        public Bullet(Point point, Size size, int moveSpeed, MoveDirection direction, Image[] sprites) 
            : base(point, size, moveSpeed)
        {
            Direction = direction;
            FreezeTime = 0;
            IsFreeze = false;
            this.sprites = sprites;
            IsJumped = false;
            JumpHeight = 0;
            Damage = 1;
            Life = 0;
            IsAlive = true;
            CanKill = false;
            Point = point;
            Size = size;
            MoveSpeed = moveSpeed;
        }

        public Image GetImage()
        {
            var temp = sprites[(int) MoveDirection];
            temp.Height = Size.Height;
            temp.Width = Size.Width;
            
            return temp;
        }

        public int FreezeTime { get; set; }
        public int Time { get; set; }
        public bool IsFreeze { get; set; }
        public int Damage { get; }
        public int Life { get; }
        public bool IsAlive { get; set; }
        public bool CanKill { get; set; }
        public int JumpHeight { get; }
        public bool IsJumped { get; }
    }
}