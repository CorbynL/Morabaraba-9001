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
            Play();
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
        public virtual int CastInput() //Like a spell, 'cause you're a wizzzard, Harry...
        {
            int input = -1;
            Console.WriteLine("\nPlease enter a coordinate:");

            while (true)
            {
                input = ConvertUserInput(Console.ReadLine());
                if (input == -1)
                {
                    Console.WriteLine("\nInvalid coordinate input. Please enter a VALID coordinate:");
                    continue;
                }
                else break;
            }
            return input;            
        }

        // Converts the the coordinates to index        (returns -1 if invalid)
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
        public virtual void ValidInputAndPlace()
        {
            int input = CastInput();
            while (!board.CanPlaceAt(input))
            {
                Console.WriteLine("\nCan't Place there!");
                input = CastInput();
            }
            board.Place((int)CurrentPlayer, input);
        }

        // Selects a cow owned by the current player with prompt dialog
        private int selectCow()
        {
            Console.WriteLine("Please select the cow you want to move");
            int posFrom = ConvertUserInput(Console.ReadLine());
            while(true)
            {
                if(posFrom != -1 && board.isPlayerCow((int)CurrentPlayer, posFrom))
                {
                    return posFrom;
                }
                Console.WriteLine("Please select One of YOUR cows");
                posFrom = ConvertUserInput(Console.ReadLine());
            }
        }

        // Selects a new possition for the cow to move to with prompt dialog 
        private int selectNewPos(int oldPos)
        {
            Console.WriteLine("Please select where you want you cow to move");
            int posTo = ConvertUserInput(Console.ReadLine());
            while (true)
            {
                if(posTo != -1 && board.CanPlaceAt(posTo) && board.IsValidMove(oldPos, posTo))
                {
                    return posTo;
                }
                Console.WriteLine("Please select a valid possition where there are no cows!");
                posTo = ConvertUserInput(Console.ReadLine());
            }
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

        private void checkForMills()
        {
            board.UpdateMills();
            if (board.areNewMills((int)CurrentPlayer))
            {
                Console.WriteLine("You have formed a MILL! Select a cow to KILL!!!");
                int pos = CastInput();
                while (true)
                {
                    if (board.Cows[pos].PlayerID == (int)CurrentPlayer || board.Cows[pos].PlayerID == -1)
                    {
                        Console.WriteLine("Can't kill that one! Pick another one.");
                        pos = CastInput();
                    }
                    else { break; }
                }
                board.KillCow(pos);
            }
        }

        //public void Play();

        public virtual void Play()
        {
            int CowsLeft = 24;                                                   // ********************** Set to 6 for testing ****************************
            while (CurrentPhase != Phase.Winner)
            {
                board.DrawBoard();
                board.UpdateMills();

                switch (CurrentPhase)
                {
                    //
                    // Move to method when completed
                    //
                    case Phase.Placing:
                        Console.WriteLine(String.Format("Player {0}: You have {1} cows left to move", (int)CurrentPlayer, (CowsLeft + 1) / 2));
                        ValidInputAndPlace();               // Loops until a valid input is recieved 
                        checkForMills();
                        CowsLeft--;
                        if (CowsLeft == 0)                  // If there are no Cows left, Change to moving phase
                            CurrentPhase = Phase.Moving;
                        SwitchPlayer();
                        break;

                    case Phase.Moving:
                        if (board.numCowRemaining((int)CurrentPlayer) <= 2)     // Winning state (Current player has less than 2 cows
                        {
                            CurrentPhase = Phase.Winner;
                            SwitchPlayer();
                            break;
                        }
                        // Select cow
                        int posFrom = selectCow();
                        // Get new cow position
                        int posTo = selectNewPos(posFrom);
                        // Move cow
                        board.Move((int)CurrentPlayer, posFrom, posTo);
                        checkForMills();
                        SwitchPlayer();
                        break;

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
