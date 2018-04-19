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

        public GameSession()
        {
            board = new Board();
            Player_1 = new Player();
            Player_1 = new Player();
            box = new CowBox();
            Current_Phase = Phase.Placing;

        }
        
        public void Start()
        {

        }

        public void Play()
        {
            switch (Current_Phase)
            {
                case Phase.Placing:
                    throw new NotImplementedException();
                    break;
                case Phase.Killing:
                    throw new NotImplementedException();
                    break;
                case Phase.Moving:
                    throw new NotImplementedException();
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

