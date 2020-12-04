<Query Kind="Statements" />

var path = Path.GetDirectoryName(Util.CurrentQueryPath);
var data = File.ReadAllLines(Path.Combine(path, "day03.txt"));

// *** STEP 1 ***
var sw1 = Stopwatch.StartNew();
var col = 0;
var len = data[0].Length;
var trees = 0;
for (int row = 0; row < data.Length; row++)
{
	col %= len;
	
	if (data[row][col] == '#')
	{
		trees++;
	}
	
	col += 3;
}
sw1.Stop();

// *** STEP 2 ***
var sw2 = Stopwatch.StartNew();
var steps = new List<(int c, int r)>
{
	(1, 1),
	(3, 1),
	(5, 1),
	(7, 1),
	(1, 2)
};

var treesTot = 1;
len = data[0].Length;
foreach (var step in steps)
{
	col = 0;
	trees = 0;
	for (int row = 0; row < data.Length; row += step.r)
	{
		col %= len;

		if (data[row][col] == '#')
		{
			trees++;
		}

		col += step.c;
	}

	treesTot *= trees;
}
sw2.Stop();

// *** REPORT ***
@$"==== DAY 03 ====
Part 1
Solution: {trees}
Time: {sw1.ElapsedMilliseconds} ms
Part 2
Solution: {treesTot}
Time: {sw2.ElapsedMilliseconds} ms".Dump();
