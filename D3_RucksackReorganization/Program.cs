var lines = File.ReadLines("./data.txt");

var points = " abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
int GetPoints(char priority) => points.IndexOf(priority);

IEnumerable<char> GetCompartment1(string rucksack) => rucksack.Take(rucksack.Length / 2);
IEnumerable<char> GetCompartment2(string rucksack) => rucksack.Skip(rucksack.Length / 2).Take(rucksack.Length / 2);
char GetPriorityPerRucksack(string rucksack) => GetCompartment1(rucksack).Intersect(GetCompartment2(rucksack)).First();

char GetPriorityPerGroup(string[] rucksacks) => rucksacks[0].ToCharArray().Intersect(rucksacks[1].ToCharArray()).Intersect(rucksacks[2].ToCharArray()).First();

var prioritiesTotal = lines.Select(GetPriorityPerRucksack)
    .Select(GetPoints)
    .Sum();

Console.WriteLine("Part1");
Console.WriteLine(prioritiesTotal);

var prioritiesTotal2 = lines.Chunk(3)
    .Select(GetPriorityPerGroup)
    .Select(GetPoints)
    .Sum();

Console.WriteLine("Part2");
Console.WriteLine(prioritiesTotal2);