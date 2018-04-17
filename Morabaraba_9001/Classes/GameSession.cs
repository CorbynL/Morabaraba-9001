using System;
using System.Linq;
using Morabaraba_9001.Classes;

namespace Morabaraba_9001.Classes
{
    public class GameSession : IGameSession
    {
        public int CowsLeft;

        public IBoard board { get; private set; }

        public enum Player { Red = 0, Blue = 1 };
        public enum Phase { Placing, Moving, Killing, Winner };

        public Player CurrentPlayer { get; private set; } //Switch between players each turn
        public Phase CurrentPhase { get ; private set; }

        public GameSession()
        {  
            //Start game
            board = new Board();
            CurrentPlayer = Player.Red;
            CurrentPhase = Phase.Placing;
            CowsLeft = 24;
        }


        #region Game Play

        public void Start()
        {
            while(CurrentPhase != Phase.Winner)
            {
                int input = CastInput();
                Play(input);
            }
        }

        public void Winner()
        {
            Console.WriteLine(string.Format("Player {0} is the winner!\nIf you would like to play again, type y and q to quit.\n", (int)CurrentPlayer + 1));
        }

        public virtual void Play(int input)
        {           
            switch (CurrentPhase)
            {
                #region Placing
                case Phase.Placing:
                    CowsLeft--;
                    if (CowsLeft == 0)                  // If there are no Cows left, Change to moving phase
                        CurrentPhase = Phase.Moving;
                    board.Place((int)CurrentPlayer, input);

                    if (board.areNewMills((int)CurrentPlayer))
                    {
                        CurrentPhase = Phase.Killing;
                        break;
                    }
                    SwitchPlayer();
                    break;
                #endregion

                #region Moving
                case Phase.Moving:                   

                    if (input == -42)
                    {
                        CurrentPhase = Phase.Winner;
                        SwitchPlayer();
                        Winner();
                        break;
                    }
                    if (input == -420)
                    {
                        CurrentPhase = Phase.Winner;
                        Winner();
                        break;
                    }
                    //Carried 2 inputs on a single number
                    int posFrom = input / 100;
                    int posTo = input - (posFrom * 100);                 

                    board.Move(posFrom, posTo);
                    
                    if (board.areNewMills((int)CurrentPlayer))
                    {
                        CurrentPhase = Phase.Killing;
                        break;
                    }
                    SwitchPlayer();
                    break;
                #endregion

                #region Killing
                case Phase.Killing:
                    board.KillCow(input);

                    if (CowsLeft > 0)
                    {
                        CurrentPhase = Phase.Placing;
                        SwitchPlayer();
                    }
                    else
                    {
                        CurrentPhase = Phase.Moving;
                        SwitchPlayer();
                    }                   

                    break;
                #endregion                
            }
        }

        #endregion

        #region User Input

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

        public int getPlaceInput()
        {
            Console.WriteLine(String.Format("\n{0} Cows left to place", (CowsLeft + 1) / 2));
            Console.WriteLine("\nPlease enter a coordinate:");

            int input = ConvertUserInput(Console.ReadLine());
            while (true)
            {
                if (input != -1 && board.CanPlaceAt(input))
                    return input;
                Console.WriteLine("\nInvalid coordinate input. Please enter a VALID coordinate:");
                input = ConvertUserInput(Console.ReadLine());
            }
        }

        public int getMoveInput()
        {
            if (board.numCowRemaining((int)CurrentPlayer) == 2)
            {                
                return -42; //To be specific to ensure no numbers are confused in actual input
            }

            if (board.numCowRemaining(((int) CurrentPlayer + 1) % 2) == 2)
            {
                return -420; //To be specific to ensure no numbers are confused in actual input
            }

            Console.WriteLine("\nPlease select the cow you want to move");
            int posFrom = ConvertUserInput(Console.ReadLine());
            while (true)
            {
                if (posFrom == -1 || !board.canMoveFrom((int) CurrentPlayer, posFrom))
                {
                    Console.WriteLine("\nInvalid choice. Please choose a VALID cow:");
                    posFrom = ConvertUserInput(Console.ReadLine());
                }
                else break;
            }
            Console.WriteLine("\nPlease select where you want you cow to move");
            int posTo = ConvertUserInput(Console.ReadLine());
            while (true)
            {
                if (posTo == -1 || !board.canMoveTo((int) CurrentPlayer, posFrom, posTo))
                {
                    Console.WriteLine("\nInvalid choice. Please choose a VALID cow:");
                    posTo = ConvertUserInput(Console.ReadLine());
                }
                else
                {
                    //I kinda like what I did here, and yes I know its lazy and probably breaks a principle
                    string input1 = posFrom.ToString();
                    string input2 = posTo.ToString();
                    if (input1.Length == 1)
                        input1 = "0" + input1;
                    if (input2.Length == 1)
                        input2 = "0" + input2;

                    return Convert.ToInt32(input1 + input2);
                }
                
            }
        }

        public int getKillInput()
        {
            Console.WriteLine("\nYou formed a mill, choose an enemy cow to kill");
            int input = ConvertUserInput(Console.ReadLine());
            while (true)
            {
                if (board.CanKillAt((int)CurrentPlayer, input))
                    return input;
                Console.WriteLine("\nYou cannot kill that cow, choose a different enemy cow to kill");
                input = ConvertUserInput(Console.ReadLine());
            }
        }

        // Bigger and better!
        public virtual int CastInput() //Like a spell, 'cause you're a wizzzard, Harry...
        {
            board.DrawBoard();
            Console.WriteLine(String.Format("\n{0} State \t Player {1}'s turn", CurrentPhase, (int)CurrentPlayer+1));
            switch (CurrentPhase)
            {
                case Phase.Placing:
                     return getPlaceInput();

                case Phase.Moving:
                    return getMoveInput();

                case Phase.Killing:
                    return getKillInput();               
            }
            return -1;
        }

        #endregion

        #region Change states
        private void SwitchPlayer()
        {
            if (CurrentPlayer == Player.Red) CurrentPlayer = Player.Blue;
            else CurrentPlayer = Player.Red;
        }

        #endregion





        #region Nolonger usefull Code
        /*

        private void checkForMills()
        {            
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


         // Loops until an input is recieved that is not ontop of another cow      
        public virtual int getInput()
        {
            board.DrawBoard();
            Console.WriteLine(String.Format("\nCurrent state: {0}", CurrentPhase));
            switch (CurrentPhase)
            {
                case Phase.Placing:
                    int input = CastInput();
                    while (!board.CanPlaceAt(input))
                    {
                        Console.WriteLine("\nCan't Place there!");
                        input = CastInput();
                    }
                    return input;

                case Phase.Moving:
                    // Select cow
                    int posFrom = selectCow();
                    // Get new cow position
                    int posTo = selectNewPos(posFrom);
                    // Move cow
                    board.Move((int)CurrentPlayer, posFrom, posTo);
                    return -1;  // Move completed

                case Phase.Killing:
                    input = CastInput();
                    while (!board.CanKillAt((int)CurrentPlayer, input))
                    {
                        Console.WriteLine("\nCan't kill that cow!");
                        input = CastInput();
                    }
                    return input;

                default: return -1;
            }   
        }


        
        //For General input of anything
        public virtual int CastInput() //Like a spell, 'cause you're a wizzzard, Harry...
        {
            Console.WriteLine("\nPlease enter a coordinate:");
            int input = ConvertUserInput(Console.ReadLine());
            while (true)
            {
                if (input != -1)
                    return input;
                Console.WriteLine("\nInvalid coordinate input. Please enter a VALID coordinate:");
                input = ConvertUserInput(Console.ReadLine());
            }
        }

        // Selects a cow owned by the current player with prompt dialog
        private int selectCow()
        {
            Console.WriteLine("Please select the cow you want to move");
            int posFrom = ConvertUserInput(Console.ReadLine());
            while(true)
            {
                if(posFrom != -1 && board.isPlayerCow((int)CurrentPlayer, posFrom))
                    return posFrom;
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
                    return posTo;
                Console.WriteLine("Please select a valid position where there are no cows!");
                posTo = ConvertUserInput(Console.ReadLine());
            }
        }





        */
        #endregion
    }
}
