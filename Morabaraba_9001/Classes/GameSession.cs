using System;
using System.Linq;
using Morabaraba_9001.Classes;
using Morabaraba_9001.Interfaces;


namespace Morabaraba_9001.Classes
{
    public enum Phase { Placing, Killing, Moving, Winning }

    public class GameSession : IGameSession
    {
        public IBoard board { get; private set; }

        public IPlayer Current_Player { get; private set; }

        public IPlayer Player_1 { get; }

        public IPlayer Player_2 { get; }

        public ICowBox box { get; private set; }

        public IReferee referee { get; private set; }        

        public Phase Current_Phase { get; private set; }

        public GameSession(IBoard b, IPlayer p1, IPlayer p2, ICowBox cow, IReferee r)
        {
            board = b;
            Player_1 = p1;
            Player_2 = p2;
            box = cow;
            referee = r;

            Current_Player = Player_1;
            Current_Phase = Phase.Placing;

        }
        
        public void Start()
        {
            while (true)
            {
                Play();
            }
        }

        public void Play()
        {
            board.drawBoard(Current_Phase.ToString(), Current_Player.Color.ToString());      // Changed this so that board.Cows is not public anymore
            int input; 

            switch (Current_Phase)
            {                
                case Phase.Placing:
                    input = External.PlaceInput();
                    DoPlacePhase(input);
                    break;
                case Phase.Killing:
                    input = External.KillPosInput();
                    DoKillPhase(input);
                    break;
                case Phase.Moving:
                    input = External.MoveFromInput();
                    DoMovePhase(input);
                    break;                
            }
        }

        //I've done it this way to prevent a loop when asking for input. For testing purposes
        // Needs additional printing functionallity
        private void DoPlacePhase(int input)
        {            
                if(Current_Player.Place(input, board, referee, Current_Phase))
            {
                if (board.areNewMills(Current_Player.Color))
                {
                    Current_Phase = Phase.Killing;
                }
                else if (box.IsEmpty()){
                    Current_Phase = Phase.Moving; SwitchPlayer(); return;
                }
                else SwitchPlayer();
            }
        }

        private void DoMovePhase(int input)
        {
            if (Current_Player.Select(input, board, referee)){
                int inputTo = External.MoveToInput();                                   // We might need to move this for testing but lets see how it goes

                if (Current_Player.Move(input, inputTo, board, referee,Current_Phase)){
                    if (board.areNewMills(Current_Player.Color))
                    {
                        Current_Phase = Phase.Killing;
                    }
                    else SwitchPlayer();
                }
            }
            
        }

        private void DoKillPhase(int input)
        {

            if (Current_Player.Kill(input, board,referee)){
                if (box.IsEmpty())
                {
                    Current_Phase = Phase.Moving;
                    return;
                }
                Current_Phase = Phase.Placing;
                SwitchPlayer();
            }
        }


        private void SwitchPlayer()
        {
            if (Current_Player.Color == Color.Red) { Current_Player = Player_2; }
            else Current_Player = Player_1;
        }

        public void Winner()
        {
            throw new NotImplementedException();
        }
    }
}

