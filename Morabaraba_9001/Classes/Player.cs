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

        public bool Place(int position, IBoard board, IReferee referee)
        {
            if (referee.CanPlace(Color, position)){
                ICow cow = Box.TakeCow(Color);
                board.Place(cow, position);
                return true;
            }
            else return false;
        }

        public bool Kill(int position, IBoard board, IReferee referee)
        {
            if (referee.CanKill(Color, position)){
                board.Kill(position);
                return true;
            }
            else return false;
        }

        public void Move(int firstPosition, int secondPosition, IBoard board)
        {
            throw new NotImplementedException();
        }
    }
}
