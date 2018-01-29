using System.Collections.Generic;

namespace Gcode.Entity.Base {
	/// <inheritdoc />
	public abstract class GCodeCommandSetBase : IGCodeCommandSet<IGcodeCommand> {
		/// <inheritdoc />
		public ICollection<IGcodeCommand> GCodeCommandFrameSet { get; set; }
	}
}
