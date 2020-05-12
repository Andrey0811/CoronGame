using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using CoronGame.Common;
using CoronGame.Factories;
using CoronGame.Models;
using CoronGame.Models.Common;
using CoronGame.Models.Enums;
using CoronGame.Models.Interfaces;

namespace CoronGame.Maps
{
    public class Map : IImagable, IGameObject, IJump
    {
        public readonly int DistanceBetweenObj;
        public readonly int DistanceFromX;
        public readonly int DistanceFromY;
        public readonly double Inaccuracy;
        private readonly Random rnd;
        public readonly int[,] Board;
        
        private readonly List<Point> cellStartPoints;
        public readonly Point PlayerStartPoint;
        private readonly List<Point> enemyStartPoints;
        
        private readonly Image image;
        private readonly EnemyFactory playerFactory;
        private readonly CellFactory cellFactory;
        private readonly PlayerBuilder playerBuilder;
        private readonly BulletBuilder bulletBuilder;
        private readonly BlindBuilder blindBuilder;
        private readonly LifeBuilder lifeBuilder;
        
        public Size Size { get; }
        public Point Point { get; set; }

        public Map(int mapWidth = 506, int mapHeight = 560, int distanceBetweenObj = 18,
        int distanceFromX = 10, int distanceFromY = 60, double inaccuracy = 0.001)
        {
            DistanceBetweenObj = distanceBetweenObj;
            DistanceFromX = distanceFromX;
            DistanceFromY = distanceFromY;
            Inaccuracy = inaccuracy;
            rnd = new Random();
            Board = InitBoard();
            IsJumped = false;
            JumpHeight = 0;
            
            Point = new Point(mapWidth / 2, mapHeight / 2 + DistanceFromY - DistanceFromX);
            Size = new Size(mapWidth, mapHeight);
            image = ImageParser.Parse(GlobalConstants.ClassicMapImage);
            
            cellStartPoints = new List<Point>
            {
                GetNewPoint(new Point(13, 17), 1)
            };

            PlayerStartPoint = GetNewPoint(new Point(13, 23));

            enemyStartPoints = new List<Point>
            {
                GetNewPoint(new Point(13, 14)),
                GetNewPoint(new Point(13, 11)),
                GetNewPoint(new Point(15, 14))
            };

            playerFactory = new EnemyFactory();
            cellFactory = new CellFactory();
            playerBuilder = new PlayerBuilder(3);
            bulletBuilder = new BulletBuilder();
            blindBuilder = new BlindBuilder();
            lifeBuilder = new LifeBuilder();
        }

        public bool CanMove(IMoveable obj, MoveDirection direction)
        {
            var walker = new Walker(direction);
            var point = new Point(obj.Point.X - DistanceFromX, obj.Point.Y - DistanceFromY);
            
            if (Math.Abs(point.Y % DistanceBetweenObj) < Inaccuracy &&
                Math.Abs(point.X % DistanceBetweenObj) < Inaccuracy)
            {
                point.Y /= DistanceBetweenObj;
                point.X /= DistanceBetweenObj;
                point.Offset(walker.X, walker.Y);

                if (point.X >= Board.GetLength(1)) 
                    point.X = - (point.X - 2 * Board.GetLength(1) + 1);

                if (point.X >= 0) 
                    return Board[(int)point.Y, (int)point.X] == 1;
                
                if (obj is Player)
                {
                    obj.Point = Math.Abs(obj.Point.X - DistanceFromX) < Inaccuracy 
                        ? new Point(DistanceFromX + DistanceBetweenObj * 28, obj.Point.Y) 
                        : new Point(DistanceFromX, obj.Point.Y);

                    return true;
                }

                obj.MoveDirection = Math.Abs(obj.Point.X - DistanceFromX) < Inaccuracy
                    ? MoveDirection.Right
                    : MoveDirection.Left;

                return false;
            }

            if (Math.Abs(point.Y % DistanceBetweenObj) < Inaccuracy)
            {
                if (direction == MoveDirection.Left || direction == MoveDirection.Right)
                    return true;
            }
            else if (Math.Abs(point.X % DistanceBetweenObj) < Inaccuracy)
                if (direction == MoveDirection.Up || direction == MoveDirection.Down)
                    return true;

            return false;
        }

        public bool CanMoveOfPoint(Point startPoint, MoveDirection direction)
        {
            var walker = new Walker(direction);
            var point = new Point(startPoint.X - DistanceFromX, startPoint.Y - DistanceFromY);
            
            if (Math.Abs(point.Y % DistanceBetweenObj) < Inaccuracy &&
                Math.Abs(point.X % DistanceBetweenObj) < Inaccuracy)
            {
                point.Y /= DistanceBetweenObj;
                point.X /= DistanceBetweenObj;
                point.Offset(walker.X, walker.Y);

                if (point.X >= Board.GetLength(1)) 
                    point.X = - (point.X - 2 * Board.GetLength(1) + 1);

                if (point.X >= 0) 
                    return Board[(int)point.Y, (int)point.X] == 1;

                return false;
            }

            if (Math.Abs(point.Y % DistanceBetweenObj) < Inaccuracy)
            {
                if (direction == MoveDirection.Left || direction == MoveDirection.Right)
                    return true;
            }
            else if (Math.Abs(point.X % DistanceBetweenObj) < Inaccuracy)
                if (direction == MoveDirection.Up || direction == MoveDirection.Down)
                    return true;

            return false;
        }

        public Image GetImage() => image;

        public Field InitGate() =>
            new Field(new Point(
                    DistanceBetweenObj * 13 + DistanceBetweenObj / 2 + DistanceFromX,
                    DistanceBetweenObj * 12 + DistanceBetweenObj / 6 + DistanceFromY), 
                new Size(45, 5));

        public Cell InitCell() =>
            rnd.Next(2) switch
            {
                0 => CellFactory.CreateCell(CellTypes.Rich, 
                    cellStartPoints[rnd.Next(cellStartPoints.Count)]),
                1 => CellFactory.CreateCell(CellTypes.Simple, 
                    cellStartPoints[rnd.Next(cellStartPoints.Count)]),
                _ => throw new ArgumentException()
            };

        public Player InitPlayer() =>
            playerBuilder.CreatePlayer(PlayerStartPoint);

        public Enemy InitEnemy()
        {
            return rnd.Next(3) switch
            {
                0 => EnemyFactory.CreateEnemy(EnemyTypes.Tank, 
                    enemyStartPoints[rnd.Next(enemyStartPoints.Count)]),
                1 => EnemyFactory.CreateEnemy(EnemyTypes.Speed, 
                    enemyStartPoints[rnd.Next(enemyStartPoints.Count)]),
                2 => EnemyFactory.CreateEnemy(EnemyTypes.Simple, 
                    enemyStartPoints[rnd.Next(enemyStartPoints.Count)]),
                _ => throw new ArgumentException()
            };
        }

        public Bullet InitBullet(Point point, MoveDirection direction) => 
            BulletBuilder.CreatBullet(point, direction);

        public Blind InitBlind(Point point, MoveDirection direction) =>
            BlindBuilder.CreatBullet(point, direction);

        public LifeOne InitLife(Point point) => 
            LifeBuilder.CreatLife(point);

        public MoveDirection FindWay(IMoveable obj) =>
            FindPathCage.FindPath(obj, this);

        private static int[,] InitBoard()
        {
            return new[,]
            {
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0},
                {0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0},
                {0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0},
                {0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0},
                {0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {0, 1, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0},
                {0, 1, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0},
                {0, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0},
                {0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0},
                {0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0},
                {0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 1, 1, 1, 1},
                {0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 1, 0, 1, 1},
                {0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 1, 0, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 1, 1, 1},
                {0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 1, 1, 1, 1},
                {0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0},
                {0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0},
                {0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0},
                {0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0},
                {0, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1},
                {0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0},
                {0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0},
                {0, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0},
                {0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
                {0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
                {0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
            };
        }

        private Point GetNewPoint(Point point, int move = 0) =>
            new Point(
                DistanceBetweenObj * point.X + DistanceBetweenObj / 2 + move + DistanceFromX,
                DistanceBetweenObj * point.Y + DistanceFromY);

        public int JumpHeight { get; }
        public bool IsJumped { get; }
    }
}