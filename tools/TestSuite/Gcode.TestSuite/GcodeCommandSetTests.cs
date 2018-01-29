using Gcode.Entity;
using Gcode.Utils.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gcode.TestSuite
{
	/// <summary>
	/// Gcode command tests
	/// </summary>
	[TestClass]
	public class GcodeCommandSetTests
	{
		[TestMethod]
		public void FrameSetTest1()
		{
			var frameset = new GcodeCommandSet();
			Assert.IsInstanceOfType(frameset, typeof(GcodeCommandSet));
		}

		[TestMethod]
		public void FrameSetTest2()
		{
			var frameset = new GcodeCommandSet();
			var c = frameset.GCodeCommandFrameSet;
			Assert.IsTrue(c != null);
		}

		[TestMethod]
		public void FrameSetTest3()
		{
			var frameset = new GcodeCommandSet();
			var c = frameset.GCodeCommandFrameSet;
			Assert.IsTrue(c.Count == 0);
		}

		[TestMethod]
		public void FrameSetTest4()
		{
			var frameset = new GcodeCommandSet();
			var frame = new GcodeCommandFrame();
			frameset.GCodeCommandFrameSet.Add(frame);
			var c = frameset.GCodeCommandFrameSet;
			Assert.IsTrue(c.Count == 1);
		}


		
	}
}
