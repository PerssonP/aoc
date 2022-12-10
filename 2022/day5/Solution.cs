namespace Y2022;

public class Day5
{
  public static void Solve()
  {
    string[] inputs = File.ReadAllLines("./day5/input.txt");
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
}