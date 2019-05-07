using Gcode.Test.Infrastructure;
using Gcode.Utils.SlicerParser;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gcode.Test.Parser
{
	[TestClass]
	public class Simplify3dInfoParserTests
	{
		[TestMethod]
		public void Simplify3dInfoParserTest1()
		{
			var src = TestSuiteDataSource.GetDataSourceArray("Simplify3d_Mont_Box_x6_29_04.gcode");
			if (src?.Length > 0)
			{
				var simplify3dParser = new Simplify3dParser();
				var res = simplify3dParser.GetSlicerInfo(src);
				Assert.IsNotNull(res);
			}
		}
	}
}
