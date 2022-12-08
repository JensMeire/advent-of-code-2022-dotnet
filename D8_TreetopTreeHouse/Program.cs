var lines = File.ReadLines("./data.txt");

var grid = lines.Select(line => line.Select(c => (int)c - '0').ToList()).ToList();
var yMax = grid.Count();
var xMax = grid.First().Count();
var outsideCount = (yMax * 2) + ((xMax - 2) * 2);

bool CanBeSeen(int x, int y)
{
    var canBeSeen = false;
    var init = grid[y][x];

    var left = grid[y].Take(x);
    if (left.All(t => t < init))
        return true;

    var right = grid[y].Skip(x + 1);
    if (right.All(t => t < init))
        return true;

    var fullCol = grid.Select(row => row[x]).ToList();

    var top = fullCol.Take(y);
    if (top.All(t => t < init))
        return true;

    var bottom = fullCol.Skip(y + 1);
    if (bottom.All(t => t < init))
        return true;

    return false;
}

int ScenicScore(int x, int y)
{
    var init = grid[y][x];

    var left = grid[y].Take(x).Reverse().TakeWhile(t => t < init).Count();
    if (x - left != 0) left++;

    var right = grid[y].Skip(x + 1).TakeWhile(t => t < init).Count();
    if (x + right != xMax - 1) right++;

    var fullCol = grid.Select(row => row[x]).ToList();
    var top = fullCol.Take(y).Reverse().TakeWhile(t => t < init).Count();
    if (y - top != 0) top++;

    var bottom = fullCol.Skip(y + 1).TakeWhile(t => t < init).Count();
    if (y + bottom != yMax - 1) bottom++;


    return left * right * top * bottom;
}

var highestScenicScore = 0;
for (int y = 1; y < yMax - 1; y++)
{
    for (int x = 1; x < xMax - 1; x++)
    {
        // Part1
        // if (CanBeSeen(x, y)) outsideCount++;
        var scenic = ScenicScore(x, y);
        if (scenic > highestScenicScore) highestScenicScore = scenic;
    }
}

// Console.WriteLine(outsideCount);
Console.WriteLine(highestScenicScore);