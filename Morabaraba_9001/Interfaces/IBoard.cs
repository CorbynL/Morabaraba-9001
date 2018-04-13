using System;
using System.Collections.Generic;
using System.Text;
using Morabaraba_9001.Classes;

namespace Morabaraba_9001
{
    public interface IBoard
    {
        IEnumerable<Cow> Cows { get; }

        void Move(Cow cow, int Destination);

        void KillCow(Cow cow);

        void DrawBoard();

        int getMove();
    }
}
