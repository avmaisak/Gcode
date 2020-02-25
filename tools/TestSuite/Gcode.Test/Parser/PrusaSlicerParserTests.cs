
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
			string[] src = TestSuiteDataSource.GetDataSourceArray(@"prusa_slicer\matreshka.gcode");
			if (src?.Length > 0)
			{
				var parser = new PrusaSlicerParser();
				var res = parser.GetSlicerInfo(src);
				Assert.IsNotNull(res);
				Assert.IsTrue(res.Edition == string.Empty);
				Assert.IsTrue(res.Version == "2.2.0-alpha2+win64");
				Assert.IsTrue(res.FilamentUsedExtruder1 == (decimal)11773.6);
				Assert.IsTrue(res.FilamentDiameter == (decimal)1.75);
				Assert.IsTrue(res.FilamentUsedExtruder1Volume == (decimal)20603.800);
				Assert.IsTrue(res.EstimatedBuildTime == (decimal)321.17);
			}
		}
		[TestMethod]
		public void PrusaSlicerParserTest2()
		{
			string[] src = TestSuiteDataSource.GetDataSourceArray(@"prusa_slicer\20mm_hollow_cube_0.15mm_PLA_MINI_37m.gcode");
			if (src?.Length > 0)
			{
				var parser = new PrusaSlicerParser();
				var res = parser.GetSlicerInfo(src);
				Assert.IsNotNull(res);
				Assert.IsTrue(res.Edition == string.Empty);
				Assert.IsTrue(res.Version == "2.2.0-alpha2+win64");
				Assert.IsTrue(res.FilamentUsedExtruder1 == (decimal)912.1);
				Assert.IsTrue(res.FilamentDiameter == (decimal)1.75);
				Assert.IsTrue(res.FilamentUsedExtruder1Volume == (decimal)1596.175);
				Assert.IsTrue(res.EstimatedBuildTime == (decimal)37.35);
			}
		}
		[TestMethod]
		public void PrusaSlicerParserTest3()
		{
			string[] src = TestSuiteDataSource.GetDataSourceArray(@"prusa_slicer\v2alpha4\testtest.gcode");
			if (src?.Length > 0)
			{
				var parser = new PrusaSlicerParser();
				var res = parser.GetSlicerInfo(src);
				Assert.IsNotNull(res);
				Assert.IsTrue(res.Edition == string.Empty);
				Assert.IsTrue(res.Version == "2.2.0-alpha4+win64");
				Assert.IsTrue(res.FilamentUsedExtruder1 == (decimal)10604.9);
				Assert.IsTrue(res.FilamentUsedExtruder2 == (decimal)62.3);
				Assert.IsTrue(res.FilamentDiameter == (decimal)1.75);
				Assert.IsTrue(res.EstimatedBuildTime == (decimal)123.25);
			}
		}
		[TestMethod]
		public void PrusaSlicerParserTest4()
		{
			string[] src = TestSuiteDataSource.GetDataSourceArray(@"prusa_slicer\2.2.0-beta\test.gcode");
			if (src?.Length > 0)
			{
				var parser = new PrusaSlicerParser();
				var res = parser.GetSlicerInfo(src);
				Assert.IsNotNull(res);
				Assert.IsTrue(res.Edition == string.Empty);
				Assert.IsTrue(res.Version == "2.2.0-beta+win64");
				Assert.IsTrue(res.FilamentUsedExtruder1 == (decimal)636.7);
				Assert.IsTrue(res.FilamentUsedExtruder2 == null);
				Assert.IsTrue(res.FilamentDiameter == (decimal)1.75);
				Assert.IsTrue(res.EstimatedBuildTime == (decimal)19.92);
			}
		}
	}
}
