using BenchmarkDotNet.Attributes;
using System.Text.RegularExpressions;

namespace Y2023;

public partial class Day5
{
  private readonly string[] inputs;

  public Day5()
  {
    inputs = File.ReadAllLines("./day5/input.txt");
  }

  public void Solve()
  {
    Console.WriteLine(Part1());
    Console.WriteLine(Part2());
  }

  [Benchmark]
  public long Part1()
  {
    var seed_soil = MakeMapForSection("seed-to-soil map:");
    var soil_fertilizer = MakeMapForSection("soil-to-fertilizer map:");
    var fertilizer_water = MakeMapForSection("fertilizer-to-water map:");
    var water_light = MakeMapForSection("water-to-light map:");
    var light_temperature = MakeMapForSection("light-to-temperature map:");
    var temperature_humidity = MakeMapForSection("temperature-to-humidity map:");
    var humidity_location = MakeMapForSection("humidity-to-location map:");

    long[] seeds = NumbersPattern().Matches(inputs[0]).Select(m => long.Parse(m.Value)).ToArray();

    long min = seeds
      .Select(x => GetMappedValue(x, seed_soil))
      .Select(x => GetMappedValue(x, soil_fertilizer))
      .Select(x => GetMappedValue(x, fertilizer_water))
      .Select(x => GetMappedValue(x, water_light))
      .Select(x => GetMappedValue(x, light_temperature))
      .Select(x => GetMappedValue(x, temperature_humidity))
      .Select(x => GetMappedValue(x, humidity_location))
      .Min();

    return min;
  }

  [Benchmark]
  public long Part2()
  {
    return 0;
  }

  private IEnumerable<Func<long, long?>> MakeMapForSection(string key) => inputs
    .SkipWhile(s => s != key).Skip(1).TakeWhile(s => s != "")
    .Select(s =>
    {
      string[] values = s.Split(' ');
      return (sourceStart: long.Parse(values[1]), sourceEnd: long.Parse(values[1]) + long.Parse(values[2]) - 1, destStart: long.Parse(values[0]));
    })
    .Select(parsed => new Func<long, long?>((long from) =>
      (from >= parsed.sourceStart && from <= parsed.sourceEnd) ? parsed.destStart + (from - parsed.sourceStart) : null));

  private long GetMappedValue(long from, IEnumerable<Func<long, long?>> list)
  {
    long? result = null;
    var enumerator = list.GetEnumerator();
    while (result == null && enumerator.MoveNext())
      result = enumerator.Current(from);
    return result ?? from;
  }

  [GeneratedRegex(@"\d+")]
  private static partial Regex NumbersPattern();
}