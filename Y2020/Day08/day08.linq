<Query Kind="Statements" />

var path = Path.GetDirectoryName(Util.CurrentQueryPath);
var data = File.ReadAllLines(Path.Combine(path, "input.txt"));

var instructions = data
.Select(x => Regex.Match(x, @"^(\w{3}) ([+|-]\d+)"))
.Select(x => new Instruction { Operation = x.Groups[1].Value, Value = int.Parse(x.Groups[2].Value) });

Func<IEnumerable<Instruction>, (int pos, int acc)> execute = (instructions) =>
{
	var acc = 0;
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
	
	return (pos, acc);
};

// *** STEP 1 ***
var sw1 = Stopwatch.StartNew();
var (pos, acc) = execute(instructions);
var result1 = acc;
sw1.Stop();

// *** STEP 2 ***
var sw2 = Stopwatch.StartNew();
var i = 0;
while (pos < instructions.Count())
{
	var modifiedInstr = instructions.ToList();
	i = modifiedInstr.FindIndex(i, x => x.Operation == "jmp" || x.Operation == "nop");
	modifiedInstr[i].Operation = modifiedInstr[i].Operation switch
	{
		"jmp" => "nop",
		"nop" => "jmp",
		_ => throw new Exception()
	};
	(pos, acc) = execute(modifiedInstr);
	i++;
}
var result2 = acc;
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

