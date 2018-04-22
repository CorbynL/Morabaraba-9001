using System;
using System.Collections.Generic;
using System.Text;
using Morabaraba_9001.Classes;

namespace Morabaraba_9001.Interfaces
{
    public enum Color { Red, Blue, Black };

    public interface IPlayer
    {
        Color Color { get; }
        bool Place(int position, IBoard board, IReferee referee, Phase currPhase);
        bool Kill(int position, IBoard board, IReferee referee);
        bool Move(int Destination, int secondPosition, IBoard board, IReferee referee, Phase currPhase);
        bool Select(int position, IBoard board, IReferee referee);
        bool IsLooser(IBoard board, Color color);
        bool MoveFlying(int Destination, int secondPosition, IBoard board, IReferee referee, Phase currPhase);
    }
}
