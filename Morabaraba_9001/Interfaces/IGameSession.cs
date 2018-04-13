using System;
using System.Collections.Generic;
using System.Text;

namespace Morabaraba_9001
{
    public interface IGameSession
    {
        IBoard board { get; }
        void Winner();
        bool IsDraw();
        int CastInput();
        void Play();
    }
}
