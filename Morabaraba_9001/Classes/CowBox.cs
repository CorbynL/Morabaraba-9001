using Morabaraba_9001.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Morabaraba_9001.Classes
{
    public class CowBox : ICowBox
    {
        private ICow[] Cows;

        public CowBox()
        {
            Cows = new ICow[24];

            for (int i = 0; i < 24; i++)
            {
                if (i < 12) { Cows[i] = new Cow(-1, Color.Red); }
                else { Cows[i] = new Cow(-1, Color.Blue); }
            }
        }

        public int RemainingCows(Color c)
        {
            return Cows.Select(x => x.Color == c).ToArray().Length;
        }

        public ICow TakeCow(Color c)
        {
            ICow cow = Cows.First(x => x.Color == c);
            List<ICow> list =  Cows.ToList();
            list.Remove(cow);
            Cows = list.ToArray();
            return cow;
        }

        public bool IsEmpty()
        {
            return Cows.Length == 0;
        }
    }
}