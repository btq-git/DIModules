namespace BTQ.Core.Extensions
{
	public static class StringExtension
	{
		public static bool IsNullOrWhiteSpace(this string str)
		{
			return string.IsNullOrWhiteSpace(str);
		}

		public static bool IsNullOrEmpty(this string str)
		{
			return string.IsNullOrEmpty(str);
		}

		public static string OrEmpty(this string val)
		{
			return val ?? string.Empty;
		}
	}
}