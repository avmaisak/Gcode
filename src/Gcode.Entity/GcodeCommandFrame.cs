using Gcode.Entity.Base;

namespace Gcode.Entity
{
	/// <inheritdoc />
	/// <summary>
	/// Кадр
	/// </summary>
	public class GcodeCommandFrame : GcodeCommandFrameBase
	{
		/// <inheritdoc />
		public override long N { get; set; }
		/// <inheritdoc />
		public override int? M { get; set; }
		/// <inheritdoc />
		public override int? G { get; set; }
		/// <inheritdoc />
		public override double? T { get; set; }
		/// <inheritdoc />
		public override double? P { get; set; }
		public override double? X { get; set; }
		/// <inheritdoc />
		public override double? Y { get; set; }
		/// <inheritdoc />
		public override double? Z { get; set; }
		/// <inheritdoc />
		public override double? F { get; set; }
		/// <inheritdoc />
		public override double? A { get; set; }
		/// <inheritdoc />
		public override double? B { get; set; }
		/// <inheritdoc />
		public override double? C { get; set; }
		/// <inheritdoc />
		public override double? S { get; set; }
		/// <inheritdoc />
		public override double? R { get; set; }
		/// <inheritdoc />
		public override int? D { get; set; }
		/// <inheritdoc />
		public override double? I { get; set; }
		/// <inheritdoc />
		public override double? J { get; set; }
		/// <inheritdoc />
		public override double? K { get; set; }
		/// <inheritdoc />
		public override double? L { get; set; }
		/// <inheritdoc />
		public override double? H { get; set; }
		/// <inheritdoc />
		public override double? E { get; set; }
		/// <inheritdoc />
		public override string Comment { get; set; }
	}
}
