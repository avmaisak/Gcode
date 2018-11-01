using System;
using Gcode.Utils;
using Gcode.Utils.Entity;
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
		public void GcodeCommandFrameTest2_1()
		{
			for (var i = 0; i < 95000; i++)
			{
				var gcode = new GcodeCommandFrame { X = i, Y = i * 2 };
				var gcodeStr = GcodeParser.ToStringCommand(gcode);
				Assert.IsNotNull(gcodeStr);

				var rawString = $"G1 X{i}.131 Y{i}.91 Z{i}.833 E0 F360";
				var gcodeConverted = GcodeParser.ToGCode(rawString);
				Assert.IsInstanceOfType(gcodeConverted, typeof(GcodeCommandFrame));
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
		[TestMethod]
		public void GcodeCommandFrameTest4()
		{

			for (var i = 0; i < 7500000; i++)
			{
				var random = new Random();
				var value = 1 + (random.NextDouble() * (i - 1));
				var c = new GcodeCommandFrame
				{
					A = value,
					B = value,
					C = value,
					R = value,
					D = i,
					I = value,
					J = value,
					K = value,
					L = value,
					H = value
				};

				Assert.AreEqual(c.A, value);
				Assert.AreEqual(c.B, value);
				Assert.AreEqual(c.C, value);
				Assert.AreEqual(c.R, value);
				Assert.AreEqual(c.D, i);
				Assert.AreEqual(c.I, value);
				Assert.AreEqual(c.J, value);
				Assert.AreEqual(c.K, value);
				Assert.AreEqual(c.L, value);
				Assert.AreEqual(c.H, value);
			}
		}
		[TestMethod]
		public void GcodeCommandFrameTest5()
		{

			for (var i = 0; i < 7500000; i++)
			{
				var c = new GcodeCommandFrame();

				Assert.AreEqual(c.A, null);
				Assert.AreEqual(c.B, null);
				Assert.AreEqual(c.C, null);
				Assert.AreEqual(c.R, null);
				Assert.AreEqual(c.D, null);
				Assert.AreEqual(c.I, null);
				Assert.AreEqual(c.J, null);
				Assert.AreEqual(c.K, null);
				Assert.AreEqual(c.L, null);
				Assert.AreEqual(c.H, null);
			}
		}
	}
}
