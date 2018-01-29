using System.Collections.Generic;

namespace Gcode.Entity.Base {
	/// <inheritdoc />
	public abstract class CodeCommandFrameBase : IGCodeCommandFrame<IGcodeCommand> {
		/// <inheritdoc />
		public ICollection<IGcodeCommand> CommandSet { get; set; }
	}
}