using System;
using System.Collections.Generic;
using System.Text;

namespace Morabaraba_9001.Interfaces
{
    public enum Color { Red, Blue };

    public interface IPlayer
    {
        Color Color { get; }
        void Place(int position, IBoard board);
        void Kill(int position, IBoard board);
        void Move(int firstPosition, int secondPosition, IBoard board);
    }
}
