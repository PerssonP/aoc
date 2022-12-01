using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

using Intcodes;

namespace advent
{
  class Program
  {
    static void Main(string[] args)
    {
      day9();
    }

    static void day1()
    {
      List<string> inputs = getInputs("./day1/input.txt");
      List<int> modules = inputs.Select(x => int.Parse(x)).ToList();
      int result = 0;
      foreach (int module in modules)
      {
        bool done = false;
        int curr = module;
        while (!done)
        {
          int fuel = (curr / 3) - 2;
          if (fuel < 1)
          {
            done = true;
          }
          else
          {
            result += fuel;
            curr = fuel;
          }
        }
      }
      Console.WriteLine(result);
    }

    static void day2()
    {
      long[] inputs = getInputs("./day2/input.txt")[0].Split(',').Select(x => long.Parse(x)).ToArray();

      inputs[1] = 12;
      inputs[2] = 2;
      IntcodeMachine machine = new IntcodeMachine(inputs);
      machine.runToEnd();
      Console.WriteLine($"Part 1: {machine.memory[0]}"); // Part 1. Answer: 7210630

      bool found = false;
      for (int i = 0; i < 100; i++)
      {
        for (int j = 0; j < 100; j++)
        {
          inputs[1] = i;
          inputs[2] = j;
          machine = new IntcodeMachine(inputs);
          machine.runToEnd();
          long output = machine.memory[0];
          if (output == 19690720)
          {
            Console.WriteLine("Answer found:");
            Console.WriteLine(i + " " + j);
            Console.WriteLine((100 * i) + j); // Part 2. Answer: 3892
            found = true;
            break;
          }
        }
        if (found) break;
      }
    }

    static void day3()
    {
      List<string> inputs = getInputs("./day3/input.txt");
      List<string> wire1 = new List<string>(inputs[0].Split(','));
      List<string> wire2 = new List<string>(inputs[1].Split(','));
      var coords1 = getCoordinates(wire1);
      var coords2 = getCoordinates(wire2);
      var intersections = coords1.Intersect(coords2);
      /* Part 1 result
      List<int> distances = new List<int>();
      foreach (var cross in intersections)
        distances.Add(Math.Abs(cross.x) + Math.Abs(cross.y));
      Console.WriteLine(distances.Min());
      */
      List<int> costs = new List<int>(intersections.Count());
      foreach (var cross in intersections)
      {
        int a = coords1.FindIndex(c => c.x == cross.x && c.y == cross.y);
        int b = coords2.FindIndex(c => c.x == cross.x && c.y == cross.y);
        costs.Add(a + b);
      }
      Console.WriteLine(costs.Min() + 2);
    }

    static List<(int x, int y)> getCoordinates(List<string> wire)
    {
      List<(int x, int y)> coordinates = new List<(int x, int y)>();
      (int x, int y) prev = (x: 0, y: 0);
      foreach(string instruction in wire)
      {
        char direction = instruction[0];
        int length = int.Parse(instruction.Substring(1));
        switch (direction)
        {
          case 'L':
            for (int i = prev.x - 1; i >= prev.x - length; i--)
              coordinates.Add((i, prev.y));
            break;
          case 'R':
            for (int i = prev.x + 1; i <= prev.x + length; i++)
              coordinates.Add((i, prev.y));
            break;
          case 'U':
            for (int i = prev.y + 1; i <= prev.y + length; i++)
              coordinates.Add((prev.x, i));
            break;
          case 'D':
            for (int i = prev.y - 1; i >= prev.y - length; i--)
              coordinates.Add((prev.x, i));
            break;
          default:
            throw new Exception("Uhoh.");
        }
        prev = coordinates.Last();
      }
      return coordinates;
    }

    static void day4()
    {
      // Input:
      // 307237-769058
      IEnumerable<int> input = Enumerable.Range(307237, 769058 - 307237);
      List<int> possibles = new List<int>();
      foreach (int test in input)
      {
        int[] ints = test.ToString().Select(c => (int) char.GetNumericValue(c)).ToArray();
        if (ints[0] == ints[1]
          || ints[1] == ints[2]
          || ints[2] == ints[3]
          || ints[3] == ints[4]
          || ints[4] == ints[5])
        {
          if (ints[0] <= ints[1]
            && ints[1] <= ints[2]
            && ints[2] <= ints[3]
            && ints[3] <= ints[4]
            && ints[4] <= ints[5])
          {
            possibles.Add(test);
          }
        }
      }
      Console.WriteLine(possibles.Count()); // Part 1 result
      List<int> newPossibles = new List<int>();
      foreach (int test in possibles)
      {
        int[] ints = test.ToString().Select(c => (int) char.GetNumericValue(c)).ToArray();
        var grouped = ints.GroupBy(x => x);
        bool bad = false;
        foreach (var group in grouped)
        {
          if (group.Count() > 2) 
          {
            bad = true;
          }
          else if (group.Count() == 2)
          {
            bad = false;
            break;
          }
        }
        if (!bad) newPossibles.Add(test);
      }
      Console.WriteLine(newPossibles.Count()); // Part 2 result
    }

    static void day5()
    {
      long[] inputs = getInputs("./day5/input.txt")[0].Split(',').Select(x => long.Parse(x)).ToArray();
      IntcodeMachine machine = new IntcodeMachine(inputs);
      machine.input.Enqueue(1);
      machine.runToEnd();
      machine.printOutput(); // Part 1. Answer: 7988899

      machine = new IntcodeMachine(inputs);
      machine.input.Enqueue(5);
      machine.runToEnd();
      machine.printOutput(); // Part 2. Answer: 13758663
    }

    static void day6()
    {
      Dictionary<string, string> orbits = getInputs("./day6/input.txt").Select(x => x.Split(')')).ToDictionary(orbit => orbit[1], orbit => orbit[0]);

      int sum = 0;
      foreach (string key in orbits.Keys)
      {
        sum += getAncestors(orbits, key).Count;
      }
      Console.WriteLine(sum); // Part 1

      Stack<string> ancestorsYOU = new Stack<string>(getAncestors(orbits, "YOU"));
      Stack<string> ancestorsSAN = new Stack<string>(getAncestors(orbits, "SAN"));
      while (ancestorsYOU.Peek() == ancestorsSAN.Peek())
      {
        ancestorsYOU.Pop();
        ancestorsSAN.Pop();
      }
      Console.WriteLine(ancestorsYOU.Count + ancestorsSAN.Count); // Part 2
    }

    static List<string> getAncestors(Dictionary<string, string> orbits, string start)
    {
      List<string> list = new List<string>();
      string parent = orbits.GetValueOrDefault(start, null);
      while(parent != null)
      {
        list.Add(parent);
        parent = orbits.GetValueOrDefault(parent, null);
      }
      return list;
    }

    static void day7()
    {
      long[] inputs = getInputs("./day7/input.txt")[0].Split(',').Select(x => long.Parse(x)).ToArray();

      IEnumerable<int[]> settings = GetPermutations(new int[] { 0, 1, 2, 3, 4 }, 5).Select(x => x.ToArray());
      List<long> results = new List<long>();
      foreach (int[] setting in settings)
      {
        IntcodeMachine[] comps = Enumerable.Range(0, 5).Select(x => new IntcodeMachine(inputs, x + 1, false)).ToArray();

        for(int i = 0; i < comps.Count(); i++)
        {
          comps[i].input.Enqueue(setting[i]);
        }
        long input = 0;
        foreach (IntcodeMachine comp in comps)
        {
          comp.input.Enqueue(input);
          comp.runToEnd();
          input = comp.output.Dequeue();
        }
        
        results.Add(input);
      }
      Console.WriteLine($"Part 1 result: {results.Max()}"); // Part 1. Answer: 117312

      settings = GetPermutations(new int[] { 5, 6, 7, 8, 9 }, 5).Select(x => x.ToArray());
      results = new List<long>();
      foreach (int[] setting in settings)
      {
        IntcodeMachine[] comps = Enumerable.Range(0, 5).Select(x => new IntcodeMachine(inputs, x + 1, false)).ToArray();

        for (int i = 0; i < comps.Count(); i++)
        {
          comps[i].input = comps[i - 1 < 0 ? comps.Count() - 1 : i - 1].output;
          comps[i].input.Enqueue(setting[i]);
        }

        comps[0].input.Enqueue(0);

        bool incomplete = true;
        while (incomplete)
        {
          incomplete = false;
          foreach (IntcodeMachine comp in comps) incomplete |= comp.step();
        }

        results.Add(comps[comps.Count() - 1].output.Dequeue());
      }
      Console.WriteLine($"Part 2 result: {results.Max()}"); // Part 2. Answer: 1336480
    }

    static IEnumerable<IEnumerable<int>> GetPermutations(int[] list, int length)
    {
      if (length == 1) return list.Select(i => new int[] { i });

      return GetPermutations(list, length - 1)
          .SelectMany(x => list.Where(e => !x.Contains(e)),
              (x1, x2) => x1.Concat(new int[] { x2 }));
    }

    static void day8()
    {
      IEnumerable<int> inputs = getInputs("./day8/input.txt")[0].ToCharArray().Select(x => (int)Char.GetNumericValue(x)).ToList();
      IEnumerator<int> enumerator = inputs.GetEnumerator();
      List<(int[,] map, int zeroes)> layers = new List<(int[,], int)>();
      bool hasNext = enumerator.MoveNext();

      while (hasNext)
      {
        int[,] map = new int[25, 6];
        int size = map.GetLength(0) * map.GetLength(1);
        int x = 0, y = 0;
        int zeroes = 0;
        for (int i = 0; i < size; i++)
        {
          map[x, y] = enumerator.Current;
          if (enumerator.Current == 0) zeroes++;
          hasNext = enumerator.MoveNext();
          x++;
          if (x == 25)
          {
            x = 0;
            y++;
          }
        }
        layers.Add((map, zeroes));
      }

      var a = layers.Min(x => x.zeroes);
      var minZeroes = layers.SingleOrDefault(x => x.zeroes == layers.Min(x => x.zeroes));
      int countOnes = 0;
      int countTwos = 0;
      for (int i = 0; i < minZeroes.map.GetLength(1); i++)
      {
        for (int j = 0; j < minZeroes.map.GetLength(0); j++)
        {
          if (minZeroes.map[j, i] == 1) countOnes++;
          if (minZeroes.map[j, i] == 2) countTwos++;
        }
      }
      Console.WriteLine(countOnes * countTwos); // Part 1
      
      int[,] final = new int[25, 6];
      for (int count = layers.Count - 1; count >= 0; count--)
      {
        var layer = layers[count];
        int x = 0, y = 0;
        for (int i = 0; i < 150; i++)
        {
          int value = layer.map[x, y];
          if (value != 2) final[x, y] = value;
          x++;
          if (x == 25)
          {
            x = 0;
            y++;
          }
        }
      }

      for (int i = 0; i < 6; i++)
      {
        for (int j = 0; j < 25; j++)
        {
          if (final[j, i] == 0)
          {
            Console.Write(' ');
          }
          else
          {
            Console.Write(final[j, i]);
          }
        }
        Console.WriteLine();
      }
    }

    static void day9()
    {
      long[] inputs = getInputs("./day9/input.txt")[0].Split(',').Select(x => long.Parse(x)).ToArray();
      IntcodeMachine machine = new IntcodeMachine(inputs);
      machine.input.Enqueue(1);
      machine.runToEnd();
      machine.printOutput(); // Part 1. Answer: 2171728567 

      machine = new IntcodeMachine(inputs);
      machine.input.Enqueue(2);
      machine.runToEnd();
      machine.printOutput(); // Part2. Answer: 49815
    }

    static List<string> getInputs(string path)
    {
      List<string> inputs = new List<string>();
      try
      {
        using (StreamReader sr = new StreamReader(path))
        {
          string line;
          while((line = sr.ReadLine()) != null)
          {
            inputs.Add(line);
          }
        }
      }
      catch (IOException e)
      {
        Console.WriteLine("The file could not be read:");
        Console.WriteLine(e.Message);
      }
      return inputs;
    }
  }
}
