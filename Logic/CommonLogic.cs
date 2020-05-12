using System;
using System.Collections.Generic;
using CoronGame.Models.Common;
using CoronGame.Models.Enums;
using CoronGame.Models.Interfaces;

namespace CoronGame.Logic
{
    public partial class Engine
    {
        private readonly int minDist = 50;
        private readonly Random rnd = new Random();

        private static bool HitGameObject(IGameObject obj1, IGameObject obj2)
        {
            var x = Math.Pow(obj1.Point.X - obj2.Point.X, 2);
            var y = Math.Pow(obj1.Point.Y - obj2.Point.Y, 2);
            return Math.Sqrt(x + y) < CollisionDistance;
        }

        private MoveDirection GenerateEnemyMove(IMoveable obj)
        {
            var directions = new List<MoveDirection>();
            if (map.CanMove(obj, MoveDirection.Up) && obj.MoveDirection != MoveDirection.Down)
                directions.Add(MoveDirection.Up);

            if (map.CanMove(obj, MoveDirection.Down) && obj.MoveDirection != MoveDirection.Up)
                directions.Add(MoveDirection.Down);

            if (map.CanMove(obj, MoveDirection.Right) && obj.MoveDirection != MoveDirection.Left)
                directions.Add(MoveDirection.Right);

            if (map.CanMove(obj, MoveDirection.Left) && obj.MoveDirection != MoveDirection.Right)
                directions.Add(MoveDirection.Left);

            return directions.Count switch
            {
                0 => obj.MoveDirection,
                1 => directions[0],
                _ => directions[rnd.Next(directions.Count)]
            };
        }

        private MoveDirection GenerateMoveToPlayer(IMoveable obj, bool isNear)
        {
            var directions = new List<MoveDirection>();
            if (map.CanMove(obj, MoveDirection.Up)) 
                directions.Add(MoveDirection.Up);

            if (map.CanMove(obj, MoveDirection.Down)) 
                directions.Add(MoveDirection.Down);

            if (map.CanMove(obj, MoveDirection.Right)) 
                directions.Add(MoveDirection.Right);

            if (map.CanMove(obj, MoveDirection.Left)) 
                directions.Add(MoveDirection.Left);

            var bestDirection = MoveDirection.Up;
            var exsDistance = double.MaxValue;
            foreach (var direction in directions)
            {
                var move = new Walker(direction);
                var x = Math.Pow(player.Point.X - (obj.Point.X + move.X), 2);
                var y = Math.Pow(player.Point.Y - (obj.Point.Y + move.Y), 2);
                var temp = Math.Sqrt(x + y);
                if (isNear)
                {
                    if (!(temp < exsDistance)) continue;
                    exsDistance = temp;
                    bestDirection = direction;
                }
                else
                {
                    if (!(temp > exsDistance)) continue;
                    exsDistance = temp;
                    bestDirection = direction;
                }
            }

            return exsDistance < minDist 
                ? bestDirection 
                : GenerateEnemyMove(obj);
        }
    }
}