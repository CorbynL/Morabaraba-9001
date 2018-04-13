using System;
using System.Collections.Generic;
using System.Text;

namespace Morabaraba_9001.Classes
{
    public class Board : IBoard
    {
        public IEnumerable<Cow> Cows => throw new NotImplementedException();

        public Board() { }

        public void DrawBoard()
        {
            Console.WriteLine("\n\n\n\n\t     1     2     3     4     5     6     7\n\n"
           + "\t A ({0}) ------------({1})-------------({2})    \n\n"
           + "\t     |   \\             |        /       |    \n\n"
           + "\t B   |   ({0})-------({1})-------({2})  |    \n\n"
           + "\t     |     |  \\        |    /      |    |    \n\n"
           + "\t C   |     |   ({0})-({1})-({2})   |    |    \n\n"
           + "\t     |     |     |           |     |    |    \n\n"
           + "\t D ({0})-({1})-({2})       ({3})-({4})-({5})\n\n"
           + "\t     |     |     |           |     |    |    \n\n"
           + "\t E   |     |   ({0})-({1})-({2})   |    |    \n\n"
           + "\t     |     | /         |         \\ |    |    \n\n"
           + "\t F   |   ({0})-------({1})-------({2})  |    \n\n"
           + "\t     | /               |             \\  |    \n\n"
           + "\t G ({0}) ------------({1})-------------({2})    ");
            Console.ReadLine();
        }

        public void KillCow(Cow cow)
        {
            throw new NotImplementedException();
        }

        public void Move(Cow cow, int Destination)
        {
            throw new NotImplementedException();
        }

        public int getMove()
        {
            throw new NotImplementedException();
        }
    }
}
