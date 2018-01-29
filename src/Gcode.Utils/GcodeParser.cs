using System.Text;
using Gcode.Entity;

namespace Gcode.Utils
{
	/// <summary>
	/// парсер 
	/// </summary>
	public class GcodeParser : IGcodeParser<GcodeCommandFrame>
	{
		private GcodeCommandFrame _gcodeCommandFrame;
		/// <summary>
		/// Кадр является комментарием
		/// </summary>
		/// <param name="rawFrame"></param>
		/// <param name="commentChar"></param>
		/// <returns></returns>
		public static bool IsComment(string rawFrame, string commentChar = ";")
		{
			if (rawFrame == null)
			{
				return false;
			}

			if (!rawFrame.Contains(commentChar))
			{
				return false;
			}

			if (string.IsNullOrWhiteSpace(rawFrame))
			{
				return false;
			}

			rawFrame = rawFrame.Trim();

			if (rawFrame.StartsWith(commentChar))
			{
				return true;
			}

			return false;
		}
		/// <summary>
		/// Deserialize
		/// </summary>
		/// <param name="rawFrame">Сырой кадр</param>
		/// <returns></returns>
		public GcodeCommandFrame DeserializeObject(string rawFrame)
		{
			//нет информации о кадре
			if (string.IsNullOrWhiteSpace(rawFrame))
			{
				return null;
			}

			rawFrame = rawFrame.Trim();
			_gcodeCommandFrame = new GcodeCommandFrame();


			if (rawFrame.StartsWith(";"))
			{
				_gcodeCommandFrame.Comment = rawFrame.Replace(";", null);
				return _gcodeCommandFrame;
			}

			var currentRawFrame = new StringBuilder(rawFrame);
			return _gcodeCommandFrame;
		}
		/// <inheritdoc />
		public string SerializeObject(GcodeCommandFrame gcodeCommandFrame)
		{
			throw new System.NotImplementedException();
		}
	}
}