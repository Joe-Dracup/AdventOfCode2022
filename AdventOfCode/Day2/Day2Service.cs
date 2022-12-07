using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day2
{
    public class Day2Service : IDayService
    {
        const string FILE_PATH = @"C:\Users\I39125\source\repos\AdventOfCode2022\AdventOfCode\Day2\Day2Input.txt";
        public string SolveDay()
        {
            //Part1
            var lines = File.ReadAllLines(FILE_PATH);

            var ListGoes = new List<KeyValuePair<string, string>>();

            foreach (var item in lines)
            {
                var key = item.Substring(0, 1);
                var value = item.Substring(2);
                ListGoes.Add(new KeyValuePair<string, string>(key, value));
            }

            var part1 = SolvePart1(ListGoes);

            var part2 = SolvePart2(ListGoes);

            return $"Part1: {part1} Part2: {part2}";
        }

        private string SolvePart1(List<KeyValuePair<string, string>> listGoes)
        {
            var choicePoints = listGoes.Count(x => x.Value == "X") +
                (listGoes.Count(x => x.Value == "Y") * 2) +
                (listGoes.Count(x => x.Value == "Z") * 3);

            var winPoints = (
                listGoes.Count(x => x.Key == "C" && x.Value == "X") +
                listGoes.Count(x => x.Key == "A" && x.Value == "Y") +
                listGoes.Count(x => x.Key == "B" && x.Value == "Z")
                ) * 6;

            var drawPoints = (
                listGoes.Count(x => x.Key == "A" && x.Value == "X") +
                listGoes.Count(x => x.Key == "B" && x.Value == "Y") +
                listGoes.Count(x => x.Key == "C" && x.Value == "Z")
                ) * 3;

            return (choicePoints + winPoints + drawPoints).ToString();
        }

        private string SolvePart2(List<KeyValuePair<string, string>> listGoes)
        {
            var winDrawPoints = (listGoes.Count(x => x.Value == "Z") * 6) + (listGoes.Count(x => x.Value == "Y") * 3);

            var rockPoints =
                listGoes.Count(x => x.Key == "A" && x.Value == "Y") + //Rock - Draw
                listGoes.Count(x => x.Key == "B" && x.Value == "X") + //Paper - Lose
                listGoes.Count(x => x.Key == "C" && x.Value == "Z");  //Scissors - Win

            var paperPoints = (
                listGoes.Count(x => x.Key == "A" && x.Value == "Z") + //Rock - Win
                listGoes.Count(x => x.Key == "B" && x.Value == "Y") + //Paper - Draw
                listGoes.Count(x => x.Key == "C" && x.Value == "X")   //Scissors - Lose
                    ) * 2;

            var scissorsPoints = (
                listGoes.Count(x => x.Key == "A" && x.Value == "X") + //Rock - Lose
                listGoes.Count(x => x.Key == "B" && x.Value == "Z") + //Paper - Win
                listGoes.Count(x => x.Key == "C" && x.Value == "Y")   //Scissors - Draw
                    ) * 3;


            return $"{winDrawPoints + rockPoints + paperPoints + scissorsPoints}";
        }
    }
}
