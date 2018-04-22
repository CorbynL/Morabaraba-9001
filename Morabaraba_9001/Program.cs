using Morabaraba_9001.Classes;
using Morabaraba_9001.Interfaces;
using Morabaraba_9001.Factories;
using System;

namespace Morabaraba_9001
{
    class Program
    {

        static void Main(string[] args)
        {
            // Set the stage
            SetConsoleProperties();

            IGameSession gameSession = GameSessionFactory.CreateGameSession();

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
