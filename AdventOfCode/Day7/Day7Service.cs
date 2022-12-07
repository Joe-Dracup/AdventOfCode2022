using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AdventOfCode.Day3
{
    public class Day7Service : IDayService
    {
        const string FILE_PATH = @"C:\Users\I39125\source\repos\AdventOfCode2022\AdventOfCode\Day6\Day6Input.txt";

        public string SolveDay()
        {
            var text = File.ReadAllText(FILE_PATH);

            var part1 = SolvePart1(text);
            var part2 = SolvePart2(text);

            return $"Part1: {part1} Part2: {part2}";
        }

        private string SolvePart1(string text)
        {
            return solve(text, 4);
        }

        public bool nextAre4Unique(string text, int count)
        {
            return text.Distinct().Count() == count;
        }

        private string SolvePart2(string text)
        {
            return solve(text, 14);
        }

        private string solve(string text, int SearchNumber)
        {
            for (int i = 0; i < text.Length; i++)
            {
                if (nextAre4Unique(text.Substring(i, SearchNumber), SearchNumber))
                {
                    return $"{i + SearchNumber} - {text.Substring(i, SearchNumber)}";
                }
            }
            return "";
        }
    }
}
