<Query Kind="Statements">
  <NuGetReference>morelinq</NuGetReference>
  <Namespace>MoreLinq</Namespace>
</Query>

var input = Enumerable.Range(178416, 676461 - 178416 + 1);

var part1 = input
	.Where(x => x == int.Parse(string.Join("", x.ToString().OrderBy(x => x))))
	.Where(x => x.ToString().Pairwise((a, b) => new { a, b }).Any(p => p.a == p.b));

part1.Count().Dump();

var part2 = input
	.Where(x => x == int.Parse(string.Join("", x.ToString().OrderBy(x => x))))
	.Where(x => x.ToString().GroupBy(c => c).Select(g => new { g.Key, Count = g.Count() }).Any(g => g.Count == 2));

part2.Count().Dump();