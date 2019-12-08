<Query Kind="Statements">
  <NuGetReference>morelinq</NuGetReference>
  <Namespace>MoreLinq</Namespace>
</Query>

var timer = Stopwatch.StartNew();
var path = Path.GetDirectoryName(Util.CurrentQueryPath);
var data = File.ReadAllText($"{path}\\data.txt");
//var data = "123456789012";

var image = data
	.Batch(25 * 6);
	
//image.Dump();

var layerWithLeastZeroes = image
	.Select(r => (r: r, c: r.Count(c => c == '0')))
	.OrderBy(r => r.c)
	.Select(r => r.r)
	.First();

//layerWithLeastZeroes.Dump();

var result = layerWithLeastZeroes.Count(c => c == '1') * layerWithLeastZeroes.Count(c => c == '2');

$"Result: {result}".Dump();
$"Time: {timer.ElapsedMilliseconds} ms".Dump();