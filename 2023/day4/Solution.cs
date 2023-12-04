using BenchmarkDotNet.Attributes;
using System.Text.RegularExpressions;

namespace Y2023;

public partial class Day4
{
  private readonly string[] inputs;

  public Day4()
  {
    inputs = File.ReadAllLines("./day4/input.txt");
  }

  public void Solve()
  {
    Console.WriteLine(Part1());
    Console.WriteLine(Part2());
  }

  [Benchmark]
  public double Part1()
  {
    double sum = 0;
    foreach (string input in inputs)
    {
      string[] numbers = input.Split(':')[1].Split('|');
      var winningNumbers = Pattern().Matches(numbers[0]).Select(m => int.Parse(m.Value));
      var gameNumbers = Pattern().Matches(numbers[1]).Select(m => int.Parse(m.Value));

      int winningCount = winningNumbers.Intersect(gameNumbers).Count();
      if (winningCount > 0)
        sum += Math.Pow(2, winningCount - 1);
    }

    return sum;
  }

  [Benchmark]
  public int Part2()
  {
    var cards = new (int wins, int amount)[inputs.Length];
    for (int i = 0; i < inputs.Length; i++)
    {
      string[] numbers = inputs[i].Split(':')[1].Split('|');
      var winningNumbers = Pattern().Matches(numbers[0]).Select(m => int.Parse(m.Value));
      var gameNumbers = Pattern().Matches(numbers[1]).Select(m => int.Parse(m.Value));

      int winningCount = winningNumbers.Intersect(gameNumbers).Count();
      cards[i] = (winningCount, cards[i].amount + 1);
      for (int j = 1; j < winningCount + 1; j++)
        cards[i + j].amount += cards[i].amount;
    }

    return cards.Sum(card => card.amount);
  }

  [GeneratedRegex(@"\d+")]
  private static partial Regex Pattern();
}