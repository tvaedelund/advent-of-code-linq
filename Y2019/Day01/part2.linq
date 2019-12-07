<Query Kind="Statements">
  <NuGetReference>morelinq</NuGetReference>
  <Namespace>MoreLinq</Namespace>
</Query>

var timer = Stopwatch.StartNew();
var path = Path.GetDirectoryName(Util.CurrentQueryPath);
var data = File.ReadAllText($"{path}\\data.txt");
//var data = "14"; // 2
//var data = "1969"; // 966
//var data = "100756"; // 50346

int getFuel(int f) => (int)Math.Floor(f / 3d)  - 2;

var fuel = data
	.Split("\r\n")
	.Select(int.Parse)
	.Scan(0, (s, f) => MoreEnumerable.Unfold(f, s => s, s => getFuel(s) > 0, s => getFuel(s), s => getFuel(s)).Sum());
	
var result = fuel
	.Sum();

$"Result: {result}".Dump();
$"Time: {timer.ElapsedMilliseconds} ms".Dump();