using System.Text.RegularExpressions;

namespace Y2023;

public class Day1
{
  public static void Solve()
  {
    string[] inputs = File.ReadAllLines("./day1/input.txt");

    // Part 1
    int part1 = inputs
      .Select(line => int.Parse(line.First(c => char.IsNumber(c)).ToString() + line.Last(c => char.IsNumber(c)).ToString()))
      .Sum();

    Console.WriteLine(part1);

    // Part 2
    string pattern = @"one|two|three|four|five|six|seven|eight|nine|\d";
    int part2 = inputs
      .Select(line => int.Parse(
        ConvertMatchToDigit(Regex.Match(line, pattern).Value) + ConvertMatchToDigit(Regex.Match(line, pattern, RegexOptions.RightToLeft).Value)))
      .Sum();

    Console.WriteLine(part2);
  }

  private static string ConvertMatchToDigit(string match) => match switch
  {
    "one" => "1",
    "two" => "2",
    "three" => "3",
    "four" => "4",
    "five" => "5",
    "six" => "6",
    "seven" => "7",
    "eight" => "8",
    "nine" => "9",
    _ => match
  };
}