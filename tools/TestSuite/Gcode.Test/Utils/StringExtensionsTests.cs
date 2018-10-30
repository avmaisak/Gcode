using System.Text;
using Gcode.Common.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gcode.Test.Utils {
	[TestClass]
	public class StringExtensionsTests {
		[TestMethod]
		public void TrimStringTest() {
			var sb = new StringBuilder(string.Empty);
			var s = string.Empty;
			for (var i = 0; i < 100; i++) {
				if (i <= 0) continue;
				sb.Append(s.PadRight(i));
				var res = sb.ToString();
				Assert.IsTrue(string.IsNullOrWhiteSpace(res.TrimString()));
			}
		}
	}
}
