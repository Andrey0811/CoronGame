using System.Windows;
using System.Windows.Controls;
using CoronGame.Common;
using CoronGame.Models.Abstract;
using CoronGame.Models.Interfaces;

namespace CoronGame.Models
{
    public class Player : Essence, IImagable, IFreezable, IKillable, IJump
    {
        private readonly Image[] aliveSprites;

        public Player(Point point, int life, int speed, Image[] aliveSprites)
            : base(point, true, speed)
        {
            this.aliveSprites = aliveSprites;
            MoveSpeed = speed;
            IsFreeze = false;
            Damage = 0;
            CanKill = true;
            IsAlive = true;
            Life = life;
            IsJumped = false;
            JumpHeight = 0;
        }

        public Image GetImage()
        {
            var temp = aliveSprites[(int) MoveDirection];
            temp.Height = Size.Height;
            temp.Width = Size.Width;
            return aliveSprites[(int)MoveDirection];
        }

        public int FreezeTime { get; set; }
        public int Time { get; set; }
        public bool IsFreeze { get; set; }
        public int Damage { get; }
        public int Life { get; set; }
        public bool CanKill { get; set; }
        public int JumpHeight { get; }
        public bool IsJumped { get; }
    }
}