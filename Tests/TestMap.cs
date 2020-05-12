using System.Linq;
using CoronGame.Maps;
using CoronGame.Models.Common;
using CoronGame.Models.Enums;
using NUnit.Framework;

namespace CoronGame.Tests
{
    [TestFixture]
    public class TestMap
    {
        private Map map = new Map();
        
        [Test, Order(0)]
        public void CheckStrategyOut()
        {
            var temp = map.InitEnemy();
            //Assert.True(MoveDirection.Up == map.FindWayOutOfSpawn(temp.FirstOrDefault()));
        }

        [Test, Order(1)]
        public void CheckStrategyTo()
        {
            var temp = map.InitEnemy();
            //Assert.True(MoveDirection.Up == map.FindWayToSpawn(temp.FirstOrDefault()));
        }

        [Test, Order(2)]
        public void CheckCanMove()
        {
            var temp = map.InitPlayer();
            Assert.False(map.CanMove(temp, MoveDirection.Down));
            Assert.True(map.CanMove(temp, MoveDirection.Left));
            Assert.True(map.CanMove(temp, MoveDirection.Right));
            Assert.False(map.CanMove(temp, MoveDirection.Up));
        }
    }
}