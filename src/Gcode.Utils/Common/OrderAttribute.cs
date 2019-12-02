using System;

namespace Gcode.Utils.Common {
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class OrderAttribute : Attribute
	{
		public OrderAttribute(int order = 0) => Order = order;
		public int Order { get; }
	}
}
