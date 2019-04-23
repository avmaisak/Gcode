using Gcode.Utils.Entity;

namespace Gcode.Utils.SlicerParser
{
	public abstract class SlicerParserBase<T> where T: SlicerInfo
	{
		public abstract T GetSlicerInfo(string[] fileContent);
	}
}
