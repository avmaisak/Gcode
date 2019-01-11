using System.Text;
using Gcode.Utils.Common;
using LibBase.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gcode.Test.Utils {
	[TestClass]
	public class StringExtensionsTests {
		[TestMethod]
		public void TrimStringTest1() {
			var sb = new StringBuilder(string.Empty);
			var s = string.Empty;
			for (var i = 0; i < 100; i++) {
				if (i <= 0) continue;
				sb.Append(s.PadRight(i));
				var res = sb.ToString();
				Assert.IsTrue(string.IsNullOrWhiteSpace(res.TrimString()));
			}
		}
		[TestMethod]
		public void TrimStringTest2()
		{
			var str = " str str str  ";
			Assert.IsNotNull(str.TrimString());
		}
		[TestMethod]
		public void TrimStringTest2_1()
		{
			var str = " str str str  ";
			Assert.IsTrue(str.TrimString() == "str str str");
		}
		[TestMethod]
		public void TrimStringTest3()
		{
			Assert.IsTrue(string.Empty == string.Empty.TrimString());
		}
		[TestMethod]
		public void TrimStringTest4()
		{
			var str = null as string;
			var res = string.Empty;
			Assert.IsTrue(res == str.TrimString());
		}
		[TestMethod]
		public void GcodeParserStringExtensionsTest1()
		{
			var gcode = "M206 T3 P200 X89".ToGcodeCommandFrame();
			Assert.IsNotNull(gcode.M);
			Assert.AreEqual(206, gcode.M.Value);
			Assert.IsNotNull(gcode.T);
			Assert.AreEqual(3, gcode.T.Value);
			Assert.IsNotNull(gcode.P);
			Assert.AreEqual(200, gcode.P.Value);
			Assert.IsNotNull(gcode.X);
			Assert.AreEqual(89, gcode.X.Value);
		}
		[TestMethod]
		public void GcodeParserStringExtensionsTest2_SplittedString()
		{
			for (var i = 1; i < 100500; i++)
			{
				var gcode = $"M{i}T{i}P{i}X{i}".ToGcodeCommandFrame();
				Assert.IsNotNull(gcode.M);
				Assert.AreEqual(i, gcode.M.Value);
				Assert.IsNotNull(gcode.T);
				Assert.AreEqual(i, gcode.T.Value);
				Assert.IsNotNull(gcode.P);
				Assert.AreEqual(i, gcode.P.Value);
				Assert.IsNotNull(gcode.X);
				Assert.AreEqual(i, gcode.X.Value);
			}
			
		}
	}
}
