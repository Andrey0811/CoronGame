using System.Windows;

namespace CoronGame.Models.Interfaces
{
    public interface IGameObject
    {
        Size Size { get; }

        Point Point { get; set; }
    }
}