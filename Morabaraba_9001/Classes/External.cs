using Morabaraba_9001.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Morabaraba_9001.Classes
{
    /// <summary>
    /// Handles all interaction with the console
    /// </summary>
    static class External 
    {

        /// <summary>
        /// Converts the coordinates to an index on the board
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        static private int convertUserInput(string s)
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

        /// <summary>
        /// Contains the board string
        /// </summary>
        /// <returns></returns>
        static public void DrawBoard(ICow[] Cows, string State, string PlayerName)
        {
            Console.Clear();
            StringBuilder builder = new StringBuilder("\n\n\n\n\t     1     2     3     4     5     6     7\n\n");
            builder.Append("\t A  ({0})---------------({1})---------------({2})\n\n");
            builder.Append("\t     |   \\             |              /  |    \n\n");
            builder.Append("\t B   |    ({3})---------({4})---------({5})    |\n\n");
            builder.Append("\t     |     |  \\        |       /   |     |    \n\n");
            builder.Append("\t C   |     |    ({6})---({7})---({8})    |     |    \n\n");
            builder.Append("\t     |     |     |           |     |     |    \n\n");
            builder.Append("\t D  ({9})---({10})---({11})         ({12})---({13})---({14})\n\n");
            builder.Append("\t     |     |     |           |     |     |    \n\n");
            builder.Append("\t E   |     |    ({15})---({16})---({17})    |     |    \n\n");
            builder.Append("\t     |     | /         |         \\ |     |\n\n");
            builder.Append("\t F   |    ({18})---------({19})---------({20})    |\n\n");
            builder.Append("\t     | /               |              \\  |    \n\n");
            builder.Append("\t G  ({21})---------------({22})---------------({23})    ");
  
            Console.WriteLine(String.Format(builder.ToString(), (object[])BoardPopulation(Cows)));
            Console.WriteLine(String.Format("\nState: {0} \t Player: {1}\n", State, PlayerName));
        }
        
        /// <summary>
        /// Used to Get each symbol of each Cow and add it to the board string
        /// </summary>
        /// <param name="cows"></param>
        /// <returns></returns>
        static IEnumerable<string> BoardPopulation(IEnumerable<ICow> cows)
        {
            return cows.Select(x => (x.Symbol.ToString())).ToArray();
        }

        /// <summary>
        /// Get the Input to place a cow
        /// </summary>
        /// <returns></returns>
        static public int PlaceInput()
        {
            Console.WriteLine("\nPlease enter a coordinate to place your cow:");

            int input = convertUserInput(Console.ReadLine());
            while (true)
            {
                if (input != -1 )//&& board.CanPlaceAt(input))
                    return input;
                Console.WriteLine("\nInvalid coordinate input. Please enter a VALID coordinate:");
                input = convertUserInput(Console.ReadLine());
            }
        }

        /// <summary>
        ///  Asks to select a cow and returns the input
        /// </summary>
        /// <returns></returns>
        static public int MoveFromInput()
        {
            Console.WriteLine("\nPlease select the cow you want to move:\n");
            int posFrom = convertUserInput(Console.ReadLine());
            while (true)
            {
                if (posFrom != -1)
                    return posFrom;
                Console.WriteLine("\nInvalid choice. Please choose a VALID cow:\n");
                posFrom = convertUserInput(Console.ReadLine());
            }
        }

        /// <summary>
        /// Asks to select the possition that you want to move a cow to
        /// </summary>
        /// <returns></returns>
        static public int MoveToInput()
        {
            Console.WriteLine("\nPlease select where you want you cow to move:\n");
            int posTo = convertUserInput(Console.ReadLine());
            while (true)
            {
                if (posTo != -1)
                    return posTo;
                posTo = convertUserInput(Console.ReadLine());
                Console.WriteLine("\nInvalid choice. Please choose a VALID cow:\n");
            }
        }



        /// <summary>
        ///  Asks to select a cow to kill
        /// </summary>
        /// <returns></returns>
        static public int KillPosInput()
        {
            Console.WriteLine("\nYou formed a mill, choose an enemy cow to kill:\n");
            int input = convertUserInput(Console.ReadLine());
            while (true)
            {
                if (input != -1)
                    return input;
                Console.WriteLine("\nYou cannot kill that cow, choose a different enemy cow to kill:\n");
                input = convertUserInput(Console.ReadLine());
            }
        }
    }
}
