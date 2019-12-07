<Query Kind="Statements">
  <NuGetReference>morelinq</NuGetReference>
  <Namespace>MoreLinq</Namespace>
</Query>

var timer = Stopwatch.StartNew();
var path = Path.GetDirectoryName(Util.CurrentQueryPath);
var data = File.ReadAllText($"{path}\\data.txt");
//var data = "1,0,0,0,99"; // 2
//var data = "2,3,0,3,99"; // 6
//var data = "2,4,4,5,99,0"; // 9801
//var data = "1,1,1,4,99,5,6,0,99"; // 30
//var data = "1,9,10,3,2,3,11,0,99,30,40,50"; // 3500

var code = data
	.Split(',')
	.Select(int.Parse)
	.ToArray();
	
code[1] = 12;
code[2] = 2;

var i = 0;
while (code[i] != 99 || i >= code.Length)
{
	var temp = code[i] switch
	{
		1 => code[code[i + 1]] + code[code[i + 2]],
		2 => code[code[i + 1]] * code[code[i + 2]],
		_ => 0
	};
	
	code[code[i + 3]] = temp;
	
	i += 4;
}

var result = code[0];

$"Result: {result}".Dump();
$"Time: {timer.ElapsedMilliseconds} ms".Dump();