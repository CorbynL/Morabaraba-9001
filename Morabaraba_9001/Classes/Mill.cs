using Morabaraba_9001.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Morabaraba_9001.Classes
{
    public class Mill : IMill
    {
        public int[] Positions;
        public bool isNew { get; set; }
        public int Id { get; set; }
        
        public Mill(int[] Positions, int Id = -1)
        {
            this.Positions = Positions;
            isNew = false;
            this.Id = Id;
        }
    }
}
