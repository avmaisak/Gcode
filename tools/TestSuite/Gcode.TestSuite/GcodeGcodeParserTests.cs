
using System;
using Gcode.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gcode.TestSuite
{
	/// <summary>
	/// Gcode command tests
	/// </summary>
	[TestClass]
	public class GcodeGcodeParserTests
	{
		[TestMethod]
		public void FrameSetIsCommentTest1()
		{
			var s = "; head speed 63.800003, filament speed 0.000000, preload 0.000000";
			var res = GcodeParser.IsComment(s);
			Assert.IsTrue(res);
		}
		[TestMethod]
		public void FrameSetIsCommentTest2()
		{
			var s = "   ; head speed 63.800003, filament speed 0.000000, preload 0.000000";
			var res = GcodeParser.IsComment(s);
			Assert.IsTrue(res);
		}

		[TestMethod]
		public void FrameSetIsCommentTest3()
		{
			var s = " head speed 63.800003, filament speed 0.000000, preload 0.000000";
			var res = GcodeParser.IsComment(s);
			Assert.IsFalse(res);
		}
		[TestMethod]
		public void FrameSetIsCommentTest4()
		{
			var s = "; 'Destring/Wipe/Jump Path', 0.0 [feed mm/s], 63.8 [head mm/s]";
			var res = GcodeParser.IsComment(s);
			Assert.IsTrue(res);
		}
		[TestMethod]
		public void FrameSetIsCommentTest5()
		{
			var s = ";";
			var res = GcodeParser.IsComment(s);
			Assert.IsTrue(res);
		}
		[TestMethod]
		public void FrameSetIsCommentTest6()
		{
			var s = "     ;     ";
			var res = GcodeParser.IsComment(s);
			Assert.IsTrue(res);
		}

		[TestMethod]
		public void FrameSetIsCommentTest7()
		{
			var s = string.Empty;
			var res = GcodeParser.IsComment(s);
			Assert.IsFalse(res);
		}

		[TestMethod]
		public void FrameSetIsCommentTest8()
		{
			var res = GcodeParser.IsComment(null);
			Assert.IsFalse(res);
		}
	}
}
