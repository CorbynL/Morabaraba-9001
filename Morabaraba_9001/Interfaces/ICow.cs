using System;
using System.Collections.Generic;
using System.Text;

namespace Morabaraba_9001.Interfaces
{
    public interface ICow
    {
        IPlayer PlayerID { get; }
        char Symbol { get; }        
    }
}
