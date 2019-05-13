using System;
using System.Linq;
using Gcode.Utils.Entity;
using Gcode.Utils.Entity.Base;
using Gcode.Utils.Entity.Interfaces;

namespace Gcode.Utils.SlicerParser
{
	public class SlicerParserDefault: SlicerParserBase<ISlicerInfo>
	{
		public override ISlicerInfo GetSlicerInfo(string[] fileContent)
		{
			ISlicerInfo slicerInfo = new SlicerInfoBase();
			var frames = fileContent.Select(x => x.ToGcodeCommandFrame()).ToList();

			slicerInfo.FilamentUsedExtruder1 = Math.Round(Convert.ToDecimal(frames.Where(x => x.E != null).Sum(x => x.E)), 2);

			return slicerInfo;
		}
	}
}
