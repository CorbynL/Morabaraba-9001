using System;
using System.Collections.Generic;
using System.Text;

namespace Morabaraba_9001
{
    public interface ICow
    {
        int Position { get; }
        int PlayerID { get; }
        char Symbol { get; }
        void Move(int Destination);
    }
}
