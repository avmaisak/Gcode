using Gcode.Utils.Entity;

namespace Gcode.Utils.Common {
	public static class StringExtensions {
		/// <summary>
		/// ToGcodeCommandFrame
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static GcodeCommandFrame ToGcodeCommandFrame(this string str)
		{
			return GcodeParser.ToGCode(str);
		}
	}
}
