using System;

namespace Gcode.Utils.Infrastructure
{
	public static class StringExtensions
	{
		public static string[] Split(this string str, string splitter)
		{
			return str.Split(new[] { splitter }, StringSplitOptions.None);
		}
		public static string TrimString(this string str)
		{
			var res = string.Empty;

			if (str == null)
			{
				return string.Empty;
			}

			if (!string.IsNullOrWhiteSpace(str))
			{
				res = str.Trim();
			}

			return res;
		}
	}
}
