<Query Kind="Program" />

void Main()
{
	var path = Path.GetDirectoryName(Util.CurrentQueryPath);
	var data = File.ReadAllLines(Path.Combine(path, "day03.txt"));
	
	// *** STEP 1 ***
	var sw1 = Stopwatch.StartNew();
	var trees = CountTrees(data, 3, 1);
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
	foreach (var step in steps)
	{
		treesTot *= CountTrees(data, step.c, step.r);
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
}

// You can define other methods, fields, classes and namespaces here
private int CountTrees(string[] data, int c, int r)
{
	var col = 0;
	var trees = 0;
	for (int row = 0; row < data.Length; row += r)
	{
		col %= data[row].Length;

		if (data[row][col] == '#')
		{
			trees++;
		}

		col += c;
	}

	return trees;
}