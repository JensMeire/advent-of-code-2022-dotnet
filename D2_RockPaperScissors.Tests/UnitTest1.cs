namespace D2_RockPaperScissors.Tests;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void Part1()
    {
        var rounds = Parser.Parse("./testdata.txt");
        var total = rounds.Select(x => x.GetOutcomePoints()).Sum();
        Assert.AreEqual(total, 15);
    }

    [TestMethod]
    public void Part2()
    {
        var rounds = Parser.Parse2("./testdata.txt");
        var total = rounds.Select(x => x.GetOutcomePoints()).Sum();
        Assert.AreEqual(total, 12);
    }
}
