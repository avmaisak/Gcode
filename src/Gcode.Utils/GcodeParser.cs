using System.Collections.Generic;
using System.Linq;
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
		#region private

		private string _rawFrame;
		private readonly string _commentChar;
		private void InitRawFrame(string rawFrame)
		{
			_rawFrame = rawFrame.TrimString();
		}

		#endregion

		public string GetRawFrame()
		{
			return _rawFrame;
		}
		/// <summary>
		/// парсер
		/// </summary>
		/// <param name="rawFrame"></param>
		/// <param name="commentChar"></param>
		public GcodeParser(string rawFrame, string commentChar = ";")
		{
			InitRawFrame(rawFrame);
			_commentChar = commentChar;
		}
		/// <summary>
		/// IsNullOrEror
		/// </summary>
		/// <returns></returns>
		public bool IsNullOrErorFrame => string.IsNullOrWhiteSpace(_rawFrame);
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

				if (!_rawFrame.StartsWith(_commentChar) && _rawFrame.Contains(_commentChar))
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

			NormalizeRawFrame();


			return null;
		}
		/// <summary>
		/// Перебор сегментов
		/// </summary>
		public ICollection<KeyValuePair<string, string>> HandleSegments()
		{
			var res = new List<KeyValuePair<string, string>>();
			//сегмент кадра. разделитель пробел
			var frameSegments = _rawFrame.Split(" ");

			//Перебор сегментов
			for (var i = 0; i < frameSegments.Length; i++)
			{
				//Сегмент кадра
				var frameSegment = frameSegments[i];
				//Длина сегмента
				var frameSegmentLength = frameSegment.Length;
				//команда сегмента
				var frameSegmentCommandName = frameSegment.Substring(0, 1);
				//значение команды сегмента
				var frameSegmentCommandValue = frameSegment.Substring(1, frameSegmentLength - 1);
				res.Add(new KeyValuePair<string, string>(frameSegmentCommandName, frameSegmentCommandValue));
			}

			return res;
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

			var resultFrame = new List<string>();
			string frame;
			var commentString = string.Empty;

			if (IsComment || EmptyComment || IsNullOrErorFrame)
			{
				return _rawFrame;
			}

			if (ContainsComment)
			{
				var frameCommentArr = _rawFrame.Split(";");
				frame = frameCommentArr[0].Trim();
				commentString = frameCommentArr[1].Trim();
			}
			else
			{
				frame = _rawFrame;
			}

			for (var i = 0; i < frame.Length; i++)
			{
				var charRawFrame = frame[i];

				var isIntPrev = false;
				if (i > 0)
				{
					isIntPrev = char.IsNumber(frame[i - 1]);
				}

				var isLetter = char.IsLetter(charRawFrame);
				var res = charRawFrame.ToString();

				if (isLetter && isIntPrev)
				{
					res = $" {res}";
				}
				resultFrame.Add(res);
			}

			var resultFrameStr = string.Join(string.Empty, resultFrame.ToArray());

			if (!string.IsNullOrWhiteSpace(commentString))
			{
				resultFrameStr = $"{resultFrameStr} ;{commentString}";
			}

			return resultFrameStr;
		}
	}
}