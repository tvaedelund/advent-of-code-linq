<Query Kind="Statements" />

var path = Path.GetDirectoryName(Util.CurrentQueryPath);
var data = File.ReadAllLines(Path.Combine(path, "input.txt"));

// *** STEP 1 ***
var sw1 = Stopwatch.StartNew();
var paper = data
.Select(x => x.Split('x').Select(int.Parse).OrderBy(y => y).ToArray())
.Select(x => 3 * x[0] * x[1] + 2 * x[0] * x[2] + 2 * x[1] * x[2]);
var result1 = paper.Sum();
sw1.Stop();

// *** STEP 2 ***
var sw2 = Stopwatch.StartNew();
var ribbon = data
.Select(x => x.Split('x').Select(int.Parse).OrderBy(y => y).ToArray())
.Select(x => 2 * x[0] + 2 * x[1] + x[0] * x[1] * x[2]);
var result2 = ribbon.Sum();
sw2.Stop();

// *** REPORT ***
@$"==== Y2015 D02 ====
Part 1
Solution: {result1}
Time: {sw1.ElapsedMilliseconds} ms
Part 2
Solution: {result2}
Time: {sw2.ElapsedMilliseconds} ms".Dump();

