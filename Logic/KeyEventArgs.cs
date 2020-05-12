using System;
using System.Windows.Input;
using CoronGame.Models.Common;
using CoronGame.Models.Enums;

namespace CoronGame.Logic
{
    public class KeyEventArgs : EventArgs
    {
        public KeyEventArgs(Key key)
        {
            switch (key)
            {
                case Key.RightCtrl:
                    Act = Act.Blind;
                    break;
                case Key.Up:
                    Act = Act.Move;
                    MoveDirection = MoveDirection.Up;
                    break;
                case Key.Down:
                    Act = Act.Move;
                    MoveDirection = MoveDirection.Down;
                    break;
                case Key.Left:
                    Act = Act.Move;
                    MoveDirection = MoveDirection.Left;
                    break;
                case Key.Right:
                    Act = Act.Move;
                    MoveDirection = MoveDirection.Right;
                    break;
            }
        }

        public Act Act { get; }
        public MoveDirection MoveDirection { get; }
    }
}
