namespace Y2022;

public class Day7
{
  public static void Solve()
  {
    string[] input = File.ReadAllLines("./day7/input.txt");
    Dictionary<string, int> directorySizes = new Dictionary<string, int>();
    Stack<string> currentDir = new Stack<string>();

    foreach (string line in input)
    {
      string[] commands = line.Split(' ');
      if (commands[0] == "$")
      {
        if (commands[1] == "cd")
        {
          if (commands[2] == "..")
            currentDir.Pop();
          else if (commands[2] == "/")
            currentDir.Clear();
          else
            currentDir.Push(commands[2]);
        }
      }
      else if (int.TryParse(commands[0], out int size))
      {
        for (int i = 0; i <= currentDir.Count; i++)
        {
          string path = string.Join('/', currentDir.TakeLast(i));
          directorySizes[path] = directorySizes.GetValueOrDefault(path) + size;
        }
      }
    }

    Console.WriteLine(directorySizes.Where(entry => entry.Value <= 100000).Sum(entry => entry.Value)); // Part 1
    Console.WriteLine(directorySizes // Part 2
      .Where(entry => entry.Value > 30000000 - (70000000 - directorySizes[""]))
      .Min(entry => entry.Value)
    );
  }
}