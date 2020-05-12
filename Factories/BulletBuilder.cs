using System.Windows;
using System.Windows.Controls;
using CoronGame.Common;
using CoronGame.Models;
using CoronGame.Models.Common;
using CoronGame.Models.Enums;

namespace CoronGame.Factories
{
    public class BulletBuilder
    {
        private readonly Image image = ImageParser.Parse(GlobalConstants.BulletImage);
        
        public Bullet CreatBullet(Point point, MoveDirection direction) => 
            new Bullet(point, new Size(15, 15), 1, direction, image);
    }
}