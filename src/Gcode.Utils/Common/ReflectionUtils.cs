using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace Gcode.Utils.Common
{
	public static class ReflectionUtils
	{
		public static List<KeyValuePair<string, string>> GetProperties(this object item, CultureInfo culture = null)
		{
			var result = new List<KeyValuePair<string, string>>();
			Thread.CurrentThread.CurrentCulture = culture ?? CultureInfo.InvariantCulture;

			if (item == null) return result;

			var type = item.GetType();
			var props = type.GetProperties();
			if (props.Length == 0)
			{
				return result;
			}

			var propertiesOrdered =
					(from property in type.GetProperties()
					 where Attribute.IsDefined(property, typeof(OrderAttribute))
					 orderby ((OrderAttribute)property.GetCustomAttributes(typeof(OrderAttribute), false).Single()).Order
					 select property).ToList();

			var propertiesUnordered =
				(from property in type.GetProperties()
				 where !Attribute.IsDefined(property, typeof(OrderAttribute))
				 select property).ToList();

			var propResult = new List<PropertyInfo>();

			if (propertiesOrdered.Any())
			{
				propResult.AddRange(propertiesOrdered);
			}

			if (propertiesUnordered.Any())
			{
				propResult.AddRange(propertiesUnordered);
			}

			result.AddRange(
				from pi in propResult
				let selfValue = type.GetProperty(pi.Name)?.GetValue(item, null)
				select selfValue != null
					? new KeyValuePair<string, string>(pi.Name, selfValue.ToString())
					: new KeyValuePair<string, string>(pi.Name, null));
			return result;
		}
	}
}
