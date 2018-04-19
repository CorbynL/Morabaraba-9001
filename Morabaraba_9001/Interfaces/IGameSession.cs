using System;
using System.Collections.Generic;
using System.Text;

namespace Morabaraba_9001.Interfaces
{
    public interface IGameSession
    {
        IBoard board { get; }
        IPlayer Current_Player { get; }
        IPlayer Player_1 { get; }
        IPlayer Player_2 { get; }
        ICowBox box { get; }
        IReferee referee { get; }
        void Play(int input);
        void Winner();
    }
}
