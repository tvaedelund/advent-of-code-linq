<Query Kind="Statements">
  <NuGetReference>morelinq</NuGetReference>
  <Namespace>static MoreLinq.Extensions.SubsetsExtension</Namespace>
  <Namespace>System.Numerics</Namespace>
</Query>

var path = Path.GetDirectoryName(Util.CurrentQueryPath);
var test = false;
var input = File.ReadAllLines(Path.Combine(path, test ? "input-test.txt" : "input.txt"));

Func<string, string, string> applyMask = (value, mask) =>
{
	var result = value.ToCharArray();
	
	for (int i = 0; i < mask.Length; i++)
	{
		if (mask[i] != 'X')
		{
			result[i] = mask[i];
		}
	}
	
	return new string(result);
};

Func<string[], long> solvePart1 = (program) =>
{
	var values = new Dictionary<int, long>();
	var i = 0;
	while (i < program.Length)
	{
		var mask = Regex.Match(program[i++], @"^(mask = )(.*)").Groups[2].Value;

		Match mem;
		while (i < program.Length && ((mem = Regex.Match(program[i], @"^(mem\[)(\d*)(\] = )(\d*)")).Success))
		{
			var value = Convert.ToString(int.Parse(mem.Groups[4].Value), 2).PadLeft(36, '0');
			var addr = int.Parse(mem.Groups[2].Value);
			values[addr] = Convert.ToInt64(applyMask(value, mask), 2);
			i++;
		}
	}
		
	return values.Sum(x => x.Value);
};

// *** STEP 1 ***
var sw1 = Stopwatch.StartNew();
var result1 = solvePart1(input);
sw1.Stop();

// *** STEP 2 ***
var sw2 = Stopwatch.StartNew();
var result2 = 0;
sw2.Stop();

// *** REPORT ***
@$"==== DAY 14 ====
Part 1
Solution: {result1}
Time: {sw1.ElapsedMilliseconds} ms
Part 2
Solution: {result2}
Time: {sw2.ElapsedMilliseconds} ms".Dump();

