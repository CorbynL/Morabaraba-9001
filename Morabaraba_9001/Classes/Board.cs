using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Morabaraba_9001.Classes
{
    public class Board : IBoard
    {
        public IEnumerable<Cow> Cows { get; private set; }

        private IEnumerable<string> BoardPopulation(IEnumerable<Cow> cows)
        {
            return cows.Select(x => (x.Symbol.ToString())).ToArray();
        } 

        public Board()
        {
            initialiseCows();            
        }

        private void initialiseCows()
        {
            Cows = new Cow[24];
            Cows = Cows.Select((x,index) => new Cow(index, -1)).ToArray();
        }

        public void DrawBoard()
        {
            Console.WriteLine(String.Format(getBoardString(),(object[])BoardPopulation(Cows)));
        }

        private string getBoardString()
        {
            StringBuilder builder = new StringBuilder("\n\n\n\n\t     1     2     3     4     5     6     7\n\n");
            builder.Append("\t G   ({0}) -------------({1})---------------({2})\n\n");
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
            builder.Append("\t G   ({21}) -------------({22})---------------({23})    ");
            return builder.ToString();
        }

        public bool isCowAt(int pos)
        {
            return Cows.ElementAt(pos).Position != -1;
        }

        public void Place(int ID, int Destination)
        {
            if (Cows.ElementAt(Destination).PlayerID != -1)
                throw new Exception(); //Maybe a fancier, more meaningful exception?
            else Cows = Cows.Select(x => (x.Position != Destination) ? x : new Cow(Destination, ID)); //I feel like this could be done better because it still runs through the whole list of cows 
        }

        public void KillCow(int ID, int Destination)
        {
            throw new NotImplementedException();
        }

        private void Remove(int ID, int Destination)
        {
            throw new NotImplementedException();
        }

        public void Move(int ID, int firstDestination, int secondDestination)
        {
            throw new NotImplementedException();
        }

        public int getMove()
        {
            throw new NotImplementedException();
        }
    }
}
