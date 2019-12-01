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
      day1();
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
