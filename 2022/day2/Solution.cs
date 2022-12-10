namespace Y2022;

public class Day2
{
  public static void Solve()
  {
    List<string> inputs = File.ReadAllLines("./day2/input.txt").ToList();
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