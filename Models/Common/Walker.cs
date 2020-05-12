using System;
using CoronGame.Models.Enums;

namespace CoronGame.Models.Common
{
    public class Walker
    {
        
        public int X { get; }

        public int Y { get; }
        
        public Walker(MoveDirection moveDirection)
        {
            //X = 0;
            //Y = 0;
            switch (moveDirection)
            {
                case MoveDirection.Up:
                    X = 0;
                    Y = -1;
                    break;
                case MoveDirection.Down:
                    X = 0;
                    Y = 1;
                    break;
                case MoveDirection.Left:
                    X = -1;
                    Y = 0;
                    break;
                case MoveDirection.Right:
                    X = 1;
                    Y = 0;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}