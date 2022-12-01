namespace D1_CalorieCounting.Tests;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void Part1()
    {
        var elfs = ElfParser.Parse("./testdata.txt");
        var maxCalorieElf = elfs.MaxBy(x => x.TotalCalories);
        Assert.AreEqual(maxCalorieElf?.TotalCalories, 24000);
    }

    [TestMethod]
    public void Part2()
    {
        var elfs = ElfParser.Parse("./testdata.txt");
        var top3 = elfs.Select(x => x.TotalCalories)
            .Order()
            .TakeLast(3)
            .Sum();
        Assert.AreEqual(top3, 45000);
    }
}
