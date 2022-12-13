using System.Text.RegularExpressions;

var lines = File.ReadLines("./data.txt").ToList();
var operationRegex = new Regex(".*(old) (.) (.*)");
var divisibleRegex = new Regex(".*divisible by (.*)");
var monkeyIndexRegex = new Regex(".*throw to monkey (.*)");


var monkeys = new List<Monkey>();
for(var i = 0; i < lines.Count; i += 7)
{
    var items = lines[i + 1].Split(": ")[1].Split(", ").Select(w => new Item { WorryLevel = long.Parse(w) });
    
    var operationMatch = operationRegex.Match(lines[i + 2]);
    var arethmicChar = operationMatch.Groups[2].Value;
    var amount = operationMatch.Groups[3].Value;
    Action<Item> operation = (Item item) =>
    {
        switch(arethmicChar)
        {
            case "*": item.WorryLevel = item.WorryLevel * (amount == "old" ? item.WorryLevel : int.Parse(amount)); break;
            case "-": item.WorryLevel = item.WorryLevel - (amount == "old" ? item.WorryLevel : int.Parse(amount)); break;
            case "+": item.WorryLevel = item.WorryLevel + (amount == "old" ? item.WorryLevel : int.Parse(amount)); break;
            case "/": item.WorryLevel = item.WorryLevel / (amount == "old" ? item.WorryLevel : int.Parse(amount)); break;
            default: throw new NotImplementedException();
        }
    };

    var divisible = int.Parse(divisibleRegex.Match(lines[i + 3]).Groups[1].Value);
    var trueMonkeyIndex = int.Parse(monkeyIndexRegex.Match(lines[i + 4]).Groups[1].Value);
    var falseMonkeyIndex = int.Parse(monkeyIndexRegex.Match(lines[i + 5]).Groups[1].Value);
    var getTrueMonkey = () => monkeys[trueMonkeyIndex];
    var getFalseMonkey = () => monkeys[falseMonkeyIndex];
    monkeys.Add(new Monkey(items.ToList(), operation, divisible, getTrueMonkey, getFalseMonkey));
}
var dominator = monkeys.Select(x => x.DevisableTest).Aggregate((a, x) => a * x);

for (var i = 1; i <= 10000; i++)
{
    foreach(var monkey in monkeys)
    {
        monkey.CurrentItems.ForEach(i => { i.WorryLevel = i.WorryLevel % dominator; });
        foreach(var item in monkey.CurrentItems)
        {
            monkey.Inspect(item);
            monkey.Test(item);
        }
        monkey.CurrentItems.Clear();
    }
}

var top2 = monkeys.OrderByDescending(x => x.InspectedItems).Take(2).ToList();
Console.WriteLine(top2[1].InspectedItems * top2[0].InspectedItems );

class Item
{
    public long WorryLevel { get; set; }
}

class Monkey
{
    public List<Item> CurrentItems { get; set; }
    public Action<Item> Operation { get; set; }
    public int DevisableTest { get; set; }
    public Func<Monkey> GetTrueMonkey { get; set; }
    public Func<Monkey> GetFalseMonkey { get; set; }
    public long InspectedItems { get; set; }

    public Monkey(List<Item> currentItems, Action<Item> operation, int devisable, Func<Monkey> getTrueMonkey, Func<Monkey> getFalseMonkey)
    {
        CurrentItems = currentItems;
        Operation = operation;
        DevisableTest = devisable;
        GetTrueMonkey = getTrueMonkey;
        GetFalseMonkey = getFalseMonkey;
    }

    public void Throw(Monkey monkey, Item item)
    {
        monkey.CurrentItems.Add(item);
    }

    public void Inspect(Item item)
    {
        Operation(item);
    }

    public void Test(Item item)
    {
        InspectedItems++;
        // Part 1
        // item.WorryLevel = item.WorryLevel / 3;
        if (item.WorryLevel % DevisableTest == 0) Throw(GetTrueMonkey(), item);
        else Throw(GetFalseMonkey(), item);
    }
}