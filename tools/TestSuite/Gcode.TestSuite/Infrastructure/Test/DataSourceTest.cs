using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gcode.TestSuite.Infrastructure.Test {
	[TestClass]
	public class DataSourceTest {
		private static string Ds100Gcode =>TestSuiteDataSource.GetDataSource("100.gcode");
		private static string BigFile => TestSuiteDataSource.GetDataSource("pattern_blade_fp_piece2_v1.gcode");
		[TestMethod]
		public void DataSourceTest1() {
			var ds = Ds100Gcode;
			Assert.IsTrue(!string.IsNullOrWhiteSpace(ds));
		}
		[TestMethod]
		public void DataSourceTest2() {
			var ds = Ds100Gcode;
			Assert.IsTrue(ds.StartsWith("; KISSlicer - PRO", StringComparison.OrdinalIgnoreCase));
		}
		[TestMethod]
		public void DataSourceTest3() {
			var ds = BigFile;
			Assert.IsTrue(!string.IsNullOrWhiteSpace(ds));
		}
		[TestMethod]
		public void DataSourceTest4() {
			var ds = BigFile;
			Assert.IsTrue(ds.StartsWith("; KISSlicer - PRO", StringComparison.OrdinalIgnoreCase));
		}
	}
}
