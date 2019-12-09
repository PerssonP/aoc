﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace advent
{
  class Program
  {
    static void Main(string[] args)
    {
      day7();
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
      List<string> inputs = getInputs("./day2/input.txt");
      List<int> numbers = new List<string>(inputs[0].Split(',')).Select(x => int.Parse(x)).ToList();
      bool found = false;
      for (int i = 0; i < 100; i++)
      {
        for (int j = 0; j < 100; j++)
        {
          List<int> inputnew = new List<int>(numbers);
          inputnew[1] = i;
          inputnew[2] = j;
          int output = intcodes(inputnew);
          Console.WriteLine(output);
          if (output == 19690720)
          {
            Console.WriteLine("Answer found:");
            Console.WriteLine(i + " " + j);
            Console.WriteLine((100 * i) + j);
            found = true;
            break;
          }
        }
        if (found) break;
      }
    }
    static int intcodes(List<int> input)
    {
      for (int i = 0; i < input.Count; i += 4)
      {
        int op = input[i];
        if (op == 99)
        {
          break;
        }
        int input1 = input[input[i + 1]];
        int input2 = input[input[i + 2]];
        int target = input[i + 3];
        if (op == 1)
        {
          input[target] = input1 + input2;
        }
        else if (op == 2)
        {
          input[target] = input1 * input2;
        }
      }
      return input[0];
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
      List<string> inputs = getInputs("./day5/input.txt");
      List<int> numbers = new List<string>(inputs[0].Split(',')).Select(x => int.Parse(x)).ToList();
      intcodes2(numbers);
    }

    static void intcodes2(List<int> input)
    {
      int i = 0;
      while(i < input.Count)
      {
        int[] instruction = input[i].ToString().PadLeft(5, '0').Select(c => (int)char.GetNumericValue(c)).ToArray();
        string modeThird = instruction[0] == 0 ? "pos" : "imm";
        string modeSecond = instruction[1] == 0 ? "pos" : "imm";
        string modeFirst = instruction[2] == 0 ? "pos" : "imm";
        int op = int.Parse(string.Concat(instruction[3], instruction[4]));

        int input1, input2, target;
        switch (op)
        {
          case 1: // addition
            input1 = modeFirst == "pos" ? input[input[i + 1]] : input[i + 1];
            input2 = modeSecond == "pos" ? input[input[i + 2]] : input[i + 2];
            target = modeThird == "pos" ? input[i + 3] : throw new Exception("Target should never be in immediate mode");
            input[target] = input1 + input2;
            i += 4;
            break;
          case 2: // multiplication
            input1 = modeFirst == "pos" ? input[input[i + 1]] : input[i + 1];
            input2 = modeSecond == "pos" ? input[input[i + 2]] : input[i + 2];
            target = modeThird == "pos" ? input[i + 3] : throw new Exception("Target should never be in immediate mode");
            input[target] = input1 * input2;
            i += 4;
            break;
          case 3: // read
            Console.Write("Enter input:");
            int consoleInput;
            if(!int.TryParse(Console.ReadLine(), out consoleInput)) throw new Exception("Invalid input, should be integer");
            target = modeFirst == "pos" ? input[i + 1] : throw new Exception("Target should never be in immediate mode");
            input[target] = consoleInput;
            i += 2;
            break;
          case 4: // write
            input1 = modeFirst == "pos" ? input[input[i + 1]] : input[i + 1];
            Console.WriteLine($"Output: {input1}");
            i += 2;
            break;
          case 5: // jump-if-true
            input1 = modeFirst == "pos" ? input[input[i + 1]] : input[i + 1];
            input2 = modeSecond == "pos" ? input[input[i + 2]] : input[i + 2];
            if (input1 != 0)
            {
              i = input2;
            }
            else
            {
              i += 3;
            }
            break;
          case 6: // jump-if-false
            input1 = modeFirst == "pos" ? input[input[i + 1]] : input[i + 1];
            input2 = modeSecond == "pos" ? input[input[i + 2]] : input[i + 2];
            if (input1 == 0)
            {
              i = input2;
            }
            else
            {
              i += 3;
            }
            break;
          case 7: // less than
            input1 = modeFirst == "pos" ? input[input[i + 1]] : input[i + 1];
            input2 = modeSecond == "pos" ? input[input[i + 2]] : input[i + 2];
            target = modeThird == "pos" ? input[i + 3] : throw new Exception("Target should never be in immediate mode");
            if (input1 < input2)
            {
              input[target] = 1;
            }
            else
            {
              input[target] = 0;
            }
            i += 4;
            break;
          case 8: // equals
            input1 = modeFirst == "pos" ? input[input[i + 1]] : input[i + 1];
            input2 = modeSecond == "pos" ? input[input[i + 2]] : input[i + 2];
            target = modeThird == "pos" ? input[i + 3] : throw new Exception("Target should never be in immediate mode");
            if (input1 == input2)
            {
              input[target] = 1;
            }
            else
            {
              input[target] = 0;
            }
            i += 4;
            break;
          case 99:
            Console.WriteLine("Halting...");
            return;
          default:
            throw new Exception("Invalid op-code");
        }
      }
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
      List<int> inputs = getInputs("./day7/input.txt")[0].Split(',').Select(x => int.Parse(x)).ToList();

      IEnumerable<int[]> settings = GetPermutations(new int[]{ 0, 1, 2, 3, 4 }, 5).Select(x => x.ToArray());
      List<int> results = new List<int>();
      foreach (int[] setting in settings)
      {
        int output1 = (int)intcodes3(inputs, new int[] { setting[0], 0 }, true);
        int output2 = (int)intcodes3(inputs, new int[] { setting[1], output1 }, true);
        int output3 = (int)intcodes3(inputs, new int[] { setting[2], output2 }, true);
        int output4 = (int)intcodes3(inputs, new int[] { setting[3], output3 }, true);
        int output5 = (int)intcodes3(inputs, new int[] { setting[4], output4 }, true);
        results.Add(output5);
      }
      Console.WriteLine($"Result: {results.Max()}");
    }

    static IEnumerable<IEnumerable<int>> GetPermutations(int[] list, int length)
    {
      if (length == 1) return list.Select(i => new int[] { i });

      return GetPermutations(list, length - 1)
          .SelectMany(x => list.Where(e => !x.Contains(e)),
              (x1, x2) => x1.Concat(new int[] { x2 }));
    }

    static int? intcodes3(List<int> instructions, int[] inputs, bool returnOnFirstOutput)
    {
      int read = 0;
      int i = 0;
      while (i < instructions.Count)
      {
        int[] instruction = instructions[i].ToString().PadLeft(5, '0').Select(c => (int)char.GetNumericValue(c)).ToArray();
        string modeThird = instruction[0] == 0 ? "pos" : "imm";
        string modeSecond = instruction[1] == 0 ? "pos" : "imm";
        string modeFirst = instruction[2] == 0 ? "pos" : "imm";
        int op = int.Parse(string.Concat(instruction[3], instruction[4]));

        int input1, input2, target;
        switch (op)
        {
          case 1: // addition
            input1 = modeFirst == "pos" ? instructions[instructions[i + 1]] : instructions[i + 1];
            input2 = modeSecond == "pos" ? instructions[instructions[i + 2]] : instructions[i + 2];
            target = modeThird == "pos" ? instructions[i + 3] : throw new Exception("Target should never be in immediate mode");
            instructions[target] = input1 + input2;
            i += 4;
            break;
          case 2: // multiplication
            input1 = modeFirst == "pos" ? instructions[instructions[i + 1]] : instructions[i + 1];
            input2 = modeSecond == "pos" ? instructions[instructions[i + 2]] : instructions[i + 2];
            target = modeThird == "pos" ? instructions[i + 3] : throw new Exception("Target should never be in immediate mode");
            instructions[target] = input1 * input2;
            i += 4;
            break;
          case 3: // read
            int consoleInput;
            if (inputs != null && read < inputs.Length)
            {
              consoleInput = inputs[read];
              read++;  
            }
            else
            {
              Console.Write("Enter input:");
              if (!int.TryParse(Console.ReadLine(), out consoleInput)) throw new Exception("Invalid input, should be integer");
            }
            target = modeFirst == "pos" ? instructions[i + 1] : throw new Exception("Target should never be in immediate mode");
            instructions[target] = consoleInput;
            i += 2;
            break;
          case 4: // write
            input1 = modeFirst == "pos" ? instructions[instructions[i + 1]] : instructions[i + 1];
            if (returnOnFirstOutput)
            {
              return input1;
            }
            else
            {
              Console.WriteLine($"Output: {input1}");
            }
            i += 2;
            break;
          case 5: // jump-if-true
            input1 = modeFirst == "pos" ? instructions[instructions[i + 1]] : instructions[i + 1];
            input2 = modeSecond == "pos" ? instructions[instructions[i + 2]] : instructions[i + 2];
            if (input1 != 0)
            {
              i = input2;
            }
            else
            {
              i += 3;
            }
            break;
          case 6: // jump-if-false
            input1 = modeFirst == "pos" ? instructions[instructions[i + 1]] : instructions[i + 1];
            input2 = modeSecond == "pos" ? instructions[instructions[i + 2]] : instructions[i + 2];
            if (input1 == 0)
            {
              i = input2;
            }
            else
            {
              i += 3;
            }
            break;
          case 7: // less than
            input1 = modeFirst == "pos" ? instructions[instructions[i + 1]] : instructions[i + 1];
            input2 = modeSecond == "pos" ? instructions[instructions[i + 2]] : instructions[i + 2];
            target = modeThird == "pos" ? instructions[i + 3] : throw new Exception("Target should never be in immediate mode");
            if (input1 < input2)
            {
              instructions[target] = 1;
            }
            else
            {
              instructions[target] = 0;
            }
            i += 4;
            break;
          case 8: // equals
            input1 = modeFirst == "pos" ? instructions[instructions[i + 1]] : instructions[i + 1];
            input2 = modeSecond == "pos" ? instructions[instructions[i + 2]] : instructions[i + 2];
            target = modeThird == "pos" ? instructions[i + 3] : throw new Exception("Target should never be in immediate mode");
            if (input1 == input2)
            {
              instructions[target] = 1;
            }
            else
            {
              instructions[target] = 0;
            }
            i += 4;
            break;
          case 99:
            Console.WriteLine("Halting...");
            return null;
          default:
            throw new Exception($"Invalid op-code: {op}");
        }
      }
      return null;
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
