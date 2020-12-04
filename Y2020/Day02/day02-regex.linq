<Query Kind="Statements" />

var path = Path.GetDirectoryName (Util.CurrentQueryPath);
var data = File.ReadAllLines(Path.Combine(path, "day02.txt"));

var sw1 = Stopwatch.StartNew();
var result1 = data
.Select(x => Regex.Match(x, @"(\d+)-(\d+)\s(\w):\s(\w+)", RegexOptions.Compiled).Groups)
.Select(x => Enumerable.Range(int.Parse(x[1].Value), int.Parse(x[2].Value) - int.Parse(x[1].Value) + 1).Contains(x[4].Value.Count(c => c == char.Parse(x[3].Value))))
.Where(x => x)
.Count();
sw1.Stop();

var sw2 = Stopwatch.StartNew();
var result2 = data
.Select(x => Regex.Match(x, @"(\d+)-(\d+)\s(\w):\s(\w+)", RegexOptions.Compiled).Groups)
.Select(x => (x[4].Value[int.Parse(x[1].Value) - 1] == char.Parse(x[3].Value) || x[4].Value[int.Parse(x[2].Value) - 1] == char.Parse(x[3].Value)) && x[4].Value[int.Parse(x[1].Value) - 1] != x[4].Value[int.Parse(x[2].Value) - 1])
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

