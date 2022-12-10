var lines = File.ReadLines("./data.txt");
var value = 1;
var actions = new List<Action<int>>();
var signals = new List<int> { 20, 60, 100, 140, 180, 220 };
foreach (var line in lines)
{
    if(line.StartsWith("addx"))
    {
        var amount = int.Parse(line.Split(" ")[1]);
        actions.Add((int c) => {
            if (c >= value && c <= value + 2) Console.Write("#");
            else Console.Write(".");
        });
        actions.Add((int c) => {
            if (c >= value && c <= value + 2) Console.Write("#");
            else Console.Write(".");
            value += amount;
        });
    }

    if (line == "noop")
    {
        actions.Add((int c) => {
            if (c >= value && c <= value + 2) Console.Write("#");
            else Console.Write(".");
        });
    }
}
var result = 0;
for(var i = 0; i < actions.Count; i++)
{
    var action = actions[i];
    var cycle = i + 1;


    if(signals.Contains(cycle))
    {
        result += cycle * value;
        
    }
    action(cycle % 40);
    if (cycle % 40 == 0)
    {
        Console.WriteLine("");
    }

}

Console.WriteLine(result);
