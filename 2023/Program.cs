using BenchmarkDotNet.Running;

if (args.Length == 0)
{
  Console.WriteLine("Enter command (solve/benchmark) followed by the requested day to run");
  return;
}

string command = args[0];
int day = args.Length > 1 ? int.Parse(args[1]) : DateTime.Now.Day;
Type? type = Type.GetType($"Y2023.Day{day}");

if (type == null)
{
  Console.WriteLine($"Day {day} not found");
  return;
}

switch (command)
{
  case "solve":
    var instance = Activator.CreateInstance(type);
    var method = type.GetMethod("Solve");
    method!.Invoke(instance, null);
    break;
  case "benchmark":
    var summary = BenchmarkRunner.Run(type.Assembly);
    Console.WriteLine(summary);
    break;
  default:
    throw new NotImplementedException("Invalid command");
}