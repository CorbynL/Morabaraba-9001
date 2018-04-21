using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Morabaraba_9001.Interfaces;

namespace Morabaraba_9001.Classes
{
    public class Board : IBoard
    {
        //public IEnumerable<Cow> Cows { get; private set; }        // Remove this if everyone is happy with it
        public ICow[] Cows { get; private set; }                                          // Note the change to an array to simplify placing cows
        public IMill[] Mills { get; private set; }

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
            Cows = new ICow[24];
            Cows = Cows.Select((x, index) => new Cow(index, Color.Black)).ToArray();
        }


        public IEnumerable<ICow> getCowsByPlayer(Color c)
        {
            return Cows.Where(x => x.Color == c);
        }

        #endregion

        #region Mill Functions

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


        public int[][] MoveSets = new int[][]
        {
         new int[] {1,3,9},             //0
         new int[] {0,2,4},             //1
         new int[] {1,5,14},            //2
         new int[] {0,4,6,10},          //3
         new int[] {1,3,5,7},           //4
         new int[] {2,4,8,13},          //5
         new int[] {3,7,11},            //6
         new int[] {4,6,8},             //7
         new int[] {5,7,12},            //8
         new int[] {0,10,21},           //9
         new int[] {3,9,11,18},         //10
         new int[] {6,10,15},           //11
         new int[] {8,13,17},           //12
         new int[] {5,12,14,20},        //13
         new int[] {2,13,23},           //14
         new int[] {11,16,18},          //15
         new int[] {15,17,19},          //16
         new int[] {12,16,20},          //17
         new int[] {10,15,19,21},       //18
         new int[] {16,18,20,22},       //19
         new int[] {13,17,19,23},       //20
         new int[] {9,18,22},           //21
         new int[] {19,21,23},          //22
         new int[] {14,20,22},          //23
        };

        public bool IsSurrounded(int Position)
        {
            foreach(int space in MoveSets[Position]){
                if (!isCowAt(space)) {
                    return false;
                }
            }
            return true;
        }


        public void Move(int firstDestination, int secondDestination)
        {        
            Cows[secondDestination] = new Cow(secondDestination, Cows[firstDestination].Color);
            Cows[firstDestination] = new Cow(firstDestination, Color.Black);
        }

        public ICow Occupant(int Destination)
        {
            return Cows[Destination];
        }

        // Returns true if the cow is owned by the player
        public bool isPlayerCow(Color c, int pos)
        {
            return Cows[pos].Color == c;
        }

        /*
        //Check if none flying cow is surrounded
        public bool canMoveFrom(Color c, int Destination)
        {            
            if (numCowRemaining(c) == 3)
            {
                makeCowsFly(c);
            }       

            int[] EmptyNeighbours = MoveSets[Destination].Where(x => Cows[x].Color == Color.Black).ToArray();
            if (EmptyNeighbours.Length == 0 && typeof(FlyingCow) != Cows[Destination].GetType())
                return false;

            return Cows[Destination].Color == c; 
                
        }
        
        public bool canMoveTo(int ID, int firstDestination, int secondDestination)
        {
            ICow c = Occupant(firstDestination);
            if (typeof(FlyingCow) == Cows[firstDestination].GetType())
                return CanPlaceAt(secondDestination);

            int[] EmptyNeighbours = MoveSets[firstDestination].Where(x => Cows[x].Color == Color.Black).ToArray();
            if (EmptyNeighbours.Length == 0)
                return false;

            return Cows[secondDestination].Color == Color.Black && MoveSets[firstDestination].Contains(secondDestination);
        }
        
        */

        #endregion
        #region Mill Functions
     
        private void UpdateMills()           // This should be private... once we figure out how to use it for testing
        {
            foreach (Mill current in Mills)
            {
                Color playerID = Cows[current.Positions[0]].Color;
                if (playerID == Cows[current.Positions[1]].Color && playerID == Cows[current.Positions[2]].Color)
                {
                    if (current.color != playerID)
                    {
                        current.updateMill(playerID, true);
                    }
                    else if (current.color == playerID)
                        current.updateMill(playerID, false);
                }
                else if (current.color != Color.Black)
                {
                    current.updateMill(Color.Black, false);
                }
            }
        }
        

        public bool areNewMills(Color playerID)
        {
            foreach (Mill mill in Mills)
            {
                if (mill.color == playerID && mill.isNew) { return true; }
            }
            return false;
        }


        public bool CowInAMill (Color ID, int Destination)
        {
            IMill[] PlayerOwnedMills = Mills.Where(x => x.color == ID).ToArray();
            foreach (Mill mill in PlayerOwnedMills)
                if (mill.Positions[0] == Destination
                    || mill.Positions[1] == Destination
                    || mill.Positions[2] == Destination)
                    return true;
            return false;
        }


        private bool AreAllCowsInMills (Color ID)
        {
            IMill[] PlayerOwnedMills = Mills.Where(x => x.color == ID).ToArray();
            ICow[] PlayerOwnedCows = Cows.Where(x => x.Color == ID).ToArray();

            for (int i = 0; i < PlayerOwnedCows.Length; i++)
                if (!CowInAMill(ID, PlayerOwnedCows[i].Position))
                    return false;
            return true;
        }

        #endregion

        #region Cow Funcitons

        public int[] ConnectedSpaces(int Position)
        {
            return MoveSets[Position];
        }

        public int numCowsOnBoard()
        {
            int num = 0;

            foreach(Cow c in Cows)
            {
                if(c.Color != Color.Black) { num++; }
            }
            return num;
        }

        // Returns the number of cows remaining on the board for the given player
        public int numCowRemaining(Color c)
        {
            int count = 0;
            foreach(Cow current in Cows)
            {
                if (current.Color == c)
                    count++;
            }
            return count;
        }


        public bool isCowAt(int pos)
        {
            return Cows.ElementAt(pos).Color != Color.Black;
        }
       

        public bool CanPlaceAt(int Position)
        {
            return Cows.ElementAt(Position).Color == Color.Black;
        }


        public bool CanKillAt(Color ID, int Destination)
        {
            if (Destination == -1)
                return false;
            //Long Check: First see if the cow you want to kill is not empty or your own, then check if it's in a mill but if all cows are in a mill then you can kill it.
            return (Cows[Destination].Color != Color.Black && Cows[Destination].Color != ID) && (!CowInAMill(getOppColor(ID), Destination) || AreAllCowsInMills(getOppColor(ID)));
        }

        #endregion

        #region GamePlay functions

        public void makeCowsFly(Color c)
        {
            Cows = Cows.Select(x =>
                {
                    if (x.Color == c) { return new FlyingCow(x.Position, Color.Black); }
                    else return x;
                }
                ).ToArray();
        }


        private Color getOppColor(Color c)
        {
            if (c == Color.Red) { return Color.Blue; }
            else return Color.Red;
        }

        
        public void Place(ICow Cow, int Destination)
        {
            Cows[Destination] = Cow;
            UpdateMills();
        }
        

        public void Kill(int Destination)
        {
            Cows[Destination] = new Cow();
        }


        #endregion






        #region Old Code

        /*

        // Removes a cow at a given destination
        public void KillCow(int Destination)
        {
            Cows[Destination] = new Cow(Cows[Destination].Position, Color.Black);
        }



                // Is there an algorithm to get all the moves from a certain position?
        public int[][] MoveSets = new int[][]
        {
         new int[] {1,3,9},             //0
         new int[] {0,2,4},             //1
         new int[] {1,5,14},            //2
         new int[] {0,4,6,10},          //3
         new int[] {1,3,5,7},           //4
         new int[] {2,4,8,13},          //5
         new int[] {3,7,11},            //6
         new int[] {4,6,8},             //7
         new int[] {5,7,12},            //8
         new int[] {0,10,21},           //9
         new int[] {3,9,11,18},         //10
         new int[] {6,10,15},           //11
         new int[] {8,13,17},           //12
         new int[] {5,12,14,20},        //13
         new int[] {2,13,23},           //14
         new int[] {11,16,18},          //15
         new int[] {15,17,19},          //16
         new int[] {12,16,20},          //17
         new int[] {10,15,19,21},       //18
         new int[] {16,18,20,22},       //19
         new int[] {13,17,19,23},       //20
         new int[] {9,18,22},           //21
         new int[] {19,21,23},          //22
         new int[] {14,20,22},          //23
        };
        


        
        //Check if none flying cow is surrounded
        public bool canMoveFrom(Color c, int Destination)
        {            
            if (numCowRemaining(c) == 3)
            {
                makeCowsFly(c);
            }       

            int[] EmptyNeighbours = MoveSets[Destination].Where(x => Cows[x].Color == Color.Black).ToArray();
            if (EmptyNeighbours.Length == 0 && typeof(FlyingCow) != Cows[Destination].GetType())
                return false;

            return Cows[Destination].Color == c; 
                
        }
        
        public bool canMoveTo(int ID, int firstDestination, int secondDestination)
        {
            ICow c = Occupant(firstDestination);
            if (typeof(FlyingCow) == Cows[firstDestination].GetType())
                return CanPlaceAt(secondDestination);

            int[] EmptyNeighbours = MoveSets[firstDestination].Where(x => Cows[x].Color == Color.Black).ToArray();
            if (EmptyNeighbours.Length == 0)
                return false;

            return Cows[secondDestination].Color == Color.Black && MoveSets[firstDestination].Contains(secondDestination);
        }




            */
        #endregion
    }
}
