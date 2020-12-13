<Query Kind="Statements">
  <NuGetReference>morelinq</NuGetReference>
  <Namespace>static MoreLinq.Extensions.SubsetsExtension</Namespace>
</Query>

var path = Path.GetDirectoryName(Util.CurrentQueryPath);
var test = false;
var input = File.ReadAllLines(Path.Combine(path, test ? "input-test.txt" : "input.txt"));

Func<string[], int> solvePart1 = (instructions) =>
{
	var facing = 90; // east
	var direction = 0; // north
	var (x, y) = (0, 0); // start

	foreach (var instr in instructions)
	{
		var m = Regex.Match(instr, @"^(\w)(\d*)");
		var (action, value) = (char.Parse(m.Groups[1].Value), int.Parse(m.Groups[2].Value));

		facing = action switch
		{
			'L' => ((facing - value) % 360 + 360) % 360,
			'R' => ((facing + value) % 360 + 360) % 360,
			_ => facing
		};

		direction = action switch
		{
			'N' => 0,
			'S' => 180,
			'E' => 90,
			'W' => 270,
			_ => facing
		};

		if (!new[] { 'L', 'R' }.Any(x => x == action))
		{
			(x, y) = direction switch
			{
				0 => (x, y + value),
				90 => (x + value, y),
				180 => (x, y - value),
				270 => (x - value, y),
			};
		}
	}

	return Math.Abs(Math.Abs(x) + Math.Abs(y));
};

Func<int, int, int, bool, (int x, int y)> rotate = (degrees, x, y, cw) =>
{
	var (xNew, yNew) = (x, y);
	var i = 0;
	while (i++ < degrees / 90)
	{
		if (cw)
		{
			(xNew, yNew) = (y, 0 - x);
		}
		else
		{
			(xNew, yNew) = (0 - y, x);
		}

		(x, y) = (xNew, yNew);
	}

	return (x, y);
};

Func<string[], int> solvePart2 = (instructions) =>
{
	var (xS, yS) = (0, 0); // start ship
	var (xW, yW) = (10, 1); // start waypoint

	foreach (var instr in instructions)
	{
		var m = Regex.Match(instr, @"^(\w)(\d*)");
		var (action, value) = (char.Parse(m.Groups[1].Value), int.Parse(m.Groups[2].Value));

		// move/rotate waypoint
		(xW, yW) = action switch
		{
			'N' => (xW, yW + value),
			'S' => (xW, yW - value),
			'E' => (xW + value, yW),
			'W' => (xW - value, yW),
			'L' => rotate(value, xW, yW, false),
			'R' => rotate(value, xW, yW, true),
			'F' => (xW, yW)
		};

		// move ship
		if (action == 'F')
		{
			(xS, yS) = (xS + xW * value, yS + yW * value);
		}
	}

	return Math.Abs(xS) + Math.Abs(yS);
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
@$"==== DAY 12 ====
Part 1
Solution: {result1}
Time: {sw1.ElapsedMilliseconds} ms
Part 2
Solution: {result2}
Time: {sw2.ElapsedMilliseconds} ms".Dump();

