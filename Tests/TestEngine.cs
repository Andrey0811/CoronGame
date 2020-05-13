using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CoronGame.Logic;
using CoronGame.Maps;
using CoronGame.Models;
using CoronGame.Models.Enums;
using NUnit.Framework;

namespace CoronGame.Tests
{
    [TestFixture]
    public class TestEngine
    {
        private readonly Engine engine = new Engine(new Map(), new Render(new Canvas()));
        
        [Test, Order(0)]
        public void CheckIsHitGameObject()
        {
            engine.player = new Player(new Point(0, 0), 2, 1, new Image[0]);
            engine.enemies.Add(new Enemy(
                new Point(1, 0), 1, true, true, 0, new Image[0]));
            Assert.True(Engine.HitGameObject(engine.enemies.First(), engine.player));
            engine.enemies.Clear();
            engine.enemies.Add(new Enemy(
                new Point(100, 0), 1, true, true, 0, new Image[0]));
            Assert.False(Engine.HitGameObject(engine.enemies.First(), engine.player));
            engine.enemies.Clear();
        }
        
        [Test, Order(1)]
        public void CheckInvertMoveDirection()
        {
            Assert.True(engine.InvertMoveDirection(MoveDirection.Down) == MoveDirection.Up);
            Assert.True(engine.InvertMoveDirection(MoveDirection.Right) == MoveDirection.Left);
        }
        
        [Test, Order(2)]
        public void CheckGenerateEnemyMove()
        {
            engine.enemies.Add(new Enemy(
                new Point(1, 0), 1, true, true, 0, new Image[0]));
            var temp = engine.GenerateEnemyMove(engine.enemies.First());
            Assert.True(temp == MoveDirection.Left || temp == MoveDirection.Right);
            engine.enemies.Clear();
        }
        
        [Test, Order(3)]
        public void CheckGenerateMoveToPlayer()
        {
            engine.player = new Player(new Point(0, 0), 2, 1, new Image[0])
            {
                MoveDirection = MoveDirection.Left
            };
            engine.enemies.Add(new Enemy(
                new Point(1, 0), 1, true, true, 0, new Image[0]));
            Assert.True(engine.GenerateMoveToPlayer(engine.enemies.First(), true) == MoveDirection.Left);
            engine.player = new Player(new Point(100, 0), 2, 1, new Image[0]);
            var temp = engine.GenerateMoveToPlayer(engine.enemies.First(), false);
            Assert.True(temp == MoveDirection.Left || temp == MoveDirection.Right);
        }

        [Test, Order(4)]
        public void CheckFindPlayer()
        {
            engine.player = new Player(new Point(0, 0), 2, 1, new Image[0]);
            engine.enemies.Add(new Enemy(
                new Point(1, 0), 1, true, true, 0, new Image[0]));
            engine.enemies.First().MoveDirection = MoveDirection.Left;
            Assert.True(engine.FindPlayer(engine.enemies.First()));
            engine.enemies.First().MoveDirection = MoveDirection.Right;
            Assert.False(engine.FindPlayer(engine.enemies.First()));
            engine.enemies.Clear();
        }
    }
}