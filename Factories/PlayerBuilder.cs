using System.Windows;
using CoronGame.Common;
using CoronGame.Models;

namespace CoronGame.Factories
{
    public class PlayerBuilder
    {
        private readonly int speed;

        public PlayerBuilder(int speed)
        {
            this.speed = speed;
        }

        public Player CreatePlayer(Point point)
        {
            var temp = new GetImage(GlobalConstants.PlayerAliveImagesPath);
            return new Player(point, 2, speed, temp.AliveImages);
        }
    }
}