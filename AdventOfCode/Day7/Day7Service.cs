using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AdventOfCode.Day3
{
    public class Day7Service : IDayService
    {
        const string FILE_PATH = @"C:\Users\I39125\source\repos\AdventOfCode2022\AdventOfCode\Day7\Day7Input.txt";

        public string SolveDay()
        {
            var fileInput = File.ReadAllLines(FILE_PATH);

            List<Directory> directories = SetupDirectories(fileInput);

            var part1 = SolvePart1(directories);
            var part2 = SolvePart2(directories);

            return $"Part1: {part1} Part2: {part2}";
        }

        private List<Directory> SetupDirectories(string[] fileInput)
        {
            List<Directory> directories = new List<Directory>();

            string currentPath = "";
            int currentSize;
            Directory currentDirectory = new Directory();

            foreach (var line in fileInput)
            {
                if (line.Contains("$ cd"))
                {
                    if (line.Contains(".."))
                    {
                        currentPath = currentPath.Replace($"{currentDirectory.Name}/", "");
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(currentDirectory.Name))
                        {
                            directories.Add(currentDirectory);
                        }

                        var directoryName = line.Substring(5, line.Length - 5); //5 is length of "$ cd "
                        currentPath += $"{directoryName}/";
                        currentDirectory = new Directory()
                        {
                            Name = directoryName,
                            Path = currentPath,
                        };
                    }
                }
                else if (Int32.TryParse(line.Substring(0, line.IndexOf(' ')), out currentSize))
                {
                    currentDirectory.Size += currentSize;
                }
            }

            directories.Add(currentDirectory);
            return directories;
        }

        private long SolvePart1(List<Directory> directories)
        {
            //Could be miss ordered?
            //directories = directories.OrderByDescending(x => x.Path.Count(y => y == '/')).ToList();
            //directories = directories.OrderByDescending(x => directories.Count(y => y.Path.Contains(x.Path))).ToList();

            directories.ForEach(x => x.IncreaseSizeToIncludeChildren(directories));

            return directories.Where(x => x.Size < 100000).Sum(x => x.Size);
        }

        private string SolvePart2(List<Directory> directories)
        {
            return "";
        }
    }
    class Directory
    {
        public string Name { get; set; }
        public long Size { get; set; }
        public string Path { get; set; }

        internal void IncreaseSizeToIncludeChildren(List<Directory> directories)
        {
            this.Size += directories.Where(x => x.Path.Contains(this.Path)).Sum(x => x.Size);
        }
    }
}
