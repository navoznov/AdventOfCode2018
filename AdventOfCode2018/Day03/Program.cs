using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day03
{
    class Program
    {
        static void Main(string[] args)
        {
            var program = new Program();
            program.Solve1();
            program.Solve2();
        }

        private void Solve1()
        {
            var matrix = new byte[1000,1000];
            var counter = 0;
            var lines = File.ReadAllLines("input1.txt");
            foreach (var line in lines)
            {
                //#76 @ 23,683: 26x28
                var itemRegex = new Regex(@"#(?<id>\d+) @ (?<x>\d+),(?<y>\d+): (?<width>\d+)x(?<height>\d+)");
                var match = itemRegex.Match(line);
                var x = int.Parse(match.Groups["x"].Value);
                var y = int.Parse(match.Groups["y"].Value);
                var width = int.Parse(match.Groups["width"].Value);
                var height = int.Parse(match.Groups["height"].Value);
                for (var i = x; i < x+width; i++)
                {
                    for (var j = y; j < y+height; j++)
                    {
                        if (matrix[i, j] == 0)
                        {
                            matrix[i, j] = (byte) 1;
                        }
                        else if(matrix[i, j] == 1)
                        {
                            matrix[i, j] = (byte) 2;
                            counter++;
                        }
                    }
                }
            }

            Console.WriteLine(counter);
        }

        private void Solve2()
        {
            var matrix = new int[1000, 1000];
            var lines = File.ReadAllLines("input1.txt");


            var notOverlapedIds = new List<int>();
            foreach (var line in lines)
            {
                var isOverlaped = false;
                //#76 @ 23,683: 26x28
                var itemRegex = new Regex(@"#(?<id>\d+) @ (?<x>\d+),(?<y>\d+): (?<width>\d+)x(?<height>\d+)");
                var match = itemRegex.Match(line);
                var id = int.Parse(match.Groups["id"].Value);
                var x = int.Parse(match.Groups["x"].Value);
                var y = int.Parse(match.Groups["y"].Value);
                var width = int.Parse(match.Groups["width"].Value);
                var height = int.Parse(match.Groups["height"].Value);
                for (var i = x; i < x + width; i++)
                {
                    for (var j = y; j < y + height; j++)
                    {
                        if (matrix[i, j] > 0 )
                        {
                            isOverlaped = true;
                            notOverlapedIds.Remove(matrix[i, j]);
                        }

                        matrix[i, j] = id;
                    }
                }

                if (!isOverlaped)
                {
                    notOverlapedIds.Add(id);
                }
            }

            Console.WriteLine($"{notOverlapedIds.Single()}");
        }
    }
}
