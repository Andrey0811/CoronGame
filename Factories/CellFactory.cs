using System;
using System.Windows;
using CoronGame.Common;
using CoronGame.Models;
using CoronGame.Models.Enums;

namespace CoronGame.Factories
{
    public class CellFactory
    {
        public Cell CreateCell(CellTypes type, Point point)
        {
            return type switch
            {
                CellTypes.Rich => new Cell(point, new Size(26, 26), 2, 
                    ImageParser.Parse(GlobalConstants.RichCellImage),
                    400),
                CellTypes.Simple => new Cell(point, new Size(26, 26), 1,
                    ImageParser.Parse(GlobalConstants.SimpleCellImage), 100),
                _ => throw new ArgumentException()
            };
        }
    }
}
