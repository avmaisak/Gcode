using System;
using System.Linq;
using Gcode.Utils.Entity;

namespace Gcode.Utils.SlicerParser
{
	/// <summary>
	/// Simplify3D
	/// </summary>
	public class Simplify3dParser : SlicerParserBase<Simplify3dInfo>
	{
		public override Simplify3dInfo GetSlicerInfo(string[] fileContent)
		{
			var name = fileContent[0];
			if (name == null || !name.Contains("Simplify3D"))
			{
				return null;
			}

			var res = new Simplify3dInfo
			{
				Name = name.Split(new[] { "Version" }, StringSplitOptions.RemoveEmptyEntries)[0].Split(' ')[4]?.Replace("(R)", string.Empty) ?? string.Empty,
				Version = name.Split(new[] { "Version" }, StringSplitOptions.RemoveEmptyEntries)[1].Trim(),
				// Simplify3D does not provide edition
				Edition = null,
			};

			if (string.IsNullOrWhiteSpace(res.Name))
			{
				return null;
			}

			var buildTime = fileContent.FirstOrDefault(x => x.Contains("Build time:"));
			if (!string.IsNullOrWhiteSpace(buildTime))
			{
				var hours = 0;
				var minutes = 0;
				//  hours
				if (buildTime.Contains("hour"))
				{
					hours = Convert.ToInt32(buildTime.Split(new[] { "hours" }, StringSplitOptions.RemoveEmptyEntries)[0].Split(' ')?[5] ?? "0");
				}

				if (buildTime.Contains("minute"))
				{
					minutes = Convert.ToInt32(buildTime.Split(new[] { "minutes" }, StringSplitOptions.RemoveEmptyEntries)[0].Split(' ')?[7] ?? "0");
				}

				res.EstimatedBuildTime = Convert.ToDecimal(hours) * (decimal) 60.00 + Convert.ToDecimal(minutes);
			}

			var buildCostStr = fileContent.FirstOrDefault(x => x.Contains("Material cost:"));
			if (!string.IsNullOrWhiteSpace(buildCostStr))
			{
				res.EstimatedBuildCost = Convert.ToDecimal(buildCostStr.Split(' ')?[5]?.Replace(".",",") ?? "0" );
			}

			var filamentUsage = fileContent.FirstOrDefault(x => x.Contains("Filament length:"));
			if (!string.IsNullOrWhiteSpace(filamentUsage))
			{
				res.FilamentUsedExtruder1 = Convert.ToDecimal(filamentUsage.Split(' ')?[5]?.Replace(".",",") ?? "0");
			}

			var volume = fileContent.FirstOrDefault(x => x.Contains("Plastic volume:"));
			if (!string.IsNullOrWhiteSpace(volume))
			{
				res.FilamentUsedExtruder1Volume = Convert.ToDecimal(volume.Split(' ')?[5]?.Replace(".",",") ?? "0");
			}

			var filamentDiameter = fileContent.FirstOrDefault(x => x.StartsWith(";   filamentDiameters,"));
			if (!string.IsNullOrWhiteSpace(filamentDiameter))
			{
				res.FilamentDiameter = Convert.ToDecimal(filamentDiameter.Split(',')[1].Split('|')[0].Replace(".",","));
			}

			return res;
		}
	}
}
