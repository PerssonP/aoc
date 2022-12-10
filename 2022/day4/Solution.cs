namespace Y2022;

public class Day4
{
  public static void Solve()
  {
    List<string> inputs = File.ReadAllLines("./day4/input.txt").ToList();

    int fullyContainedCnt = 0;
    int anyOverlapCnt = 0;
    foreach (string input in inputs)
    {
      int[] elf1 = input.Split(',')[0].Split('-').Select(int.Parse).ToArray();
      int[] elf2 = input.Split(',')[1].Split('-').Select(int.Parse).ToArray();

      IEnumerable<int> elf1Range = Enumerable.Range(elf1[0], elf1[1] - elf1[0] + 1);
      IEnumerable<int> elf2Range = Enumerable.Range(elf2[0], elf2[1] - elf2[0] + 1);
      IEnumerable<int> intersect = elf1Range.Intersect(elf2Range);

      if (intersect.SequenceEqual(elf1Range) || intersect.SequenceEqual(elf2Range)) fullyContainedCnt++;
      if (intersect.Any()) anyOverlapCnt++;
    }

    Console.WriteLine(fullyContainedCnt);
    Console.WriteLine(anyOverlapCnt);
  }
}