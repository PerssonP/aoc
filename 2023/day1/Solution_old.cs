
namespace Y2023;

public class Day1_old
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
    List<int> numbers = [];
    foreach (string line in inputs)
    {
      string? start = null, end = null;
      string workS = line, workE = line;
      
      while (start == null)
      {
        if (char.IsNumber(workS[0]))
          start = workS[0].ToString();
        else if (workS.StartsWith("one"))
          start = "1";
        else if (workS.StartsWith("two"))
          start = "2";
        else if (workS.StartsWith("three"))
          start = "3";
        else if (workS.StartsWith("four"))
          start = "4";
        else if (workS.StartsWith("five"))
          start = "5";
        else if (workS.StartsWith("six"))
          start = "6";
        else if (workS.StartsWith("seven"))
          start = "7";
        else if (workS.StartsWith("eight"))
          start = "8";
        else if (workS.StartsWith("nine"))
          start = "9";
        else
          workS = workS[1..];
      }

      while (end == null)
      {
        if (char.IsNumber(workE[^1]))
          end = workE[^1].ToString();
        else if (workE.EndsWith("one"))
          end = "1";
        else if (workE.EndsWith("two"))
          end = "2";
        else if (workE.EndsWith("three"))
          end = "3";
        else if (workE.EndsWith("four"))
          end = "4";
        else if (workE.EndsWith("five"))
          end = "5";
        else if (workE.EndsWith("six"))
          end = "6";
        else if (workE.EndsWith("seven"))
          end = "7";
        else if (workE.EndsWith("eight"))
          end = "8";
        else if (workE.EndsWith("nine"))
          end = "9";
        else
          workE = workE[..^1];
      }

      numbers.Add(int.Parse(start + end));
    }

    Console.WriteLine(numbers.Sum());
  }
}