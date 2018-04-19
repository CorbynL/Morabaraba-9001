using System;
using System.Collections.Generic;
using System.Text;
using Morabaraba_9001.Classes;

namespace Morabaraba_9001.Interfaces
{
    public interface IBoard
    {
        ICow[] Cows { get; }
        IMill[] Mills { get;}
        ICow Occupant(int Destination);
        void Place(ICow Cow, int Destination);
        void Move(int firstDestination , int secondDestination);
        void Kill(int Destination);
        void initialiseCows();
        void initialiseMills();
    }
}
