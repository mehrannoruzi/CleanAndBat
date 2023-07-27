namespace CleanAndBat.CrossCutting.Tools;

public class Helper
{
	public static int GetRandomInt(int length)
	{
		return int.Parse(new string((from s in Enumerable.Repeat("123456789", length)
									  select s[new Random().Next(9)]).ToArray()));
	}

	public static long GetRandomLong(int length)
	{
		return long.Parse(new string((from s in Enumerable.Repeat("123456789", length)
									  select s[new Random().Next(9)]).ToArray()));
	}
}