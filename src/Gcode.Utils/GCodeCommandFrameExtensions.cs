using Gcode.Utils.Entity;

namespace Gcode.Utils
{
	public static class GCodeCommandFrameExtensions
	{
		public static int CheckSum(this GcodeCommandFrame f)
		{
			return f.FrameCrc();
		}
	}
}
