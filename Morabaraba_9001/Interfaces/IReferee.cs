using System;
using System.Collections.Generic;
using System.Text;

namespace Morabaraba_9001.Interfaces
{
    public interface IReferee
    {

        bool CanKill(Color color, int Destination);
        bool CanPlace(Color color, int Destination);
        bool CanMove(Color color, int FirstDestination, int SecondDestination);
        bool AreFlying(Color color);
    }
}
