using System;
using System.Collections.Generic;
using System.Text;

namespace Morabaraba_9001
{
    public interface IGameSession
    {
        IBoard board { get; }
        IPlayer Winner();
        bool IsDraw();
        int CastInput();
        void Play();
    }
}
