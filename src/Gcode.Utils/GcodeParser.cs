using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Common.Utils;
using Gcode.Entity;
using Gcode.Utils.Interfaces;

namespace Gcode.Utils
{
	/// <summary>
	/// парсер 
	/// </summary>
	public class GcodeParser : IGcodeParser<GcodeCommandFrame>
	{
		#region private
		/// <summary>
		/// Культура
		/// </summary>
		private static readonly CultureInfo Culture = CultureInfo.InvariantCulture;
		private string _rawFrame;
		private readonly string _commentChar;
		private void InitRawFrame(string rawFrame)
		{
			_rawFrame = rawFrame.TrimString();
		}
		#endregion
		/// <summary>
		/// парсер
		/// </summary>
		/// <param name="rawFrame"></param>
		/// <param name="commentChar"></param>
		public GcodeParser(string rawFrame = null, string commentChar = ";")
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
		/// Перебор сегментов
		/// </summary>
		public ICollection<KeyValuePair<string, string>> HandleSegments()
		{
			//сегмент кадра. разделитель пробел
			var frameSegments = _rawFrame.Split(" ");

			//Перебор сегментов
			return (
					from frameSegment in frameSegments
					let frameSegmentLength = frameSegment.Length
					let frameSegmentCommandName = frameSegment.Substring(0, 1)
					let frameSegmentCommandValue = frameSegment.Substring(1, frameSegmentLength - 1)
					select new KeyValuePair<string, string>(
							frameSegmentCommandName, frameSegmentCommandValue)
				).ToList();
		}
		/// <summary>
		/// Deserialize
		/// </summary>
		/// <returns></returns>
		public GcodeCommandFrame DeserializeObject()
		{
			//инициализация кадра
			var gcodeCommandFrame = new GcodeCommandFrame();
			//нет информации о кадре
			if (IsNullOrErorFrame)
			{
				return null;
			}

			//пустой комментарий
			if (EmptyComment)
			{
				gcodeCommandFrame.Comment = string.Empty;
				return gcodeCommandFrame;
			}

			//является комментарием
			if (IsComment)
			{
				gcodeCommandFrame.Comment = _rawFrame.Replace(_commentChar, string.Empty);
				return gcodeCommandFrame;
			}

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

			NormalizeRawFrame();
			var segments = HandleSegments();

			gcodeCommandFrame = ToGcodeCommandFrame(segments);
			return gcodeCommandFrame;
		}
		/// <inheritdoc />
		/// <summary>
		/// Deserialize
		/// </summary>
		/// <param name="raw"></param>
		/// <returns></returns>
		public GcodeCommandFrame DeserializeObject(string raw)
		{
			_rawFrame = raw.TrimString();
			return DeserializeObject();
		}

		/// <inheritdoc />
		/// <summary>
		/// Serialize
		/// Команды в каждом кадре выполняются одновременно, 
		/// поэтому порядок команд в кадре строго не оговаривается, 
		/// но традиционно предполагается, 
		/// что первыми указываются подготовительные команды 
		/// (например, выбор рабочей плоскости, скоростей перемещений по осям и др.), 
		/// затем задание координат перемещения, 
		/// затем выбора режимов обработки и технологические команды.
		/// Максимальное число элементарных команд и заданий координат в одном кадре 
		/// зависит от конкретного интерпретатора языка управления станками, 
		/// но для большинства популярных интерпретаторов (стоек управления) не превышает 6.
		/// </summary>
		/// <param name="gcodeCommandFrame"></param>
		/// <param name="ignoreComments"></param>
		/// <returns></returns>
		public string SerializeObject(GcodeCommandFrame gcodeCommandFrame, bool ignoreComments = false)
		{

			var o = gcodeCommandFrame;
			var res = string.Empty;
			var objectProperties = ReflectionUtils.GetProperties(o);
			var commentString = string.Empty;
			var addedLines = 0;
			foreach (var objProp in objectProperties)
			{
				var commandSeparator = " ";
				var isNotEmpty = objProp.Value != null && !string.IsNullOrWhiteSpace(objProp.Value.Trim());

				if (isNotEmpty)
				{
					if (objProp.Key != "Comment")
					{
						if (addedLines == 0)
						{
							commandSeparator = string.Empty;
						}

						if (objProp.Value.Contains(","))
						{
							objProp.Value.Replace(',', '.');
						}

						res += $"{commandSeparator}{objProp.Key.ToUpper()}{objProp.Value}";
						addedLines++;
					}
					else
					{
						if (!ignoreComments)
						{
							commentString = objProp.Value;
						}
					}
				}
			}

			if (!string.IsNullOrWhiteSpace(commentString))
			{
				return $"{res} ;{commentString}";
			}

			return res;
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
		/// <summary>
		/// ToGcodeCommandFrame
		/// </summary>
		/// <returns></returns>
		public static GcodeCommandFrame ToGcodeCommandFrame(IEnumerable<KeyValuePair<string, string>> frameSegments)
		{
			var gcodeCommandFrame = new GcodeCommandFrame();
			foreach (var frameSegment in frameSegments)
			{
				//команда
				var key = frameSegment.Key;
				//значение
				var value = frameSegment.Value;
				//получить свойство кадра
				var fieldInfo = gcodeCommandFrame.GetType().GetProperty(key);
				//свойство есть
				if (fieldInfo != null)
				{
					//получить информацию свойства поля кадра
					var fileldInfoType = fieldInfo.PropertyType;
					// тип универсален, и универсальный тип - Nullable
					if (fileldInfoType.IsGenericType && fileldInfoType.GetGenericTypeDefinition() == typeof(Nullable<>))
					{
						//объект назначения
						var obj = gcodeCommandFrame;
						//значение поля
						var fieldValue = value;
						//свойства поля кадра
						fileldInfoType = fileldInfoType.GetGenericArguments()[0];
						//указание значения сегмента кадра

						fieldInfo.SetValue(obj, Convert.ChangeType(fieldValue, fileldInfoType, Culture));
					}
				}
			}
			return gcodeCommandFrame;
		}
	}
}