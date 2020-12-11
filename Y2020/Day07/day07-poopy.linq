<Query Kind="Statements" />

var path = Path.GetDirectoryName(Util.CurrentQueryPath);
var data = File.ReadAllLines(Path.Combine(path, "input.txt"));

var bags = data
.Select(x => x.Split(new string[] { " bags contain ", "s, ", ", ", "no other bag", "s.", ".", " bag" }, StringSplitOptions.RemoveEmptyEntries))
.Select(x => new { parent = x[0], children = x.Skip(1).Select(y => string.Join(" ", y.Split(' ').Skip(1))) });

Func<string, bool, bool> GetIt = null;
GetIt = (parent, status) =>
{
	foreach (var child in bags.Single(x => x.parent == parent).children)
	{
		if (child == "shiny gold")
		{
			return true;
		}
		else
		{
			status = GetIt(child, false);
		}
	}

	return status;
};

var hierachy = new List<bool>();
foreach (var bag in bags)
{
	hierachy.Add(GetIt(bag.parent, false));
}

// *** STEP 1 ***
var sw1 = Stopwatch.StartNew();
var result1 = hierachy.Count(x => x);
sw1.Stop();

// *** STEP 2 ***
var sw2 = Stopwatch.StartNew();
var result2 = 0;
sw2.Stop();

// *** REPORT ***
@$"==== DAY 07 ====
Part 1
Solution: {result1}
Time: {sw1.ElapsedMilliseconds} ms
Part 2
Solution: {result2}
Time: {sw2.ElapsedMilliseconds} ms".Dump();

