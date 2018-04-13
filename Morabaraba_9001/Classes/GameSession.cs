using System;
using System.Collections.Generic;
using System.Text;

namespace Morabaraba_9001.Classes
{
    public class GameSession : IGameSession
    {
        public enum Player { Red = 0, Blue = 1 }

        private Player CurrentPlayer; //Switch between players each turn

        public GameSession()
        {
            board = new Board();
            CurrentPlayer = Player.Red;
            Play();
        }

        public IBoard board { get; private set; }        

        //For General input of anything
        public int CastInput() //Like a spell, 'cause you're a wizzzard, Harry...
        {
            int input = -1;
            Console.WriteLine("Please enter a coordinate:");

            while (true)
            {
                Console.ReadKey();
                input = ConvertUserInput(Console.ReadLine());
                if (input == -1)
                {
                    Console.WriteLine("Invalid coordinate input. Please enter a VALID coordinate:");
                    continue;
                }
                else break;
            }
            return input;            
        }

        private int ConvertUserInput (string s)
        {
            switch (s.ToLower())
            {
                case "a1": return 0;
                case "a4": return 1;
                case "a7": return 2;
                case "b2": return 3;
                case "b4": return 4;
                case "b6": return 5;
                case "c3": return 6;
                case "c4": return 7;
                case "c5": return 8;
                case "d1": return 9;
                case "d2": return 10;
                case "d3": return 11;
                case "d5": return 12;
                case "d6": return 13;
                case "d7": return 14;
                case "e3": return 15;
                case "e4": return 16;
                case "e5": return 17;
                case "f2": return 18;
                case "f4": return 19;
                case "f6": return 20;
                case "g1": return 21;
                case "g4": return 22;
                case "g7": return 23;
                
                default: return -1;
            }

        }

        public bool IsDraw()
        {
            throw new NotImplementedException();
        }

        public void Play()
        {
            board.DrawBoard();
        }

        public void Winner()
        {
            throw new NotImplementedException();
        }
    }
}
