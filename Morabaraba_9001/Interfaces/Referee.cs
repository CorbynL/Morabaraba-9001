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

        public bool CanSelect(Color color, IBoard board, int Position)
        {
            bool isYourCow = board.Occupant(Position).Color == color;
            bool cowNotSurrounded = !board.IsSurrounded(Position);

            return isYourCow && cowNotSurrounded;
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
            int[][] MoveSets = new int[][] {
                 new int[] { 1, 3, 9 },             //0
                 new int[] { 0, 2, 4 },             //1
                 new int[] { 1, 5, 14 },            //2
                 new int[] { 0, 4, 6, 10 },         //3
                 new int[] { 1, 3, 5, 7 },          //4
                 new int[] { 2, 4, 8, 13 },         //5
                 new int[] { 3, 7, 11 },            //6
                 new int[] { 4, 6, 8 },             //7
                 new int[] { 5, 7, 12 },            //8
                 new int[] { 0, 10, 21 },           //9
                 new int[] { 3, 9, 11, 18 },        //10
                 new int[] { 6, 10, 15 },           //11
                 new int[] { 8, 13, 17 },           //12
                 new int[] { 5, 12, 14, 20 },       //13
                 new int[] { 2, 13, 23 },           //14
                 new int[] { 11, 16, 18 },          //15
                 new int[] { 15, 17, 19 },          //16
                 new int[] { 12, 16, 20 },          //17
                 new int[] { 10, 15, 19, 21 },      //18
                 new int[] { 16, 18, 20, 22 },      //19
                 new int[] { 13, 17, 19, 23 },      //20
                 new int[] { 9, 18, 22 },           //21
                 new int[] { 19, 21, 23 },          //22
                 new int[] { 14, 20, 22 },          //23
            };

            bool isValidMove = Array.Exists(MoveSets[FirstDestination], element => element == SecondDestination);
            bool noCowInMove = Board.Occupant(SecondDestination).Color == Color.Black;

            return isValidMove && noCowInMove;
        }
       
        public bool CanPlace(Color color, int Destination)
        {
            bool hasCows = (Box.RemainingCows(color) > 0);
            bool spaceIsFree = Board.Occupant(Destination).Color == Color.Black;

            return hasCows && spaceIsFree;
        }
    }
}
