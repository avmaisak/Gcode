using Gcode.Common.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gcode.TestSuite {
	[TestClass]
	public class StringExtensionsTests {
		[TestMethod]
		public void TrimStringTest() {
			var s = string.Empty;
			for (var i = 0; i < 1000; i++) {
				if (i <= 0) continue;
				s += " ";
				Assert.IsTrue(string.IsNullOrWhiteSpace(s.TrimString()));
			}

		}
	}
}
