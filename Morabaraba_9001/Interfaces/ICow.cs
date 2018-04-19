using System;
using System.Collections.Generic;
using System.Text;

namespace Morabaraba_9001.Interfaces
{
    public interface ICow
    {
        //int Position { get; }
        IPlayer PlayerID { get; }
        char Symbol { get; }
        //void Move(int Destination);
        //void changeID(int ID);
    }
}
