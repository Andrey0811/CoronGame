using CoronGame.Models.Common;
using CoronGame.Models.Enums;

namespace CoronGame.Models.Interfaces
{
    public interface IMoveable : IGameObject
    {
        MoveDirection MoveDirection { get; set; }
        
        void MakeMove();
    }
}