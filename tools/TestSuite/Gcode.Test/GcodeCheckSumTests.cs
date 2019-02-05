using Gcode.Test.Infrastructure;
using Gcode.Utils;
using Gcode.Utils.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gcode.Test
{
	[TestClass]
	public class GcodeCheckSumTests
	{
		[TestMethod]
		public void CheckSumTest1()
		{
			var cmd = TestSuiteDataSource.TestSyntheticCodes[0];
			var g = GcodeParser.ToGCode(cmd);
			g.N = 1;
			var crc = g.FrameCrc();
			Assert.IsInstanceOfType(crc, typeof(int));
		}
		[TestMethod]
		public void CheckSumTest2()
		{
			for (var i = 1; i < TestSuiteDataSource.TestSyntheticCodes.Length; i++)
			{
				var cmd = TestSuiteDataSource.TestSyntheticCodes[i];
				var g = GcodeParser.ToGCode(cmd);
				g.N = i;
				var crc = g.FrameCrc();
				Assert.IsInstanceOfType(crc, typeof(int));
			}
		}
		[TestMethod]
		public void CheckSumTest3()
		{
			for (var i = 1; i < TestSuiteDataSource.TestSyntheticCodes.Length; i++)
			{
				var cmd = TestSuiteDataSource.TestSyntheticCodes[i];
				var g = GcodeParser.ToGCode(cmd);
				g.N = i;
				var crc = g.FrameCrc();
				Assert.IsTrue(crc >= 0, $"{i} {cmd}");
			}
		}
		[TestMethod]
		public void CheckSumTest4()
		{
			var ds = TestSuiteDataSource.Ds100Gcode.Split("\n");
			for (var i = 1; i < ds.Length; i++)
			{
				var cmd = ds[i];
				var g = GcodeParser.ToGCode(cmd);
				g.N = i;
				var crc = g.FrameCrc();
				Assert.IsTrue(crc >= 0, $"{i} {cmd}");
			}
		}
		[TestMethod]
		public void CheckSumTest4M115()
		{
			var cmd = TestSuiteDataSource.TestSyntheticCodes[6];
			var g = GcodeParser.ToGCode(cmd);
			g.N = 6;
			var crc = g.FrameCrc();
			Assert.IsTrue(crc >= 0, $"{cmd}");
		}
		[TestMethod]
		public void CheckSumTest5()
		{
			var gcode = new GcodeCommandFrame();
			try {
				gcode.FrameCrc();
				Assert.Fail(); // If it gets to this line, no exception was thrown
			}
			catch
			{
				// ignored
			}
		}
		[TestMethod]
		public void CheckSumTest6()
		{
			var gcode = new GcodeCommandFrame { N = -1 };
			try {
				gcode.FrameCrc();
				Assert.Fail(); // If it gets to this line, no exception was thrown
			}
			catch
			{
				// ignored
			}
		}
	}
}
