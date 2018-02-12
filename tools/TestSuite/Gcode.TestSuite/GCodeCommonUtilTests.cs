using Gcode.Common.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gcode.TestSuite {
	[TestClass]
	public class GCodeCommonUtilTests {
		[TestMethod]
		public void RemoveAllSpacesTest1() {
			var s = "a                            b           c";
			var expected = "abc";
			Assert.AreEqual(expected, s.RemoveAllSpaces());
		}
		[TestMethod]
		public void RemoveAllSpacesTest2() {
			var s = "a                b    ;       c";
			var expected = "ab;c";
			Assert.AreEqual(expected, s.RemoveAllSpaces());
		}
		[TestMethod]
		public void RemoveAllSpacesTest3() {
			var s = "a         -  -  -     b    ;       c";
			var expected = "a---b;c";
			Assert.AreEqual(expected, s.RemoveAllSpaces());
		}
	}
}
