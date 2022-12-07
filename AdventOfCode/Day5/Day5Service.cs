using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day3
{
    public class Day5Service : IDayService
    {
        const string FILE_PATH = @"C:\Users\I39125\source\repos\AdventOfCode2022\AdventOfCode\Day5\Day5Input.txt";
        const int CRATESTACK_LASTLINE = 7, NUM_CRATES = 9;

        public string SolveDay()
        {
            var lines = File.ReadAllLines(FILE_PATH);

            List<Stack<char>> stackList = new List<Stack<char>>();
            List<Stack<char>> part2StackList = new List<Stack<char>>();


            for (int i = 0; i < NUM_CRATES; i++)
            {
                stackList.Add(new Stack<char>());
                part2StackList.Add(new Stack<char>());
            }

            for (int row = CRATESTACK_LASTLINE; row >= 0; row--)
            {
                int currentColumn = 0;
                for (int column = 1; column <= lines[row].Length; column += 4)
                {
                    var letter = lines[row].Skip(column).Take(1).First();
                    if (letter != ' ')
                    {
                        stackList[currentColumn].Push(letter);
                        part2StackList[currentColumn].Push(letter);
                    }
                    currentColumn++;
                }
            }

            var part1 = SolvePart1(stackList, lines);
            var part2 = SolvePart2(part2StackList, lines);

            return $"Part1: {part1} Part2: {part2}";
        }

        private string SolvePart1(List<Stack<char>> list, string[] lines)
        {
            for (int i = CRATESTACK_LASTLINE + 3; i < lines.Length; i++)
            {
                var splitString = lines[i].Split(" ");

                int numCrates = Convert.ToInt32(splitString[1]), prevColumn = Convert.ToInt32(splitString[3]) - 1, toColumn = Convert.ToInt32(splitString[5]) - 1;

                for (int y = 0; y < numCrates; y++)
                {
                    if (list[prevColumn].Count > 0)
                    {
                        list[toColumn].Push(list[prevColumn].Pop());
                    }
                }
            }

            string returnString = "";
            foreach (var item in list)
            {
                if(item.Count > 0)
                {
                    returnString += item.Pop();
                }
                else
                {
                    returnString += " ";
                }
            }
            return returnString;
        }

        private string SolvePart2(List<Stack<char>> list, string[] lines)
        {
            for (int i = CRATESTACK_LASTLINE + 3; i < lines.Length; i++)
            {
                var splitString = lines[i].Split(" ");

                int numCrates = Convert.ToInt32(splitString[1]), prevColumn = Convert.ToInt32(splitString[3]) - 1, toColumn = Convert.ToInt32(splitString[5]) - 1;

                List<char> tempList = new List<char>();

                for (int y = 0; y < numCrates; y++)
                {
                    if (list[prevColumn].Count > 0)
                    {
                        tempList.Add(list[prevColumn].Pop());
                    }
                }

                tempList.Reverse();
                tempList.ForEach(x => list[toColumn].Push(x));
            }

            string returnString = "";
            foreach (var item in list)
            {
                if (item.Count > 0)
                {
                    returnString += item.Pop();
                }
                else
                {
                    returnString += " ";
                }
            }
            return returnString;
        }
    }
}
