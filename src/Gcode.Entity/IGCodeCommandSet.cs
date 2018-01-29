using System.Collections.Generic;

namespace Gcode.Entity {
	/// <summary>
	/// Набор команд
	/// </summary>
	public interface IGCodeCommandSet<T> {
		/// <summary>
		/// Набор кадров
		/// </summary>
		ICollection<T> GCodeCommandFrameSet { get; set; }
	}
}