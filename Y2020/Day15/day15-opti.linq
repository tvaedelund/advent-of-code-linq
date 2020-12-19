<Query Kind="Statements" />

var path = Path.GetDirectoryName(Util.CurrentQueryPath);
var test = false;
var input = "11,18,0,20,1,7,16";

if (test)
{
	input = "3,1,2";
}

Func<string, int, int> solvePart1And2 = (numbers, iterations) =>
{
	// select start numbers into dictionary => (value, index) = where did we last see the value
	var game = new Dictionary<int, int>(numbers
		.Split(',')
		.Select((x, i) => new { n = int.Parse(x), i })
		.ToDictionary(pair => pair.n, pair => pair.i)
	);

	var last = game.Last().Key;

	// start on the second to last item since the last item should not be considered yet
	for (int i = game.Count - 1; i < iterations; i++)
	{
		var next = game.ContainsKey(last) ? i - game[last] : 0;
		game[last] = i;
		last = next;
	}
	
	return game.Single(x => x.Value == iterations - 1).Key;
};

// *** STEP 1 ***
var sw1 = Stopwatch.StartNew();
var result1 = solvePart1And2(input, 2020);
sw1.Stop();

// *** STEP 2 ***
var sw2 = Stopwatch.StartNew();
var result2 = solvePart1And2(input, 30000000);
sw2.Stop();

// *** REPORT ***
@$"==== DAY 15 ====
Part 1
Solution: {result1}
Time: {sw1.ElapsedMilliseconds} ms
Part 2
Solution: {result2}
Time: {sw2.ElapsedMilliseconds} ms".Dump();

