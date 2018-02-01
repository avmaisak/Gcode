using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace Gcode.Common.Utils {
	public static class ReflectionUtils {
		public static List<KeyValuePair<string, string>> GetProperties(object item) {
			
			Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
			var result = new List<KeyValuePair<string, string>>();
			
			if (item == null) return result;

			var type = item.GetType();
			var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

			result.AddRange(
				from pi in properties
				let selfValue = type.GetProperty(pi.Name).GetValue(item, null)
				select selfValue != null
					? new KeyValuePair<string, string>(pi.Name, selfValue.ToString())
					: new KeyValuePair<string, string>(pi.Name, null));
			return result;
		}
	}
}
