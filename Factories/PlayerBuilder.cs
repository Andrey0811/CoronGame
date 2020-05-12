using System.Windows;
using System.Windows.Controls;
using CoronGame.Common;
using CoronGame.Models;

namespace CoronGame.Factories
{
    public class PlayerBuilder
    {
        private readonly string basicImageName = "basic.png";

        private readonly string[] movementImagesNames =
        {
            "up.png", "down.png", "left.png", "right.png"
        };

        private Image[][] aliveImages;
        //private Image[] dyingImages;

        private int speed;

        public PlayerBuilder(int speed)
        {
            CreatAlivePlayerImagesArray();
            //CreatDyingPlayerImagesArray();
            this.speed = speed;
        }

        public Player CreatePlayer(Point point) => 
            new Player(point, 2, speed, aliveImages/*, dyingImages*/);

        private void CreatAlivePlayerImagesArray()
        {
            aliveImages = new Image[4][];
            for (var i = 0; i < 4; i++)
            {
                aliveImages[i] = new Image[3];
                aliveImages[i][0] = ImageParser.Parse(GlobalConstants.PlayerAliveImagesPath + basicImageName);
            }

            for (var i = 0; i < movementImagesNames.Length; i++)
            {
                aliveImages[i / 2][i % 2 + 1] =
                    ImageParser.Parse(GlobalConstants.PlayerAliveImagesPath + movementImagesNames[i]);
            }
        }

        /*private void CreatDyingPlayerImagesArray()
        {
            dyingImages = new Image[10];
            for (var i = 0; i < 10; i++)
            {
                dyingImages[i] = new Image();
                dyingImages[i] =
                    ImageParser.Parse(GlobalConstants.PlayerDyingImagePath + i + ImagesFileExtension);
            }
        }*/
    }
}