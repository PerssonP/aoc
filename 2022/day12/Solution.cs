namespace Y2022;

public class Day12
{
  public static void Solve()
  {
    string[] input = File.ReadAllLines("./day12/input.txt");
    Dictionary<(int x, int y), Node> nodes = new();
    for (int i = 0; i < input[0].Length; i++)
      for (int j = 0; j < input.Length; j++)
        nodes.Add((i, j), new Node() { x = i, y = j, c = input[j][i], distance = int.MaxValue });

    Node startingNode = nodes.Values.First(node => node.c == 'S');
    Node goal = nodes.Values.First(node => node.c == 'E');
    startingNode.c = 'a';
    goal.c = 'z';
    Console.WriteLine($"Part 1: {Dijkstra(nodes, startingNode, goal)}");

    int min = nodes.Values.Where(node => node.c == 'a').Select(testNode =>
      {
        foreach (Node node in nodes.Values)
        {
          node.distance = int.MaxValue;
          node.prev = null;
        }
        return Dijkstra(nodes, testNode, goal);
      })
      .Where(i => i >= 0)
      .Min();

    Console.WriteLine($"Part 2: {min}");
  }


  public static int Dijkstra(Dictionary<(int x, int y), Node> nodes, Node startingNode, Node goal)
  {
    HashSet<Node> unvisitedSet = nodes.Values.ToHashSet();
    Node currentNode = startingNode;
    currentNode.distance = 0;

    bool targetVisited = false;
    while (!targetVisited)
    {
      var neighborCoords = new (int, int)[]
      {
        (currentNode.x, currentNode.y - 1),
        (currentNode.x + 1, currentNode.y),
        (currentNode.x, currentNode.y + 1),
        (currentNode.x - 1, currentNode.y)
      };

      foreach ((int x, int y) in neighborCoords)
      {
        if (nodes.TryGetValue((x, y), out Node? node) && unvisitedSet.Contains(node) && node.c <= currentNode.c + 1)
        {
          int newDistance = currentNode.distance + 1;
          if (newDistance < node.distance)
          {
            node.distance = newDistance;
            node.prev = currentNode;
          }
        }
      }

      unvisitedSet.Remove(currentNode);
      if (currentNode == goal)
      {
        targetVisited = true;
      }
      else
      {
        Node nextNode = unvisitedSet.Min()!;
        if (nextNode.distance == int.MaxValue) return -1;
        currentNode = nextNode;
      }
    }

    int steps = 0;
    currentNode = goal;
    while (currentNode.prev != null)
    {
      steps++;
      currentNode = currentNode.prev;
    }

    return steps;
  }

  public class Node : IComparable<Node>
  {
    public int x;
    public int y;
    public char c;
    public int distance;
    public Node? prev;

    public int CompareTo(Node? other)
    {
      if (other == null) return 1;
      return distance - other.distance;
    }
  }
}