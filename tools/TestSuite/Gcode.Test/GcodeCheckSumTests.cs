using Gcode.Test.Infrastructure;
using Gcode.Utils;
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
			var crc = GcodeCrc.FrameCrc(g);
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
				var crc = GcodeCrc.FrameCrc(g);
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
				var crc = GcodeCrc.FrameCrc(g);
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
				var crc = GcodeCrc.FrameCrc(g);
				Assert.IsTrue(crc >= 0, $"{i} {cmd}");
			}
		}
		[TestMethod]
		public void CheckSumTest4M115()
		{
			var cmd = TestSuiteDataSource.TestSyntheticCodes[6];
			var g = GcodeParser.ToGCode(cmd);
			g.N = 6;
			var crc = GcodeCrc.FrameCrc(g);
			Assert.IsTrue(crc >= 0, $"{cmd}");
		}
	}
}
