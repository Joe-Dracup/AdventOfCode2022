using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day3
{
    public class Day4Service : IDayService
    {
        const string FILE_PATH = @"C:\Users\I39125\source\repos\AdventOfCode2022\AdventOfCode\Day4\Day4Input.txt";
        public string SolveDay()
        {
            var lines = File.ReadAllLines(FILE_PATH);

            var part1 = SolvePart1(lines);
            var part2 = SolvePart2(lines);

            return $"Part1: {part1} Part2: {part2}";
        }

        private int SolvePart1(string[] lines)
        {
            return lines.Count(pair => isContained(pair.Split(',')));
            
        }

        private int SolvePart2(string[] lines)
        {
            return lines.Count(pair => isOverlap(pair.Split(',')));
        }

        private bool isContained(string[] elves)
        {
            return (
                Convert.ToInt32(elves[1].Split('-').First()) >= Convert.ToInt32(elves[0].Split('-').First()) && 
                Convert.ToInt32(elves[1].Split('-').Last()) <= Convert.ToInt32(elves[0].Split('-').Last())
                ) || (
                Convert.ToInt32(elves[0].Split('-').First()) >= Convert.ToInt32(elves[1].Split('-').First()) &&
                Convert.ToInt32(elves[0].Split('-').Last()) <= Convert.ToInt32(elves[1].Split('-').Last())
                );
        }

        private bool isOverlap(string[] elves)
        {
            return 
                (
                    (
                        Convert.ToInt32(elves[1].Split('-').First()) >= Convert.ToInt32(elves[0].Split('-').First()) ||
                        Convert.ToInt32(elves[1].Split('-').Last()) >= Convert.ToInt32(elves[0].Split('-').First())
                    ) &&
                    (
                        Convert.ToInt32(elves[1].Split('-').First()) <= Convert.ToInt32(elves[0].Split('-').Last()) ||
                        Convert.ToInt32(elves[1].Split('-').Last()) <= Convert.ToInt32(elves[0].Split('-').Last())
                    )
                ) || (
                    (
                        Convert.ToInt32(elves[0].Split('-').First()) >= Convert.ToInt32(elves[1].Split('-').First()) ||
                        Convert.ToInt32(elves[0].Split('-').Last()) >= Convert.ToInt32(elves[1].Split('-').First())
                    ) &&                      
                    (                         
                        Convert.ToInt32(elves[0].Split('-').First()) <= Convert.ToInt32(elves[1].Split('-').Last()) ||
                        Convert.ToInt32(elves[0].Split('-').Last()) <= Convert.ToInt32(elves[1].Split('-').Last())
                    )
                );
        }
    }
}
