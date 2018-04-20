using Morabaraba_9001.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Morabaraba_9001.Classes
{
    public class Mill : IMill
    {
        public int[] Positions {get; private set; }
        public bool isNew  {get; private set; }
        public Color color { get; private set; }

        public Mill(int[] Positions, Color c)
        {
            this.Positions = Positions;
            isNew = false;
            color = c;
        }

        public void updateMill(Color c, bool isNew)
        {
            color = c;
            this.isNew = isNew;
        }

        public void noLongerNew()
        {
            isNew = false;
        }

        public void updateMill(Color c, bool isNew)
        {
            throw new NotImplementedException();
        }
    }
}
