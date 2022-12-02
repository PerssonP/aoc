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

    var matrix = new Dictionary<char, Dictionary<char, int>>()
    {
      { 'A', new Dictionary<char, int>() {{ 'X', 4 }, { 'Y', 8 }, { 'Z', 3 }} },
      { 'B', new Dictionary<char, int>() {{ 'X', 1 }, { 'Y', 5 }, { 'Z', 9 }} },
      { 'C', new Dictionary<char, int>() {{ 'X', 7 }, { 'Y', 2 }, { 'Z', 6 }} },
    };

    foreach (string input in inputs)
    {
      char[] moves = input.Split(' ').Select(char.Parse).ToArray();
      score += matrix[moves[0]][moves[1]];
    }

    Console.WriteLine(score);
    score = 0;

    matrix = new Dictionary<char, Dictionary<char, int>>()
    {
      { 'A', new Dictionary<char, int>() {{ 'X', 3 }, { 'Y', 4 }, { 'Z', 8 }} },
      { 'B', new Dictionary<char, int>() {{ 'X', 1 }, { 'Y', 5 }, { 'Z', 9 }} },
      { 'C', new Dictionary<char, int>() {{ 'X', 2 }, { 'Y', 6 }, { 'Z', 7 }} },
    };

    foreach (string input in inputs)
    {
      char[] moves = input.Split(' ').Select(char.Parse).ToArray();
      score += matrix[moves[0]][moves[1]];
    }

    Console.WriteLine(score);
  }

}
