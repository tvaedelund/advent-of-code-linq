<Query Kind="Statements" />

var path = Path.GetDirectoryName(Util.CurrentQueryPath);
var data = File.ReadAllLines(Path.Combine(path, "input-test2.txt"));

var regColor = new Regex(@"^(\w+ \w+) bags ");
var regBags = new Regex(@"((\d) (\w+ \w+)) bags?");

Func<List<Bag>> GetBags = () =>
{
	var allBags = new List<Bag>();
	foreach (var line in data)
	{
		var bag = new Bag
		{
			Color = regColor.Match(line).Groups[1].Value,
			Bags = new List<(int Count, string Color)>()
		};

		foreach (Match match in regBags.Matches(line).Where(m => m.Groups[2].Value != ""))
		{
			bag.Bags.Add((int.Parse(match.Groups[2].Value), match.Groups[3].Value));
		}

		allBags.Add(bag);
	}
	return allBags;
};

Func<List<Bag>, Bag, bool> ContainsShinyGoldBag = (bags, bag) =>
{
	var stack = new Stack<string>(bag.Bags.Select(x => x.Color));

	while (stack.Count() > 0)
	{
		var current = stack.Pop();

		if (current == "shiny gold")
		{
			return true;
		}

		foreach (var color in bags.Single(x => x.Color == current).Bags.Select(x => x.Color))
		{
			stack.Push(color);
		}
	}

	return false;
};

Func<List<Bag>, int> GetResult1 = (bags) =>
{
	var results = new List<bool>();
	foreach (var bag in bags)
	{
		results.Add(ContainsShinyGoldBag(bags, bag));
	}
	return results.Count(x => x);
};

Func<List<Bag>, int> ShinyGoldBagContains = (bags) =>
{
	var counter = 0;
	var shinyGoldBag = bags.Single(x => x.Color == "shiny gold").Bags;
	var stack = new Stack<(int, string)>();
	foreach (var bag in shinyGoldBag)
	{
		for (int i = 0; i < bag.Count; i++)
		{
			stack.Push((1, bag.Color));
		}
	}
		
	while (stack.Count() > 0)
	{
		var current = stack.Pop();
		counter += current.Item1;

		foreach (var bag in bags.Single(x => x.Color == current.Item2).Bags)
		{
			stack.Push(bag);
		}
	}
	
	return counter;
};

var bags = GetBags();

// *** STEP 1 ***
var sw1 = Stopwatch.StartNew();
var result1 = GetResult1(bags);
sw1.Stop();

// *** STEP 2 ***
var sw2 = Stopwatch.StartNew();
var result2 = ShinyGoldBagContains(bags);
sw2.Stop();

// *** REPORT ***
@$"==== DAY 07 ====
Part 1
Solution: {result1}
Time: {sw1.ElapsedMilliseconds} ms
Part 2
Solution: {result2}
Time: {sw2.ElapsedMilliseconds} ms".Dump();

record Bag
{
	public string Color { get; set; }
	public List<(int Count, string Color)> Bags { get; set; }
}

