using System;
using System.Collections.Generic;
using System.Text;

namespace Morabaraba_9001.Classes
{
    public class Board : IBoard
    {
        public IEnumerable<Cow> Cows => throw new NotImplementedException();

        public void DrawBoard()
        {
            throw new NotImplementedException();
        }

        public void KillCow(Cow cow)
        {
            throw new NotImplementedException();
        }

        public void Move(Cow cow, int Destination)
        {
            throw new NotImplementedException();
        }
    }
}
