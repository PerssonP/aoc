using System.Reflection;

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
    List<string> inputs = File.ReadAllLines("./inputs/day1.txt").ToList();
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
    List<string> inputs = File.ReadAllLines("./inputs/day2.txt").ToList();
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

  public static void Day3()
  {
    List<string> inputs = File.ReadAllLines("./inputs/day3.txt").ToList();

    int sumComps = 0, sumBadges = 0;
    List<string> work = new List<string>(3);
    foreach (string input in inputs)
    {
      work.Add(input);

      /* Part 1 */
      char sharedItem = input.Take(input.Length / 2).Intersect(input.Skip(input.Length / 2)).First();
      int prio = sharedItem >= 'a' ? sharedItem - 'a' + 1 : sharedItem - 'A' + 27;
      sumComps += prio;

      if (work.Count == 3)
      {
        /* Part 2 */
        sharedItem = work[0].Intersect(work[1]).Intersect(work[2]).First();
        prio = sharedItem >= 'a' ? sharedItem - 'a' + 1 : sharedItem - 'A' + 27;
        sumBadges += prio;
        work = new List<string>(3);
      }
    }

    Console.WriteLine(sumComps);
    Console.WriteLine(sumBadges);
  }

  public static void Day4()
  {
    List<string> inputs = File.ReadAllLines("./inputs/day4.txt").ToList();

    int fullyContainedCnt = 0;
    int anyOverlapCnt = 0;
    foreach (string input in inputs)
    {
      int[] elf1 = input.Split(',')[0].Split('-').Select(int.Parse).ToArray();
      int[] elf2 = input.Split(',')[1].Split('-').Select(int.Parse).ToArray();

      IEnumerable<int> elf1Range = Enumerable.Range(elf1[0], elf1[1] - elf1[0] + 1);
      IEnumerable<int> elf2Range = Enumerable.Range(elf2[0], elf2[1] - elf2[0] + 1);
      IEnumerable<int> intersect = elf1Range.Intersect(elf2Range);

      if (intersect.SequenceEqual(elf1Range) || intersect.SequenceEqual(elf2Range)) fullyContainedCnt++;
      if (intersect.Any()) anyOverlapCnt++;
    }

    Console.WriteLine(fullyContainedCnt);
    Console.WriteLine(anyOverlapCnt);
  }

  public static void Day5()
  {
    string[] inputs = File.ReadAllLines("./inputs/day5.txt");
    var initial = inputs.TakeWhile((input) => !string.IsNullOrEmpty(input));
    var instructions = inputs.SkipWhile((input) => !string.IsNullOrEmpty(input)).Skip(1);
    Stack<char>[] stacks = new Stack<char>[int.Parse(initial.Last()[^2].ToString())];
    for (int i = 0; i < stacks.Length; i++) stacks[i] = new Stack<char>();

    /* Set up initial stacks */
    foreach (string line in initial.Reverse().Skip(1))
    {
      for (int i = 1; i < line.Length; i += 4)
      {
        char ch = line[i];
        if (ch != ' ') stacks[i / 4].Push(ch);
      }
    }

    /* Part 1 */
    /*
    foreach (string line in instructions)
    {
      string[] instruction = line.Split(' ');
      int amount = int.Parse(instruction[1]), from = int.Parse(instruction[3]) - 1, to = int.Parse(instruction[5]) - 1;
      for (int i = 0; i < amount; i++) stacks[to].Push(stacks[from].Pop());
    }
    Console.WriteLine(stacks.Select(stack => stack.Pop()).ToArray());
    */

    /* Part 2 */
    foreach (string line in instructions)
    {
      string[] instruction = line.Split(' ');
      int amount = int.Parse(instruction[1]), from = int.Parse(instruction[3]) - 1, to = int.Parse(instruction[5]) - 1;
      Stack<char> inbetween = new Stack<char>();
      for (int i = 0; i < amount; i++) inbetween.Push(stacks[from].Pop());
      for (int i = 0; i < amount; i++) stacks[to].Push(inbetween.Pop());
    }
    Console.WriteLine(stacks.Select(stack => stack.Pop()).ToArray());
  }

  public static void Day6()
  {
    string input = File.ReadAllText("./inputs/day6.txt");

    char[] charsP1 = input.Take(4).ToArray();
    for (int i = 4; i < input.Length; i++)
    {
      charsP1[i % 4] = input[i];
      HashSet<char> uniques = new HashSet<char>();
      if (charsP1.All(uniques.Add)) // HashSet.Add() returns false if item cannot be added because it's not unique
      {
        Console.WriteLine(i + 1);
        break;
      }
    }

    char[] charsP2 = input.Take(14).ToArray();
    for (int i = 14; i < input.Length; i++)
    {
      charsP2[i % 14] = input[i];
      HashSet<char> uniques = new HashSet<char>();
      if (charsP2.All(uniques.Add))
      {
        Console.WriteLine(i + 1);
        break;
      }
    }
  }
}
