
using Gcode.Entity.Interfaces;

namespace Gcode.Entity.Base {
	/// <inheritdoc />
	public abstract class GcodeCommandFrameBase : IGcodeCommandFrame{
		public abstract long  N { get; set; }
		/// <inheritdoc />
		public abstract int? M { get; set; }
		/// <inheritdoc />
		public abstract int? G { get; set; }
		/// <inheritdoc />
		public abstract double? T { get; set; }
		/// <inheritdoc />
		public abstract double? P { get; set; }
		public abstract double? X { get; set; }
		/// <inheritdoc />
		public abstract double? Y { get; set; }
		/// <inheritdoc />
		public abstract double? Z { get; set; }
		/// <inheritdoc />
		public abstract double? F { get; set; }
		/// <inheritdoc />
		public abstract double? A { get; set; }
		/// <inheritdoc />
		public abstract double? B { get; set; }
		/// <inheritdoc />
		public abstract double? C { get; set; }
		/// <inheritdoc />
		public abstract double? S { get; set; }
		/// <inheritdoc />
		public abstract double? R { get; set; }
		/// <inheritdoc />
		public abstract int? D { get; set; }
		/// <inheritdoc />
		public abstract double? I { get; set; }
		/// <inheritdoc />
		public abstract double? J { get; set; }
		/// <inheritdoc />
		public abstract double? K { get; set; }
		/// <inheritdoc />
		public abstract double? L { get; set; }
		/// <inheritdoc />
		public abstract double? H { get; set; }
		/// <inheritdoc />
		public abstract double? E { get; set; }
		/// <inheritdoc />
		public abstract string Comment { get; set; }
	}
}