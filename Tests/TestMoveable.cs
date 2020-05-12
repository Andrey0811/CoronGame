using System;
using System.Windows;
using System.Windows.Controls;
using CoronGame.Models;
using NUnit.Framework;

namespace CoronGame.Tests
{
    [TestFixture]
    public class TestMoveable
    {
        private Cell temp;

        [Test, Order(0)]
        public void CheckZeroSpeed()
        {
            temp = new Cell(new Point(3, 5), new Size(0, 0), 0, new Image[] { }, 0);
            temp.MakeMove();
            Assert.True(Math.Abs(temp.Point.X - 3) < 0.001 && Math.Abs(temp.Point.Y - 5) < 0.001);
        }
        
        [Test, Order(1)]
        public void CheckZeroPoint()
        {
            temp = new Cell(new Point(0, 0), new Size(0, 0), 2, new Image[] { }, 0);
            temp.MakeMove();
            Assert.True(Math.Abs(temp.Point.X - 2) < 0.001 && Math.Abs(temp.Point.Y) < 0.001);
        }
        
        [Test, Order(2)]
        public void CheckOneOne()
        {
            temp = new Cell(new Point(1, 1),new Size(0, 0), 1, new Image[] { }, 0);
            temp.MakeMove();
            Assert.True(Math.Abs(temp.Point.X - 1) < 0.001 && Math.Abs(temp.Point.Y) < 0.001);
        }
        
        [Test, Order(3)]
        public void CheckNormal()
        {
            temp = new Cell(new Point(3, 5), new Size(0, 0), 2, new Image[] { }, 0);
            temp.MakeMove();
            Assert.True(Math.Abs(temp.Point.X - 1) < 0.001 && Math.Abs(temp.Point.Y) < 0.001);
        }
    }
}