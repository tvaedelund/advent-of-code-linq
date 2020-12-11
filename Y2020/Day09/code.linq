<Query Kind="Statements">
  <NuGetReference>morelinq</NuGetReference>
  <Namespace>MoreLinq</Namespace>
</Query>

var path = Path.GetDirectoryName(Util.CurrentQueryPath);
var test = false;
var input = File.ReadAllLines(Path.Combine(path, test ? "input-test.txt" : "input.txt"))
.Select(long.Parse)
.ToArray();

Func<long[], int, long> getFirstRuleBrealer = (numbers, preamble) =>
{
	for (int i = preamble; i < numbers.Length; i++)
	{
		var preambles = numbers.Skip(i - preamble).Take(preamble);
		var combos = preambles.Cartesian(preambles, (a, b) => a + b);
				
		if (!combos.Any(x => x == numbers[i]))
		{
			return numbers[i];
		}
	}
	
	return 0;
};

Func<long[], long, long> findEncWeakness = (numbers, target) =>
{
	var i = 2;
	while (true)
	{
		var windows = numbers.Window(i++);
		var sums = windows.Select(x => new { min = x.Min(), max = x.Max(), s = x.Sum() });
		
		if (sums.Any(x => x.s == target))
		{
			return sums.Single(x => x.s == target).min + sums.Single(x => x.s == target).max;
		}
	}
};

// *** STEP 1 ***
var sw1 = Stopwatch.StartNew();
var result1 = getFirstRuleBrealer(input, test ? 5 : 25);
sw1.Stop();

// *** STEP 2 ***
var sw2 = Stopwatch.StartNew();
var result2 = findEncWeakness(input, getFirstRuleBrealer(input, test ? 5 : 25));
sw2.Stop();

// *** REPORT ***
@$"==== DAY 09 ====
Part 1
Solution: {result1}
Time: {sw1.ElapsedMilliseconds} ms
Part 2
Solution: {result2}
Time: {sw2.ElapsedMilliseconds} ms".Dump();

