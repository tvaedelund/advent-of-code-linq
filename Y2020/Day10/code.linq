<Query Kind="Statements" />

var path = Path.GetDirectoryName(Util.CurrentQueryPath);
var input = File.ReadAllLines(Path.Combine(path, "input-test.txt"))
.Select(int.Parse)
.OrderBy(x => x)
.ToList();

input = input.Prepend(0).ToList();
input.Add(input.Max() + 3);

Func<List<int>, List<int>> getDifferences = (ratings) =>
{
	var diffs = new List<int>();

	for (int i = 0; i < ratings.Count - 1; i++)
	{
		diffs.Add(ratings[i + 1] - ratings[i]);
	}

	return diffs;
};

Func<List<int>, int> getValueToRemove = (ratings) =>
{
	for (int i = 0; i < ratings.Count - 2; i++)
	{
		if (ratings[i + 2] - ratings[i] <= 3)
		{
			return ratings[i + 1];
		}
	}

	return 0;
};

Func<List<int>, int> getArrangements = (ratings) =>
{
	var test = from m in Enumerable.Range(0, 1 << ratings.Count)
			   select
				   from i in Enumerable.Range(0, ratings.Count)
				   where (m & (1 << i)) != 0
				   select ratings[i];

	//var b = test.Where(x => x.Count() > 0).Where(x => x.Min() == 0 && x.Max() == ratings.Max()).Dump();
	//b.Select(x => x.Zip(x.Skip(1), (p, c) => (p, c)).Where(zip => zip.c - zip.p <= 3)).Dump();

	test.Select(x => x.Take(test.Count() - 1).Select((c, i) => new { c, n = x.ElementAt(i) })).Dump();

	return 0;
};

// *** STEP 1 ***
var sw1 = Stopwatch.StartNew();
var diffs = getDifferences(input);
var result1 = diffs.Count(x => x == 1) * diffs.Count(x => x == 3);
sw1.Stop();

// *** STEP 2 ***
var sw2 = Stopwatch.StartNew();
var result2 = getArrangements(input);
sw2.Stop();

// *** REPORT ***
@$"==== DAY 10 ====
Part 1
Solution: {result1}
Time: {sw1.ElapsedMilliseconds} ms
Part 2
Solution: {result2}
Time: {sw2.ElapsedMilliseconds} ms".Dump();

