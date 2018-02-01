using System;
using System.Globalization;
using System.Net;

namespace Gcode.TestSuite
{
	public static class TestSuiteDataSource
	{
		public static string GetDataSource(string fileName)
		{
			var uri = new Uri($"https://downloads.s1.rus-bit.com/public/Gcode/{fileName}".ToString(CultureInfo.InvariantCulture));
#pragma warning disable SecurityIntelliSenseCS // MS Security rules violation
			using (var wc = new WebClient())
			{
				return wc.DownloadString(uri);
			}
#pragma warning restore SecurityIntelliSenseCS // MS Security rules violation
		}
	}
}
