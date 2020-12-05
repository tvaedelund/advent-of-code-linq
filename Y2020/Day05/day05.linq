<Query Kind="Statements" />

var path = Path.GetDirectoryName(Util.CurrentQueryPath);
var data = File.ReadAllLines(Path.Combine(path, "input.txt"));

Func<char[], int, int, int> reduce = (moves, l, u) =>
{
	while (moves.Length > 0)
	{
		var m = moves.First();
		u = m == 'F' || m == 'L' ? u - ((u - l) / 2) - 1 : u;
		l = m == 'B' || m == 'R' ? l + ((u - l) / 2) + 1 : l;
		moves = moves.Skip(1).ToArray();
	}
	
	return l;
};

// *** STEP 1 ***
var sw1 = Stopwatch.StartNew();
var ids = new int[data.Length];
for (int i = 0; i < data.Length; i++)
{
	var row = reduce(data[i].Take(7).ToArray(), 0, 127);
	var col = reduce(data[i].Skip(7).ToArray(), 0, 7);
	ids[i] = row * 8 + col;
}
var result1 = ids.Max();
sw1.Stop();

// *** STEP 2 ***
var sw2 = Stopwatch.StartNew();
var result2 = 0;
ids = ids.OrderBy(x => x).ToArray();
for (int i = 0; i < ids.Length; i++)
{
	var id = i + ids.Min();
	if (ids[i] != id)
	{
		result2 = id;
		break;
	}
}
sw2.Stop();

// *** REPORT ***
@$"==== DAY 05 ====
Part 1
Solution: {result1}
Time: {sw1.ElapsedMilliseconds} ms
Part 2
Solution: {result2}
Time: {sw2.ElapsedMilliseconds} ms".Dump();

