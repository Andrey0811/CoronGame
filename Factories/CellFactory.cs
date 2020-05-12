using System;
using System.Windows;
using CoronGame.Common;
using CoronGame.Models;
using CoronGame.Models.Enums;

namespace CoronGame.Factories
{
    public class CellFactory
    {
        public static Cell CreateCell(CellTypes type, Point point) =>
            type switch
            {
                CellTypes.Rich => new Cell(point, new Size(26, 26), 2, 
                    new GetImage(GlobalConstants.RichCellImagePath).AliveImages, 400),
                CellTypes.Simple => new Cell(point, new Size(26, 26), 1,
                    new GetImage(GlobalConstants.SimpleCellImagePath).AliveImages, 100),
                _ => throw new ArgumentException()
            };
    }
}
