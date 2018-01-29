using System;

namespace Gcode.Utils.Infrastructure
{
	public static class StringExtensions
	{
		public static string[] Split(this string str, string splitter)
		{
			return str.Split(new[] { splitter }, StringSplitOptions.None);
		}
	}
}
