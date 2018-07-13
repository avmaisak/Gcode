using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Gcode.Entity.Interfaces {
	/// <summary>
	/// Набор команд
	/// </summary>
	[SuppressMessage("ReSharper", "UnusedMemberInSuper.Global")]
	public interface IGcodeCommandSet<T> where T : IGcodeCommandFrame {
		/// <summary>
		/// Набор кадров
		/// </summary>
		// ReSharper disable once UnusedMemberInSuper.Global
		ICollection<T> GCodeCommandFrameSet { get; set; }
	}
}