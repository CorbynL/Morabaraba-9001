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

        private Phase Current_Phase;

        public GameSession(IBoard b, IPlayer p1, IPlayer p2, ICowBox cow, IReferee r)
        {
            board = b;
            Player_1 = p1;
            Player_2 = p2;
            box = cow;
            referee = r;

        }
        
        public void Start()
        {
            Current_Player = Player_1;
            Current_Phase = Phase.Placing;

            while (true)
            {
                Play();
            }
        }

        public void Play()
        {
            External.DrawBoard(board.Cows, Current_Phase.ToString(), Current_Player.Color.ToString());
            int input = External.PlaceInput();

            switch (Current_Phase)
            {                
                case Phase.Placing:
                    DoPlacePhase(input);

                    break;
                case Phase.Killing:
                    input = External.KillPosInput();
                    break;
                case Phase.Moving:
                    input = External.MoveToInput();
                    input = External.MoveFromInput();
                    break;                
            }
        }

        private void DoPlacePhase(int input)
        {            
            if (referee.CanPlace(Current_Player.Color, input))
            {
                Current_Player.Place(input, board);
                if (box.IsEmpty()) { Current_Phase = Phase.Moving; return; }
                else SwitchPlayer();
            }
        }

        private void SwitchState ()
        {
            throw new NotImplementedException();
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

