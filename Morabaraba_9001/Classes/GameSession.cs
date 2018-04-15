using System;
using System.Linq;
using Morabaraba_9001.Classes;

namespace Morabaraba_9001.Classes
{
    public class GameSession : IGameSession
    {
        public IBoard board { get; private set; }

        public enum Player { Red = 0, Blue = 1 };
        public enum Phase { Placing, Moving, Winner };

        public Player CurrentPlayer { get; private set; } //Switch between players each turn
        public Phase CurrentPhase { get; private set; }

        public GameSession()
        {
            // Set the stage
            SetConsoleProperties();
            
            //Start game
            board = new Board();
            CurrentPlayer = Player.Red;
            CurrentPhase = Phase.Placing;            
        }

        public void Start()
        {
            Play();
        }

        // Sets Console properties, such as height width etc
        private void SetConsoleProperties()
        {
            Console.SetWindowSize(70, 50);
        }

        #region User Input

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

        public int ConvertUserInput (string s)
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
        private void ValidInputAndPlace(int input)
        {
            while (!board.CanPlaceAt(input))
            {
                Console.WriteLine("\nCan't Place there!");
                input = CastInput();
            }
            board.Place((int)CurrentPlayer, input);
            SwitchPlayer();
        }

        #endregion      

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
            int CowsLeft = 24;
            while (CurrentPhase != Phase.Winner)
            {
                board.DrawBoard();
                Console.WriteLine(String.Format("You have {0}",(CowsLeft+1)/2));
                int input = CastInput();
                
                switch (CurrentPhase)
                {
                    //
                    // Move to method when completed
                    //
                    case Phase.Placing:
                        ValidInputAndPlace(input);       // Loops until a valid input is recieved 
                        CowsLeft--;
                        if (CowsLeft == 0)              // If there are no Cows left, Change to moving phase
                            CurrentPhase = Phase.Moving;
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
