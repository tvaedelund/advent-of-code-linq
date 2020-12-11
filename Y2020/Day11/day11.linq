<Query Kind="Statements">
  <NuGetReference>morelinq</NuGetReference>
  <Namespace>static MoreLinq.Extensions.SubsetsExtension</Namespace>
</Query>

var path = Path.GetDirectoryName(Util.CurrentQueryPath);
var test = false;
var input = File.ReadAllLines(Path.Combine(path, test ? "input-test.txt" : "input.txt"))
.Select(x => x.ToCharArray())
.ToArray();

Action<char[][]> printLayout = (layout) =>
{
	Console.WriteLine(string.Join("\r\n", layout.Select(x => string.Join("", x))));
	Console.WriteLine("-----------------------");
};

Func<char[][], int, int, char[]> getAdjacentSeats = (layout, row, col) =>
{
	var seats = new List<char>();

	for (int x = -1; x <= 1; x++)
	{
		for (int y = -1; y <= 1; y++)
		{
			if (x != 0 || y != 0)
			{
				if (row + x >= 0 && row + x < layout.Length && col + y >= 0 && col + y < layout.First().Length)
				{
					seats.Add(layout[row + x][col + y]);
				}
			}
		}
	}

	return seats.ToArray();
};

Func<char[][], int, int, char> applyRuleEmpty = (layout, row, col) =>
{
	var seats = getAdjacentSeats(layout, row, col);

	if (seats.Any(c => c == '#'))
		return 'L';
	else
		return '#';
};

Func<char[][], int, int, char> applyRuleOccupied = (layout, row, col) =>
{
	var seats = getAdjacentSeats(layout, row, col);

	if (seats.Count(c => c == '#') >= 4)
		return 'L';
	else
		return '#';
};

Func<char[][], (char[][], bool)> applyRules = (layout) => {
	var newLayout = layout.Select(x => x.ToArray()).ToArray();
	var changed = false;
	for (int row = 0; row < layout.Length; row++)
	{
		for (int col = 0; col < layout[row].Length; col++)
		{
			newLayout[row][col] = layout[row][col] switch
			{
				'L' => applyRuleEmpty(layout, row, col),
				'#' => applyRuleOccupied(layout, row, col),
				'.' => '.'
			};
			
			if (newLayout[row][col] != layout[row][col])
			{
				changed = true;
			}
		}
	}
	
	return (newLayout, changed);
};

Func<char[][], int> countOccupiedSeats = (layout) =>
{
	return layout.SelectMany(x => x).Count(x => x == '#');
};

// *** STEP 1 ***
var sw1 = Stopwatch.StartNew();
var (newLayout, changed) = applyRules(input);
while (changed)
{
	(newLayout, changed) = applyRules(newLayout);
}
var result1 = newLayout.SelectMany(x => x).Count(x => x == '#');
sw1.Stop();

// *** STEP 2 ***
var sw2 = Stopwatch.StartNew();
var result2 = 0;
sw2.Stop();

// *** REPORT ***
@$"==== DAY 11 ====
Part 1
Solution: {result1}
Time: {sw1.ElapsedMilliseconds} ms
Part 2
Solution: {result2}
Time: {sw2.ElapsedMilliseconds} ms".Dump();

