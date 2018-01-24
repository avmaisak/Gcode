using Gcode.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gcode.TestSuite {
	/// <summary>
	/// Gcode command tests
	/// </summary>
	[TestClass]
	public class GcodeCommandTest {
		/// <summary>
		/// Gcode command
		/// </summary>
		private GcodeCommand _gcodeCommand;
		[TestMethod]
		public void CheckInstanceType() {
			_gcodeCommand = new GcodeCommand();
			
			Assert.IsInstanceOfType(_gcodeCommand, typeof(GcodeCommand));
		}
		[TestMethod]
		public void CheckInstanceNameTypeStr() {
			_gcodeCommand = new GcodeCommand();
			var t = _gcodeCommand.ToString();
			Assert.AreEqual(t, "Gcode.Entity.GcodeCommand");
		}
	}
}
