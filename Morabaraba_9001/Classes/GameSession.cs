using System;
using System.Collections.Generic;
using System.Text;

namespace Morabaraba_9001.Classes
{
    public class GameSession : IGameSession
    {
        public GameSession()
        {
            board = new Board();
            Play();
        }

        public IBoard board { get; private set; }

        public bool IsDraw()
        {
            throw new NotImplementedException();
        }

        public void Play()
        {
            board.DrawBoard();
        }

        public IPlayer Winner()
        {
            throw new NotImplementedException();
        }
    }
}
