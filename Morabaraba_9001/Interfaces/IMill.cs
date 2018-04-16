using System;
using System.Collections.Generic;
using System.Text;

namespace Morabaraba_9001.Interfaces
{
    public interface IMill
    {
        int[] Positions { get; }
        bool isNew { get; }
        int Id { get; }

        void updateMill(int newID,bool isNew);
    }
}
