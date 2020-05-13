using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using CoronGame.Maps;
using CoronGame.Models;
using CoronGame.Models.Common;
using CoronGame.Models.Enums;
using CoronGame.Models.Interfaces;

namespace CoronGame.Logic
{
    public partial class Engine
    {
        private int beginningTimeInterval = 10;
        private int nextRoundTimeInterval = 50;
        private int intervalRespawnCell = 70;
        private int intervalRespawnEnemy = 120;
        private const int ChanceToSpawnCell = 555;
        private const int TimerLength = 18;
        private const int CollisionDistance = 10;
        private readonly int minDist = 50;
        private readonly Random rnd = new Random();
        private readonly Map map;
        private readonly IGameRenderer render;
        private readonly SoundProvider.SoundProvider soundProvider;
        public Player player;
        private PlayerLife playerLife;
        private Field field;
        private List<Cell> cells;
        public List<Enemy> enemies;
        private readonly DispatcherTimer timer;
        private bool isGameStarted;
        private MoveDirection playerNextMove;
        private Score score;
        private List<Blind> blinds;
        private List<Bullet> bullets;
        //private List<LifeOne> lifes;
        private Award currentScoreAward;

        public Engine(Map map, IGameRenderer render)
        {
            blinds = new List<Blind>();
            bullets = new List<Bullet>();
            //lifes = new List<LifeOne>();
            score = new Score(new Point(map.Size.Width / 2 + 25, 15), 
                new Size(0, 20));
            this.map = map;
            this.render = render;
            isGameStarted = false;
            timer = new DispatcherTimer();
            soundProvider = new SoundProvider.SoundProvider();
            this.render.UiActionHappened += ActionHappened;
            StartGame();
        }

        public void InitGame()
        {
            InitGameObjects();
        }

        private void InitGameObjects()
        {
            soundProvider.BeginningPlay();
            field = map.InitGate();
            InitPlayers();
        }

        private void InitPlayers()
        {
            enemies = new List<Enemy> {map.InitEnemy(), map.InitEnemy()};
            
            if (player == null)
            {
                player = map.InitPlayer();
                playerLife = new PlayerLife(player.Life);
            }
            else
            {
                player.IsAlive = true;
                player.Point = map.PlayerStartPoint;
            }

            cells = new List<Cell> {map.InitCell()};
        }

        private void StartGame()
        {
            timer.Interval = TimeSpan.FromMilliseconds(TimerLength);
            timer.Tick += GameLoop;
            timer.Start();
        }

        private void GameLoop(object sender, EventArgs e)
        {
            if (isGameStarted) 
                UpdatePositions();

            beginningTimeInterval--;
            DrawObj();
        }
        public static MoveDirection InvertMoveDirection(MoveDirection direction) =>
            direction switch
            {
                MoveDirection.Down => MoveDirection.Up,
                MoveDirection.Left => MoveDirection.Right,
                MoveDirection.Right => MoveDirection.Left,
                MoveDirection.Up => MoveDirection.Down,
                _ => throw new ArgumentException()
            };

        private void ActionHappened(object sender, KeyEventArgs @event)
        {
            if (!isGameStarted && beginningTimeInterval <= 0)
            {
                foreach (var enemy in enemies) 
                    enemy.Point = new Point(enemy.Point.X + 9, enemy.Point.Y);

                isGameStarted = true;
            }

            switch (@event.Act)
            {
                case Act.Move:
                    playerNextMove = @event.MoveDirection switch
                    {
                        MoveDirection.Up => MoveDirection.Up,
                        MoveDirection.Down => MoveDirection.Down,
                        MoveDirection.Left => MoveDirection.Left,
                        MoveDirection.Right => MoveDirection.Right,
                        _ => playerNextMove
                    };
                    break;
                case Act.Blind:
                    var tempPoint = player.Point;
                    var direction = InvertMoveDirection(player.MoveDirection);
                    var walker = new Walker(direction);
                    tempPoint.Offset(2 * walker.X, 2 * walker.Y);
                    blinds.Add(map.InitBlind(tempPoint, direction));
                    break;
                default:
                    throw new ArgumentException();
            }
        }

        public bool FindPlayer(Enemy enemy)
        {
            var walker = new Walker(enemy.MoveDirection);
            var tempPoint = enemy.Point;
            tempPoint.Offset(walker.X, walker.Y);
            
            if (Math.Abs(enemy.Point.X - player.Point.X) < map.Inaccuracy 
                && Math.Abs(enemy.Point.Y - player.Point.Y) < map.Inaccuracy)
                return false;
            
            while (map.CanMoveOfPoint(tempPoint, enemy.MoveDirection))
            {
                if (Math.Abs(tempPoint.X - player.Point.X) < map.Inaccuracy 
                    && Math.Abs(tempPoint.Y - player.Point.Y) < map.Inaccuracy)
                    return true;
                tempPoint.Offset(walker.X, walker.Y);
            }

            return false;
        }
        
        public static bool HitGameObject(IGameObject obj1, IGameObject obj2)
        {
            var x = Math.Pow(obj1.Point.X - obj2.Point.X, 2);
            var y = Math.Pow(obj1.Point.Y - obj2.Point.Y, 2);
            return Math.Sqrt(x + y) < CollisionDistance;
        }

        public MoveDirection GenerateEnemyMove(IMoveable obj)
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

        public MoveDirection GenerateMoveToPlayer(IMoveable obj, bool isNear)
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