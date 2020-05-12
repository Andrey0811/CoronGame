using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using CoronGame.Models.Interfaces;

namespace CoronGame.Models.Common
{
    public class Score : IGameObject
    {
        private const string ScoreText = "SCORE: ";

        public Score(Point point, Size size)
        {
            Point = point;
            Size = size;
            Text = ScoreText;
            Value = 0;
            
        }

        public int Value { get; set; }

        private string Text { get; set; }

        public TextBlock Format()
        {
            var textBlock = new TextBlock
            {
                Text = Text + Value,
                Foreground = new SolidColorBrush(Colors.White),
                FontSize = 20,
                FontWeight = FontWeights.Bold
            };
            return textBlock;
        }

        public Size Size { get; }
        public Point Point { get; set; }
    }
}