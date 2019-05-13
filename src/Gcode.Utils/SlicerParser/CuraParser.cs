using System;
using System.Linq;
using Gcode.Utils.Entity;

namespace Gcode.Utils.SlicerParser
{
	public class CuraParser: SlicerParserBase<CuraSlicerInfo>
	{
		public override CuraSlicerInfo GetSlicerInfo(string[] fileContent)
		{
			var slicerInfo = new CuraSlicerInfo();
			var name = fileContent.FirstOrDefault(x=> x.Contains("Cura_"));
			if (string.IsNullOrWhiteSpace(name))
			{
				return null;
			}

			slicerInfo.Name = name.Split(' ')?[2];
			if (string.IsNullOrWhiteSpace(slicerInfo.Name))
			{
				return null;
			}

			slicerInfo.Version = name.Split(' ')?[3];

			var buildTime = fileContent.FirstOrDefault(x => x.StartsWith(";TIME"));
			if (buildTime != null)
			{
				slicerInfo.EstimatedBuildTime = Convert.ToDecimal(buildTime?.Split(':')?[1].Replace(".",","));
			}

			var filamentUsed = fileContent.FirstOrDefault(x => x.StartsWith(";Filament used"));

			if (!string.IsNullOrWhiteSpace(filamentUsed))
			{
				// convert ot milimeters, because cura provides in meters.
				slicerInfo.FilamentUsedExtruder1 = Convert.ToDecimal(filamentUsed.Split(':')?[1]?.Split(',')[0]?.Trim().Replace("m", string.Empty).Replace(".", ",")) * (decimal) 1000.00;
			}

			if (slicerInfo.FilamentUsedExtruder1 != null && slicerInfo.FilamentUsedExtruder1 > 0 && slicerInfo.FilamentDiameter != null && slicerInfo.FilamentDiameter > 0)
			{
				// обьем = сечение * длину
				slicerInfo.FilamentUsedExtruder1Volume = slicerInfo.FilamentDiameter * slicerInfo.FilamentUsedExtruder1;
			}

			if (slicerInfo.FilamentUsedExtruder2 != null && slicerInfo.FilamentUsedExtruder2 > 0 && slicerInfo.FilamentDiameter != null && slicerInfo.FilamentDiameter > 0)
			{
				// обьем = сечение * длину
				slicerInfo.FilamentUsedExtruder2Volume = slicerInfo.FilamentDiameter * slicerInfo.FilamentUsedExtruder2;
			}

			return slicerInfo;
		}
	}
}
