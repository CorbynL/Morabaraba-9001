using Morabaraba_9001.Classes;
using Morabaraba_9001.Interfaces;
using System;

namespace Morabaraba_9001
{
    class Program
    {

        static void Main(string[] args)
        {
            // Set the stage
            SetConsoleProperties();

            ICowBox box = new CowBox();
            IBoard board = new Board();
            IPlayer P1 = new Player(Color.Red, box);
            IPlayer P2 = new Player(Color.Blue,box);
            IReferee referee = new Referee(board,box);

            IGameSession gameSession = new GameSession(board, P1, P2, box, referee);

            gameSession.Start();

            if (Console.ReadLine() == "y")
            {
                Main(null);
            }
            else Environment.Exit(0);
        }

        #region Set Console Properties for the game

        // Sets Console properties, such as height width etc
        static void SetConsoleProperties()
        {
            Console.SetWindowSize(70, 50);
        }

        #endregion
    }
}
