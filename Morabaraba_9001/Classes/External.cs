using System;
using System.Collections.Generic;
using System.Text;

namespace Morabaraba_9001.Classes
{
    class InputOutput
    {

        #region User Input
        
        // Converts the the coordinates to index        (returns -1 if invalid)
        private int ConvertUserInput(string s)
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

        public int getPlaceInput()
        {
            Console.WriteLine("\nPlease enter a coordinate:");

            int input = ConvertUserInput(Console.ReadLine());
            while (true)
            {
                if (input != -1 )//&& board.CanPlaceAt(input))
                    return input;
                Console.WriteLine("\nInvalid coordinate input. Please enter a VALID coordinate:");
                input = ConvertUserInput(Console.ReadLine());
            }
        }

        public int getMoveFromInput()
        {
            Console.WriteLine("\nPlease select the cow you want to move");
            int posFrom = ConvertUserInput(Console.ReadLine());
            while (true)
            {
                if (posFrom != -1)
                {
                    return posFrom;
                }
                Console.WriteLine("\nInvalid choice. Please choose a VALID cow:");
                posFrom = ConvertUserInput(Console.ReadLine());
            }
        }
        public int getMoveToInput()
        {
            Console.WriteLine("\nPlease select where you want you cow to move");
            int posTo = ConvertUserInput(Console.ReadLine());
            while (true)
            {
                if (posTo != -1)
                {
                    return posTo;
                }
                posTo = ConvertUserInput(Console.ReadLine());
                Console.WriteLine("\nInvalid choice. Please choose a VALID cow:");
            }
        }
        

        public int getKillPosInput()
        {
            Console.WriteLine("\nYou formed a mill, choose an enemy cow to kill");
            int input = ConvertUserInput(Console.ReadLine());
            while (true)
            {
                if (input != -1)
                    return input;
                Console.WriteLine("\nYou cannot kill that cow, choose a different enemy cow to kill");
                input = ConvertUserInput(Console.ReadLine());
            }
        }

        #endregion




    }
}
