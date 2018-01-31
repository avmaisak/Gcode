using System.Collections.Generic;
using System.Reflection;

namespace Common.Utils
{
	public static class ReflectionUtils
	{
		public static List<KeyValuePair<string, string>> GetProperties(object item) //where T : class
		{
			var result = new List<KeyValuePair<string, string>>();
			if (item != null)
			{
				var type = item.GetType();
				var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
				foreach (var pi in properties)
				{
					var selfValue = type.GetProperty(pi.Name).GetValue(item, null);
					result.Add(
						selfValue != null
						? new KeyValuePair<string, string>(pi.Name, selfValue.ToString())
						: new KeyValuePair<string, string>(pi.Name, null));
				}
			}
			return result;
		}
	}
}
