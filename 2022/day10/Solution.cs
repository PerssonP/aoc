namespace Y2022;

public class Day10
{
  public static void Solve()
  {
    using StreamReader sr = new("./day10/input.txt");

    int cycle = 0, x = 1;
    bool working = false;
    (int cycle, int value) workingAdd = (0, 0);
    int signalStrengthSum = 0;
    while (!sr.EndOfStream || working)
    {
      if (!working) // Evaluate next command if not working
      {
        string[] command = sr.ReadLine()!.Split(' ');
        switch (command[0])
        {
          case "noop":
            break;
          case "addx":
            workingAdd = (cycle + 2, int.Parse(command[1]));
            working = true;
            break;
          default:
            throw new Exception($"Unsupported command {command[0]}");
        }
      }

      if ((cycle + 21) % 40 == 0) // Part 1
      {
        int signal = x * (cycle + 1);
        signalStrengthSum += x * (cycle + 1);
      }

      // Part 2  
      if (Enumerable.Range(x % 40 - 1, 3).Any(pos => pos == (cycle % 40)))
        Console.Write('#');
      else
        Console.Write('.');

      if ((cycle + 1) % 40 == 0)
        Console.WriteLine();

      cycle++; // Cycle finished, increase cycle count

      if (working && workingAdd.cycle == cycle)
      {
        x += workingAdd.value;
        working = false;
      }
    }

    Console.WriteLine(signalStrengthSum);
  }
}