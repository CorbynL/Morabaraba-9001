using System;
using System.Collections.Generic;
using System.Text;

namespace Morabaraba_9001.Classes
{
    public class Cow : ICow
    {
        public int Position { get; private set; }

        public int PlayerID { get; private set; }

        public char Symbol { get; private set; }

        public Cow(int Position = -1, int PlayerID = -1)
        {
            this.Position = Position;
            this.PlayerID = PlayerID;
            getSymbol();
            
        }
        public void Move(int Destination)
        {
            Position = Destination;
        }

        private void getSymbol()
        {
            switch (PlayerID)
            {
                case 0: Symbol = 'R';
                    break;
                case 1: Symbol = 'B';
                    break;
                case -1: Symbol = ' ';
                    break;
                default: throw new ArgumentException("Given ID is invalid");

            }
        }
    }
}
