using System;
using Gcode.Test.Infrastructure;
using Gcode.Utils;
using Gcode.Utils.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gcode.Test
{
	/// <summary>
	/// Gcode command tests
	/// </summary>
	[TestClass]
	public class GcodeParserTests
	{
		[TestMethod]
		public void GcodeParserSyntheticTests1()
		{
			foreach (var r in TestSuiteDataSource.TestSyntheticCodes)
			{
				var gcode = GcodeParser.ToGCode(r);
				Assert.IsInstanceOfType(gcode, typeof(GcodeCommandFrame));
			}
		}
		[TestMethod]
		public void GcodeParserTests1()
		{
			var ds = TestSuiteDataSource.Ds100Gcode.Split("\n");
			foreach (var r in ds)
			{
				var gcode = GcodeParser.ToGCode(r);
				Assert.IsInstanceOfType(gcode, typeof(GcodeCommandFrame), $"{r}");
			}
		}
		[TestMethod]
		public void GcodeParserTests2()
		{
			//M206 T3 P200 X89 ; extruder normal steps per mm
			var ds = TestSuiteDataSource.TestSyntheticCodes[0];
			var gcode = GcodeParser.ToGCode(ds);
			Assert.IsNotNull(gcode.M);
			Assert.AreEqual(206, gcode.M.Value);
			Assert.IsNotNull(gcode.T);
			Assert.AreEqual(3, gcode.T.Value);
			Assert.IsNotNull(gcode.P);
			Assert.AreEqual(200, gcode.P.Value);
			Assert.IsNotNull(gcode.X);
			Assert.AreEqual(89, gcode.X.Value);
			Assert.AreEqual("extruder normal steps per mm", gcode.Comment);
		}
		[TestMethod]
		public void GcodeParserTestsCheckSumIcTest1()
		{
			var ds = TestSuiteDataSource.TestSyntheticCodes[0];
			var gcode = GcodeParser.ToGCode(ds);
			gcode.N = 1;
			gcode.CheckSum = GcodeCrc.FrameCrc(gcode);
			var resStr = GcodeParser.ToStringCommand(gcode);

			Assert.IsNotNull(resStr);
			Assert.IsTrue(resStr.Contains($"*{gcode.CheckSum}"));
		}
		[TestMethod]
		public void GcodeOrderSegmentTest1()
		{
			//M206 T3 P200 X89 ;extruder normal steps per mm
			var ds = TestSuiteDataSource.TestSyntheticCodes[0];
			var gcode = GcodeParser.ToGCode(ds);
			gcode.N = 1;
			var dsExpected = $"N{gcode.N} {ds}";
			var res = GcodeParser.ToStringCommand(gcode);
			Assert.AreEqual(dsExpected, res);
		}
		[TestMethod]
		public void GcodeOrderSegmentTest2()
		{
			var ds = TestSuiteDataSource.Ds100Gcode.Split("\n");
			foreach (var d in ds)
			{
				var s = d.Replace("\r", null);
				if (s == ";") continue;
				var gcode = GcodeParser.ToGCode(s);
				var gcodeStr = GcodeParser.ToStringCommand(gcode);
				var expectedResult = $"{s}";
				if (string.IsNullOrWhiteSpace(gcode.Comment))
				{
					Assert.AreEqual(expectedResult.Trim(), gcodeStr.Trim(), gcodeStr.Trim());
				}

			}
		}
		[TestMethod]
		public void GcodeComment()
		{
			const string cmd = ";> ololo G1 X1 Y1 XZ0 ; this frame comment";
			var gcode = GcodeParser.ToGCode(cmd);

			var gcodeRes = GcodeParser.ToStringCommand(gcode);
			Assert.AreEqual(cmd, gcodeRes);
		}
		[TestMethod]
		public void ToJsonTest1()
		{
			const string raw = "G1 X626.713 Y251.523 E12.01248; Haha";
			var expected = "{\"G\":\"1\",\"X\":\"626.713\",\"Y\":\"251.523\",\"E\":\"12.01248\",\"Comment\":\"Haha\"}";
			//\r\n\t
			expected = expected.Replace("\r", null);
			expected = expected.Replace("\n", null);
			expected = expected.Replace("\t", null);
			var res = GcodeParser.ToJson(raw);
			Assert.AreEqual(expected, res);
		}
		[TestMethod]
		public void ToJsonTest2()
		{
			var cmd = new GcodeCommandFrame
			{
				G = 1,
				X = 626.713,
				Y = 251.523,
				E = 12.01248,
				Comment = "Haha"
			};
			var res = cmd.ToJson();
			const string expected = "{\"G\":\"1\",\"X\":\"626.713\",\"Y\":\"251.523\",\"E\":\"12.01248\",\"Comment\":\"Haha\"}";
			Assert.AreEqual(expected, res);
		}
		[TestMethod]
		public void ToJsonTest3()
		{
			var ds = TestSuiteDataSource.Ds100Gcode.Split("\n");
			foreach (var r in ds)
			{
				var gcode = GcodeParser.ToGCode(r);
				var res = gcode.ToJson();
				Assert.IsTrue(res.StartsWith("{") && res.EndsWith("}"));
			}
		}
		[TestMethod]
		public void ToJsonTest4()
		{
			var ds = TestSuiteDataSource.Ds100Gcode.Split("\n");
			foreach (var r in ds)
			{
				var res = GcodeParser.ToJson(r);
				Assert.IsTrue(res.StartsWith("{") && res.EndsWith("}"));
			}
		}
		[TestMethod]
		public void NormalizeTest1()
		{

			var g = "M109 S205 ; wait for temperature to be reached";
			var res = g.NormalizeRawFrame();
			const string expected = "M109 S205 ;wait for temperature to be reached";
			Assert.AreEqual(expected, res);
		}
		[TestMethod]
		public void NormalizeTest2()
		{

			var g = "G1                    E      - 2.00000                 F2400.00000  ;              NormalizeTest2";
			var res = g.NormalizeRawFrame();
			const string expected = "G1 E-2.00000 F2400.00000 ;NormalizeTest2";
			Assert.AreEqual(expected, res);
		}
		[TestMethod]
		public void ContainsCommentTest1()
		{
			const string res = "M82 ; use absolute distances for extrusion";
			Assert.IsTrue(res.ContainsComment());
		}
		[TestMethod]
		public void ContainsCommentTest2()
		{
			const string res = "M109 S205";
			Assert.IsFalse(res.ContainsComment());
		}
		[TestMethod]
		public void ContainsCommentTest3()
		{
			const string res = "";
			Assert.IsFalse(res.ContainsComment());
		}
		[TestMethod]
		public void ContainsCommentTest4()
		{
			const string res = ";";
			Assert.IsFalse(res.ContainsComment());
		}
		[TestMethod]
		public void ContainsCommentTest5()
		{
			const string res = "   ; thin_walls = 1";
			Assert.IsFalse(res.ContainsComment());
		}
		[TestMethod]
		public void IsCommentTest1()
		{
			const string res = "M82 ; use absolute distances for extrusion";
			Assert.IsFalse(res.IsComment());
		}
		[TestMethod]
		public void IsCommentTest2()
		{
			const string res = "; perimeter_extrusion_width = 0";
			Assert.IsTrue(res.IsComment());
		}
		[TestMethod]
		public void IsCommentTest3()
		{
			const string res = "             ;                perimeter_extrusion_width = 0;asddsadasdasd";
			Assert.IsTrue(res.IsComment());
		}
		[TestMethod]
		public void IsNullOrErorFrameTest1()
		{
			const string res = "; perimeter_extrusion_width = 0";
			Assert.IsFalse(res.IsNullOrErrorFrame());
		}
		[TestMethod]
		public void IsEmptyCommentTest1()
		{
			const string res = "; perimeter_extrusion_width = 0";
			Assert.IsFalse(res.IsEmptyComment());
		}
		[TestMethod]
		public void IsEmptyCommentTest2()
		{
			const string res = "            ;            ";
			Assert.IsTrue(res.IsEmptyComment());
		}
		[TestMethod]
		public void IsEmptyCommentTest3()
		{
			const string res = "    ";
			Assert.IsFalse(res.IsEmptyComment());
		}
		[TestMethod]
		public void ExtensionsToStringTest1()
		{
			for (var i = -100500; i < 100500; i++)
			{
				var g = new GcodeCommandFrame { X = i, Y = i * 5, Z = i * 2 };
				Assert.AreEqual(g.X , i);
				Assert.AreEqual(g.Y , i * 5);
				Assert.AreEqual(g.Z , i* 2);
				var gcodeStr = g.ToString();
				Assert.IsTrue(!string.IsNullOrWhiteSpace(gcodeStr));
				Assert.AreEqual($"X{i} Y{i * 5} Z{i* 2}", gcodeStr);
			}
		}
		[TestMethod]
		public void ExtensionsCheckSumTest1()
		{
			for (var i = 2; i < 100500; i++)
			{
				var g = new GcodeCommandFrame { N = Math.Abs(i + 1), X = i, Y = i * 5, Z = i * 2 };
				Assert.AreEqual(g.X , i);
				Assert.AreEqual(g.Y , i * 5);
				Assert.AreEqual(g.Z , i* 2);
				var gcodeStr = g.ToString();
				Assert.IsTrue(!string.IsNullOrWhiteSpace(gcodeStr));
				Assert.AreEqual($"N{Math.Abs(i + 1)} X{i} Y{i * 5} Z{i* 2}", gcodeStr);
				var cs = g.CheckSum();
				Assert.IsTrue(cs >= 0, $"((( RES: {cs} >> {gcodeStr}");
			}
		}
	}
}