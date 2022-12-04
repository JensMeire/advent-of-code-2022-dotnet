var lines = File.ReadLines("./data.txt");
IEnumerable<(Range range1, Range range2)> ranges = lines.Select(line =>
{
    var splitted = line.Split(",");
    var firstPart = splitted[0].Split("-");
    var secondPart = splitted[1].Split("-");
    return (new Range(firstPart[0], firstPart[1]), new Range(secondPart[0], secondPart[1]));
});


var includes = ranges.Where(pair => pair.range1.Includes(pair.range2) || pair.range2.Includes(pair.range1));
Console.WriteLine("Part1");
Console.WriteLine(includes.Count());

var overlaps = ranges.Where(pair => pair.range1.Overlaps(pair.range2));
Console.WriteLine("Part2");
Console.WriteLine(overlaps.Count());

class Range
{
    public int Start { get; set; }
    public int End { get; set; }

    public Range(string start, string end)
    {
        Start = int.Parse(start);
        End = int.Parse(end);
    }

    public bool Includes(Range range) => Start <= range.Start && End >= range.End;
    public bool Overlaps(Range range) => int.Max(Start, range.Start) <= int.Min(End, range.End);
}