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



        #region Change states
        private void SwitchPlayer()
        {
            if (CurrentPlayer == Player.Red) CurrentPlayer = Player.Blue;
            else CurrentPlayer = Player.Red;
        }

        #endregion





        #region Nolonger usefull Code
        /*
       
        public void DrawBoard()
        {
            Console.WriteLine(String.Format(getBoardString(), (object[])BoardPopulation(Cows)));
        }

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
