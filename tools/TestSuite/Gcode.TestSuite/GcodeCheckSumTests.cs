using Gcode.Utils;
using Gcode.Utils.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gcode.TestSuite
{
	[TestClass]
	public class GcodeCheckSumTests
	{

		[TestMethod]
		public void GcodeCheckSumTest1()
		{
			var gcodeCommands = TestSuiteDataSource.ReadTextFromFile("pattern_blade_fp_piece2_v1.gcode").Split("\r\n");
			if (gcodeCommands == null || gcodeCommands.Length == 0)
			{
				return;
			}

			for (var i = 1; i < gcodeCommands.Length; i++)
			{
				var frame = gcodeCommands[i];
				var parser = new GcodeParser(frame);

				if (!parser.IsComment)
				{
					var frameCrc = GcodeCrc.FrameCrc(i, frame);
					Assert.IsInstanceOfType(frameCrc, typeof(int));
				}

			}
		}
		[TestMethod]
		public void GcodeCheckSumTest2()
		{
			var gcodeCommands = TestSuiteDataSource.ReadTextFromFile("pattern_blade_fp_piece2_v1.gcode").Split("\r\n");
			if (gcodeCommands == null || gcodeCommands.Length == 0)
			{
				return;
			}

			for (var i = 1; i < gcodeCommands.Length; i++)
			{
				var frame = gcodeCommands[i];
				var parser = new GcodeParser(frame);

				if (!parser.IsComment)
				{
					var frameCrc = GcodeCrc.FrameCrc(i, frame);
					Assert.IsTrue(frameCrc >= 0, $"CRC: {frameCrc} Failed at {i},frame: {frame} ");
				}

			}
		}

		[TestMethod]
		public void GcodeCheckSumTest3()
		{
			var gcodeCommands = TestSuiteDataSource.ReadTextFromFile("100.gcode").Split("\r\n");
			if (gcodeCommands == null || gcodeCommands.Length == 0)
			{
				return;
			}

			for (var i = 1; i < gcodeCommands.Length; i++)
			{
				var frame = gcodeCommands[i];
				var parser = new GcodeParser(frame);

				if (!parser.IsComment)
				{
					var frameCrc = GcodeCrc.FrameCrc(i, frame);
					Assert.IsTrue(frameCrc >= 0, $"CRC: {frameCrc} Failed at {i},frame: {frame} ");
				}

			}
		}
	}
}
