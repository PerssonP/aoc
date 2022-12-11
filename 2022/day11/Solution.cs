namespace Y2022;

public class Day11
{
  public static void Solve()
  {
    Console.WriteLine($"P1: {MonkeyBusiness(20)}");
    //Console.WriteLine($"P2: {MonkeyBusiness(10000, false)}");
  }

  private static long MonkeyBusiness(int amountRounds)
  {
    string[] input = File.ReadAllLines("./day11/input.txt");

    List<Monkey> monkeys = new();
    foreach (string[] instructions in input.Chunk(7))
    {
      monkeys.Add(new Monkey(
        id: int.Parse(instructions[0].Split(' ')[1][..^1].ToString()),
        items: instructions[1].Split(':')[1].Split(',').Select(long.Parse),
        operators: instructions[2].Split('=')[1].Trim().Split(' '),
        testDivisibleBy: int.Parse(instructions[3].Split(' ')[^1]),
        ifTrue: int.Parse(instructions[4].Split(' ')[^1]),
        ifFalse: int.Parse(instructions[5].Split(' ')[^1])
      ));
    }

    foreach (int i in Enumerable.Range(0, amountRounds))
    {
      foreach (Monkey monkey in monkeys)
      {
        foreach (int j in Enumerable.Range(0, monkey.ItemQueue.Count))
        {
          long item = monkey.ItemQueue.Dequeue();
          item = monkey.Operation(item);
          item /= 3;
          int monkeyToThrowTo = monkey.Test(item);
          monkeys[monkeyToThrowTo].ItemQueue.Enqueue(item);
        }
      }
    }

    monkeys.Sort((m1, m2) => (int)(m2.InspectedCount - m1.InspectedCount));
    return monkeys[0].InspectedCount * monkeys[1].InspectedCount;
  }

  private class Monkey
  {
    public int Id;
    public Queue<long> ItemQueue;
    public Func<long, long> Operation;
    public Func<long, int> Test;
    public long InspectedCount = 0;
    public Monkey(int id, IEnumerable<long> items, string[] operators, int testDivisibleBy, int ifTrue, int ifFalse)
    {
      Id = id;
      ItemQueue = new Queue<long>(items);
      Operation = old =>
      {
        InspectedCount++;
        long var1 = operators[0] == "old" ? old : long.Parse(operators[0]);
        long var2 = operators[2] == "old" ? old : long.Parse(operators[2]);

        return operators[1] switch
        {
          "+" => var1 + var2,
          "*" => var1 * var2,
          _ => throw new NotImplementedException(),
        };
      };
      Test = value => value % testDivisibleBy == 0 ? ifTrue : ifFalse;
    }
  }
}