using Gcode.Common.Utils;
using Gcode.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gcode.TestSuite
{
	[TestClass]
	public class GcodeCheckSumTests
	{
		private static string Ds100Gcode => TestSuiteDataSource.GetDataSource("100.gcode");
		private static string BigFile => TestSuiteDataSource.GetDataSource("pattern_blade_fp_piece2_v1.gcode");
		
		[TestMethod]
		public void GcodeCheckSumTest1()
		{
			var gcodeCommands = BigFile.Split("\n");

			for (var i = 1; i < gcodeCommands.Length; i++)
			{
				var frame = gcodeCommands[i];
				var parser = new GcodeParser(frame);

				if (parser.IsComment) continue;
				var frameCrc = GcodeCrc.FrameCrc(i, frame);
				Assert.IsInstanceOfType(frameCrc, typeof(int));

			}
		}
		[TestMethod]
		public void GcodeCheckSumTest2()
		{
			var gcodeCommands = BigFile.Split("\n");
			if (gcodeCommands == null || gcodeCommands.Length == 0)
			{
				return;
			}

			for (var i = 1; i < gcodeCommands.Length; i++)
			{
				var frame = gcodeCommands[i];
				var parser = new GcodeParser(frame);

				if (parser.IsComment) continue;
				var frameCrc = GcodeCrc.FrameCrc(i, frame);
				Assert.IsTrue(frameCrc >= 0, $"CRC: {frameCrc} Failed at {i},frame: {frame} ");

			}
		}
		[TestMethod]
		public void GcodeCheckSumTest3()
		{
			var gcodeCommands = Ds100Gcode.Split("\n");
			if (gcodeCommands == null || gcodeCommands.Length == 0)
			{
				return;
			}

			for (var i = 1; i < gcodeCommands.Length; i++)
			{
				var frame = gcodeCommands[i];
				var parser = new GcodeParser(frame);

				if (parser.IsComment) continue;
				var frameCrc = GcodeCrc.FrameCrc(i, frame);
				Assert.IsTrue(frameCrc >= 0, $"CRC: {frameCrc} Failed at {i},frame: {frame} ");

			}
		}
	}
}
