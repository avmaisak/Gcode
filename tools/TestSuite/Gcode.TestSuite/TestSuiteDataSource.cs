using System.IO;

namespace Gcode.TestSuite
{
	public static class TestSuiteDataSource
	{
		//28.gcode.modified
		public static string ReadTextFromFile(string fileName)
		{
			var filePath = $"C:\\Users\\User\\Documents\\Projects\\Github\\!Rb\\Gcode\\misc\\gcode\\{fileName}";
			if (File.Exists(fileName))
			{
				var fileContent = File.ReadAllText(filePath);
				return fileContent;
			}
			return string.Empty;
		}

	}
}
