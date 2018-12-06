using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day05
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
            var input = File.ReadAllText("input1.txt");
            var polymer = ProducePolymer(input);

            Console.WriteLine(polymer.Length);
        }

        private void Solve2()
        {
            var input = File.ReadAllText("input1.txt");
            var minLength = int.MaxValue;

            for (var letter = 'a'; letter <= 'z'; letter++)
            {
                var polymer = ProducePolymer(input, letter);
                minLength = Math.Min(minLength, polymer.Length);
            }

            Console.WriteLine(minLength);
        }

        private static string ProducePolymer(string line, char? ignoreLetter = null )
        {
            var sb = new StringBuilder(line);
            var wasReaction = false;
            char? ignoreLowerLetter = null;
            char? ignoreUpperLetter = null;
            if (ignoreLetter != null)
            {
                ignoreLowerLetter = char.ToLower(ignoreLetter.Value);
                ignoreUpperLetter = char.ToUpper(ignoreLetter.Value);
            }
            
            while (!wasReaction)
            {
                wasReaction = true;
                for (var i = sb.Length - 1; i >= 1; i--)
                {
                    var currentChar = sb[i];
                    if (ignoreLetter.HasValue && (currentChar== ignoreLowerLetter.Value || currentChar == ignoreUpperLetter.Value))
                    {
                        sb.Remove(i, 1);
                        continue;
                    }

                    var prevChar = sb[i - 1];
                    if (currentChar == prevChar || char.ToUpper(currentChar) != char.ToUpper(prevChar))
                    {
                        continue;
                    }

                    sb = sb.Remove(i - 1, 2);
                    i--;
                    wasReaction = false;
                }
            }

            return sb.ToString();
        }
    }
}
