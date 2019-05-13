using System.Collections.Generic;
using Gcode.Utils.Entity.Interfaces;

namespace Gcode.Utils.Entity.Base {
	/// <inheritdoc />
	public abstract class GcodeCommandSetBase : IGcodeCommandSet {
		public ICollection<GcodeCommandFrame> GCodeCommandFrameSet { get; set; }
	}
}
