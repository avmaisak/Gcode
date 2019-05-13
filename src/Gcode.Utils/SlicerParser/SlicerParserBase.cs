using Gcode.Utils.Entity.Interfaces;

namespace Gcode.Utils.SlicerParser
{
	public abstract class SlicerParserBase<T> where T: ISlicerInfo
	{
		public abstract T GetSlicerInfo(string[] fileContent);
	}
}
