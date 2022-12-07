using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day3
{
    public class Day3Service : IDayService
    {
        const string FILE_PATH = @"C:\Users\I39125\source\repos\AdventOfCode2022\AdventOfCode\Day3\Day3Input.txt";
        public string SolveDay()
        {
            var lines = File.ReadAllLines(FILE_PATH);

            var part1 = SolvePart1(lines);
            var part2 = SolvePart2(lines);

            return $"Part1: {part1} Part2: {part2}";
        }

        private int SolvePart1(string[] lines)
        {
            var intersections = new List<char>();
            foreach (var ogString in lines)
            {
                var firstHalf = ogString.Substring(0, ogString.Length / 2);
                var lastHalf = ogString.Substring(ogString.Length / 2, ogString.Length / 2);

                intersections.AddRange(firstHalf.Intersect(lastHalf).ToList());
            }

            return intersections.Sum(x => FindNumberValue(x));
        }

        private int SolvePart2(string[] lines)
        {
            int groupSize = 3;
            int totalValue = 0;

            for (int i = 0; i < lines.Length; i += groupSize)
            {
                var currentElfSet = lines.Skip(i).Take(groupSize);

                var intersect = currentElfSet.Aggregate(currentElfSet.First().ToList(),
                    (h, e) => { return h.Intersect(e).ToList(); });

                //sum probably uneeded but will stop multiple shared items from causing issues
                totalValue += intersect.Sum(x=>FindNumberValue(x));
            }

            return totalValue;
        }

        private int FindNumberValue(char character)
        {
            return (int)character - (Char.IsUpper(character) ? 38 : 96);
        }
    }
}
