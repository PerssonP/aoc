int nbr = 0;

if (args.Length == 0 && DateTime.Now.Month == 12)
{
  nbr = DateTime.Now.Day;
}
else if (args.Length > 0)
{
  nbr = int.Parse(args[0]);
}
else
{
  Console.WriteLine("Enter number of day to run as the first argument");
  return;
}

Type? type = Type.GetType($"Y2023.Day{nbr}");
if (type == null)
{
  Console.WriteLine("Day not found");
}
else
{
  type.GetMethod("Solve")!.Invoke(null, null);
}
