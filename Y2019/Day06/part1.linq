<Query Kind="Program">
  <NuGetReference>morelinq</NuGetReference>
  <Namespace>MoreLinq</Namespace>
</Query>

void Main()
{
	var timer = Stopwatch.StartNew();
	var path = Path.GetDirectoryName(Util.CurrentQueryPath);
	var input = File.ReadAllLines($"{path}\\data.txt");
	//var input = File.ReadAllLines($"{path}\\data.test.txt");

	// get all objects and it's parents (object, parent)
	var objects = input
		.Select(o => o.Split(')'))
		.Scan((o: "COM", p: (string)null), (state, next) => (state.o = next[1], state.p = next[0]))
		.ToLookup(o => o.p);

	// recursively build tree and add level to each object
	Func<string, int, IEnumerable<Planet>> build = null;
	build = (curr, lvl) =>
		objects[curr]
			.Select(x =>
			{
				var nextLvl = lvl + 1;
				return new Planet { c = x.o, ch = build(x.o, nextLvl), lvl = nextLvl };
			});

	var orbits = build(null, -1)
		.First();

	// traverse tree and sum levels
	var result = MoreEnumerable.TraverseBreadthFirst(orbits, o => o.ch)
		.Sum(o => o.lvl);

	$"Result: {result}".Dump();
	$"Time: {timer.ElapsedMilliseconds} ms".Dump();
}

class Planet
{
	public string c { get; set; }
	public IEnumerable<Planet> ch { get; set; }
	public int lvl { get; set; }
}