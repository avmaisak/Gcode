using Gcode.Common.Utils;
using Gcode.Entity;
using Gcode.TestSuite.Infrastructure;
using Gcode.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gcode.TestSuite
{
	/// <summary>
	/// Gcode command tests
	/// </summary>
	[TestClass]
	public class GcodeParserTests
	{
		[TestMethod]
		public void GcodeParserSyntheticTests1()
		{
			foreach (var r in TestSuiteDataSource.TestSyntheticCodes)
			{
				var gcode = GcodeParser.ToGCode(r);
				Assert.IsInstanceOfType(gcode, typeof(GcodeCommandFrame));
			}
		}
		[TestMethod]
		public void GcodeParserTests1()
		{
			var ds = TestSuiteDataSource.Ds100Gcode.Split("\n");;
			for (var i = 0; i < ds.Length; i++)
			{
				var r = ds[i];
				
				var gcode = GcodeParser.ToGCode(r);
				Assert.IsInstanceOfType(gcode, typeof(GcodeCommandFrame), $"{r}");
			}
		}

		[TestMethod]
		public void GcodeParserTests2()
		{
			//M206 T3 P200 X89 ; extruder normal steps per mm
			var ds = TestSuiteDataSource.TestSyntheticCodes[0];
			var gcode = GcodeParser.ToGCode(ds);
			Assert.IsNotNull(gcode.M);
			Assert.AreEqual(206,gcode.M.Value);
			Assert.IsNotNull(gcode.T);
			Assert.AreEqual(3,gcode.T.Value);
			Assert.IsNotNull(gcode.P);
			Assert.AreEqual(200,gcode.P.Value);
			Assert.IsNotNull(gcode.X);
			Assert.AreEqual(89,gcode.X.Value);
			Assert.AreEqual("extruder normal steps per mm",gcode.Comment);
		}
	}
}
