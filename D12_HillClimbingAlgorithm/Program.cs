// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
var indexes = "SabcdefghijklmnopqrstuvwxyzE";
var lines = File.ReadLines("./data.txt").ToList();
var grid = new Dictionary<string, Point>();
var yMax = lines.Count() - 1;
var xMax = lines.First().Count() - 1;

Point startPosition = null;
Point endPosition = null;
for (var y = 0; y < lines.Count(); y++)
{
    var line = lines[y];
    var res = new List<Point>();
    for(var x = 0; x < line.Length; x++)
    {
        var c = line[x];
        var point = new Point
        {
            X = x,
            Y = y,
            Height = indexes.IndexOf(c),
            ShortestDistance = 2000000,
            Previous = null,
            IsVisited = false,
        };
        grid.Add(point.GetKey, point);
        if (c == 'E')
        {
            endPosition = point;
            point.Height = indexes.IndexOf("z");
        }
        if (c == 'S')
        {
            startPosition = point;
            point.Height = indexes.IndexOf("a");
            point.ShortestDistance = 0;
        }
    }
}

// var unvisited = grid.ToDictionary(x => x.Key, x => x.Value);
// var visited = new List<Point>();
// 
// while (unvisited.Any())
// {
//     var current = unvisited.FirstOrDefault(f => f.Value.ShortestDistance == unvisited.Min(x => x.Value.ShortestDistance)).Value;
//     if (current == null) throw new NotImplementedException();
//     var neighbours = GetNeighbours(current).ToList();
// 
//     // var validNeighbours = neighbours.Where(n => Math.Abs(n.Height - current.Height) == 1).ToList();
//     
//     neighbours.ForEach(x => {
//         var ridge = Math.Abs(x.Height - current.Height);
//         var newDistance = current.ShortestDistance + ridge;
//         x.Previous = newDistance < x.ShortestDistance ? current : x.Previous;
//         x.ShortestDistance = newDistance < x.ShortestDistance ? newDistance : x.ShortestDistance;
//     });
// 
//     visited.Add(current);
//     current.IsVisited = true;
//     unvisited.Remove(current.GetKey);
//     Console.WriteLine(unvisited.Count);
// }

GetPath(endPosition);

void GetPath(Point end)
{
    var queue = new Queue<Point>();
    queue.Enqueue(end);
    var foundA = false;
    while(queue.Count != 0)
    {
        var point = queue.Dequeue();
        point.IsVisited = true;
        var neighbours = GetNeighbours(point);
        neighbours.ForEach(n => {
            n.Previous = point;
            n.IsVisited = true;
            if (n.Height == 1 && !foundA)
            {
                foundA = true;
                var count = 0;
                Point curr = n;
                while (curr != null)
                {
                    count++;
                    curr = curr.Previous;
                }
                Console.WriteLine("Part 2");
                Console.WriteLine(count);
            }
            queue.Enqueue(n);
        });

    }

}

var count = 0;
Point curr = startPosition;
while(curr != null)
{
    count++;
    curr = curr.Previous;
}
Console.WriteLine(count);
Console.WriteLine();

List<Point> GetNeighbours(Point point)
{
    //return unvisited.Where(p => (p.X == point.X && p.Y == point.Y + 1) || (p.X == point.X && p.Y == point.Y - 1) || (p.X == point.X + 1 && p.Y == point.Y) || (p.X == point.X - 1 && p.Y == point.Y));
    var neighbours = new List<Point>();
    if (point.X != 0)
        neighbours.Add(grid[$"{point.Y}-{point.X - 1}"]);

    if (point.X != xMax)
        neighbours.Add(grid[$"{point.Y}-{point.X + 1}"]);

    if (point.Y != 0)
        neighbours.Add(grid[$"{point.Y - 1}-{point.X}"]);

    if (point.Y != yMax)
        neighbours.Add(grid[$"{point.Y + 1}-{point.X}"]);

    return neighbours.Where(x => !x.IsVisited && point.Height - x.Height < 2).ToList(); ;
    
}


class Point
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Height { get; set; }
    public int ShortestDistance { get; set; }
    public Point Previous { get; set; }
    public bool IsVisited { get; set; }

    public override bool Equals(object? obj)
    {
        var point = obj as Point;
        return point != null && X == point.X && Y == point.Y;
    }

    public string GetKey => $"{Y}-{X}";
}