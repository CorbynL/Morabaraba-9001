using System;
using System.Collections.Generic;
using System.Text;

namespace Morabaraba_9001.Interfaces
{
    public interface ICowBox
    {
        ICow TakeCow(Color c);
        int RemainingCows(Color c);
    }
}
