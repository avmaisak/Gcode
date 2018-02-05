using System;
using System.Runtime.CompilerServices;

namespace Gcode.Common.Utils
{
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class OrderAttribute : Attribute {
		private readonly int _order;
		public OrderAttribute([CallerLineNumber] int order = 0)
		{
			_order = order;
		}
		public int Order { get { return _order; } }
	}
}
