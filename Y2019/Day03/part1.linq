<Query Kind="Statements">
  <NuGetReference>morelinq</NuGetReference>
  <Namespace>MoreLinq</Namespace>
</Query>

var timer = Stopwatch.StartNew();
var path = Path.GetDirectoryName(Util.CurrentQueryPath);
var data = File.ReadAllText($"{path}\\data.txt");
//var data = "R8,U5,L5,D3\r\nU7,R6,D4,L4"; // 6
//var data = "R75,D30,R83,U83,L12,D49,R71,U7,L72\r\nU62,R66,U55,R34,D71,R55,D58,R83"; // 159
//var data = "R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51\r\nU98,R91,D20,R16,D67,R40,U7,R15,U6,R7"; // 135

int getSteps(string s) => int.Parse(s[1..]);
int getDistance(int x, int y) => Math.Abs(0 - x) + Math.Abs(0 - y);

var points = data
	.Split("\r\n")
	.Select(p => p.Split(',')
	.Scan(new[] { (x: 0, y: 0) }, (pos, dir) =>
	{
		var steps = getSteps(dir);
		var last = pos.Last();
		var range = Enumerable.Range(1, steps);

		return
			(dir[0] == 'U') ? range.Select(p => (last.x, last.y + p)).ToArray() :
			(dir[0] == 'R') ? range.Select(p => (last.x + p, last.y)).ToArray() :
			(dir[0] == 'D') ? range.Select(p => (last.x, last.y - p)).ToArray() :
							  range.Select(p => (last.x - p, last.y)).ToArray();
	}))
	.SelectMany(p => p)
	.SelectMany(p => p);

var intersections = points 
	.GroupBy(p => new { p.x, p.y })
	.Select(g => new { g.Key, Count = g.Count() })
	.Where(g => g.Count > 1)
	.Select(i => getDistance(i.Key.x, i.Key.y))
	.Where(d => d > 0)
	.OrderBy(d => d);
	
var result = intersections.First();
var resultB = intersections.Skip(1).First();

$"Result: {result}".Dump();
$"ResultB: {resultB}".Dump();
$"Time: {timer.ElapsedMilliseconds} ms".Dump();

// 439 = too low
// Erhm...
// Sometimes it's the lowest and sometimes it's the second lowest
// The correct answer for the input is the second lowest