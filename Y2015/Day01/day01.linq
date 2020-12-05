<Query Kind="Statements">
  <NuGetReference>morelinq</NuGetReference>
  <Namespace>MoreLinq</Namespace>
  <Namespace>System.Net</Namespace>
</Query>

var path = Path.GetDirectoryName(Util.CurrentQueryPath);
var data = File.ReadAllText(Path.Combine(path, "input.txt"));

// *** STEP 1 ***
var sw1 = Stopwatch.StartNew();
var result1 = data
.Select(x => x == '(' ? 1 : x == ')' ? - 1 : 0)
.Sum();
sw1.Stop();

// *** STEP 2 ***
var sw2 = Stopwatch.StartNew();
var result2 = data
.Select(x => x == '(' ? 1 : x == ')' ? -1 : 0)
.Scan(0, (acc, next) => acc + next)
.Select((x, i) => new { x, i })
.First(x => x.x == -1).i;

sw2.Stop();

// *** REPORT ***
@$"==== DAY 04 ====
Part 1
Solution: {result1}
Time: {sw1.ElapsedMilliseconds} ms
Part 2
Solution: {result2}
Time: {sw2.ElapsedMilliseconds} ms".Dump();

