<Query Kind="Statements" />

var path = Path.GetDirectoryName (Util.CurrentQueryPath);
var data = File.ReadAllLines(Path.Combine(path, "day02.txt"));

var sw1 = Stopwatch.StartNew();
var result1 = data
.Select(x => x.Split(new char[] { '-', ' ', ':' }, StringSplitOptions.RemoveEmptyEntries))
.Select(x => Enumerable.Range(int.Parse(x[0]), int.Parse(x[1]) - int.Parse(x[0]) + 1).Contains(x[3].Count(c => c == char.Parse(x[2]))))
.Where(x => x)
.Count();
sw1.Stop();

var sw2 = Stopwatch.StartNew();
var result2 = data
.Select(x => x.Split(new char[] { '-', ' ', ':' }, StringSplitOptions.RemoveEmptyEntries))
.Select(x => (x[3][int.Parse(x[0]) - 1] == char.Parse(x[2]) || x[3][int.Parse(x[1]) - 1] == char.Parse(x[2])) && x[3][int.Parse(x[0]) - 1] != x[3][int.Parse(x[1]) - 1])
.Where(x => x)
.Count();
sw2.Stop();

@$"==== DAY 02 ====
Part 1
Solution: {result1}
Time: {sw1.ElapsedMilliseconds} ms
Part 2
Solution: {result2}
Time: {sw2.ElapsedMilliseconds} ms".Dump();

