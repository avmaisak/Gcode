using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Gcode.Utils.Common;
using Gcode.Utils.Entity;
using LibBase.Extensions;

namespace Gcode.Utils
{
	/// <summary>
	/// парсер 
	/// </summary>
	public static class GcodeParser
	{
		#region private
		private static readonly CultureInfo Culture = CultureInfo.InvariantCulture;
		private static string _rawFrame;
		private const string CommentChar = ";";
		/// <summary>
		/// To Gcode CommandFrame
		/// </summary>
		/// <returns></returns>
		private static GcodeCommandFrame ToGcodeCommandFrame(IEnumerable<KeyValuePair<string, string>> frameSegments)
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
				if (fieldInfo == null) continue;

				//получить информацию свойства поля кадра
				var fileldInfoType = fieldInfo.PropertyType;
				// тип универсален, и универсальный тип - Nullable
				if (!fileldInfoType.IsGenericType || fileldInfoType.GetGenericTypeDefinition() != typeof(Nullable<>)) continue;
				//объект назначения
				var obj = gcodeCommandFrame;
				//значение поля
				var fieldValue = value;
				//свойства поля кадра
				fileldInfoType = fileldInfoType.GetGenericArguments()[0];
				//указание значения сегмента кадра
				fieldInfo.SetValue(obj, Convert.ChangeType(fieldValue, fileldInfoType, Culture));
			}
			return gcodeCommandFrame;
		}
		#endregion
		/// <summary>
		/// To GCode
		/// </summary>
		/// <returns></returns>
		public static GcodeCommandFrame ToGCode(string raw)
		{
			_rawFrame = NormalizeGcodeRawFrame(raw.TrimString());
			var frameComment = string.Empty;
			//инициализация кадра
			var gcodeCommandFrame = new GcodeCommandFrame();
			//нет информации о кадре
			if (_rawFrame.IsNullOrErrorFrame()) return new GcodeCommandFrame();
			//пустой комментарий
			if (_rawFrame.IsEmptyComment())
			{
				gcodeCommandFrame.Comment = string.Empty;
				return gcodeCommandFrame;
			}
			//является комментарием
			if (_rawFrame.IsGcodeComment())
			{
				gcodeCommandFrame.Comment = _rawFrame.ReplaceAtIndex(0, ' ').Trim();
				return gcodeCommandFrame;
			}

			//содержит комментарий
			if (_rawFrame.ContainsGcodeComment())
			{
				var r = _rawFrame.Split(CommentChar);
				if (r.Length == 2)
				{
					_rawFrame = r[0].Trim();
					frameComment = r[1].Trim();
				}
			}

			gcodeCommandFrame = ToGcodeCommandFrame(_rawFrame.ToKeyValuePair());

			if (!string.IsNullOrWhiteSpace(frameComment)) gcodeCommandFrame.Comment = frameComment;

			return gcodeCommandFrame;
		}
		/// <summary>
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
		public static string ToStringCommand(this GcodeCommandFrame gcodeCommandFrame, bool ignoreComments = false)
		{
			var cmdSegmentStringBuilder = new StringBuilder();
			var objectProperties = gcodeCommandFrame.GetProperties();
			var commentString = string.Empty;
			var addedLines = 0;

			foreach (var objProp in objectProperties)
			{
				var commandSeparator = " ";
				var isNotEmpty = !string.IsNullOrWhiteSpace(objProp.Value?.Trim());

				if (!isNotEmpty) continue;
				if (objProp.Key != "Comment")
				{
					var commandKey = objProp.Key;

					if (addedLines == 0) commandSeparator = string.Empty;

					if (objProp.Key == "CheckSum" && !string.IsNullOrWhiteSpace(objProp.Value)) commandKey = "*";

					var cmdFrameSegmentStr = $"{commandSeparator}{commandKey}{objProp.Value}";

					cmdSegmentStringBuilder.Append(cmdFrameSegmentStr);
					addedLines++;
				}
				else
				{
					if (!ignoreComments) commentString = objProp.Value;
				}
			}

			var res = !string.IsNullOrWhiteSpace(commentString) ? $"{cmdSegmentStringBuilder} ;{commentString}" : cmdSegmentStringBuilder.ToString();
			return res.Trim();
		}
		/// <summary>
		/// ToJson
		/// </summary>
		/// <returns></returns>
		public static string GcodeToJson(this string raw)
		{
			_rawFrame = NormalizeGcodeRawFrame(raw);
			const string objJsonStart = "{";
			const string objJsonEnd = "}";

			if (_rawFrame.IsGcodeComment() || _rawFrame.IsNullOrErrorFrame()) return $"{{Comment:{raw.Replace(";", null).Replace("\r", null).Trim()}}}";

			var commentString = string.Empty;

			if (_rawFrame.ContainsGcodeComment())
			{
				var arr = _rawFrame.Split(";");
				_rawFrame = arr[0].Trim();
				commentString = $",\"Comment\":\"{arr[1].Trim().Replace("\r", null)}\"";
			}

			var segments = _rawFrame.ToKeyValuePair();
			var segmentsKeyValuePair = segments as KeyValuePair<string, string>[] ?? segments.ToArray();

			var sb = new StringBuilder();
			sb.Append(objJsonStart);
			for (var i = 0; i < segmentsKeyValuePair.Length; i++)
			{
				var sep = ",";
				if (i == segmentsKeyValuePair.Length - 1) sep = string.Empty;

				var segment = segmentsKeyValuePair[i];
				var jsonObjLine = $"\"{segment.Key}\":\"{segment.Value}\"{sep}";

				sb.Append(jsonObjLine.Trim());
			}

			if (!string.IsNullOrWhiteSpace(commentString)) sb.Append(commentString);

			sb.Append(objJsonEnd);

			return sb.ToString();
		}

		/// <summary>
		/// ToJson
		/// </summary>
		/// <param name="gcodeCommandFrame"></param>
		/// <returns></returns>
		public static string ToJson(this GcodeCommandFrame gcodeCommandFrame) => GcodeToJson(ToStringCommand(gcodeCommandFrame));
		/// <summary>
		/// Normalize
		/// </summary>
		/// <param name="raw"></param>
		/// <returns></returns>
		public static string NormalizeGcodeRawFrame(this string raw)
		{
			if (raw != null) _rawFrame = raw;

			var resultFrame = new List<string>();
			string frameStr;
			var commentString = string.Empty;

			if (_rawFrame.IsGcodeComment() || _rawFrame.IsEmptyComment() || _rawFrame.IsNullOrErrorFrame())
			{
				if (string.IsNullOrWhiteSpace(_rawFrame)) return string.Empty;
				_rawFrame = _rawFrame.Trim();
				return _rawFrame;
			}

			if (_rawFrame.ContainsGcodeComment())
			{
				var frameCommentArr = _rawFrame.Split(";");
				frameStr = frameCommentArr[0].RemoveAllSpaces();
				commentString = frameCommentArr[1].Trim().Trim();
				_rawFrame = frameStr;
			}

			else
			{
				_rawFrame = _rawFrame.RemoveAllSpaces();
				frameStr = _rawFrame;
			}

			for (var i = 0; i < frameStr.Length; i++)
			{

				var charRawFrame = frameStr[i];
				var isChar = char.IsLetter(charRawFrame);

				var res = charRawFrame.ToString();
				if (isChar && i > 0) res = $" {res}";
				resultFrame.Add(res);
			}

			var resultFrameStr = string.Join(string.Empty, resultFrame.ToArray());

			if (!string.IsNullOrWhiteSpace(commentString)) resultFrameStr = $"{resultFrameStr} ;{commentString}";

			_rawFrame = string.Join(string.Empty, resultFrameStr);
			return _rawFrame;
		}
		/// <summary>
		/// Contains Comment
		/// </summary>
		/// <param name="raw"></param>
		/// <returns></returns>
		public static bool ContainsGcodeComment(this string raw)
		{
			_rawFrame = raw.Trim();
			return !_rawFrame.StartsWith(CommentChar) && _rawFrame.Contains(CommentChar);
		}
		/// <summary>
		/// IsComment
		/// </summary>
		/// <param name="raw"></param>
		/// <returns></returns>
		public static bool IsGcodeComment(this string raw)
		{
			_rawFrame = raw.Trim();
			return !_rawFrame.IsNullOrErrorFrame() && _rawFrame.StartsWith(CommentChar);
		}
		/// <summary>
		/// Is Null Or Error Frame
		/// </summary>
		/// <param name="raw"></param>
		/// <returns></returns>
		public static bool IsNullOrErrorFrame(this string raw)
		{
			_rawFrame = raw.Trim();
			return string.IsNullOrWhiteSpace(_rawFrame);
		}
		/// <summary>
		/// IsEmpty Comment
		/// </summary>
		/// <param name="raw"></param>
		/// <returns></returns>
		public static bool IsEmptyComment(this string raw)
		{
			_rawFrame = raw.Trim();
			return _rawFrame.Length == 1 && _rawFrame == CommentChar;
		}

		/// <summary>
		/// ToGcodeCommandFrame.
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static GcodeCommandFrame ToGcodeCommandFrame(this string str) => ToGCode(str);
	}
}