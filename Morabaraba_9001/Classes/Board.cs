using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Morabaraba_9001.Classes
{
    public class Board : IBoard
    {
        //public IEnumerable<Cow> Cows { get; private set; }        // Remove this if everyone is happy with it
        public Cow[] Cows { get; set; }                                          // Note the change to an array to simplify placing cows
        public Mill[] Mills { get; private set; }

        private IEnumerable<string> BoardPopulation(IEnumerable<Cow> cows)
        {
            return cows.Select(x => (x.Symbol.ToString())).ToArray();
        }

        #region Initialise Board

        public Board()
        {
            initialiseCows();
            initialiseMills();
        }

        // Create an array of default cows
        public void initialiseCows()
        {
            Cows = new Cow[24];
            Cows = Cows.Select((x, index) => new Cow(index, -1)).ToArray();
        }

        public IEnumerable<Cow> getCowsByPlayer(int ID)
        {
            return Cows.Where(x => (int)x.PlayerID == ID);
        }

        // Create empty array of empty mills
        public void initialiseMills()
        {
            Mills = new Mill[] {
                new Mill(new int[] { 0, 1, 2 }),        // A1, A4, A7
                new Mill(new int[] { 3, 4, 5 } ),       // B2, B4, B6
                new Mill(new int[] { 6, 7, 8 }),        // C3, C4, C5
                new Mill(new int[] { 9, 10, 11 }),      // D1, D2, D3
                new Mill(new int[] { 12, 13, 14 }),     // D5, D6, D7
                new Mill(new int[] { 15, 16, 17 }),     // E3, E4, E5
                new Mill(new int[] { 18, 19, 20 }),     // F2, F4, F6
                new Mill(new int[] { 21, 22, 23 }),     // G1, G4, G7
                new Mill(new int[] { 0, 9, 21 }),       // A1, D1, G1
                new Mill(new int[] { 3, 10, 18 }),      // B2, D2, F2
                new Mill(new int[] { 6, 11, 15 }),      // C3, D3, E3
                new Mill(new int[] { 1, 4, 7 }),        // A4, B4, C4
                new Mill(new int[] { 16, 19, 22 }),     // E4, F4, G4
                new Mill(new int[] { 8, 12, 17 }),      // C5, D5, E5
                new Mill(new int[] { 5, 13, 20 }),      // B6, D6, F6
                new Mill(new int[] { 2, 14, 23 }),      // A7, D7, G7
                new Mill(new int[] { 0, 3, 6 }),        // A1, B2, C3
                new Mill(new int[] { 15, 18, 21 }),     // E3, F2, G1
                new Mill(new int[] { 2, 5, 8 }),        // C5, B6, A7
                new Mill(new int[] { 17, 20, 23 })      // E5, F6, G7
            };
        }

        #endregion

        #region Output to Console
        public void DrawBoard()
        {
            Console.WriteLine(String.Format(getBoardString(), (object[])BoardPopulation(Cows)));
        }

        private string getBoardString()
        {
            StringBuilder builder = new StringBuilder("\n\n\n\n\t     1     2     3     4     5     6     7\n\n");
            builder.Append("\t A   ({0}) -------------({1})---------------({2})\n\n");
            builder.Append("\t     |   \\             |              /  |    \n\n");
            builder.Append("\t B   |    ({3})---------({4})---------({5})    |\n\n");
            builder.Append("\t     |     |  \\        |       /   |     |    \n\n");
            builder.Append("\t C   |     |    ({6})---({7})---({8})    |     |    \n\n");
            builder.Append("\t     |     |     |           |     |     |    \n\n");
            builder.Append("\t D  ({9})---({10})---({11})         ({12})---({13})---({14})\n\n");
            builder.Append("\t     |     |     |           |     |     |    \n\n");
            builder.Append("\t E   |     |    ({15})---({16})---({17})    |     |    \n\n");
            builder.Append("\t     |     | /         |         \\ |     |\n\n");
            builder.Append("\t F   |    ({18})---------({19})---------({20})    |\n\n");
            builder.Append("\t     | /               |              \\  |    \n\n");
            builder.Append("\t G   ({21}) -------------({22})---------------({23})    ");
            return builder.ToString();
        }

        #endregion

        #region Move functions

        // Is there an algorithm to get all the moves from a certain position?
        public int[][] MoveSets = new int[][]
        {
         new int[] {1,3,9},
         new int[] {0,2,4},
         new int[] {1,5,14},
         new int[] {0,4,6},
         new int[] {1,3,5,7},
         new int[] {2,4,8,13},
         new int[] {4,7,11},
         new int[] {4,6,8},
         new int[] {5,7,12},
         new int[] {0,10,21},
         new int[] {3,9,11,18},
         new int[] {6,10,15},
         new int[] {8,13,17},
         new int[] {5,12,14,20},
         new int[] {2,13,23},
         new int[] {11,16,18},
         new int[] {15,17,19},
         new int[] {12,16,20},
         new int[] {10,15,19,21},
         new int[] {16,18,20,22},
         new int[] {13,17,19,23},
         new int[] {9,18,22},
         new int[] {19,21,23},
         new int[] {14,20,22},
        };

        public void Move(int ID, int firstDestination, int secondDestination)
        {
            if(Cows[firstDestination].PlayerID != ID)
                throw new Exception("This is an invalid move. The cow is not owned by this player");
            Cows[secondDestination] = Cows[firstDestination];
            Cows[firstDestination] = new Cow();
        }

        public bool IsValidMove(int pos, int newPos)
        {
            return MoveSets[pos].Contains(newPos);
        }

        // Returns true if the cow is owned by the player
        public bool isPlayerCow(int playerId, int pos)
        {
            return Cows[pos].PlayerID == playerId;
        }



        #endregion

        #region Mill Functions

        public void UpdateMills()
        {
            foreach(Mill current in Mills)
            {
                int playerID = Cows[current.Positions[0]].PlayerID;
                if (playerID == Cows[current.Positions[1]].PlayerID && playerID == Cows[current.Positions[2]].PlayerID)
                {
                    if (current.isNew)
                        current.noLongerNew();
                    else if (current.Id != playerID && playerID != -1 && !current.isNew)
                    {
                        // New mill
                        current.updateMill(playerID);
                    }
                }
                    
            }
        }

        public bool areNewMills(int playerID)
        {
            foreach (Mill mill in Mills)
            {
                if (mill.Id == playerID && mill.isNew) { return true; }
            }
            return false;
        }

        // Removes a cow at a given destination
        public void KillCow(int Destination)
        {
            Cows[Destination] = new Cow();
        }

        public bool CanKillAt(int ID, int Destination)
        {
            //Long Check: First see if the cow you want to kill is not empty or your own, then check if it's in a mill but if all cows are in a mill then you can kill it.
            return (Cows[Destination].PlayerID != -1 && Cows[Destination].PlayerID != ID) && (!CowInAMill((ID + 1) % 2, Destination) || AreAllCowsInMills((ID + 1) % 2));
        }

        private bool CowInAMill (int ID, int Destination)
        {
            Mill[] PlayerOwnedMills = Mills.Where(x => x.Id == ID).ToArray();
            foreach (Mill mill in PlayerOwnedMills)
                if (mill.Positions[0] == Destination
                    || mill.Positions[1] == Destination
                    || mill.Positions[2] == Destination)
                    return true;
            return false;
        }

        private bool AreAllCowsInMills (int ID)
        {
            Mill[] PlayerOwnedMills = Mills.Where(x => x.Id == ID).ToArray();
            Cow[] PlayerOwnedCows = Cows.Where(x => x.PlayerID == ID).ToArray();

            for (int i = 0; i < PlayerOwnedCows.Length; i++)
                if (!CowInAMill(ID, PlayerOwnedCows[i].Position))
                    return false;
            return true;
        }

        #endregion

        #region Cow Funcitons

        // Returns the number of cows remaining on the board for the given player
        public int numCowRemaining(int playerID)
        {
            int count = 0;
            foreach(Cow current in Cows)
            {
                if (current.PlayerID == playerID)
                    count++;
            }
            return count;
        }

        public bool isCowAt(int pos)
        {
            return Cows.ElementAt(pos).Position != -1;
        }


        public void Place(int ID, int Destination)
        {
            Cows[Destination].changeID(ID);

            // Remove this if everyone is happy with the update
            //Cows = Cows.Select(x => (x.Position != Destination) ? x : new Cow(Destination, ID)); // I feel like this could be done better because it still runs through the whole list of cows 
        }

        public bool CanPlaceAt(int Position)
        {
            return Cows.ElementAt(Position).PlayerID == -1;
        }

        #endregion

        public int getMove()
        {
            throw new NotImplementedException();
        }
        
    }
}
