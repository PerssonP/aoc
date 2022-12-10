namespace Y2022;
class Program
{
  static void Main(string[] args)
  {
    if (args.Length > 0 && int.TryParse(args[0], out int nbr))
    {
      Type? type = Type.GetType($"Y2022.Day{nbr}");
      if (type == null)
      {
        Console.WriteLine("Day not found");
      }
      else
      {
        type.GetMethod("Solve")!.Invoke(null, null);
      }
    }
    else
    {
      Console.WriteLine("Enter number of day to run as the first argument");
    }
  }
}