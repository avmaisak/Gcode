using Gcode.Test.Infrastructure;
using Gcode.Utils.SlicerParser;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gcode.Test.Parser
{
	[TestClass]
	public class Slic3RParserTests
	{
		[TestMethod]
		[Ignore("Run this tests manually")]
		public void Slic3RParserTest1()
		{
			string[] src = TestSuiteDataSource.GetDataSourceArray(@"slic3r\2.gcode");
			if (src?.Length > 0)
			{
				var parser = new Slic3RParser();
				var res = parser.GetSlicerInfo(src);
				Assert.IsNotNull(res);
				Assert.IsTrue(res.Edition == string.Empty);
				Assert.IsTrue(res.Version == "1.3.0");
				Assert.IsTrue(res.FilamentUsedExtruder1 == (decimal) 675947.4);
				Assert.IsTrue(res.FilamentDiameter == (decimal) 1.75);
				Assert.IsTrue(res.FilamentUsedExtruder1Volume > 0);
			}
		}
	}
}
