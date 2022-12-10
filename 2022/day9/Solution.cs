namespace Y2022;

public class Day9
{
  public static void Solve()
  {
    Console.WriteLine($"P1: {PositionsVisitedByTail(1)}");
    Console.WriteLine($"P2: {PositionsVisitedByTail(9)}");
  }

  private static int PositionsVisitedByTail(int amountOfTails)
  {
    string[] input = File.ReadAllLines("./day9/input.txt");

    var headPos = (x: 0, y: 0);
    var tailsPos = Enumerable.Repeat((x: 0, y: 0), amountOfTails).ToList();
    HashSet<(int, int)> visitedPosSet = new HashSet<(int, int)> { tailsPos.Last() };

    foreach (string line in input)
    {
      char direction = line[0];
      int amount = int.Parse(line.Split(' ')[1]);
      Func<(int x, int y), (int, int)> moveHead = direction switch
      {
        'R' => pos => (pos.x + 1, pos.y),
        'L' => pos => (pos.x - 1, pos.y),
        'U' => pos => (pos.x, pos.y + 1),
        'D' => pos => (pos.x, pos.y - 1),
        _ => throw new Exception(),
      };

      foreach (int _ in Enumerable.Range(0, amount))
      {
        headPos = moveHead.Invoke(headPos);

        foreach (int i in Enumerable.Range(0, amountOfTails))
        {
          (int x, int y) tail = tailsPos[i];
          (int x, int y) fauxHead = i == 0 ? headPos : tailsPos[i - 1]; // Head is either the actual head or the previous tail

          int xDiff = Math.Abs(fauxHead.x - tail.x);
          int yDiff = Math.Abs(fauxHead.y - tail.y);

          if ((xDiff > 1 && yDiff > 0) || (xDiff > 0 && yDiff > 1))
          {
            // Tail needs to be moved diagonally
            tail = (tail.x > fauxHead.x ? tail.x - 1 : tail.x + 1, tail.y > fauxHead.y ? tail.y - 1 : tail.y + 1);
          }
          else if (xDiff > 1)
          {
            // Head is directly east or west
            tail = (tail.x > fauxHead.x ? tail.x - 1 : tail.x + 1, tail.y);
          }
          else if (yDiff > 1)
          {
            // Head is directly north or south
            tail = (tail.x, tail.y > fauxHead.y ? tail.y - 1 : tail.y + 1);
          }

          tailsPos[i] = tail;
        }

        visitedPosSet.Add(tailsPos.Last());

      }
    }

    return visitedPosSet.Count;
  }

}