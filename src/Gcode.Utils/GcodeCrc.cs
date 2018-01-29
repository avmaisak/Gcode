using System.Linq;

namespace Gcode.Utils {
	/// <summary>
	/// Проверка checksum
	/// </summary>
	public static class GcodeCrc {
		/// <summary>
		/// Контрольная сумма кадра
		/// http://reprap.org/wiki/G-code#.2A:_Checksum
		/// Example: *71
		/// If present, the checksum should be the last field in a line, but before a comment. 
		/// For G-code stored in files on SD cards the checksum is usually omitted.
		/// The firmware compares the checksum against a locally-computed value. 
		/// If they differ, it requests a repeat transmission of the line.
		/// Checking Example
		/// N123 [...G Code in here...] *71
		/// The RepRap firmware checks the line number and the checksum. 
		/// You can leave both of these out - RepRap will still work, but it won't do checking. 
		/// You have to have both or neither though. If only one appears, it produces an error.
		/// </summary>
		/// <param name="line">порядковый номер строки</param>
		/// <param name="frame">Кадр</param>
		/// <returns></returns>
		public static int FrameCrc(long line, string frame) {
			var f = $"N{line} {frame}";
			var check = f.Aggregate(0, (current, ch) => current ^ (ch & 0xff));
			check ^= 32;
			return check;
		}
	}
}