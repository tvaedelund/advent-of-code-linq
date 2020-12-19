<Query Kind="Statements">
  <NuGetReference>morelinq</NuGetReference>
  <Namespace>static MoreLinq.Extensions.SubsetsExtension</Namespace>
  <Namespace>System.Numerics</Namespace>
</Query>

var path = Path.GetDirectoryName(Util.CurrentQueryPath);
var test = true;
var input = "11,18,0,20,1,7,16";

if (test)
{
	input = "0,3,6";
}

Func<string, int, int> solvePart1 = (numbers, iterations) =>
{
	var game = new List<int>(numbers.Split(',').Select(int.Parse));
	
	var i = game.Count - 1;
	while (i < iterations - 1)
	{
		var last = game.Last();
		var prev = game.Take(game.Count - 1).ToList();
		var next = prev.Any(x => x == last) ? i - prev.LastIndexOf(last) : 0;

		game.Add(next);
		
		i++;
	}
		
	return game.Last();
};

// *** STEP 1 ***
var sw1 = Stopwatch.StartNew();
var result1 = solvePart1(input, 2020);
sw1.Stop();

// *** STEP 2 ***
var sw2 = Stopwatch.StartNew();
var result2 = 0; // solvePart1(input, 30000000);
sw2.Stop();

// *** REPORT ***
@$"==== DAY 15 ====
Part 1
Solution: {result1}
Time: {sw1.ElapsedMilliseconds} ms
Part 2
Solution: {result2}
Time: {sw2.ElapsedMilliseconds} ms".Dump();

