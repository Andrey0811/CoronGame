using CoronGame.Models.Common;
using CoronGame.Models.Enums;
using NUnit.Framework;

namespace CoronGame.Tests
{
    [TestFixture]
    public class TestWalker
    {
        private Walker temp;
        
        [Test, Order(0)]
        public void CheckDown()
        {
            temp = new Walker(MoveDirection.Down);
            Assert.True(temp.X == 0 && temp.Y == 1);
        }
        
        [Test, Order(1)]
        public void CheckLeft()
        {
            temp = new Walker(MoveDirection.Left);
            Assert.True(temp.X == -1 && temp.Y == 0);
        }
        
        [Test, Order(2)]
        public void CheckRight()
        {
            temp = new Walker(MoveDirection.Right);
            Assert.True(temp.X == 1 && temp.Y == 0);
        }
        
        [Test, Order(3)]
        public void CheckUp()
        {
            temp = new Walker(MoveDirection.Up);
            Assert.True(temp.X == 0 && temp.Y == -1);
        }
    }
}