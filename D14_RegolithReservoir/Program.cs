// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var grid = new Dictionary<(int x, int y), Material>();
var lines = File.ReadLines("./data.txt");
var maxX = 0;
var maxY = 0;
foreach(var line in lines)
{
    var rockLines = line.Split(" -> ");
    List<int> previous = null;
    foreach(var rockLine in rockLines)
    {
        var coordinates = rockLine.Split(",").Select(int.Parse).ToList();
        if (previous == null)
        {
            previous = coordinates;
            continue;
        }

        var startX = int.Min(coordinates.First(), previous.First());
        var endX = int.Max(coordinates.First(), previous.First());
        var startY = int.Min(coordinates.Last(), previous.Last());
        var endY = int.Max(coordinates.Last(), previous.Last());

        if (endX > maxX) maxX = endX;
        if (endY > maxY) maxY = endY;
        if(startX == endX)
        {
            for(;startY <= endY; startY++)
            {
                grid.TryAdd((startX, startY), Material.Rock);
            }
        }

        else if (startY == endY)
        {
            for (; startX <= endX; startX++)
            {
                grid.TryAdd((startX, startY), Material.Rock);
            }
        }

        previous = coordinates;
    }
}

(int x, int y) start = (500, 0);

for(var i = 0; i <= maxX + 1; i++)
{
    grid.Add((i, maxY + 1), Material.Abyss);
}

var hasAbyss = false;
var current = start;
var amount = 1;
while(!hasAbyss)
{
    (int x, int y) tempCurrent = (current.x, current.y + 1);
    var isBlocked = grid.TryGetValue(tempCurrent, out var material);
    if(!isBlocked)
    {
        
        current = tempCurrent;
        continue;
    } else if (material == Material.Abyss) hasAbyss = true;

    var tempLeft = (current.x - 1, current.y);
    tempCurrent = (current.x - 1, current.y + 1);
    var leftIsBlocked = grid.TryGetValue(tempLeft, out material);
    isBlocked = grid.TryGetValue(tempCurrent, out material);
    if (!isBlocked && !leftIsBlocked)
    {
        current = tempCurrent;
        continue;
    } else if (material == Material.Abyss) hasAbyss = true;

    var tempRight = (current.x + 1, current.y);
    tempCurrent = (current.x + 1, current.y + 1);
    var rightIsBlocked = grid.TryGetValue(tempRight, out material);
    isBlocked = grid.TryGetValue(tempCurrent, out material);
    if (!isBlocked && !rightIsBlocked)
    {
        current = tempCurrent;
        continue;
    } else if (material == Material.Abyss) hasAbyss = true;

    grid.Add(current, Material.Sand);
    current = start;
    amount++;
    Console.WriteLine(amount);
    if(amount == 23 )
    {
        Console.WriteLine();
    }
}

Console.WriteLine(amount);

enum Material
{
    Sand,
    Rock,
    Abyss
}
