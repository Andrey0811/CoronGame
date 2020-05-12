using System;
using System.Windows;
using CoronGame.Models.Common;
using CoronGame.Models.Enums;
using CoronGame.Models.Interfaces;

namespace CoronGame.Maps
{
    public static class FindPathCage
    {
        public static MoveDirection FindPath(IMoveable obj, Map map)
        {
            if (!(Math.Abs((obj.Point.Y - map.DistanceFromY) % map.DistanceBetweenObj) < map.Inaccuracy) ||
                !(Math.Abs((obj.Point.X - map.DistanceFromX) % map.DistanceBetweenObj) < map.Inaccuracy))
                return obj.MoveDirection;
            
            var point = new Point((obj.Point.Y - map.DistanceFromY) / map.DistanceBetweenObj,
                (obj.Point.X - map.DistanceFromX) / map.DistanceBetweenObj);
            var isReflected = false;
            
            if (point.X >= map.Board.GetLength(1))
            {
                isReflected = true;
                point.X = - (point.X - 2 * map.Board.GetLength(1) + 1);
            }

            var maxValue = int.MaxValue;
            var direction = MoveDirection.Up;
                
            if (point.X + 1 < map.Board.GetLength(1)
                && map.Board[(int)point.Y, (int)point.X + 1] != 0
                && map.Board[(int)point.Y, (int)point.X + 1] < maxValue)
            {
                direction = !isReflected
                    ? MoveDirection.Right
                    : MoveDirection.Left;

                maxValue = map.Board[(int)point.Y, (int)point.X + 1];
            }


            if (point.X - 1 >= 0
                && map.Board[(int)point.Y, (int)point.X - 1] != 0
                && map.Board[(int)point.Y, (int)point.X - 1] < maxValue)
            {
                direction = !isReflected
                    ? MoveDirection.Left
                    : MoveDirection.Right;

                maxValue = map.Board[(int)point.Y, (int)point.X - 1];
            }


            if (map.Board[(int)point.Y + 1, (int)point.X] != 0 
                && map.Board[(int)point.Y + 1, (int)point.X] < maxValue)
            {
                direction = MoveDirection.Down;
                maxValue = map.Board[(int)point.Y + 1, (int)point.X];
            }

            if (map.Board[(int)point.Y - 1, (int)point.X] == 0 
                || map.Board[(int)point.Y - 1, (int)point.X] >= maxValue)
                return direction;

            direction = MoveDirection.Up;

            return direction;
        }
    }
}