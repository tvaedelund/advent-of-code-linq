<Query Kind="Statements" />

var path = Path.GetDirectoryName(Util.CurrentQueryPath);
var data = File.ReadAllLines(Path.Combine(path, "input.txt"));

var instructions = data
.Select(x => Regex.Match(x, @"^(\w{3}) ([+|-]\d+)"))
.Select(x => new Instruction { Operation = x.Groups[1].Value, Value = int.Parse(x.Groups[2].Value) });

var acc = 0;
Func<IEnumerable<Instruction>, int> execute = (instructions) =>
{
	acc = 0;
	var pos = 0;
	var history = new List<int>();

	while (!history.Any(x => x == pos) && pos < instructions.Count())
	{
		history.Add(pos);

		var instr = instructions.ElementAt(pos);
		pos += instr.Operation switch
		{
			"acc" => ((Func<int>)(() =>
			{
				acc += instr.Value;
				return 1;
			}))(),
			"jmp" => instr.Value,
			"nop" => 1,
			_ => throw new Exception()
		};
	}
	
	return pos;
};

// *** STEP 1 ***
var sw1 = Stopwatch.StartNew();
execute(instructions);
var result1 = acc;
sw1.Stop();

// *** STEP 2 ***
var sw2 = Stopwatch.StartNew();

var result2 = 0;
sw2.Stop();

// *** REPORT ***
@$"==== DAY 08 ====
Part 1
Solution: {result1}
Time: {sw1.ElapsedMilliseconds} ms
Part 2
Solution: {result2}
Time: {sw2.ElapsedMilliseconds} ms".Dump();

record Instruction
{
	public string Operation { get; set; }
	public int Value { get; set; }
}

