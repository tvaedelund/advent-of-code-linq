<Query Kind="Statements" />

var timer = Stopwatch.StartNew();
var path = Path.GetDirectoryName(Util.CurrentQueryPath);
var data = File.ReadAllText($"{path}\\data.txt");
//var data = "12"; // 2
//var data = "14"; // 2
//var data = "1969"; // 654
//var data = "100756"; // 33583

var fuel = data
	.Split("\r\n")
	.Select(int.Parse)
	.Select(f => Math.Floor(f / 3d)  - 2);
	
var result = fuel
	.Sum();

$"Result: {result}".Dump();
$"Time: {timer.ElapsedMilliseconds} ms".Dump();