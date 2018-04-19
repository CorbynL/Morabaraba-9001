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
    }
}
