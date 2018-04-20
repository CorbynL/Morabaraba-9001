using System;
using System.Collections.Generic;
using System.Text;

namespace Morabaraba_9001.Interfaces
{
    public interface IMill
    {
        int[] Positions { get; }
        bool isNew { get; }
        Color color { get; }
        void updateMill(Color c, bool isNew);        
    }
}
