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
        public static Bullet CreatBullet(Point point, MoveDirection direction)
        {
            var temp = new GetImage(GlobalConstants.BulletImagePath);
            return new Bullet(point, new Size(18, 18), 1,
                direction, temp.AliveImages);
        }
    }
}