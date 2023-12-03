using System.Text.RegularExpressions;

namespace Y2023;

public class Day3
{
  public static void Solve()
  {
    string[] inputs = File.ReadAllLines("./day3/input.txt");

    int part1 = 0;
    for (int i = 0; i < inputs.Length; i++)
    {
      MatchCollection numbersOnRow = Regex.Matches(inputs[i], @"\d+");
      // Get relevant rows: i & i - 1 & i + 1
      string[] perimiterRows = inputs[Math.Clamp((i - 1), 0, inputs.Length)..Math.Clamp((i + 2), 0, inputs.Length)];
      foreach (Match n in numbersOnRow)
      {
        // Check perimiter for each match
        List<char> perimiter = [];
        foreach (string row in perimiterRows)
          perimiter.AddRange(row[Math.Clamp(n.Index - 1, 0, row.Length)..Math.Clamp(n.Index + n.Length + 1, 0, row.Length)]);
        if (Regex.Match(new string(perimiter.ToArray()), @"[^.\d]").Success)
          part1 += int.Parse(n.Value);        
      }
    }

    Console.WriteLine(part1);
  }
}