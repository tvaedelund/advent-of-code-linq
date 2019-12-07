<Query Kind="Program">
  <NuGetReference>morelinq</NuGetReference>
  <Namespace>MoreLinq</Namespace>
</Query>

void Main()
{
	var timer = Stopwatch.StartNew();
	var path = Path.GetDirectoryName(Util.CurrentQueryPath);
	var data = File.ReadAllText($"{path}\\data.txt");
	//var data = "1,0,0,0,99"; // 2
	//var data = "2,3,0,3,99"; // 6
	//var data = "2,4,4,5,99,0"; // 9801
	//var data = "1,1,1,4,99,5,6,0,99"; // 30
	//var data = "1,9,10,3,2,3,11,0,99,30,40,50"; // 3500

	var program = data
		.Split(',')
		.Select(int.Parse);

	for (int noun = 0; noun <= 99; noun++)
	{
		for (int verb = 0; verb <= 99; verb++)
		{
			var temp = RunProgram(noun, verb, new List<int>(program));
			
			if (temp == 19690720)
			{
				var result = 100 * noun + verb;
				$"Result: {result}".Dump();
				$"Time: {timer.ElapsedMilliseconds} ms".Dump();
				return;
			}
		}
	}
}

// Define other methods, classes and namespaces here
int RunProgram(int noun, int verb, List<int> instructions)
{
	instructions[1] = noun;
	instructions[2] = verb;

	var i = 0;
	while (instructions[i] != 99 && i < instructions.Count)
	{
		var input1 = instructions[instructions[i + 1]];
		var input2 = instructions[instructions[i + 2]];
		
		// https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-8#switch-expressions
		var result = instructions[i] switch
		{
			1 => input1 + input2,
			2 => input1 * input2,
			_ => 0
		};

		instructions[instructions[i + 3]] = result;

		i += 4;
	}
	
	return instructions[0];
}
