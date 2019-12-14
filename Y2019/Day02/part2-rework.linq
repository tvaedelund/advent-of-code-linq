<Query Kind="Program" />

void Main()
{
	var timer = Stopwatch.StartNew();
	var path = Path.GetDirectoryName(Util.CurrentQueryPath);

	//var data = "1,0,0,0,99"; // 2
	//var computer = new Computer(data, 0, 0);

	//var data = "2,3,0,3,99"; // 6
	//var computer = new Computer(data, 3, 0);

	//var data = "2,4,4,5,99,0"; // 9801
	//var computer = new Computer(data, 4, 4);

	//var data = "1,1,1,4,99,5,6,0,99"; // 30
	//var computer = new Computer(data, 1, 1);

	//var data = "1,9,10,3,2,3,11,0,99,30,40,50"; // 3500
	//var computer = new Computer(data, 9, 10);

	var data = File.ReadAllText($"{path}\\data.txt");
	
	var result = Runner(data);

	$"Result: {result}".Dump();
	$"Time: {timer.ElapsedMilliseconds} ms".Dump();
}

int Runner(string data)
{
	for (int noun = 0; noun <= 99; noun++)
	{
		for (int verb = 0; verb <= 99; verb++)
		{
			var computer = new Computer(data, noun, verb);

			computer.Run();

			if (computer.Program[0] == 19690720)
			{
				return 100 * noun + verb;
			}
		}
	}
	
	return 0;
}

// Define other methods, classes and namespaces here
class Computer
{
	public int[] Program { get; set; }
	public int Pos { get; set; }
	public int Opcode => Program[Pos];

	public Computer(string program, int pos1, int pos2)
	{
		Program = program
			.Split(',')
			.Select(int.Parse)
			.ToArray();

		Program[1] = pos1;
		Program[2] = pos2;

		Pos = 0;
	}

	public void Run()
	{
		while (Opcode != 99 && Pos < Program.Length)
		{
			if (Opcode == 1)
			{
				OpcodeAdd();
				continue;
			}

			if (Opcode == 2)
			{
				OpcodeMul();
				continue;
			}
		}
	}

	public int GetValue(int valueAt)
	{
		return Program[Program[Pos + valueAt]];
	}

	public void SetValue(int value)
	{
		Program[Program[Pos + 3]] = value;
	}

	public void OpcodeAdd()
	{
		var num1 = GetValue(1);
		var num2 = GetValue(2);

		SetValue(num1 + num2);

		Pos += 4;
	}

	public void OpcodeMul()
	{
		var num1 = GetValue(1);
		var num2 = GetValue(2);

		SetValue(num1 * num2);

		Pos += 4;
	}
}