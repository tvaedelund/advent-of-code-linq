<Query Kind="Statements" />

var path = Path.GetDirectoryName(Util.CurrentQueryPath);
var data = File.ReadAllText(Path.Combine(path, "input.txt"));

// *** STEP 1 ***
var sw1 = Stopwatch.StartNew();
var result1 = 0;
sw1.Stop();

// *** STEP 2 ***
var sw2 = Stopwatch.StartNew();
var result2 = 0;
sw2.Stop();

// *** REPORT ***
@$"==== DAY 04 ====
Part 1
Solution: {result1}
Time: {sw1.ElapsedMilliseconds} ms
Part 2
Solution: {result2}
Time: {sw2.ElapsedMilliseconds} ms".Dump();

