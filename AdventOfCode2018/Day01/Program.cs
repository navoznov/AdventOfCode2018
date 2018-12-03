using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day01
{
    class Program
    {
        static void Main(string[] args)
        {
            var solve1 = new Program().Solve1();
            Console.WriteLine(solve1);

            var solve2 = new Program().Solve2();
            Console.WriteLine(solve2);
        }

        public string Solve1()
        {
            return GetInput().Select(int.Parse).Sum().ToString();
        }

        public string Solve2()
        {
            var changes = GetInput().Select(int.Parse).ToArray();

            var currentFrecuency = 0;
            var frequencies = new List<int> { currentFrecuency };

            var counter = 0;
            while (true)
            {
                currentFrecuency += changes[counter];
                if (frequencies.Contains(currentFrecuency))
                {
                    break;
                }
                frequencies.Add(currentFrecuency);
                counter = ++counter % changes.Length;
            }
            return currentFrecuency.ToString();
        }

        string[] GetInput()
        {
            return File.ReadAllLines("input1.txt");
        }
    }
}
