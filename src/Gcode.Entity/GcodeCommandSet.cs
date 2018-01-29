using System.Collections.Generic;
using Gcode.Entity.Base;

namespace Gcode.Entity
{
	public class GcodeCommandSet : GcodeCommandSetBase
	{
		public GcodeCommandSet()
		{
			GCodeCommandFrameSet = new List<GcodeCommandFrameBase>();
		}
	}
}
