using System.Collections.Generic;
using System.Linq;
using Common.Utils;
using Gcode.Entity;
using Gcode.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gcode.TestSuite
{
	/// <summary>
	/// Gcode command tests
	/// </summary>
	[TestClass]
	public class GcodeParserTests
	{
		[TestMethod]
		public void FrameSetIsCommentTest1()
		{
			var s = "; head speed 63.800003, filament speed 0.000000, preload 0.000000";
			var res = new GcodeParser(s).IsComment;
			Assert.IsTrue(res);
		}
		[TestMethod]
		public void FrameSetIsCommentTest2()
		{
			var s = "   ; head speed 63.800003, filament speed 0.000000, preload 0.000000";

			var res = new GcodeParser(s).IsComment;
			Assert.IsTrue(res);
		}
		[TestMethod]
		public void FrameSetIsCommentTest3()
		{
			var s = " head speed 63.800003, filament speed 0.000000, preload 0.000000";
			var res = new GcodeParser(s).IsComment;
			Assert.IsFalse(res);
		}
		[TestMethod]
		public void FrameSetIsCommentTest4()
		{
			var s = "; 'Destring/Wipe/Jump Path', 0.0 [feed mm/s], 63.8 [head mm/s]";
			var res = new GcodeParser(s).IsComment;
			Assert.IsTrue(res);
		}
		[TestMethod]
		public void FrameSetIsCommentTest5()
		{
			var s = ";";
			var res = new GcodeParser(s).IsComment;
			Assert.IsTrue(res);
		}
		[TestMethod]
		public void FrameSetIsCommentTest6()
		{
			var s = "     ;     ";
			var res = new GcodeParser(s).IsComment;
			Assert.IsTrue(res);
		}
		[TestMethod]
		public void FrameSetIsCommentTest7()
		{
			var s = string.Empty;
			var res = new GcodeParser(s).IsComment;
			Assert.IsFalse(res);
		}
		[TestMethod]
		public void FrameSetIsCommentTest8()
		{
			var res = new GcodeParser().IsComment;
			Assert.IsFalse(res);
		}
		[TestMethod]
		public void FrameSetContainsCommentTest1()
		{
			var res = new GcodeParser().ContainsComment;
			Assert.IsFalse(res);
		}
		[TestMethod]
		public void FrameSetContainsCommentTest2()
		{

			var res = new GcodeParser("").ContainsComment;
			Assert.IsFalse(res);
		}
		[TestMethod]
		public void FrameSetContainsCommentTest3()
		{
			var res = new GcodeParser("; >   M206 T3 P149 X700.0 ; Y max len = 600.0").ContainsComment;
			Assert.IsFalse(res);
		}
		[TestMethod]
		public void FrameSetContainsCommentTest4()
		{
			var res = new GcodeParser("G1 X551.135 Y348.935 E1.23722;mooove").ContainsComment;
			Assert.IsTrue(res);
		}
		[TestMethod]
		public void FrameSetContainsCommentTest5()
		{
			var res = new GcodeParser("G1 X551.135 Y348.935 E1.23722  ;    mooove").ContainsComment;
			Assert.IsTrue(res);
		}
		[TestMethod]
		public void FrameSetContainsCommentTest6()
		{
			var res = new GcodeParser("G1 X551.135 Y348.935 E1.23722 ; mooove; dsddffs ;;;").ContainsComment;
			Assert.IsTrue(res);
		}
		[TestMethod]
		public void FrameSetContainsCommentTest7()
		{
			var res = new GcodeParser(";").ContainsComment;
			Assert.IsFalse(res);
		}
		[TestMethod]
		public void FrameSetIsNullOrErorFrameTest1()
		{
			var res = new GcodeParser().ContainsComment;
			Assert.IsFalse(res);
		}
		[TestMethod]
		public void FrameSetIsNullOrErorFrameTest2()
		{
			var res = new GcodeParser("").ContainsComment;
			Assert.IsFalse(res);
		}
		[TestMethod]
		public void FrameSetIsEmptyCommentTest1()
		{
			var res = new GcodeParser(";").EmptyComment;
			Assert.IsTrue(res);
		}
		[TestMethod]
		public void FrameSetIsEmptyCommentTest2()
		{
			var res = new GcodeParser("                          ;                         ").EmptyComment;
			Assert.IsTrue(res);
		}
		[TestMethod]
		public void NormalizeRawFrameTestSynthetic1()
		{
			const string def = "A1 B2 C3 D4 E5 F6 G7";
			var comparer = def.Replace(" ", string.Empty);
			var parser = new GcodeParser(comparer);
			var res = parser.NormalizeRawFrame();

			Assert.AreEqual(def, res);
		}
		[TestMethod]
		public void NormalizeRawFrameTestSynthetic2()
		{
			const string def = "G1 X-70 Y40 F2000 ;move to bed sensr for ex7 ";
			var comparer = def.Trim();
			var parser = new GcodeParser(comparer);
			var res = parser.NormalizeRawFrame();

			Assert.AreEqual(comparer, res);
		}
		[TestMethod]
		public void NormalizeRawFrameTestSynthetic3()
		{
			const string def = "    ; move to bed sensr for ex7  G1 X-70 Y40 F2000  ;    ;;;  ";
			var parser = new GcodeParser(def);
			var res = parser.NormalizeRawFrame();

			Assert.AreEqual(def.Trim(), res);
		}
		[TestMethod]
		public void NormalizeRawFrameTestSynthetic4()
		{
			const string def = ";";
			var parser = new GcodeParser(def);
			var res = parser.NormalizeRawFrame();

			Assert.AreEqual(def, res);
		}
		[TestMethod]
		public void NormalizeRawFrameTestSynthetic5()
		{
			const string def = "   ; >   M206 T3 P200 X89 ; extruder normal steps per mm  ";
			var parser = new GcodeParser(def);
			var res = parser.NormalizeRawFrame();

			Assert.AreEqual(def.Trim(), res);
		}
		[TestMethod]
		public void NormalizeRawFrameTestReal_Real_Cube()
		{
			var data = TestSuiteDataSource.GetDataSource("100.gcode").Split("\n");
			foreach (var d in data)
			{
				var parser = new GcodeParser(d);
				var res = parser.NormalizeRawFrame();
				Assert.IsNotNull(res);
			}
		}
		[TestMethod]
		public void NormalizeRawFrameTestReal1()
		{
			var data = TestSuiteDataSource.GetDataSource("pattern_blade_fp_piece2_v1.gcode").Split("\n");
			foreach (var d in data)
			{
				var parser = new GcodeParser(d);
				var res = parser.NormalizeRawFrame();
				Assert.IsNotNull(res);
			}

		}
		[TestMethod]
		public void NormalizeRawFrameTestReal2()
		{
			var data = TestSuiteDataSource.GetDataSource("28.gcode.modified.gcode").Split("\n");
			foreach (var d in data)
			{
				var parser = new GcodeParser(d);
				if (!parser.IsComment)
				{
					var res = parser.NormalizeRawFrame();
					Assert.IsNotNull(res);
				}
			}
		}
		[TestMethod]
		public void HandleSegmentsTest1()
		{
			var rawFrame = "G1 X602.368 Y348.476 E0.23255";
			var parser = new GcodeParser(rawFrame);
			var items = parser.HandleSegments();
			Assert.IsNotNull(items);
		}
		[TestMethod]
		public void HandleSegmentsTest2()
		{
			var rawFrame = "G1 X602.368 Y348.476 E0.23255";
			var parser = new GcodeParser(rawFrame);
			var items = parser.HandleSegments();
			Assert.IsTrue(items.Count > 0);
		}
		[TestMethod]
		public void HandleSegmentsTest3()
		{
			var rawFrame = "G1 X602.368 Y348.476 E0.23255";
			var rawSplited = rawFrame.Split(" ");
			var parser = new GcodeParser(rawFrame);
			var items = parser.HandleSegments();
			Assert.IsTrue(items.Count == rawSplited.Length);
		}
		[TestMethod]
		public void HandleSegmentsTestRealData1()
		{
			var data = TestSuiteDataSource.GetDataSource("pattern_blade_fp_piece2_v1.gcode").Split("\n");
			foreach (var d in data)
			{
				var parser = new GcodeParser(d);
				if (!parser.IsComment && !parser.ContainsComment && !parser.IsNullOrErorFrame)
				{
					var res = parser.NormalizeRawFrame();
					var rawSplited = res.Split(" ");
					var items = parser.HandleSegments();
					Assert.IsTrue(items.Count == rawSplited.Length);
				}
			}
		}
		[TestMethod]
		public void ToGcodeCommandFrameTest1()
		{
			var rawFrame = "G1 X602.368 Y348.476 E0.23255";
			var parser = new GcodeParser(rawFrame);
			var items = parser.HandleSegments();
			var gcode = GcodeParser.ToGcodeCommandFrame(items);
			Assert.IsNotNull(gcode);
		}
		[TestMethod]
		public void ToGcodeCommandFrameTest2()
		{
			var rawFrame = "G1 X602.368 Y348.476 E0.23255";
			var parser = new GcodeParser(rawFrame);
			var items = parser.HandleSegments();
			var gcode = GcodeParser.ToGcodeCommandFrame(items);
			Assert.IsTrue(gcode.G == 1);
			Assert.IsNotNull(gcode.X);
			Assert.IsNotNull(gcode.Y);
			Assert.IsNotNull(gcode.E);
		}
		[TestMethod]
		public void ToGcodeCommandFrameTest3()
		{
			var data = TestSuiteDataSource.GetDataSource("100.gcode").Split("\n");
			foreach (var d in data)
			{
				var parser = new GcodeParser(d);
				if (!parser.IsComment && !parser.ContainsComment && !parser.IsNullOrErorFrame)
				{
					var items = parser.HandleSegments();
					var gcode = GcodeParser.ToGcodeCommandFrame(items);
					Assert.IsNotNull(gcode);
				}
			}
		}
		[TestMethod]
		public void ToGcodeCommandFrameTest4()
		{
			var data = TestSuiteDataSource.GetDataSource("28.gcode.modified.gcode").Split("\n");
			foreach (var d in data)
			{
				var parser = new GcodeParser(d);
				if (!parser.IsComment && !parser.ContainsComment && !parser.IsNullOrErorFrame)
				{
					var items = parser.HandleSegments();
					var gcode = GcodeParser.ToGcodeCommandFrame(items);
					Assert.IsNotNull(gcode);
				}
			}
		}
		[TestMethod]
		public void ToGcodeCommandFrameTest5()
		{
			var data = TestSuiteDataSource.GetDataSource("pattern_blade_fp_piece2_v1.gcode").Split("\n");
			foreach (var d in data)
			{
				var parser = new GcodeParser(d);
				if (!parser.IsComment && !parser.ContainsComment && !parser.IsNullOrErorFrame)
				{
					var items = parser.HandleSegments();
					var gcode = GcodeParser.ToGcodeCommandFrame(items);
					Assert.IsNotNull(gcode);
				}
			}
		}
		[TestMethod]
		public void DeserializeObjectTestSynthetic1()
		{
			var raw = "; ^   203D20313230302E300A4D323036205433205031343920583730302E";
			var parser = new GcodeParser(raw);
			var res = parser.DeserializeObject();
			Assert.IsNotNull(res);
		}
		[TestMethod]
		public void DeserializeObjectTestSynthetic2()
		{
			var raw = "G1 X550.361 Y347.617 E0.15691";
			var parser = new GcodeParser(raw);
			var res = parser.DeserializeObject();
			Assert.AreEqual(res.G, 1);
			Assert.AreEqual(res.X, 550.361);
			Assert.AreEqual(res.Y, 347.617);
			Assert.AreEqual(res.E, 0.15691);

		}
		[TestMethod]
		public void DeserializeObjectTestReal1()
		{
			var data = TestSuiteDataSource.GetDataSource("100.gcode").Split("\n");
			var i = -1;
			foreach (var d in data)
			{
				i++;

				if (!string.IsNullOrWhiteSpace(d))
				{
					var g = new GcodeParser(d);
					var obj = g.DeserializeObject();
					Assert.IsNotNull(obj, $"nullable at raw frame : {d} line {i}");
				}
			}
		}
		[TestMethod]
		public void DeserializeObjectTestReal2()
		{
			var data = TestSuiteDataSource.GetDataSource("28.gcode.modified.gcode").Split("\n");
			var i = -1;
			foreach (var d in data)
			{
				i++;

				if (!string.IsNullOrWhiteSpace(d))
				{
					var g = new GcodeParser(d);
					var obj = g.DeserializeObject();
					Assert.IsNotNull(obj, $"nullable at raw frame : {d} line {i}");
				}
			}
		}
		[TestMethod]
		public void DeserializeObjectTestReal3()
		{
			var data = TestSuiteDataSource.GetDataSource("pattern_blade_fp_piece2_v1.gcode").Split("\n");
			var i = -1;
			foreach (var d in data)
			{
				i++;

				if (!string.IsNullOrWhiteSpace(d))
				{
					var g = new GcodeParser(d);
					var obj = g.DeserializeObject();
					Assert.IsNotNull(obj, $"nullable at raw frame : {d} line {i}");
				}
			}
		}
		[TestMethod]
		public void DeserializeTestSyntheticResearch()
		{
			var cmds = new List<GcodeCommandFrame>();
			var gcodeCommands = TestSuiteDataSource.GetDataSource("100.gcode").Split("\n");

			foreach (var fr in gcodeCommands)
			{
				var parser = new GcodeParser(fr);
				cmds.Add(parser.DeserializeObject());
			}

			var x = cmds.Min(s => s?.X ?? 0);
			Assert.AreEqual(x, -70);
		}
		[TestMethod]
		public void SerializeTestSynthetic1()
		{
			var g = new GcodeCommandFrame
			{
				G = 1,
				X = 5,
				Y = -3,
				Z = 0
			};

			var p = new GcodeParser();
			var res = p.SerializeObject(g);
			Assert.AreEqual(res, "G1 X5 Y-3 Z0");
		}
		[TestMethod]
		public void SerializeTestSynthetic2()
		{
			var g = new GcodeCommandFrame
			{
				G = 1,
				X = 5,
				Y = -3,
				Z = 0,
				Comment = "LALALAL"
			};

			var p = new GcodeParser();
			var res = p.SerializeObject(g);
			Assert.AreEqual("G1 X5 Y-3 Z0 ;LALALAL", res);
		}
		[TestMethod]
		public void SerializeTestSynthetic3()
		{
			var g = new GcodeCommandFrame
			{
				M = 109,
				S = 238
			};

			var p = new GcodeParser();
			var res = p.SerializeObject(g);
			Assert.AreEqual("M109 S238", res);
		}
		[TestMethod]
		public void SerializeTestSynthetic4()
		{
			var g = new GcodeCommandFrame
			{
				G = 92,
				Z = 12
			};

			var p = new GcodeParser();
			var res = p.SerializeObject(g);
			Assert.AreEqual("G92 Z12", res);
		}
		
		[TestMethod]
		public void SerializeTestSyntheticResearchIgnoreComments()
		{
			//var cmd = "M206 T3 P200 X89 ; extruder normal steps per mm";
			var f = new GcodeCommandFrame
			{
				M = 206,
				T = 3,
				P = 200,
				X = 89,
				Comment = "extruder normal steps per mm"
			};
			var parser = new GcodeParser();
			var res = parser.SerializeObject(f, true);
			Assert.IsTrue(res == "M206 T3 P200 X89");
		}
		[TestMethod]
		public void SerializeTestSyntheticResearchIgnoreComments1()
		{
			//var cmd = "M206 T3 P200 X89 ; extruder normal steps per mm";
			var f = new GcodeCommandFrame
			{
				M = 206,
				T = 3,
				P = 200,
				X = 89,
				Comment = "extruder normal steps per mm"
			};
			var parser = new GcodeParser();
			var res = parser.SerializeObject(f);
			Assert.IsTrue(res == "M206 T3 P200 X89 ;extruder normal steps per mm");
		}
	}
}
