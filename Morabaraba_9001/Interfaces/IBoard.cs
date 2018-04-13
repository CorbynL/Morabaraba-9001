using System;
using System.Collections.Generic;
using System.Text;
using Morabaraba_9001.Classes;

namespace Morabaraba_9001
{
    public interface IBoard
    {
        IEnumerable<Cow> Cows { get; }

        void Place(int Destination);

        void Move(int firstDestination , int secondDestination);

        void KillCow(int Destination);        

        void DrawBoard();

        int getMove();
    }
}
