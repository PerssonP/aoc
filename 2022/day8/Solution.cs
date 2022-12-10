namespace Y2022;

public class Day8
{
    public static void Solve()
  {
    int[][] input = File.ReadAllLines("./day8/input.txt").Select(line => line.Select(c => int.Parse(c.ToString())).ToArray()).ToArray();

    // Part 1
    var visableTrees = new HashSet<(int, int)>();
    /* East - West */
    for (int i = 0; i < input.Length; i++)
    {
      int tallestLeft = -1, tallestRight = -1;
      for (int j = 0; j < input[0].Length; j++)
      {
        int treeLeft = input[i][j], treeRight = input[i][^(j + 1)];
        if (treeLeft > tallestLeft)
        {
          visableTrees.Add((i, j));
          tallestLeft = treeLeft;
        }
        if (treeRight > tallestRight)
        {
          visableTrees.Add((i, input[i].Length - (j + 1)));
          tallestRight = treeRight;
        }
      }
    }

    /* North - South */
    for (int i = 0; i < input[0].Length; i++)
    {
      int tallestUp = -1, tallestDown = -1;
      for (int j = 0; j < input.Length; j++)
      {
        int treeUp = input[j][i], treeDown = input[^(j + 1)][i];
        if (treeUp > tallestUp)
        {
          visableTrees.Add((j, i));
          tallestUp = treeUp;
        }
        if (treeDown > tallestDown)
        {
          visableTrees.Add((input.Length - (j + 1), i));
          tallestDown = treeDown;
        }
      }
    }

    Console.WriteLine(visableTrees.Count);

    // Part 2

    string[] input2 = File.ReadAllLines("./day8/input.txt");
    int[,] forest = new int[input2[0].Length, input2.Length];
    for (int i = 0; i < input2.Length; i++)
      for (int j = 0; j < input2[0].Length; j++)
        forest[j, i] = int.Parse(input2[i][j].ToString());

    List<int> scores = new();

    for (int x = 1; x < forest.GetLength(0); x++)
    {
      for (int y = 1; y < forest.GetLength(1); y++)
      {
        int tree = forest[x, y];
        int amountW = 0, amountE = 0, amountN = 0, amountS = 0;

        for (int k = x - 1; k >= 0; k--) // West
        {
          amountW++;
          if (tree <= forest[k, y]) break;
        }

        for (int k = x + 1; k < forest.GetLength(0); k++) // East
        {
          amountE++;
          if (tree <= forest[k, y]) break;
        }

        for (int k = y - 1; k >= 0; k--) // North
        {
          amountN++;
          if (tree <= forest[x, k]) break;
        }

        for (int k = y + 1; k < forest.GetLength(1); k++) // South
        {
          amountS++;
          if (tree <= forest[x, k]) break;
        }

        scores.Add(amountW * amountE * amountN * amountS);
      }
    }

    Console.WriteLine(scores.Max());
  }

}