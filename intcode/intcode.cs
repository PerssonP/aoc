using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Intcodes
{
  class IntcodeMachine
  {
    private int id;
    private bool verbose;
    private bool halted = false;
    private long cnt = 0;
    public long[] memory;
    public Queue<long> input = new Queue<long>();
    public Queue<long> output = new Queue<long>();
    private long relBase = 0;

    public IntcodeMachine(long[] input, int id = 0, bool verbose = false)
    {
      memory = input.ToArray(); //Copying input-array
      this.id = id;
      this.verbose = verbose;
    }

    public void runToEnd()
    {
      bool incomplete = true;
      while (incomplete)
      {
        incomplete = this.step();
      }
    }

    // Number == which numbered input. E.g, 1 == input1, 2 == input2
    private long getParam(long mode, int number)
    {
      if (verbose) Console.WriteLine($"Getting parameter. Mode: {mode}, number: {number}");
      long pos;
      switch (mode)
      {
        case 0: // pos
          pos = memory[cnt + number];
          break;
        case 1: // imm
          pos = cnt + number;
          break;
        case 2: // rel
          pos = relBase + memory[cnt + number];
          break;
        default:
          throw new Exception("Invalid mode");
      }
      if (memory.Length <= pos) return 0; // Getting param out of range of memory
      return memory[pos];
    }

    private void setMemory(long mode, int number, long value)
    {
      if (verbose) Console.WriteLine($"Setting value: {value} at target position. Mode: {mode}, number: {number}");
      long targetPos;
      switch (mode)
      {
        case 0: // pos
          targetPos = memory[cnt + number];
          break;
        case 1: // imm
          throw new Exception("Target should never be in immidiate mode");
        case 2: // rel
          targetPos = relBase + memory[cnt + number];
          break;
        default:
          throw new Exception("Invalid mode");
      }
      if (memory.Length <= targetPos) // Extend memory
      {
        long[] temp = new long[targetPos + 1];
        Array.Copy(memory, temp, memory.Length);
        memory = temp;
      }
      memory[targetPos] = value;
    }

    // Returns true while incomplete. Returns false if halted.
    public bool step()
    {
      if (halted) return false;
      if (verbose) Console.WriteLine($"Machine #{id}: Running instruction #{cnt}");
      long[] instruction = memory[cnt].ToString().PadLeft(5, '0').Select(c => (long)char.GetNumericValue(c)).ToArray();
      long modeThird = instruction[0];
      long modeSecond = instruction[1];
      long modeFirst = instruction[2];

      int op = int.Parse(string.Concat(instruction[3], instruction[4]));
      long param1, param2;
      switch (op)
      {
        case 1: // addition
          param1 = getParam(modeFirst, 1);
          param2 = getParam(modeSecond, 2);
          setMemory(modeThird, 3, param1 + param2);
          cnt += 4;
          break;
        case 2: // multiplication
          param1 = getParam(modeFirst, 1);
          param2 = getParam(modeSecond, 2);
          setMemory(modeThird, 3, param1 * param2);
          cnt += 4;
          break;
        case 3: // read
          long consoleInput;
          if (input.Count > 0)
          {
            consoleInput = input.Dequeue();
            setMemory(modeFirst, 1, consoleInput);
            cnt += 2;
          }
          else
          {
            if (verbose) Console.WriteLine($"Machine #{id}: Waiting for input");
          }
          break;
        case 4: // write
          param1 = getParam(modeFirst, 1);
          output.Enqueue(param1);
          cnt += 2;
          break;
        case 5: // jump-if-true
          param1 = getParam(modeFirst, 1);
          param2 = getParam(modeSecond, 2);
          if (param1 != 0)
          {
            cnt = param2;
          }
          else
          {
            cnt += 3;
          }
          break;
        case 6: // jump-if-false
          param1 = getParam(modeFirst, 1);
          param2 = getParam(modeSecond, 2);
          if (param1 == 0)
          {
            cnt = param2;
          }
          else
          {
            cnt += 3;
          }
          break;
        case 7: // less than
          param1 = getParam(modeFirst, 1);
          param2 = getParam(modeSecond, 2);
          if (param1 < param2)
          {
            setMemory(modeThird, 3, 1);
          }
          else
          {
            setMemory(modeThird, 3, 0);
          }
          cnt += 4;
          break;
        case 8: // equals
          param1 = getParam(modeFirst, 1);
          param2 = getParam(modeSecond, 2);
          if (param1 == param2)
          {
            setMemory(modeThird, 3, 1);
          }
          else
          {
            setMemory(modeThird, 3, 0);
          }
          cnt += 4;
          break;
        case 9: // alter relative base
          param1 = getParam(modeFirst, 1);
          relBase += param1;
          cnt += 2;
          break;
        case 99: // halt
          if (verbose) Console.WriteLine($"Machine #{id}: Halting...");
          halted = true;
          return false;
        default:
          throw new Exception($"Invalid op-code: {op}");
      }
      return true;
    }

    public void printOutput()
    {  
      foreach (long output in output)
      {
        Console.WriteLine($"Output: {output}");
      }
    }
  }
}
