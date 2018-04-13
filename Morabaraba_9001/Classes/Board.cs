﻿using System;
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
            string[] a = cows.Select(x => (x.Symbol.ToString())).ToArray();
            return a;
        }

        public Board()
        {
            initiliseCows();
        }

        private void initiliseCows()
        {
            Cows = new Cow[24];
            for (int i = 0; i < 23; i++)
            {
                Cows = Cows.Select(x => new Cow(i, -1)).ToArray();
            }
        }

        public void DrawBoard()
        {
            Console.WriteLine(String.Format(getBoardString(),(object[])BoardPopulation(Cows)));
            Console.ReadLine();
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
