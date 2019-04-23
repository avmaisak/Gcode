using Gcode.Test.Infrastructure;
using Gcode.Utils.SlicerParser;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gcode.Test.Parser
{
	[TestClass]
	public class KisSlicerParserTests
	{
		[TestMethod]
		public void KisSlicerParserTest1()
		{
			var src = TestSuiteDataSource.GetDataSourceArray("100.gcode");
			if (src.Length > 0)
			{
				var kisSlicerParser = new KisSlicerParser();
				var res = kisSlicerParser.GetSlicerInfo(src);
				Assert.IsNotNull(res);
				Assert.IsTrue(res.Name == "KISSlicer");
				Assert.IsTrue(res.Edition == "PRO");
				Assert.IsTrue(res.Version == "version 2 Pre-Alpha 0.1.5 Win64");
				Assert.IsTrue(res.EstimatedBuildTime == (decimal)506.56);
				Assert.IsTrue(res.EstimatedBuildCost == (decimal)3130.33);
				Assert.IsTrue(res.TotalEstimatedPreCoolMinutes == (decimal)506.28);
				Assert.IsTrue(res.FilamentUsedExtruder1 == (decimal)91166.75);
				Assert.IsTrue(res.FilamentUsedExtruder1Volume == (decimal)219.282);
				Assert.IsTrue(res.FilamentUsedExtruder2 == null);
				Assert.IsTrue(res.FilamentUsedExtruder2Volume == null);
			}
		}
		[TestMethod]
		public void KisSlicerParserTest2()
		{
			var src = TestSuiteDataSource.GetDataSourceArray("pattern_blade_fp_piece2_v1.gcode");
			if (src.Length > 0)
			{
				var kisSlicerParser = new KisSlicerParser();
				var res = kisSlicerParser.GetSlicerInfo(src);
				Assert.IsNotNull(res);
				Assert.IsTrue(res.Name == "KISSlicer");
				Assert.IsTrue(res.Edition == "PRO");
				Assert.IsTrue(res.Version == "version 2 Pre-Alpha 0.1.5 Win64");
				Assert.IsTrue(res.EstimatedBuildTime == (decimal)4904.34);
				Assert.IsTrue(res.EstimatedBuildCost == (decimal)34590.45);
				Assert.IsTrue(res.TotalEstimatedPreCoolMinutes == (decimal)4902.55);
				Assert.IsTrue(res.FilamentUsedExtruder1 == (decimal)3108693.38);
				Assert.IsTrue(res.FilamentUsedExtruder1Volume == (decimal)7477.285);
				Assert.IsTrue(res.FilamentUsedExtruder2 == null);
				Assert.IsTrue(res.FilamentUsedExtruder2Volume == null);
			}
		}
		[TestMethod]
		public void KisSlicerParserTest3()
		{
			var src = TestSuiteDataSource.GetDataSourceArray("test_2_extruers.gcode");
			if (src.Length > 0)
			{
				var kisSlicerParser = new KisSlicerParser();
				var res = kisSlicerParser.GetSlicerInfo(src);
				Assert.IsNotNull(res);
				Assert.IsTrue(res.Name == "KISSlicer");
				Assert.IsTrue(res.Edition == "PRO");
				Assert.IsTrue(res.Version == "version 2 Pre-Alpha 0.1.11 Win64");
				Assert.IsTrue(res.EstimatedBuildTime == (decimal)4295.06);
				Assert.IsTrue(res.EstimatedBuildCost == (decimal)31183.29);
				Assert.IsTrue(res.TotalEstimatedPreCoolMinutes == (decimal)4293.43);
				Assert.IsTrue(res.FilamentUsedExtruder1 == (decimal)2299832.68);
				Assert.IsTrue(res.FilamentUsedExtruder1Volume == (decimal)5531.747);
				Assert.IsTrue(res.FilamentUsedExtruder2 == (decimal)885238.82);
				Assert.IsTrue(res.FilamentUsedExtruder2Volume == (decimal)2129.249);
			}
		}
	}
}
