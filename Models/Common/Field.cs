using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using CoronGame.Models.Interfaces;
using CoronGame.Models.Abstract;

namespace CoronGame.Models.Common
{
    public class Field : IGameObject, IFigurable
    {
        public Field(Point point, Size size)
        {
            Point = point;
            Size = size;
            Figure = new Rectangle
            {
                Width = Size.Width, 
                Height = Size.Height
            };
            var brush = new SolidColorBrush {Color = Colors.White};
            Figure.Fill = brush;
        }

        public Shape Figure { get; set; }
        public Size Size { get; }
        public Point Point { get; set; }
    }
}