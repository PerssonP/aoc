using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace advent
{
  class Program
  {
    static void Main(string[] args)
    {
      day2();
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
