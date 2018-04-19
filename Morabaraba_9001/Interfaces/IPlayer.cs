using System;
using System.Collections.Generic;
using System.Text;

namespace Morabaraba_9001.Interfaces
{
    public enum Color { Red, Blue };

    public interface IPlayer
    {
        Color Color { get; }
        bool Place(int position, IBoard board);
        bool Kill(int position, IBoard board);
        bool Move(int firstPosition, int secondPosition, IBoard board);
    }
}
