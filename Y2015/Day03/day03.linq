<Query Kind="Statements">
  <NuGetReference>morelinq</NuGetReference>
  <Namespace>MoreLinq</Namespace>
</Query>

var path = Path.GetDirectoryName(Util.CurrentQueryPath);
var data = File.ReadAllText(Path.Combine(path, "input.txt"));

Func<char, (int, int), (int, int)> move = (char c, (int x, int y) p) =>
{
	return c switch
	{
		'^' => (p.x, p.y += 1),
		'>' => (p.x + 1, p.y),
		'v' => (p.x, p.y -= 1),
		'<' => (p.x - 1, p.y)
	};
};

// *** STEP 1 ***
var sw1 = Stopwatch.StartNew();
var houses = data
.Select(x => x)
.Scan((0, 0), (h, c) => move(c, h))
.GroupBy(h => h)
.Count(x => x.Count() > 0);
var result1 = houses;
sw1.Stop();

// *** STEP 2 ***
var sw2 = Stopwatch.StartNew();
var santa = data
.Where((x, i) => i % 2 == 0)
.Select(x => x)
.Scan((0, 0), (h, c) => move(c, h));

var elf = data
.Where((x, i) => (i + 1) % 2 == 0)
.Select(x => x)
.Scan((0, 0), (h, c) => move(c, h));

var result2 = santa
.Concat(elf)
.GroupBy(h => h)
.Count(x => x.Count() > 0);
sw2.Stop();

// *** REPORT ***
@$"==== Y2015 D03 ====
Part 1
Solution: {result1}
Time: {sw1.ElapsedMilliseconds} ms
Part 2
Solution: {result2}
Time: {sw2.ElapsedMilliseconds} ms".Dump();

