using System;

namespace Gcode.Common.Utils {
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class OrderAttribute : Attribute {
		public OrderAttribute(int order = 0) {
			Order = order;
		}
		public int Order { get; }
	}
}
