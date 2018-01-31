using System.IO;

namespace Gcode.TestSuite
{
	public static class TestSuiteDataSource
	{
		//28.gcode.modified
		public static string GetDataSource(string fileName)
		{
			//var filePath = $"C:\\Users\\User\\Documents\\Proj\\Gcode\\misc\\gcode\\{fileName}";
			var filePath = $"{Directory.GetCurrentDirectory()}\\..\\..\\..\\..\\..\\misc\\gcode\\{fileName}";
			if (File.Exists(filePath))
			{
				var fileContent = File.ReadAllText(filePath);
				return fileContent;
			}
			return string.Empty;
		}

	}
}
