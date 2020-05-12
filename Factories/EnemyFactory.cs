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
        public static Enemy CreateEnemy(EnemyTypes type, Point point) =>
            type switch
            {
                EnemyTypes.Tank => new Enemy(point, 1, false, true, 600,
                    new GetImage(GlobalConstants.TankEnemyImagePath).AliveImages),
                EnemyTypes.Speed => new Enemy(point, 3, false, true, 700,
                    new GetImage(GlobalConstants.SpeedEnemyImagePath).AliveImages),
                EnemyTypes.Simple => new Enemy(point, 2, false, true, 200,
                    new GetImage(GlobalConstants.SimpleEnemyImagePath).AliveImages),
                _ => throw new ArgumentException()
            };
    }
}