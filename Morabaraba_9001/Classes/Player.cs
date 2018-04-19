using System;
using System.Collections.Generic;
using System.Text;
using Morabaraba_9001.Interfaces;

namespace Morabaraba_9001.Classes
{
    public class Player : IPlayer
    {
        public Color Color { get; private set; }

        public Player(Color color)
        {
            Color = color;
        }

        public bool Place(int position, IBoard board)
        {
            throw new NotImplementedException();
        }
    }
}
