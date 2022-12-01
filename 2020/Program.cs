using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace aoc2020
{
  class Program
  {
    static void Main(string[] args)
    {
      Day2();
    }


    static void Day1()
    {
      List<int> inputs = GetInputs("./inputs/day1").Select(x => int.Parse(x)).ToList();

      // Part 1
      bool found = false;
      var enum1 = inputs.GetEnumerator();
      var enum2 = inputs.GetEnumerator();
      while (!found && enum1.MoveNext())
      {
        while (!found && enum2.MoveNext())
        {
          if (enum1.Current + enum2.Current == 2020)
          {
            found = true;
            Console.WriteLine(enum1.Current * enum2.Current);
          }
        }
        enum2 = inputs.GetEnumerator();
      }

      // Part 2
      found = false;
      enum1 = inputs.GetEnumerator();
      enum2 = inputs.GetEnumerator();
      var enum3 = inputs.GetEnumerator();
      while (!found && enum1.MoveNext())
      {
        while (!found && enum2.MoveNext())
        {
          while (!found && enum3.MoveNext())
          {
            if (enum1.Current + enum2.Current + enum3.Current == 2020)
            {
              found = true;
              Console.WriteLine(enum1.Current * enum2.Current * enum3.Current);
            }
          }
          enum3 = inputs.GetEnumerator();
        }
        enum2 = inputs.GetEnumerator();
      }
    }

    static void Day2()
    {
      List<string> inputs = GetInputs("./inputs/day2");

      int cntValid1 = 0, cntValid2 = 0;
      foreach (string input in inputs)
      {
        int lower = int.Parse(input.Substring(0, input.IndexOf('-')));
        int upper = int.Parse(input.Substring(input.IndexOf('-') + 1, 2));
        char validityChar = input[input.IndexOf(' ') + 1];
        string password = input[(input.LastIndexOf(' ') + 1)..];

        // Part 1
        int count = password.Count(c => c == validityChar);
        if (count >= lower && count <= upper) cntValid1++;

        // Part 2
        if (password[lower - 1] == validityChar ^ password[upper - 1] == validityChar) cntValid2++;
      }
      Console.WriteLine(cntValid1);
      Console.WriteLine(cntValid2);
    }

    static List<string> GetInputs(string path)
    {
      List<string> inputs = new List<string>();
      try
      {
        using StreamReader sr = new StreamReader(path);
        string line;
        while ((line = sr.ReadLine()) != null)
        {
          inputs.Add(line);
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
