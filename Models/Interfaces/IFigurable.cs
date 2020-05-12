using System.Windows.Shapes;

namespace CoronGame.Models.Interfaces
{
    public interface IFigurable : IGameObject
    {
        Shape Figure { get; set; }
    }
}