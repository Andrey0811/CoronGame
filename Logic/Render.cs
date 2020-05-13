using System;
using System.Windows;
using System.Windows.Controls;
using CoronGame.Models.Interfaces;

namespace CoronGame.Logic
{
    public class Render : IGameRenderer
    {
        private readonly Canvas canvas;

        public Render(Canvas canvas)
        {
            this.canvas = canvas;
            ParentWindow.KeyDown += HandleKeyDown;
        }

        public event EventHandler<KeyEventArgs> UiActionHappened;

        private Window ParentWindow
        {
            get
            {
                var parent = canvas.Parent;
                while (!(parent is Window)) 
                    parent = LogicalTreeHelper.GetParent(parent);

                return parent as Window;
            }
        }

        public void Clear()
        {
            canvas.Children.Clear();
        }
        
        public void Draw(UIElement element, Point point, Size size)
        {
            Canvas.SetLeft(element, point.X - size.Width / 2);
            Canvas.SetTop(element, point.Y - size.Height / 2);
            canvas.Children.Add(element);
        }

        private void HandleKeyDown(object sender, System.Windows.Input.KeyEventArgs args)
        {
            var key = args.Key;
            UiActionHappened?.Invoke(this, new KeyEventArgs(key));
        }
    }
}