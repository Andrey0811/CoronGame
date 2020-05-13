using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using CoronGame.Models.Abstract;
using CoronGame.Models.Common;
using CoronGame.Models.Interfaces;

namespace CoronGame.Models
{

    public class Enemy : Essence, IKillable, IImagable, IAward, IFreezable, IJump
    {
        private readonly Color color = Colors.SkyBlue;
        private Image[] aliveSprites;

        private bool canKill;
        private int enemyBounceCount;
        
        public int JumpHeight => enemyBounceCount > 5 ? 6 : 0;

        public bool IsJumped => enemyBounceCount == 5;

        public Enemy(Point point, int speed, bool canKill, bool isAlive, int award, Image[] aliveSprites)
            : base(point, isAlive, speed)
        {
            Award = new Award(point, award, color);
            this.aliveSprites = aliveSprites;
            CanKill = canKill;
            IsFreeze = false;
            Damage = 1;
            Life = 3;
        }

        public int Damage { get; }
        public int Life { get; set; }

        public bool CanKill
        {
            get => canKill;

            set => canKill = value;
        }

        public Image GetImage()
        {
            enemyBounceCount--;
            enemyBounceCount = enemyBounceCount < 0 ? 10 : enemyBounceCount;
            
            var temp = aliveSprites[(int) MoveDirection];
            temp.Height = Size.Height;
            temp.Width = Size.Width;
            
            return temp;
        }

        public Award Award { get; }
        public int FreezeTime { get; set; }
        public int Time { get; set; }
        public bool IsFreeze { get; set; }
    }
}