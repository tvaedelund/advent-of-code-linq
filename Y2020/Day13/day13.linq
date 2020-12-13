<Query Kind="Statements">
  <NuGetReference>morelinq</NuGetReference>
  <Namespace>static MoreLinq.Extensions.SubsetsExtension</Namespace>
  <Namespace>System.Numerics</Namespace>
</Query>

var path = Path.GetDirectoryName(Util.CurrentQueryPath);
var test = false;
var input = File.ReadAllLines(Path.Combine(path, test ? "input-test.txt" : "input.txt"));

Func<string[], int> solvePart1 = (schedules) =>
{
	var target = int.Parse(schedules[0]);
	var ids = schedules[1].Split(',').Where(x => x != "x").Select(int.Parse);
	var next = new List<(int id, int time)>(); ;
	foreach (var id in ids)
	{
		for (int i = 0; i < int.MaxValue; i += id)
		{
			if (i > target)
			{
				next.Add((id, i));
				break;
			}
		}
	}

	return (next.Min(x => x.time) - target) * next.Single(x => x.time == next.Min(x => x.time)).id;
};

Func<BigInteger, BigInteger, BigInteger> modularMultiplicativeInverse = (a, mod) =>
{
	BigInteger b = a % mod;
	for (int x = 1; x < mod; x++)
	{
		if ((b * x) % mod == 1)
		{	
			return x;
		}
	}
	return 1;
};

Func<BigInteger[], BigInteger[], BigInteger> solve = (n, a) =>
{
	BigInteger prod = n.Aggregate((BigInteger)1, (i, j) => i * j);
	BigInteger p;
	BigInteger sm = 0;
	for (int i = 0; i < n.Length; i++)
	{
		p = prod / n[i];
		sm += a[i] * modularMultiplicativeInverse(p, n[i]) * p;
	}
	return sm % prod;
};

Func<string[], BigInteger> solvePart2 = (schedules) =>
{
	var ids = schedules[1].Replace('x', '0').Split(',').Select(BigInteger.Parse).ToArray();
	var reminders = new List<BigInteger>();

	for (int i = 0; i < ids.Length; i++)
	{
		if (ids[i] > 0)
		{
			reminders.Add(ids[i] - i);
		}
	}

	return solve(ids.Where(x => x > 0).ToArray(), reminders.ToArray());
};

// *** STEP 1 ***
var sw1 = Stopwatch.StartNew();
var result1 = solvePart1(input);
sw1.Stop();

// *** STEP 2 ***
var sw2 = Stopwatch.StartNew();
var result2 = solvePart2(input);
sw2.Stop();

// *** REPORT ***
@$"==== DAY 13 ====
Part 1
Solution: {result1}
Time: {sw1.ElapsedMilliseconds} ms
Part 2
Solution: {result2}
Time: {sw2.ElapsedMilliseconds} ms".Dump();

