using System;
using System.Windows;
using System.Windows.Controls;
using CoronGame.Common;
using CoronGame.Models;
using CoronGame.Models.Enums;

namespace CoronGame.Factories
{
    public class EnemyFactory
    {
        public Enemy CreateEnemy(EnemyTypes type, Point point)
        {
            EnemyGetImage temp;
            switch (type)
            {
                case EnemyTypes.Tank:
                    temp = new EnemyGetImage(GlobalConstants.TankEnemyImagePath);
                    return new Enemy(point, 1, false, true, 
                        600, temp.AliveImages);
                case EnemyTypes.Speed:
                    temp = new EnemyGetImage(GlobalConstants.SpeedEnemyImagePath);
                    return new Enemy(point, 3, false, true, 
                        700, temp.AliveImages);
                case EnemyTypes.Simple:
                    temp = new EnemyGetImage(GlobalConstants.SimpleEnemyImagePath);
                    return new Enemy(point, 2, false, true, 
                        200, temp.AliveImages);
                default:
                    throw new ArgumentException();
            }
        }
    }

    public class EnemyGetImage
    {
        private readonly string[] movementImagesNames = {"up.png", "down.png", "left.png", "rigth.png"};

        public EnemyGetImage(string aliveImagesPath)
        {
            AliveImages = CreatMovementImagesArray(aliveImagesPath);
        }

        public Image[] AliveImages { get; private set; }

        private Image[] CreatMovementImagesArray(string imagePath)
        {
            var images = new Image[4];
            for (var i = 0; i < 4; i++)
            {
                images[i] = new Image();
                images[i] = ImageParser.Parse(imagePath + this.movementImagesNames[i]);
            }

            return images;
        }
    }
}