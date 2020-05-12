using System;
using System.Windows;
using CoronGame.Logic;

namespace CoronGame.Models.Interfaces
{
    public interface IGameRenderer
    {
        event EventHandler<KeyEventArgs> UiActionHappened;
        
        void Draw(UIElement element, Point point, Size size);

        void Clear();
    }
}