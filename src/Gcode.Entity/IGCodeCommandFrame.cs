using System.Collections.Generic;

namespace Gcode.Entity {
	/// <summary>
	/// Кадр
	/// </summary>
	public interface IGCodeCommandFrame<T> {
		/// <summary>
		/// Набор команд
		/// </summary>
		ICollection<T> CommandSet { get; set; }
	}
}
