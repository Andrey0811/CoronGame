using System.Windows;
using System.Windows.Controls;
using CoronGame.Common;
using CoronGame.Models;
using CoronGame.Models.Common;
using CoronGame.Models.Enums;

namespace CoronGame.Factories
{
    public class BlindBuilder
    {
        public static Blind CreatBullet(Point point, MoveDirection direction)
        {
            var temp = new GetImage(GlobalConstants.BlindImagePath);
            return new Blind(point, new Size(18, 18), 1, 
                direction, temp.AliveImages);
        }
    }
}