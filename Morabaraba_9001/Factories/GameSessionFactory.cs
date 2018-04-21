using System;
using System.Collections.Generic;
using System.Text;
using Morabaraba_9001.Interfaces;
using Morabaraba_9001.Classes;

namespace Morabaraba_9001.Factories
{
    public static class GameSessionFactory
    {
        public static IGameSession CreateGameSession()
        {
            ICowBox box = new CowBox();
            IBoard board = new Board();
            IPlayer P1 = new Player(Color.Red, box);
            IPlayer P2 = new Player(Color.Blue, box);
            IReferee referee = new Referee(board, box);
            IGameSession gameSession = new GameSession(board, P1, P2, box, referee);
            return gameSession;
        }
    }
}
