<Query Kind="Statements" />

var path = Path.GetDirectoryName(Util.CurrentQueryPath);
var data = File.ReadAllText(Path.Combine(path, "day04.txt"));

var expected = new string[] { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };

var passports = data
.Split("\r\n\r\n");

var valid = 0;
foreach (var p in passports)
{
	var pattern = new Regex(@"(byr:(19[2-8][0-9]|199[0-9]|200[0-2]))|(iyr:(201[0-9]|2020))|(eyr:(202[0-9]|2030))|((hgt:(1[5-8][0-9]|19[0-3])cm|hgt:(59|6[0-9]|7[0-6])in))|(hcl:#([0-9]|[a-f])([0-9]|[a-f])([0-9]|[a-f])([0-9]|[a-f])([0-9]|[a-f])([0-9]|[a-f]))|(ecl:(amb|blu|brn|gry|grn|hzl|oth))|(\bpid:\d{9}\b)");
	if (pattern.Matches(p).Count == 7)
	{
		valid++;
	}
}

valid.Dump();

