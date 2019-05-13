namespace Gcode.Utils.Entity.Interfaces
{
	public interface ISlicerInfo
	{
		/// <summary>
		/// Slicer name.
		/// </summary>
		string Name { get; set; }
		/// <summary>
		/// Slicer name.
		/// </summary>
		string Version { get; set; }
		/// <summary>
		/// Slicer edition.
		/// </summary>
		string Edition { get; set; }
		/// <summary>
		/// Estimated Build Time.
		/// </summary>
		decimal? EstimatedBuildTime{ get; set; }
		/// <summary>
		/// Estimated Build Cost $.
		/// </summary>
		decimal? EstimatedBuildCost{ get; set; }
		/// <summary>
		/// Filament used extruder 1 (mm)
		/// </summary>
		decimal? FilamentUsedExtruder1 { get; set; }
		/// <summary>
		/// Filament used extruder 1 (cm^3)
		/// </summary>
		decimal? FilamentUsedExtruder1Volume { get; set; }
		/// <summary>
		/// Filament used extruder 2 (mm)
		/// </summary>
		decimal? FilamentUsedExtruder2 { get; set; }
		/// <summary>
		/// Filament used extruder 2 (cm^3)
		/// </summary>
		decimal? FilamentUsedExtruder2Volume { get; set; }
		/// <summary>
		/// Total estimated (pre-cool) minutes.
		/// </summary>
		decimal? TotalEstimatedPreCoolMinutes { get; set; }
		/// <summary>
		/// Диаметр прутка (мм)
		/// </summary>
		decimal? FiberDiameter { get; set; }
	}
}
