using System;
using System.Collections.Generic;
using System.Text;

namespace Morabaraba_9001.Interfaces
{
    public enum Color { Red, Blue, Black };

    public interface IPlayer
    {
        Color Color { get; }
        bool Place(int position, IBoard board,IReferee referee);
        bool Kill(int position, IBoard board, IReferee referee);
        bool Move(int Destination, int secondPosition, IBoard board, IReferee referee);
        bool Select(int position, IBoard board, IReferee referee);
    }
}
