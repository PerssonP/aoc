namespace Y2022;

public class Day3
{
  public static void Solve()
  {
    List<string> inputs = File.ReadAllLines("./day3/input.txt").ToList();

    int sumComps = 0, sumBadges = 0;
    List<string> work = new List<string>(3);
    foreach (string input in inputs)
    {
      work.Add(input);

      /* Part 1 */
      char sharedItem = input.Take(input.Length / 2).Intersect(input.Skip(input.Length / 2)).First();
      int prio = sharedItem >= 'a' ? sharedItem - 'a' + 1 : sharedItem - 'A' + 27;
      sumComps += prio;

      if (work.Count == 3)
      {
        /* Part 2 */
        sharedItem = work[0].Intersect(work[1]).Intersect(work[2]).First();
        prio = sharedItem >= 'a' ? sharedItem - 'a' + 1 : sharedItem - 'A' + 27;
        sumBadges += prio;
        work = new List<string>(3);
      }
    }

    Console.WriteLine(sumComps);
    Console.WriteLine(sumBadges);
  }
}