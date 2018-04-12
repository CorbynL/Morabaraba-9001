using System;
using System.Collections.Generic;
using System.Text;

namespace Morabaraba_9001.Classes
{
    public class Cow : ICow
    {
        public int Position { get; private set; }

        public int PlayerID { get; private set; }

        public void Move(int Destination)
        {
            Position = Destination;
        }
    }
}
