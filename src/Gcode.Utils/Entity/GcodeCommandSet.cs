using System.Collections.Generic;
using Gcode.Utils.Entity.Base;

namespace Gcode.Utils.Entity
{
	public class GcodeCommandSet : GcodeCommandSetBase
	{
		public GcodeCommandSet()
		{
			GCodeCommandFrameSet = new List<GcodeCommandFrame>();
		}
	}
}
