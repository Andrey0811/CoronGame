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
        private readonly Image image = ImageParser.Parse(GlobalConstants.BlindImage);
        
        public Blind CreatBullet(Point point, MoveDirection direction) => 
            new Blind(point, new Size(18, 18),  1, direction, image);
    }
}