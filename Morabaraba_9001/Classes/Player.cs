using System;
using System.Collections.Generic;
using System.Text;

namespace Morabaraba_9001.Classes
{
    public class Player : IPlayer
    {
        public int ID { get; }

        public Player (int ID)
        {
            this.ID = ID;
        }       
      
    }
}
