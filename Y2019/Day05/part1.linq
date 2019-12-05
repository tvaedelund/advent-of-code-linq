<Query Kind="Statements" />

var timer = Stopwatch.StartNew();
var path = Path.GetDirectoryName(Util.CurrentQueryPath);
var data = File.ReadAllText($"{path}\\data.txt");
var result = 0;

$"Result: {result}".Dump();
$"Time: {timer.ElapsedMilliseconds} ms".Dump();