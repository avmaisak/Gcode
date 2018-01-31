using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gcode.TestSuite {
	[TestClass]
	public class DataSourceTest {
		private static string _ds100Gcode =>TestSuiteDataSource.GetDataSource("100.gcode");
		private static string _bigFile => TestSuiteDataSource.GetDataSource("pattern_blade_fp_piece2_v1.gcode");
		[TestMethod]
		public void DataSourceTest1() {
			var ds = _ds100Gcode;
			Assert.IsTrue(!string.IsNullOrWhiteSpace(ds));
		}
		[TestMethod]
		public void DataSourceTest2() {
			var ds = _ds100Gcode;
			Assert.IsTrue(ds.StartsWith("; KISSlicer - PRO"));
		}
		[TestMethod]
		public void DataSourceTest3() {
			var ds = _bigFile;
			Assert.IsTrue(!string.IsNullOrWhiteSpace(ds));
		}
		[TestMethod]
		public void DataSourceTest4() {
			var ds = _bigFile;
			Assert.IsTrue(ds.StartsWith("; KISSlicer - PRO"));
		}
	}
}
