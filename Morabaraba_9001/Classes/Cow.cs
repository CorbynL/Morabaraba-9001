using Morabaraba_9001.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Morabaraba_9001.Classes
{
    public class Cow : ICow
    {
        public int Position { get; private set; }


        public char Symbol { get; private set; }

        public Color Color { get; private set; }

        public Cow(int Position = -1, Color Color = Color.Black)
        {
            this.Position = Position;
            this.Color = Color;
            getSymbol();
            
        }
        public void Move(int Destination)
        {
            Position = Destination;
        }

        public void changeID()
        {
            this.Color = Color;
            getSymbol();
        }

        private void getSymbol()
        {
            switch (Color)
            {
                case Color.Red: Symbol = 'R';
                    break;
                case Color.Blue: Symbol = 'B';
                    break;
                case Color.Black: Symbol = ' ';
                    break;
                default: throw new ArgumentException("Given ID is invalid");

            }
        }
    }
}
