using System;
using System.Collections.Generic;
using System.Text;
using Morabaraba_9001.Interfaces;

namespace Morabaraba_9001.Interfaces
{
    public class Referee : IReferee
    {

        private IBoard Board;
        private ICowBox Box;

        public Referee(IBoard board, ICowBox box)
        {
            Board = board;
            Box = box;
        }

        public bool AreFlying(Color color)
        {
            throw new NotImplementedException();
        }

        public bool CanKill(Color Color, int Destination)
        {
            bool notYourCow = !Board.isPlayerCow(Color, Destination);
            bool notInMill = !Board.CowInAMill(Color, Destination);
            bool cowAtPos = !Board.isPlayerCow(Color.Black, Destination);        // Checks that there is actually a cow at the possition

            return notInMill && notInMill && cowAtPos;
        }

        public bool CanMove(Color color, int FirstDestination, int SecondDestination)
        {
            throw new NotImplementedException();
        }

        public bool CanPlace(Color color, int Destination)
        {
            bool hasCows = (Box.RemainingCows(color) > 0);
            bool spaceIsFree = Board.Occupant(Destination).Color == Color.Black;

            return hasCows && spaceIsFree;
        }
    }
}
