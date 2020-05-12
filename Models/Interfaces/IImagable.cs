using System.Windows.Controls;

namespace CoronGame.Models.Interfaces
{
    public interface IImagable : IGameObject
    {
        Image GetImage();
    }
}
