using System;
using System.Collections.Generic;
using System.Text;
using Morabaraba_9001.Interfaces;

namespace Morabaraba_9001.Classes
{
    public class Player : IPlayer
    {
        public Color Color { get; private set; }
        private ICowBox Box;

        public Player(Color color, ICowBox box)
        {
            Color = color;
            Box = box;
        }

        public void Place(int position, IBoard board)
        {
            ICow cow = Box.TakeCow(Color);
            board.Place(cow, position);
        }

        public void Kill(int position, IBoard board)
        {
            board.Kill(position);
        }

        public void Move(int firstPosition, int secondPosition, IBoard board)
        {
            throw new NotImplementedException();
        }
    }
}
