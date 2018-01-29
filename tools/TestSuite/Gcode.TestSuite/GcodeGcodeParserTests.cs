
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
			var res = new GcodeParser(s).IsComment;
			Assert.IsTrue(res);
		}
		[TestMethod]
		public void FrameSetIsCommentTest2()
		{
			var s = "   ; head speed 63.800003, filament speed 0.000000, preload 0.000000";

			var res = new GcodeParser(s).IsComment;
			Assert.IsTrue(res);
		}

		[TestMethod]
		public void FrameSetIsCommentTest3()
		{
			var s = " head speed 63.800003, filament speed 0.000000, preload 0.000000";
			var res = new GcodeParser(s).IsComment;
			Assert.IsFalse(res);
		}
		[TestMethod]
		public void FrameSetIsCommentTest4()
		{
			var s = "; 'Destring/Wipe/Jump Path', 0.0 [feed mm/s], 63.8 [head mm/s]";
			var res = new GcodeParser(s).IsComment;
			Assert.IsTrue(res);
		}
		[TestMethod]
		public void FrameSetIsCommentTest5()
		{
			var s = ";";
			var res = new GcodeParser(s).IsComment;
			Assert.IsTrue(res);
		}
		[TestMethod]
		public void FrameSetIsCommentTest6()
		{
			var s = "     ;     ";
			var res = new GcodeParser(s).IsComment;
			Assert.IsTrue(res);
		}

		[TestMethod]
		public void FrameSetIsCommentTest7()
		{
			var s = string.Empty;
			var res = new GcodeParser(s).IsComment;
			Assert.IsFalse(res);
		}

		[TestMethod]
		public void FrameSetIsCommentTest8()
		{
			var res = new GcodeParser(null).IsComment;
			Assert.IsFalse(res);
		}

		[TestMethod]
		public void FrameSetContainsCommentTest1()
		{
			var res = new GcodeParser(null).ContainsComment;
			Assert.IsFalse(res);
		}
		[TestMethod]
		public void FrameSetContainsCommentTest2()
		{
			
			var res = new GcodeParser("").ContainsComment;
			Assert.IsFalse(res);
		}
		[TestMethod]
		public void FrameSetContainsCommentTest3()
		{
			var res = new GcodeParser("; >   M206 T3 P149 X700.0 ; Y max len = 600.0").ContainsComment;
			Assert.IsFalse(res);
		}

		[TestMethod]
		public void FrameSetContainsCommentTest4()
		{
			var res = new GcodeParser("G1 X551.135 Y348.935 E1.23722;mooove").ContainsComment;
			Assert.IsFalse(res);
		}

		[TestMethod]
		public void FrameSetContainsCommentTest5()
		{
			var res = new GcodeParser("G1 X551.135 Y348.935 E1.23722  ;    mooove").ContainsComment;
			Assert.IsFalse(res);
		}
		[TestMethod]
		public void FrameSetContainsCommentTest6()
		{
			var res = new GcodeParser("G1 X551.135 Y348.935 E1.23722 ; mooove; dsddffs ;;;").ContainsComment;
			Assert.IsFalse(res);
		}

		[TestMethod]
		public void FrameSetContainsCommentTest7()
		{
			var res = new GcodeParser(";").ContainsComment;
			Assert.IsFalse(res);
		}

		[TestMethod]
		public void FrameSetIsNullOrErorFrameTest1()
		{
			var res = new GcodeParser(null).ContainsComment;
			Assert.IsFalse(res);
		}
		[TestMethod]
		public void FrameSetIsNullOrErorFrameTest2()
		{
			var res = new GcodeParser("").ContainsComment;
			Assert.IsFalse(res);
		}
		[TestMethod]
		public void FrameSetIsEmptyCommentTest1()
		{
			var res = new GcodeParser(";").EmptyComment;
			Assert.IsTrue(res);
		}
		[TestMethod]
		public void FrameSetIsEmptyCommentTest2()
		{
			var res = new GcodeParser("                          ;                         ").EmptyComment;
			Assert.IsTrue(res);
		}
	}
}
