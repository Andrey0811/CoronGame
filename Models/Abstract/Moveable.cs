using System.Windows;
using CoronGame.Models.Interfaces;
using CoronGame.Models.Common;
using CoronGame.Models.Enums;

namespace CoronGame.Models.Abstract
{

    public abstract class Moveable : IGameObject, IMoveable
    {
        private MoveDirection moveDirection;
        private Walker walker;

        protected Moveable(Point point, Size size, int moveSpeed)
        {
            Point = point;
            Size = size;
            MoveSpeed = moveSpeed;
        }
        
        public MoveDirection MoveDirection
        {
            get => moveDirection;

            set
            {
                moveDirection = value;
                walker = new Walker(MoveDirection);
            }
        }

        public int MoveSpeed { get; set; }

        public void MakeMove()
        {
            var x = Point.X + walker.X * MoveSpeed;
            var y = Point.Y + walker.Y * MoveSpeed;
            Point = new Point(x, y);
        }

        public Size Size { get; set; }
        public Point Point { get; set; }
    }
}