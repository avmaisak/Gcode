using System;
using System.Text;

namespace Gcode.Common.Utils
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
		/// <summary>
		/// Replace a string char at index with another char
		/// </summary>
		/// <param name="text">string to be replaced</param>
		/// <param name="index">position of the char to be replaced</param>
		/// <param name="c">replacement char</param>
		public static string ReplaceAtIndex(this string text, int index, char c)
		{
			var stringBuilder = new StringBuilder(text) {[index] = c};
			return stringBuilder.ToString();
		}
	}
}
