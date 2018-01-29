using System.Collections.Generic;

namespace Gcode.Entity.Interfaces {
	/// <summary>
	/// Набор команд
	/// </summary>
	public interface IGcodeCommandSet<T> where T : IGcodeCommandFrame {
		/// <summary>
		/// Набор кадров
		/// </summary>
		ICollection<T> GCodeCommandFrameSet { get; set; }
	}
}