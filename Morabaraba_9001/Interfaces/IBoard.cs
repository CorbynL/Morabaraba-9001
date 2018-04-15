using System;
using System.Collections.Generic;
using System.Text;
using Morabaraba_9001.Classes;

namespace Morabaraba_9001
{
    public interface IBoard
    {
        Cow[] Cows { get; set; }

        Mill[] Mills { get;}

        void Place(int ID, int Destination);

        void Move(int ID, int firstDestination , int secondDestination);

        void UpdateMills();

        void KillCow(int Destination);

        void CanKillAt(int ID, int Destination);

        void DrawBoard();

        bool CanPlaceAt(int Position);

        int getMove();

        bool isPlayerCow(int currentPlayer, int input);

        bool isCowAt(int input);

        int numCowRemaining(int currentPlayer);

        IEnumerable<Cow> getCowsByPlayer(int ID);

        bool areNewMills(int currentPlayer);

        bool IsValidMove(int pos, int newPos);

        void initialiseCows();
    }
}
