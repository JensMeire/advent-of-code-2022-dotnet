// See https://aka.ms/new-console-template for more information
using D1_CalorieCounting;

var elfs = ElfParser.Parse("./data.txt");
var maxCalorieElf = elfs.MaxBy(x => x.TotalCalories);
Console.WriteLine(maxCalorieElf.TotalCalories);

var top3 = elfs.Select(x => x.TotalCalories)
    .Order()
    .TakeLast(3)
    .Sum();

Console.WriteLine(top3);