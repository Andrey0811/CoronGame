using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using CoronGame.Models.Interfaces;

namespace CoronGame.Models.Common
{
    public class Award : IGameObject
    {
        private int isValidCount = 50;
        private Color color;
        private TextBlock textBlock;

        public Award(Point point, int value, Color color)
        {
            Value = value;
            this.color = color;
            Point = point;
            Size = new Size(30, 20);
        }

        public int Value { get; set; }

        public bool IsValid
        {
            get
            {
                isValidCount--;
                if (isValidCount > 0)
                    return true;

                isValidCount = 50;
                return false;
            }
        }

        public TextBlock Format()
        {
            textBlock = new TextBlock
            {
                Text = Value.ToString(),
                Foreground = new SolidColorBrush(color),
                FontWeight = FontWeights.Bold,
                FontSize = 17
            };
            return textBlock;
        }

        public Size Size { get; }
        public Point Point { get; set; }
    }
}