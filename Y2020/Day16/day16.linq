<Query Kind="Statements">
  <NuGetReference>morelinq</NuGetReference>
  <Namespace>static MoreLinq.Extensions.SubsetsExtension</Namespace>
  <Namespace>System.Numerics</Namespace>
</Query>

var path = Path.GetDirectoryName(Util.CurrentQueryPath);
var test = false;
var input = File.ReadAllText(Path.Combine(path, test ? "input-test.txt" : "input.txt"));

Func<string, int> solvePart1 = (notes) =>
{
	var ranges = Regex.Matches(notes, @"\d*-\d*", RegexOptions.Multiline)
		.Select(x => x.Value.Split('-').Select(int.Parse))
		.Select(x => (x.ElementAt(0), x.ElementAt(1)));

	var text = "nearby tickets:";
	var numbers = new string(input.Skip(input.IndexOf(text) + text.Length + 2).ToArray())
		.Split("\r\n")
		.Select(x => x.Split(',').Select(int.Parse))
		.SelectMany(x => x);

	var rate = 0;
	foreach (var number in numbers)
	{
		var inRange = false;
		foreach (var range in ranges)
		{
			if (number >= range.Item1 && number <= range.Item2)
			{
				inRange = true;
				break;
			}
		}
		
		if (!inRange)
		{
			rate += number;
		}
	}

	return rate;
};

Func<string, int> solvePart2 = (notes) =>
{
	var ranges = Regex.Matches(notes, @"\d*-\d*", RegexOptions.Multiline)
		.Select(x => x.Value.Split('-').Select(int.Parse))
		.Select(x => (x.ElementAt(0), x.ElementAt(1)));

	var text = "nearby tickets:";
	var numbers = new string(input.Skip(input.IndexOf(text) + text.Length + 2).ToArray())
		.Split("\r\n")
		.Select(x => x.Split(',').Select(int.Parse))
		.SelectMany(x => x);

	var rate = 0;
	foreach (var number in numbers)
	{
		var inRange = false;
		foreach (var range in ranges)
		{
			if (number >= range.Item1 && number <= range.Item2)
			{
				inRange = true;
				break;
			}
		}

		if (!inRange)
		{
			rate += number;
		}
	}

	return rate;
};

// *** STEP 1 ***
var sw1 = Stopwatch.StartNew();
var result1 = solvePart1(input);
sw1.Stop();

// *** STEP 2 ***
var sw2 = Stopwatch.StartNew();
var result2 = 0;
sw2.Stop();

// *** REPORT ***
@$"==== DAY 16 ====
Part 1
Solution: {result1}
Time: {sw1.ElapsedMilliseconds} ms
Part 2
Solution: {result2}
Time: {sw2.ElapsedMilliseconds} ms".Dump();

