using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day1
{
    public class Day1Service : IDayService
    {
        const string FILE_PATH = @"C:\Users\I39125\source\repos\AdventOfCode2022\AdventOfCode\Day1\Day1Input.txt";
        const int NUM_ELVES = 3;
        public string SolveDay()
        {
            var inputListString = File.ReadAllLines(FILE_PATH);

            List<int> currentElfFoodList = new List<int>();
            List<int> elfTotalCalories = new List<int>();

            for (int i = 0; i < inputListString.Length; i++)
            {
                if (string.IsNullOrEmpty(inputListString[i]))
                {
                    elfTotalCalories.Add(currentElfFoodList.Sum(x => x));
                    currentElfFoodList.Clear();
                }
                else
                {
                    currentElfFoodList.Add(Convert.ToInt32(inputListString[i]));
                }
            }

            string highestCalories = elfTotalCalories.Max().ToString();

            var TotalCalories = 0;

            for (int i = 0; i < NUM_ELVES; i++)
            {
                TotalCalories += elfTotalCalories.Max();
                elfTotalCalories.Remove(elfTotalCalories.Max());
            }

            return $"Part1: {highestCalories} Part2: {TotalCalories}";
        }
    }
}
