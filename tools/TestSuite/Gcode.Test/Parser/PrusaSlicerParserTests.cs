using Gcode.Test.Infrastructure;
using Gcode.Utils.SlicerParser;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gcode.Test.Parser
{
	[TestClass]
	[Ignore("Run this tests manually")]
	public class PrusaSlicerParserTests
	{
		[TestMethod]
		public void PrusaSlicerParserTest1()
		{
			string[] src = TestSuiteDataSource.GetDataSourceArray(@"prusa_slicer\2.2.0-rc4\test_rc.gcode");
			if (src?.Length > 0)
			{
				var parser = new PrusaSlicerParser();
				var res = parser.GetSlicerInfo(src);
				Assert.IsNotNull(res);
				Assert.IsTrue(res.Edition == string.Empty);
				Assert.IsTrue(res.Version == "2.2.0-rc4+win64");
				Assert.IsTrue(res.FilamentUsedExtruder1 == (decimal)0.8);
				Assert.IsTrue(res.FilamentUsedExtruder2 == (decimal)0.1);
				Assert.IsTrue(res.FilamentDiameter == (decimal)1.75);
				Assert.IsTrue(res.EstimatedBuildTime == (decimal)6.32);
			}
		}
	}
}
