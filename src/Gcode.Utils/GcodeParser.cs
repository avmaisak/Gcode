using System.Text;
using Gcode.Entity;
using Gcode.Utils.Infrastructure;
using Gcode.Utils.Interfaces;

namespace Gcode.Utils
{
	/// <summary>
	/// парсер 
	/// </summary>
	public class GcodeParser : IGcodeParser<GcodeCommandFrame>
	{
		private string _rawFrame;
		private readonly string _commentChar;
		private void InitRawFrame(string rawFrame)
		{
			_rawFrame = rawFrame.TrimString();
		}
		public GcodeParser(string rawFrame, string commentChar = ";")
		{
			InitRawFrame(rawFrame);
			_commentChar = commentChar;
		}
		/// <summary>
		/// IsNullOrEror
		/// </summary>
		/// <returns></returns>
		private bool IsNullOrErorFrame => string.IsNullOrWhiteSpace(_rawFrame);
		/// <summary>
		/// Кадр является комментарием
		/// </summary>
		/// <returns></returns>
		public bool IsComment => !IsNullOrErorFrame && _rawFrame.StartsWith(_commentChar);
		/// <summary>
		/// ContainsComment
		/// </summary>
		/// <returns></returns>
		public bool ContainsComment {
			get {
				if (IsNullOrErorFrame)
				{
					return false;
				}

				if (!_rawFrame.StartsWith(_commentChar) && !_rawFrame.Contains(_commentChar))
				{
					return true;
				}

				return false;
			}
		}
		/// <summary>
		/// EmptyComment
		/// </summary>
		public bool EmptyComment => _rawFrame.Length == 1 && _rawFrame == _commentChar;
		/// <summary>
		/// Deserialize
		/// </summary>
		/// <returns></returns>
		public GcodeCommandFrame DeserializeObject()
		{
			//нет информации о кадре
			if (IsNullOrErorFrame)
			{
				return null;
			}
			//пустой комментарий
			if (EmptyComment)
			{
				return null;
			}
			//инициализация кадра
			var gcodeCommandFrame = new GcodeCommandFrame();
			//является комментарием
			if (IsComment)
			{
				gcodeCommandFrame.Comment = _rawFrame.Replace(_commentChar, string.Empty);
			}
			else
			{
				//содержит комментарий
				if (ContainsComment)
				{
					var r = _rawFrame.Split(_commentChar);
					if (r.Length == 2)
					{
						_rawFrame = r[0].Trim();
						gcodeCommandFrame.Comment = r[1].Trim();
					}
				}
			}



			return null;
		}
		/// <inheritdoc />
		/// <summary>
		/// Deserialize
		/// </summary>
		/// <param name="raw"></param>
		/// <returns></returns>
		public GcodeCommandFrame DeserializeObject(string raw)
		{
			throw new System.NotImplementedException();
		}
		/// <inheritdoc />
		/// <summary>
		/// Serialize
		/// </summary>
		/// <param name="gcodeCommandFrame"></param>
		/// <returns></returns>
		public string SerializeObject(GcodeCommandFrame gcodeCommandFrame)
		{
			throw new System.NotImplementedException();
		}
		/// <summary>
		/// Normalize frame
		/// </summary>
		/// <returns></returns>
		public string NormalizeRawFrame()
		{
			
			var currentRawFrame = new StringBuilder(_rawFrame);

			for (var i = 0; i < _rawFrame.Length; i++)
			{
				var charRawFrame = _rawFrame[i];
				
				var isIntPrev = false;
				if (i > 0) {
					isIntPrev = char.IsNumber(_rawFrame[i - 1]);
				}

				var isLetter = char.IsLetter(charRawFrame);

				if (isLetter && isIntPrev)
				{
					currentRawFrame.Insert(i, " ");
				}

			}

			return currentRawFrame.ToString();
		}
	}
}