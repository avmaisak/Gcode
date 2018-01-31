using System;
using System.Net;

namespace Gcode.TestSuite
{
	public static class TestSuiteDataSource
	{
		public static string GetDataSource(string fileName)
		{
			var uri = new Uri($"https://raw.githubusercontent.com/rus-bit/Gcode/master/misc/gcode/{fileName}");
#pragma warning disable SecurityIntelliSenseCS // MS Security rules violation
			var wc = new WebClient().DownloadString(uri);
#pragma warning restore SecurityIntelliSenseCS // MS Security rules violation

			return wc;

			//var filePath = $"{Directory.GetCurrentDirectory()}\\..\\..\\..\\..\\..\\misc\\gcode\\{fileName}";

			//if (File.Exists(filePath))
			//{
			//	var fileContent = File.ReadAllText(filePath);
			//	return fileContent;
			//}
			//return string.Empty;
		}

	}
}
