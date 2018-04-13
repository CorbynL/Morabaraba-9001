using System;
using System.Collections.Generic;
using System.Text;
using Morabaraba_9001.Classes;

namespace Morabaraba_9001
{
    public interface IBoard
    {
        IEnumerable<Cow> Cows { get; }

        void Place(Cow cow, int Destination);

        void Move(Cow cow, int Destination);

        void KillCow(Cow cow);

        void Remove(int Destination);

        void DrawBoard();

        int getMove();
    }
}
