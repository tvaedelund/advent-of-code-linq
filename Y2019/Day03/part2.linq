<Query Kind="Statements">
  <NuGetReference>morelinq</NuGetReference>
  <Namespace>MoreLinq</Namespace>
</Query>

var timer = Stopwatch.StartNew();
var path = Path.GetDirectoryName(Util.CurrentQueryPath);
var data = File.ReadAllText($"{path}\\data.txt");
//var data = "R8,U5,L5,D3\r\nU7,R6,D4,L4"; // 30
//var data = "R75,D30,R83,U83,L12,D49,R71,U7,L72\r\nU62,R66,U55,R34,D71,R55,D58,R83"; // 610
//var data = "R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51\r\nU98,R91,D20,R16,D67,R40,U7,R15,U6,R7"; // 410

int getSteps(string s) => int.Parse(s[1..]);

var wires = data
	.Split("\r\n")
	.Select(p => p.Split(',')
	.Scan(new[] { (x: 0, y: 0, s: 0) }, (pos, dir) =>
		{
			var steps = getSteps(dir);
			var last = pos.Last();
			var range = Enumerable.Range(1, steps);

			return dir[0] switch
			{
				'U' => range.Select(p => (last.x, last.y + p, last.s + p)).ToArray(),
				'R' => range.Select(p => (last.x + p, last.y, last.s + p)).ToArray(),
				'D' => range.Select(p => (last.x, last.y - p, last.s + p)).ToArray(),
				_ => range.Select(p => (last.x - p, last.y, last.s + p)).ToArray()
			};
		})
		.SelectMany(p => p)
	);
	
var distinctwires = wires
	.Select(ws => ws
		.DistinctBy(w => (x: w.x, y: w.y))
	)
	.SelectMany(ws => ws);
				
var intersections = distinctwires
	.GroupBy(p => new { p.x, p.y })
	.Select(g => new { g.Key, Count = g.Count(), Steps = g.Select(gs => gs.s) })
	.Where(g => g.Count > 1)
	.Select(i => i.Steps.Sum())
	.Where(d => d > 0)
	.OrderBy(d => d);
	
var result = intersections.First();

$"Result: {result}".Dump();
$"Time: {timer.ElapsedMilliseconds} ms".Dump();