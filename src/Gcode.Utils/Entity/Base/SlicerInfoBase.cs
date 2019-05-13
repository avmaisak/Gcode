using Gcode.Utils.Entity.Interfaces;

namespace Gcode.Utils.Entity.Base
{
	/// <summary>
	/// Slicer info.
	/// </summary>
	public class SlicerInfoBase : ISlicerInfo
	{
		/// <summary>
		/// Slicer name.
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// Slicer name.
		/// </summary>
		public string Version { get; set; }
		/// <summary>
		/// Slicer edition.
		/// </summary>
		public string Edition { get; set; }
		/// <summary>
		/// Estimated Build Time.
		/// </summary>
		public decimal? EstimatedBuildTime{ get; set; }
		/// <summary>
		/// Estimated Build Cost $.
		/// </summary>
		public decimal? EstimatedBuildCost{ get; set; }
		/// <summary>
		/// Filament used extruder 1 (mm)
		/// </summary>
		public decimal? FilamentUsedExtruder1 { get; set; }
		/// <summary>
		/// Filament used extruder 1 (cm^3)
		/// </summary>
		public decimal? FilamentUsedExtruder1Volume { get; set; }
		/// <summary>
		/// Filament used extruder 2 (mm)
		/// </summary>
		public decimal? FilamentUsedExtruder2 { get; set; }
		/// <summary>
		/// Filament used extruder 2 (cm^3)
		/// </summary>
		public decimal? FilamentUsedExtruder2Volume { get; set; }
		/// <summary>
		/// Total estimated (pre-cool) minutes.
		/// </summary>
		public decimal? TotalEstimatedPreCoolMinutes { get; set; }
		/// <summary>
		/// Диаметр прутка
		/// </summary>
		public decimal? FilamentDiameter { get; set; }
	}
}
