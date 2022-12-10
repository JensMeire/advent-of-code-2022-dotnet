var lines = File.ReadLines("./testdata.txt");
(int x, int y) h = (0, 0);
(int x, int y) t = (0, 0);

var visisted = new List<(int x, int y)>();
var rope = new Rope((int x, int y) =>
{
    if (visisted.Any(v => x == v.x && y == v.y)) return;
    visisted.Add((x, y));
}, 10);

foreach(var line in lines)
{
    var splitted = line.Split(" ");
    var direction = splitted[0];
    var amount = int.Parse(splitted[1]);
    Action moveDirection = () => Console.WriteLine("No"); ;

    if (direction == "D")
        moveDirection = rope.MoveDown;
    if (direction == "U")
        moveDirection = rope.MoveUp;
    if (direction == "R")
        moveDirection = rope.MoveRight;
    if (direction == "L")
        moveDirection = rope.MoveLeft;


    for (int i = 0; i < amount; i++)
    {
        moveDirection();
    }
}

Console.WriteLine(visisted.Count);

class Knot
{
    public int X { get; set; }
    public int Y { get; set; }
    public Knot(int x, int y)
    {
        X = x;
        Y = y;
    }

    public bool IsTouching(Knot knot) => Math.Abs(knot.X - X) <= 1 && Math.Abs(knot.Y - Y) <= 1;

}

class Rope
{
    public List<Knot> Knots { get; set; }
    public Knot Head => Knots.First();
    public Knot Tail => Knots.Last();


    public Action<int, int> TailMoved { get; }

    public Rope(Action<int, int> tailMoved, int amount)
    {
        TailMoved = tailMoved;
        Knots = new List<Knot>();
        for (int i = 0; i < amount; i++)
        {
            Knots.Add(new Knot(0,0));
        }
    }

    private void NotifyTailMoved()
    {
        TailMoved(Tail.X, Tail.Y);
    }

    public void MoveDown()
    {
        Knot prev = null;
        Knots.ForEach(k =>
        {
            if (k == Head)
            {
                k.Y--;
                prev = k;
                return;
            }
            if (k.IsTouching(prev))
            {
                prev = k;
                return;
            }
            if (prev.X > k.X) k.X++;
            else if (prev.X < k.X) k.X--;
            k.Y--;
            if (k == Tail) NotifyTailMoved();
            prev = k;
        });
    }

    public void MoveUp()
    {
        Knot prev = null;
        Knots.ForEach(k =>
        {
            if (k == Head)
            {
                k.Y++;
                prev = k;
                return;
            }
            if (k.IsTouching(prev))
            {
                prev = k;
                return;
            }
            if (prev.X > k.X) k.X++;
            else if(prev.X < k.X) k.X--;
            k.Y++;
            if (k == Tail) NotifyTailMoved();
            prev = k;
        });
    }

    public void MoveRight()
    {
        Knot prev = null;
        Knots.ForEach(k =>
        {
            if (k == Head)
            {
                k.X++;
                prev = k;
                return;
            }
            if (k.IsTouching(prev))
            {
                prev = k;
                return;
            }
            if (prev.Y > k.Y) k.Y++;
            else if (prev.Y < k.Y) k.Y--;
            k.X++;
            if (k == Tail) NotifyTailMoved();
            prev = k;
        });
    }

    public void MoveLeft()
    {
        Knot prev = null;
        Knots.ForEach(k =>
        {
            if (k == Head)
            {
                k.X--;
                prev = k;
                return;
            }
            if (k.IsTouching(prev))
            {
                prev = k;
                return;
            }
            if (prev.Y > k.Y) k.Y++;
            else if(prev.Y < k.Y) k.Y--;
            k.X--;
            if (k == Tail) NotifyTailMoved();
            prev = k;
        });
    }
}

