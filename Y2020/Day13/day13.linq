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

Func<string[], long> solvePart2 = (schedules) =>
{
	var busses = schedules[1].Replace('x', '0').Split(',').Select((b, i) => (long.Parse(b), i)).Where(x => x.Item1 > 0).ToArray();
	
	var jump = busses[0].Item1;
	var i = jump;
	foreach (var b in busses.Skip(1))
	{
		while ((i + b.Item2) % b.Item1 != 0)
		{
			i += jump;
		}
		
		jump *= b.Item1;
	}
			
	return i;
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

