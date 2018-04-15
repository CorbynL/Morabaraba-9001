using System;
using System.Collections.Generic;
using System.Text;

namespace Morabaraba_9001
{
    public interface IGameSession
    {
        IBoard board { get; }
        int CastInput();
        bool IsDraw();
        void Play(int input);
        void Winner();       
    }
}
