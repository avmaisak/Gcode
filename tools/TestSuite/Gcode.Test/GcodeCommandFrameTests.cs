using Gcode.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gcode.Test
{
	/// <summary>
	/// Gcode command tests
	/// </summary>
	[TestClass]
	public class GcodeCommandTests
	{

		[TestMethod]
		public void GcodeCommandFrameTest1()
		{
			var frame = new GcodeCommandFrame();
			Assert.IsInstanceOfType(frame, typeof(GcodeCommandFrame));
		}

		[TestMethod]
		public void GcodeCommandFrameTest2()
		{
			for (var i = 0; i < 500; i++)
			{
				var frame = new GcodeCommandFrame { N = i };
				Assert.IsInstanceOfType(frame, typeof(GcodeCommandFrame));
			}
		}
		[TestMethod]
		public void GcodeCommandFrameTest3()
		{
			for (var i = 0; i < 500; i++)
			{
				var frame = new GcodeCommandFrame { N = i };
				Assert.AreEqual(i, frame.N);
			}
		}

	}
}
