namespace Y2022;

public class Day6
{
 public static void Solve()
  {
    string input = File.ReadAllText("./day6/input.txt");

    char[] charsP1 = input.Take(4).ToArray();
    for (int i = 4; i < input.Length; i++)
    {
      HashSet<char> uniques = new HashSet<char>();
      if (charsP1.All(uniques.Add)) // HashSet.Add() returns false if item cannot be added because it's not unique
      {
        Console.WriteLine(i);
        break;
      }
      charsP1[i % 4] = input[i];
    }

    char[] charsP2 = input.Take(14).ToArray();
    for (int i = 14; i < input.Length; i++)
    {
      HashSet<char> uniques = new HashSet<char>();
      if (charsP2.All(uniques.Add))
      {
        Console.WriteLine(i);
        break;
      }
      charsP2[i % 14] = input[i];
    }
  }

}