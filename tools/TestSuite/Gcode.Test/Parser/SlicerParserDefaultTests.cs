using Gcode.Test.Infrastructure;
using Gcode.Utils.SlicerParser;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gcode.Test.Parser
{
	[TestClass]
	public class SlicerParserDefaultTests
	{
		[TestMethod]
		[Ignore("Run this tests manually")]
		public void SlicerCompareTest1()
		{
			var src = TestSuiteDataSource.GetDataSourceArray("100.gcode");
			if (src?.Length > 0)
			{
				var kisSlicerParser = new KisSlicerParser();
				var defaultParser = new SlicerParserDefault();
				var kisSlicerParserResult = kisSlicerParser.GetSlicerInfo(src);
				var defaultParserResult = defaultParser.GetSlicerInfo(src);

				Assert.AreEqual(kisSlicerParserResult.FilamentUsedExtruder1, defaultParserResult.FilamentUsedExtruder1);
			}
		}
	}
}