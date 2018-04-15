using System;
using System.Linq;

namespace Morabaraba_9001.Classes
{
    public class GameSession : IGameSession
    {
        public IBoard board { get; private set; }

        private enum Player { Red = 0, Blue = 1 };
        private enum Phase { Placing, Moving, Winner };

        private Player CurrentPlayer; //Switch between players each turn
        private Phase CurrentPhase;

        public GameSession()
        {
            // Set the stage
            setConsoleProperties();
            
            //Start game
            board = new Board();
            CurrentPlayer = Player.Red;
            CurrentPhase = Phase.Placing;
            Play();
        }
        

        //For General input of anything
        public int CastInput() //Like a spell, 'cause you're a wizzzard, Harry...
        {
            int input = -1;
            Console.WriteLine("\nPlease enter a coordinate:");

            while (true)
            {
                input = ConvertUserInput(Console.ReadLine());
                if (input == -1)
                {
                    board.DrawBoard();
                    Console.WriteLine("\nInvalid coordinate input. Please enter a VALID coordinate:");
                    continue;
                }
                else break;
            }
            return input;            
        }

        //Is there an algorithm to get all the moves from a certain position?
        private int[][] MoveSets = new int[][]
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

        private bool isValidMove(int pos, int newPos)
        {
            return MoveSets[pos].Contains(newPos);
        }

        private int ConvertUserInput (string s)
        {
            switch (s.ToLower())
            {
                case "a1": return 0;
                case "a4": return 1;
                case "a7": return 2;
                case "b2": return 3;
                case "b4": return 4;
                case "b6": return 5;
                case "c3": return 6;
                case "c4": return 7;
                case "c5": return 8;
                case "d1": return 9;
                case "d2": return 10;
                case "d3": return 11;
                case "d5": return 12;
                case "d6": return 13;
                case "d7": return 14;
                case "e3": return 15;
                case "e4": return 16;
                case "e5": return 17;
                case "f2": return 18;
                case "f4": return 19;
                case "f6": return 20;
                case "g1": return 21;
                case "g4": return 22;
                case "g7": return 23;
                
                default: return -1;
            }

        }

        // Loops until an input is recieved that is not ontop of another cow
        private void ValidInputToPlace(int input)
        {
            while (!board.CanPlaceAt(input))
            {
                Console.WriteLine("\nCan't Place there!");
                input = CastInput();
            }
            board.Place((int)CurrentPlayer, input);
            SwitchPlayer();
        }

        // Sets Console properties, such as height width etc
        private void setConsoleProperties()
        {
            Console.SetWindowSize(70, 50);
        }

        public bool IsDraw()
        {
            throw new NotImplementedException();
        }

        private void SwitchPlayer()
        {
            if (CurrentPlayer == Player.Red) CurrentPlayer = Player.Blue;
            else CurrentPlayer = Player.Red;
        }

        public void Play()
        {
            while (CurrentPhase != Phase.Winner)
            {
                board.DrawBoard();
                int input = CastInput();

                switch (CurrentPhase)
                {
                    //
                    // Move to method when completed
                    //
                    case Phase.Placing:
                        ValidInputToPlace(input);       // Loops until a valid input is recieved 
                        break;

                    case Phase.Moving:
                        throw new NotImplementedException();

                    case Phase.Winner:
                        throw new NotImplementedException();
                }
                Console.Clear();
            }
        }



        public void Winner()
        {
            throw new NotImplementedException();
        }
    }
}
