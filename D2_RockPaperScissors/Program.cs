// See https://aka.ms/new-console-template for more information
using D2_RockPaperScissors;

Console.WriteLine("Hello, World!");

var rounds = Parser.Parse("./data.txt");
var total = rounds.Select(x => x.GetOutcomePoints()).Sum();
Console.WriteLine(total);

//foreach(var round in rounds)
//{
//    Console.WriteLine(round.OpponentsGesture.GetHandGesture().ToString());
//    Console.WriteLine(round.YourGesture.GetHandGesture().ToString());
//    Console.WriteLine(round.GetOutcomePoints());
//}

var rounds2 = Parser.Parse2("./data.txt");
var total2 = rounds2.Select(x => x.GetOutcomePoints()).Sum();
Console.WriteLine(total2);