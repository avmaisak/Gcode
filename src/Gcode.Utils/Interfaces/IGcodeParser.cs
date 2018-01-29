namespace Gcode.Utils.Interfaces
{
	public interface IGcodeParser<T>
	{
		T DeserializeObject(string raw);
		string SerializeObject(T gcodeCommandFrame);
	}
}
