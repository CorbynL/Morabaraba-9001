using System;
using System.Linq;
using Morabaraba_9001.Classes;
using Morabaraba_9001.Interfaces;

namespace Morabaraba_9001.Classes
{
    public class GameSession : IGameSession
    {
        public IBoard board { get; private set; }

        public IPlayer Current_Player { get; private set; }

        public IPlayer Player_1 { get; }

        public IPlayer Player_2 { get; }

        public ICowBox box { get; private set; }

        public IReferee referee { get; private set; }

        private enum Phase { Placing, Killing, Moving, Winning }

        private Phase Current_Phase;

        public GameSession(IBoard b, IPlayer p1, IPlayer p2, ICowBox cow, IReferee r)
        {
            board = b;
            Player_1 = p1;
            Player_1 = p2;
            box = cow;
            referee = r;

        }
        
        public void Start()
        {

        }

        public void Play()
        {
            int input;

            switch (Current_Phase)
            {
                case Phase.Placing:
                    input = External.PlaceInput();
                    if(referee.CanPlace(Current_Player.Color, input))
                    {
                        Current_Player.Place(input, board);
                    }
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

        private void SwitchState ()
        {
            throw new NotImplementedException();
        }

        private void SwitchPlayer()
        {
            throw new NotImplementedException();
        }

        public void Winner()
        {
            throw new NotImplementedException();
        }
    }
}

