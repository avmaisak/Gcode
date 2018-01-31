using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gcode.TestSuite
{
	[TestClass]
	public class DataSourceTest {
		[TestMethod]
		public void DataSourceTest1()
		{
			var ds = TestSuiteDataSource.GetDataSource("100.gcode");
			Assert.IsTrue(!string.IsNullOrWhiteSpace(ds));
		}
		[TestMethod]
		public void DataSourceTest2()
		{
			var ds = TestSuiteDataSource.GetDataSource("100.gcode");
			Assert.IsTrue(ds.StartsWith("; KISSlicer - PRO"));
		}
	}
}
