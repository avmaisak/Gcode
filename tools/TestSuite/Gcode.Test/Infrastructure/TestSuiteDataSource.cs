using System.IO;

namespace Gcode.Test.Infrastructure {
	public static class TestSuiteDataSource
	{
		private static readonly string InternalTestFolder = $@"{Directory.GetCurrentDirectory()}\..\..\..\..\..\TestData\";
		public static string Ds100Gcode => GetDataSource("100.gcode");
		public static string[] TestSyntheticCodes { get; } =
		{
			"M206 T3 P200 X89 ;extruder normal steps per mm",
			"G1 X24 Y0 F12000 ;offset ex5 nozzle from kx0",
			"M109 S238  ;wait for hotend",
			"M104 S242",
			"G1 X-70 Y40 F2000 ;move to bed sensr",
			"G1 X553.44 Y251.064 E0.01052 F2196",
			"M115"
		};

		public static string GetDataSource(string fileName)
		{
			return File.ReadAllText($@"{InternalTestFolder}{fileName}");
		}

		public static string[] GetDataSourceArray(string fileName)
		{
			return File.ReadAllLines($"{InternalTestFolder}{fileName}");
		}
	}
}