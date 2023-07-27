namespace CleanAndBat.CrossCutting.Variables;

public static class GlobalVariables
{
	public static class CacheSettings
	{
		public static string MenuKey(int userId) => $"MenuModel_{userId}";


		public static DateTime MenuTimeout() => DateTime.Now.AddHours(3);
	}
}