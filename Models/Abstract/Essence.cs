using System.Windows;
using CoronGame.Models.Common;
using CoronGame.Models.Interfaces;

namespace CoronGame.Models.Abstract
{
    public abstract class Essence : Moveable
    {
        protected Essence(Point point, bool isAlive, int speed)
            : base(point, new Size(30, 30), speed)
        {
            IsAlive = isAlive;
        }

        public bool IsAlive { get; set; }
    }
}