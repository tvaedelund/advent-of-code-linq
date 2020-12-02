<Query Kind="Statements" />

var path = Path.GetDirectoryName (Util.CurrentQueryPath);
var data = File.ReadAllLines(Path.Combine(path, "day01.txt")).Select(int.Parse);

var sw1 = Stopwatch.StartNew();
var set1 = data.SelectMany(a => data.Select(b => new { a, b, sum = a + b }));
var item1 = set1.First(x => x.sum == 2020); 
var result1 = (item1.a * item1.b);
sw1.Stop();

var sw2 = Stopwatch.StartNew();
var set2 = set1.SelectMany(ab => data.Select(c => new { a = ab.a, b = ab.b, c, sum = ab.a + ab.b + c }));
var item2 = set2.First(x => x.sum == 2020);
var result2 = (item2.a * item2.b * item2.c);
sw2.Stop();

@$"==== DAY 01 ====
Part 1
Solution: {result1}
Time: {sw1.ElapsedMilliseconds} ms
Part 2
Solution: {result2}
Time: {sw2.ElapsedMilliseconds} ms".Dump();

