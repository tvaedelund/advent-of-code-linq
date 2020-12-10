<Query Kind="Statements">
  <NuGetReference>morelinq</NuGetReference>
  <Namespace>MoreLinq</Namespace>
  <Namespace>System.Security.Cryptography</Namespace>
</Query>

var path = Path.GetDirectoryName(Util.CurrentQueryPath);
var data = File.ReadAllLines(Path.Combine(path, "input.txt"));

Func<string, bool> hasVowels = (input) =>
{
	return input.ToCharArray().Count(c => "aeiou".ToCharArray().Contains(c)) >= 3;
};

Func<string, bool> hasDoubleLetters = (input) =>
{
	return input.ToCharArray().Window(2).Select(x => x.Distinct()).Where(x => x.Count() == 1).Any();
};

Func<string, bool> hasNoInvalidCombos = (input) =>
{
	return !input.ToCharArray().Window(2).Select(x => string.Join("", x)).Intersect(new[] { "ab", "cd", "pq", "xy"}).Any();
};

Func<string, bool> isValid = (input) =>
{
	return hasVowels(input) && hasDoubleLetters(input) && hasNoInvalidCombos(input);
};

// *** STEP 1 ***
var sw1 = Stopwatch.StartNew();
var result1 = 0;
foreach (var line in data)
{
	result1 += isValid(line) ? 1 : 0;
}
sw1.Stop();

// *** STEP 2 ***
var sw2 = Stopwatch.StartNew();
var result2 = 0;
sw2.Stop();

// *** REPORT ***
@$"==== Y2015 DAY 05 ====
Part 1
Solution: {result1}
Time: {sw1.ElapsedMilliseconds} ms
Part 2
Solution: {result2}
Time: {sw2.ElapsedMilliseconds} ms".Dump();

