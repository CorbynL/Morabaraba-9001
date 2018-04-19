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

        IPlayer Player_1;

        IPlayer Player_2;

        public ICowBox box { get; private set; }

        public IReferee referee { get; private set; }

        public GameSession()
        {
            board = new Board();
            Player_1 = new Player();
            Player_1 = new Player();
            box = new CowBox();

        }

        public void Play()
        {
            switch
        }

        public void Winner()
        {
            throw new NotImplementedException();
        }
    }
}
