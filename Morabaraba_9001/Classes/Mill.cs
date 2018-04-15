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
        public int Id  {get; private set; }

        public Mill(int[] Positions, int Id = -1)
        {
            this.Positions = Positions;
            isNew = false;
            this.Id = Id;
        }

        public void updateMill(int newID)
        {
            Id = newID;
            isNew = true;
        }

        public void noLongerNew()
        {
            isNew = false;
        }
    }
}
