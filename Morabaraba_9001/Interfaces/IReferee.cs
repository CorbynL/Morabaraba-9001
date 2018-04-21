using System;
using System.Collections.Generic;
using System.Text;
using Morabaraba_9001.Classes;

namespace Morabaraba_9001.Interfaces
{
    public interface IReferee
    {

        bool CanKill(Color color, int Destination);
        bool CanPlace(Color color, int Destination, Phase currPhase);
        bool CanMove(Color color, int FirstDestination, int SecondDestination, Phase currPhase);
        bool AreFlying(Color color);
        bool CanSelect(Color color, IBoard board, int Position);
    }
}
