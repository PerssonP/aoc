
using System.Text.RegularExpressions;

namespace Y2023;

public class Day2
{
  public static void Solve()
  {
    string[] inputs = File.ReadAllLines("./day2/input.txt");

    int validGamesSum = 0, minCubesSum = 0;
    foreach (string line in inputs)
    {
      string pattern = @"\d+ red|\d+ blue|\d+ green";
      MatchCollection matches = Regex.Matches(line, pattern);

      // Part 1
      bool invalidGame = matches
        .Any(m => m.Value.Split(' ') switch
        {
          [var n, "red"] => int.Parse(n) > 12,
          [var n, "green"] => int.Parse(n) > 13,
          [var n, "blue"] => int.Parse(n) > 14,
          _ => throw new Exception()
        });
      if (!invalidGame) validGamesSum += int.Parse(line.Split(' ')[1][..^1]);

      // Part 2
      Dictionary<string, int> minCubes = new()
      {
        { "red", 0 },
        { "green", 0 },
        { "blue", 0 }
      };
      foreach (Match match in matches)
      {
        // match ex = "15 red"
        int n = int.Parse(match.Value.Split(' ')[0]);
        string color = match.Value.Split(' ')[1];
        if (n > minCubes[color]) minCubes[color] = n;
      }
      minCubesSum += minCubes["red"] * minCubes["green"] * minCubes["blue"];
    }

    Console.WriteLine(validGamesSum);
    Console.WriteLine(minCubesSum);
  }
}