using Morabaraba_9001.Classes;
using System;

namespace Morabaraba_9001
{
    class Program
    {
        private static GameSession gameSession;

        static void Main(string[] args)
        {
            // Set the stage
            SetConsoleProperties();

            gameSession = new GameSession();
            gameSession.Start();
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
