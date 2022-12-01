﻿namespace _2022;
class Program
{
    static void Main(string[] args)
    {
        Day1();
    }

    static void Day1()
    {
        List<string> inputs = File.ReadAllLines("./inputs/day1").ToList();
        List<int> elfCals = new List<int>(new int[] { 0 });

        foreach (string input in inputs)
        {
            if (string.IsNullOrEmpty(input))
            {
                elfCals.Add(0);
            }
            else
            {
                elfCals[^1] += int.Parse(input);
            }
        }

        int part1 = elfCals.Max();
        Console.WriteLine(part1);

        int max1 = part1;
        elfCals.Remove(max1);
        int max2 = elfCals.Max();
        elfCals.Remove(max2);
        int max3 = elfCals.Max();

        int part2 = max1 + max2 + max3;
        Console.WriteLine(part2);
    }

}
