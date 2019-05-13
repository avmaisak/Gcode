using Gcode.Test.Infrastructure;
using Gcode.Utils.SlicerParser;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gcode.Test.Parser
{
	[TestClass]
	public class CuraParserTests
	{
	[TestMethod]
		public void CuraParserTest1()
		{
			// Before running tests, unpack \tools\TestData\cura\cura.7z file!
			var src = TestSuiteDataSource.GetDataSourceArray(@"cura\CFFFP_2.gcode");
			if (src?.Length > 0)
			{
				var parser = new CuraParser();
				var res = parser.GetSlicerInfo(src);
				Assert.IsNotNull(res);
				Assert.IsTrue(res.Name == "Cura_SteamEngine");
				Assert.IsTrue(res.Version == "4.0.0");
				Assert.IsTrue(res.EstimatedBuildTime == 762088);
				Assert.IsTrue(res.FilamentUsedExtruder1 == (decimal) 181555.00);
			}
		}
	}
}
