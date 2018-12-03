using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day02
{
    class Program
    {
        static void Main(string[] args)
        {
            var program = new Program();
            Console.WriteLine(program.Solve1());
            Console.WriteLine(program.Solve2());
        }

        private string Solve1()
        {

            var lines = File.ReadLines("input1.txt");
            var twoCounter = 0;
            var threeCounter = 0;

            foreach (var line in lines)
            {
                var groupCounts = line.ToCharArray().GroupBy(x => x, x => x).Select(x => x.Count()).Distinct().ToArray();
                if (groupCounts.Contains(2))
                {
                    twoCounter++;
                }
                if (groupCounts.Contains(3))
                {
                    threeCounter++;
                }
            }

            return (twoCounter * threeCounter).ToString();

        }
        private string Solve2()
        {
            var lines = File.ReadLines("input1.txt").ToArray();

            for (int i = 0; i < lines.Length-1; i++)
            {
                for (int j = i+1; j < lines.Length; j++)
                {
                    if (IsCorrect(lines[i], lines[j], out int position))
                    {
                        return lines[i].Remove(position, 1);
                    }
                }
            }

            return null;
        }

        private bool IsCorrect(string firstId, string secondId, out int invalidPosition)
        {
            invalidPosition = -1;
            for (int i = 0; i < firstId.Length; i++)
            {
                if (firstId[i]!=secondId[i])
                {
                    if (invalidPosition>-1)
                    {
                        invalidPosition = -1;
                        return false;
                    }
                    invalidPosition = i;
                }
            }

            return true;
        }
    }
}
                