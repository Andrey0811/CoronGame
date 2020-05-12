using System.Windows.Input;
using CoronGame.Models.Common;
using CoronGame.Models.Enums;
using NUnit.Framework;
using KeyEventArgs = CoronGame.Logic.KeyEventArgs;

namespace CoronGame.Tests
{
    [TestFixture]
    public class TestKeyEvents
    {
        private KeyEventArgs temp;
        
        [Test, Order(0)]
        public void CheckDown()
        {
            temp = new KeyEventArgs(Key.Down);
            Assert.True(temp.MoveDirection == MoveDirection.Down);
        }
        
        [Test, Order(1)]
        public void CheckLeft()
        {
            temp = new KeyEventArgs(Key.Left);
            Assert.True(temp.MoveDirection == MoveDirection.Left);
        }
        
        [Test, Order(2)]
        public void CheckRight()
        {
            temp = new KeyEventArgs(Key.Right);
            Assert.True(temp.MoveDirection == MoveDirection.Right);
        }
        
        [Test, Order(3)]
        public void CheckUp()
        {
            temp = new KeyEventArgs(Key.Up);
            Assert.True(temp.MoveDirection == MoveDirection.Up);
        }
        
        [Test, Order(3)]
        public void CheckLeftCtrl()
        {
            temp = new KeyEventArgs(Key.RightCtrl);
            Assert.True(temp.Act == Act.Blind);
        }
    }
}