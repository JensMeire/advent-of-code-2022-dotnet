using System.Text.RegularExpressions;

var lines = File.ReadLines("./data.txt").ToList();
var stacks = lines.Where(l => l.Contains("[")).ToList();
var commands = lines.Where(l => l.Contains("move"));

var supplyStacks = new List<List<char>>();
var rows = stacks.Last().Replace(" ", "").Replace("[", "").Replace("]", "").Length;
for (int i = 0; i < rows; i++)
{
    supplyStacks.Add(new List<char>());
}

for (int i = 0; i < stacks.Count; i++)
{
    var stack = stacks[i];
    for (int j = 0; j < rows; j++)
    {
        var index = (j * 3) + (1 + j);
        var supply = stack[index];
        if (supply == ' ') continue;
        supplyStacks[j].Add(supply);
    }

}

foreach (var stack in supplyStacks)
{
    stack.Reverse();
}

var pattern = @"move ([0-9]+) from ([0-9]+) to ([0-9]+)";
List<(int amount, int fromIndex, int toIndex)> moveCommands = commands.Select(c =>
{
    var match = Regex.Match(c, pattern);
    return (int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value) - 1, int.Parse(match.Groups[3].Value) - 1);
}).ToList();

foreach(var command in moveCommands)
{
    // Add Reverse for part1
    var supplies = supplyStacks[command.fromIndex].TakeLast(command.amount).ToList();
    supplyStacks[command.toIndex].AddRange(supplies);
    supplyStacks[command.fromIndex].RemoveRange(supplyStacks[command.fromIndex].Count - command.amount, command.amount);
}

foreach (var stack in supplyStacks)
{
    Console.WriteLine("STACK");
    foreach (var supply in stack)
    {
        Console.WriteLine(supply);
    }
}
var result = "";
foreach (var stack in supplyStacks)
{
    result += stack.Last();
}

Console.WriteLine(result);