using System.Reflection;
using System.Runtime.ExceptionServices;

namespace _2022;
class Program
{
  static void Main(string[] args)
  {
    if (args.Length > 0 && int.TryParse(args[0], out int nbr))
    {
      MethodInfo? method = typeof(Program).GetMethod($"Day{nbr}");
      if (method == null)
      {
        Console.WriteLine("No method found");
      }
      else
      {
        method.Invoke(null, null);
      }
    }
    else
    {
      Console.WriteLine("Enter number of day to run as the first argument");
    }
  }

  public static void Day1()
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

  public static void Day2()
  {
    List<string> inputs = File.ReadAllLines("./inputs/day2").ToList();
    int score = 0;

    foreach (string input in inputs)
    {
      string[] moves = input.Split(' ');

      switch (moves)
      {
        case ["A", "X"]:
          score += 4;
          break;
        case ["B", "X"]:
          score += 1;
          break;
        case ["C", "X"]:
          score += 7;
          break;
        case ["A", "Y"]:
          score += 8;
          break;
        case ["B", "Y"]:
          score += 5;
          break;
        case ["C", "Y"]:
          score += 2;
          break;
        case ["A", "Z"]:
          score += 3;
          break;
        case ["B", "Z"]:
          score += 9;
          break;
        case ["C", "Z"]:
          score += 6;
          break;
      }
    }
    Console.WriteLine(score);
    score = 0;

    foreach (string input in inputs)
    {
      string[] moves = input.Split(' ');

      switch (moves)
      {
        case ["A", "X"]:
          score += 3;
          break;
        case ["B", "X"]:
          score += 1;
          break;
        case ["C", "X"]:
          score += 2;
          break;
        case ["A", "Y"]:
          score += 4;
          break;
        case ["B", "Y"]:
          score += 5;
          break;
        case ["C", "Y"]:
          score += 6;
          break;
        case ["A", "Z"]:
          score += 8;
          break;
        case ["B", "Z"]:
          score += 9;
          break;
        case ["C", "Z"]:
          score += 7;
          break;
      }
    }
    Console.WriteLine(score);
  }

}
