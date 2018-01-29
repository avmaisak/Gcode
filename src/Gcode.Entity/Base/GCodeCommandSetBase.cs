using System.Collections.Generic;
using Gcode.Entity.Interfaces;

namespace Gcode.Entity.Base {
	/// <inheritdoc />
	public abstract class GcodeCommandSetBase : IGcodeCommandSet<GcodeCommandFrameBase> {
		/// <inheritdoc />
		public ICollection<GcodeCommandFrameBase> GCodeCommandFrameSet { get; set; }
	}
}
