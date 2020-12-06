<Query Kind="Statements">
  <Namespace>System.Security.Cryptography</Namespace>
</Query>

var path = Path.GetDirectoryName(Util.CurrentQueryPath);
var data = "bgvyzdsv";

Func<string, string> md5 = (input) =>
{
	using (MD5 md5 = MD5.Create())
	{
		byte[] inputBytes = Encoding.ASCII.GetBytes(input);
		byte[] hashBytes = md5.ComputeHash(inputBytes);
		
		return BitConverter.ToString(hashBytes).Replace("-", string.Empty);
	}
};

// *** STEP 1 ***
var sw1 = Stopwatch.StartNew();
var result1 = 0;
while (!md5($"{data}{result1}").StartsWith("00000"))
{
	result1++;
}
sw1.Stop();

// *** STEP 2 ***
var sw2 = Stopwatch.StartNew();
var result2 = 0;
while (!md5($"{data}{result2}").StartsWith("000000"))
{
	result2++;
}
sw2.Stop();

// *** REPORT ***
@$"==== Y2015 DAY 04 ====
Part 1
Solution: {result1}
Time: {sw1.ElapsedMilliseconds} ms
Part 2
Solution: {result2}
Time: {sw2.ElapsedMilliseconds} ms".Dump();

