<Query Kind="Statements">
  <NuGetReference>morelinq</NuGetReference>
  <Namespace>MoreLinq</Namespace>
</Query>

var timer = Stopwatch.StartNew();
var path = Path.GetDirectoryName(Util.CurrentQueryPath);
var data = File.ReadAllText($"{path}\\data.txt");
var size = 25 * 6;

//var data = "0222112222120000";
//var size = 2 * 2;

var image = data
	.Batch(size)
	.Select(l => l.ToArray());

//image.Dump();

var decoded = new char[size];

for (int i = 0; i < size; i++)
{
	foreach (var layer in image)
	{
		if (layer[i] == '2')
		{
			continue;
		}
		
		decoded[i] = layer[i];
		break;
	}
}

foreach (var row in decoded.Batch(25))
{
	foreach (var pixel in row)
	{
		Console.Write((pixel == '1') ? '#' : ' ');
	}
	
	Console.WriteLine();
}

$"Time: {timer.ElapsedMilliseconds} ms".Dump();