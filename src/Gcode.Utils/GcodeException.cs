using System;

namespace Gcode.Utils
{
	/// <summary>
	/// GcodeException.
	/// </summary>
	public class GcodeException : Exception { 
		public GcodeException(string message): base (message: message) {}
	}
}
