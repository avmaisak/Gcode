using System.Linq;
using LibBase.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gcode.Test {
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
		[TestMethod]
		public void RemoveAllSpacesTest4() {
			var s = "a";
			for (var i = 0; i < 500; i++) {
				s += " ";
			}

			Assert.AreEqual("a", s.RemoveAllSpaces());
		}
		[TestMethod]
		public void ReplaceAtIndexTest1() {
			var s = "Lorem Ipsum is simply dummy text";
			var res = s.ReplaceAtIndex(5, 'X');

			var expected = "LoremXIpsum is simply dummy text";
			Assert.AreEqual(expected, res);
		}
		[TestMethod]
		public void HandleSegmentsTest1() {
			const string s = "G1 X80.151 Y102.000 F7800.000";
			var res = s.ToKeyValuePair();
			Assert.AreEqual(res.Count(), 4);
		}
		[TestMethod]
		public void HandleSegmentsTest2() {
			const string str = "G1 X80.151 Y102.000 F7800.000";
			var res = str.ToKeyValuePair();
			var ss = res.FirstOrDefault(s => s.Key == "G" && s.Value == "1");
			Assert.IsNotNull(ss);
		}
	}
}