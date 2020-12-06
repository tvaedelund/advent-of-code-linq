<Query Kind="Statements" />

var path = Path.GetDirectoryName(Util.CurrentQueryPath);
var data = File.ReadAllText(Path.Combine(path, "input.txt"));

var answers = data
.Split("\r\n\r\n");

// *** STEP 1 ***
var sw1 = Stopwatch.StartNew();
var result1 = answers
.Select(x => x.Replace("\r\n", ""))
.Select(x => x.Distinct())
.Sum(x => x.Count());
sw1.Stop();

// *** STEP 2 ***
var sw2 = Stopwatch.StartNew();
var result2 = answers
.Select(x => new
{
	persons = x.Split("\r\n").Count(),
	agreed = x
	.GroupBy(x => x)
	.Select(x => x.Count())
})
.Select(x => x.agreed.Where(y => y == x.persons))
.Sum(x => x.Count());
sw2.Stop();

// *** REPORT ***
@$"==== DAY 06 ====
Part 1
Solution: {result1}
Time: {sw1.ElapsedMilliseconds} ms
Part 2
Solution: {result2}
Time: {sw2.ElapsedMilliseconds} ms".Dump();

