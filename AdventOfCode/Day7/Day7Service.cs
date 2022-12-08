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
            List<Directory> directoriesP2 = SetupDirectories(fileInput);

            var part1 = SolvePart1(directories);
            var part2 = SolvePart2(directoriesP2);

            return $"Part1: {part1} Part2: {part2}";
        }

        private List<Directory> SetupDirectories(string[] fileInput)
        {
            List<Directory> directories = new List<Directory>();

            string currentPath = "", currentName = "";
            int currentSize;
            Directory currentDirectory = new Directory();

            foreach (var line in fileInput)
            {
                if (line.Contains("$ cd"))
                {
                    if (line.Contains(".."))
                    {
                        currentPath = currentPath.Substring(0, currentPath.Length - currentName.Length);
                        currentName = directories.Where(y => y.Path == currentPath).Select(x => x.Name).First();
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(currentDirectory.Name))
                        {
                            directories.Add(currentDirectory);
                        }

                        currentName = line.Substring(5, line.Length - 5) + "/"; //5 is length of "$ cd "
                        currentPath += currentName;
                        currentDirectory = new Directory()
                        {
                            Name = currentName,
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
            directories = directories.OrderByDescending(x => x.Path.Count(y => y == '/')).ToList();

            directories.ForEach(x => x.IncreaseSizeToIncludeChildren(directories));

            return directories.Where(x => x.Size < 100000).Sum(x => x.Size);
        }

        private long SolvePart2(List<Directory> directories)
        {
            var totalDiskSpaceUsed = 70000000 - directories.Sum(x => x.Size);

            var totalSizeNeeded = 30000000 - totalDiskSpaceUsed;

            directories.ForEach(x => x.IncreaseSizeToIncludeChildren(directories));

            var smallestDirectory = directories.Where(x=>x.Size > totalSizeNeeded).OrderBy(x=>x.Size).First();

            return smallestDirectory.Size;
        }
    }
    class Directory
    {
        public string Name { get; set; }
        public long Size { get; set; }
        public string Path { get; set; }

        internal void IncreaseSizeToIncludeChildren(List<Directory> directories)
        {
            this.Size = directories.Where(x => x.Path.Contains(this.Path)).Sum(x => x.Size);
        }
    }
}
