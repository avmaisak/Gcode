using Gcode.Utils.Common;
using Gcode.Utils.Entity.Base;

namespace Gcode.Utils.Entity
{
	/// <inheritdoc />
	/// <summary>
	/// Кадр
	/// </summary>
	public class GcodeCommandFrame : GcodeCommandFrameBase
	{
		[Order(1)]
		public override long? N { get; set; }
		[Order(2)]
		public override int? M { get; set; }
		[Order(3)]
		public override int? G { get; set; }
		[Order(4)]
		public override double? T { get; set; }
		[Order(5)]
		public override double? P { get; set; }
		[Order(6)]
		public override double? X { get; set; }
		[Order(7)]
		public override double? Y { get; set; }
		[Order(8)]
		public override double? Z { get; set; }
		[Order(9)]
		public override double? E { get; set; }
		[Order(10)]
		public override double? F { get; set; }
		[Order(11)]
		public override double? A { get; set; }
		[Order(12)]
		public override double? B { get; set; }
		[Order(13)]
		public override double? C { get; set; }
		[Order(14)]
		public override double? S { get; set; }
		[Order(15)]
		public override double? R { get; set; }
		[Order(16)]
		public override int? D { get; set; }
		[Order(17)]
		public override double? I { get; set; }
		[Order(18)]
		public override double? J { get; set; }
		[Order(19)]
		public override double? K { get; set; }
		[Order(20)]
		public override double? L { get; set; }
		[Order(21)]
		public override double? H { get; set; }
		[Order(22)]
		public override string Comment { get; set; }
		[Order(23)]
		public override int? CheckSum { get; set; }
		public override string ToString()
		{
			return GcodeParser.ToStringCommand(this);
		}
		public string ToJson()
		{
			return GcodeParser.ToJson(this);
		}
	}
}
