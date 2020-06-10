using System;
using System.Linq;
using Gcode.Utils.Entity.Slicer;
using LibBase.Extensions;

namespace Gcode.Utils.SlicerParser
{
	/// <summary>
	/// KISSlicer Gcode parser.
	/// </summary>
	public class KisSlicerParser : SlicerParserBase<KisSlicerInfo>
	{
		public override KisSlicerInfo GetSlicerInfo(string[] fileContent)
		{
			var name = fileContent.FirstOrDefault(x => x.StartsWith("; KISSlicer "));
			if (name == null) return null;

			var res = new KisSlicerInfo
			{
				Name = name.Split(';')[1]?.Split('-')[0]?.Trim() ?? string.Empty,
				Version = fileContent.FirstOrDefault(x => x.StartsWith("; version"))?.Split(';')?[1]?.Trim() ?? string.Empty,
				Edition = name.Split(';')[1]?.Split('-')[1]?.Trim() ?? string.Empty
			};

			if (string.IsNullOrWhiteSpace(res.Name)) return null;

			var buildTimeStr = fileContent.FirstOrDefault(x => 
				x.StartsWith("; Estimated Build Time:") ||
				x.StartsWith("; Estimated-in-GUI Build Time") 
			);

			if (buildTimeStr != null) res.EstimatedBuildTime = 
				Convert.ToDecimal(
					buildTimeStr
					.Trim()
					.Split(':')?[1]?
					.Split(new[] { "minutes" }, StringSplitOptions.RemoveEmptyEntries)[0]
					.Trim()
					.Replace(".",",")
			);

			var buildCostStr = fileContent.FirstOrDefault(x => 
				x.StartsWith("; Estimated Build Cost:") ||
				x.StartsWith("; Estimated-in-GUI Build Cost") 
			);

			if (buildCostStr != null) res.EstimatedBuildCost = Convert.ToDecimal(buildCostStr.Split('$')?[1]?.Trim().Replace(".",","));

			var totalEstimatedPreCoolMinutes = fileContent.FirstOrDefault(x => x.StartsWith("; Total estimated (pre-cool) minutes:"));
			if (totalEstimatedPreCoolMinutes != null)
			{
				res.TotalEstimatedPreCoolMinutes = Convert.ToDecimal(totalEstimatedPreCoolMinutes.Split(':')?[1]?.Trim().Replace(".",","));
			}

			var filamentUsageExist = fileContent.FirstOrDefault(x => x.StartsWith("; Filament used per extruder:")) != null;
			if (filamentUsageExist)
			{
				var filamentUsageExt1 = fileContent.FirstOrDefault(x => 
					x.StartsWith(";    Ext 1 = ") ||
					x.StartsWith(";    Ext #1  =") 
				);

				if (filamentUsageExt1 != null)
				{
					filamentUsageExt1 = filamentUsageExt1.TrimString();

					res.FilamentUsedExtruder1  = Convert.ToDecimal(
						filamentUsageExt1
							.Split('=')[1]?
							.Split(new[] { "mm" }, StringSplitOptions.RemoveEmptyEntries)[0]
							.Replace(".",",")
							.Trim()
					);

					res.FilamentUsedExtruder1Volume = Convert.ToDecimal(
						filamentUsageExt1
							.Split('(')[1]
							.Split(new[] { "cm" }, StringSplitOptions.RemoveEmptyEntries)[0]
							.Replace(".",",")
							.Trim()
					);
				}

				var filamentUsageExt2 = fileContent.FirstOrDefault(x => 
					x.StartsWith(";    Ext 2 = ") ||
					x.StartsWith(";    Ext #2  =")
				);

				if (filamentUsageExt2 != null)
				{
					res.FilamentUsedExtruder2  = Convert.ToDecimal(
						filamentUsageExt2
							.Split('=')[1]?
							.Split(new[] { "mm" }, StringSplitOptions.RemoveEmptyEntries)[0]
							.Replace(".",",")
							.Trim()
					);

					res.FilamentUsedExtruder2Volume = Convert.ToDecimal(
						filamentUsageExt2
							.Split('(')[1]
							.Split(new[] { "cm" }, StringSplitOptions.RemoveEmptyEntries)[0]
							.Replace(".",",")
							.Trim()
					);
				}
			}


			var fiberDiameter = fileContent.FirstOrDefault(x => x.StartsWith("; fiber_dia_mm"));
			if (!string.IsNullOrWhiteSpace(fiberDiameter)) res.FilamentDiameter = Convert.ToDecimal(fiberDiameter.Split(' ')?[3]?.Trim().Replace(".", ","));

			return res;
		}
	}
}
