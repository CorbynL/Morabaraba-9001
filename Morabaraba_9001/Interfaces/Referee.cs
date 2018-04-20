using System;
using System.Collections.Generic;
using System.Text;
using Morabaraba_9001.Interfaces;

namespace Morabaraba_9001.Interfaces
{
    public class Referee : IReferee
    {

        public Referee(IBoard board, ICowBox box)
        {

        }

        public bool AreFlying(Color color)
        {
            throw new NotImplementedException();
        }

        public bool CanKill(Color color, int Destination)
        {
            throw new NotImplementedException();
        }

        public bool CanMove(Color color, int FirstDestination, int SecondDestination)
        {
            throw new NotImplementedException();
        }

        public bool CanPlace(Color color, int Destination)
        {
            throw new NotImplementedException();
        }
    }
}
