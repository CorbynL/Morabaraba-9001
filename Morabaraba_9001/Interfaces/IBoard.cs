using System;
using System.Collections.Generic;
using System.Text;
using Morabaraba_9001.Classes;

namespace Morabaraba_9001.Interfaces
{
    public interface IBoard
    {
        ICow[] Cows { get; }
        IMill[] Mills { get;}
        ICow Occupant(int Destination);
        void Place(ICow Cow, int Destination);
        void Move(int firstDestination , int secondDestination);
        void Kill(int Destination);
        bool CowInAMill(Color ID, int Destination);
        bool isPlayerCow(Color c, int pos);
        bool areNewMills(Color playerID);
        int numCowsOnBoard();
        bool IsSurrounded(int Position);
        int[] ConnectedSpaces(int Position);
        void drawBoard(string State, string PlayerName);
        bool canAnyCowMove(Color c);
        int numPlayerCowsOnBoard(Color player);
    }
}
