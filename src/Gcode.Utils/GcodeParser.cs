using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Gcode.Common.Utils;
using Gcode.Entity;

namespace Gcode.Utils {
	/// <summary>
	/// парсер 
	/// </summary>
	public static class GcodeParser {
		#region private
		/// <summary>
		/// Культура
		/// </summary>
		private static readonly CultureInfo Culture = CultureInfo.InvariantCulture;
		private static string _rawFrame;
		private const string CommentChar = ";";
		private const string ObjJsonStart = "{";
		private const string ObjJsonEnd = "}";
		/// <summary>
		/// IsNullOrEror
		/// </summary>
		/// <returns></returns>
		private static bool IsNullOrErorFrame => string.IsNullOrWhiteSpace(_rawFrame);
		/// <summary>
		/// Кадр является комментарием
		/// </summary>
		/// <returns></returns>
		private static bool IsComment => !IsNullOrErorFrame && _rawFrame.StartsWith(CommentChar);
		/// <summary>
		/// ContainsComment
		/// </summary>
		/// <returns></returns>
		private static bool ContainsComment {
			get {
				if (IsNullOrErorFrame) {
					return false;
				}

				return !_rawFrame.StartsWith(CommentChar) && _rawFrame.Contains(CommentChar);
			}
		}
		/// <summary>
		/// EmptyComment
		/// </summary>
		private static bool EmptyComment => _rawFrame.Length == 1 && _rawFrame == CommentChar;
		/// <summary>
		/// Перебор сегментов
		/// </summary>
		private static IEnumerable<KeyValuePair<string, string>> HandleSegments() {
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
		/// Normalize frame
		/// </summary>
		/// <returns></returns>
		// ReSharper disable once InconsistentNaming
		private static string normalizeRawFrame(string frame = null) {

			if (frame != null) {
				_rawFrame = frame;
			}

			_rawFrame = _rawFrame.RemoveAllSpaces();

			var resultFrame = new List<string>();
			string frameStr;
			var commentString = string.Empty;

			if (IsComment || EmptyComment || IsNullOrErorFrame) {
				return string.Empty;
			}

			if (ContainsComment) {
				var frameCommentArr = _rawFrame.Split(";");
				frameStr = frameCommentArr[0].RemoveAllSpaces();
				commentString = frameCommentArr[1].Trim().RemoveAllSpaces();
			}

			else {
				frameStr = _rawFrame;
			}

			for (var i = 0; i < frameStr.Length; i++) {
				var charRawFrame = frameStr[i];
				var isChar = char.IsLetter(charRawFrame);

				var res = charRawFrame.ToString();
				if (isChar) {
					res = $" {res}";
				}

				//var isIntPrev = false;
				//if (i > 0)
				//{
				//	var prev = frameStr[i - 1];
				//	isIntPrev = char.IsNumber(prev) || char.IsSymbol(prev); 
				//}

				//var isLetter = char.IsLetter(charRawFrame);
				//var res = charRawFrame.ToString();

				//if (isLetter && isIntPrev) {
				//	res = $" {res}";
				//}
				resultFrame.Add(res);
			}

			var resultFrameStr = string.Join(string.Empty, resultFrame.ToArray());

			if (!string.IsNullOrWhiteSpace(commentString)) {
				resultFrameStr = $"{resultFrameStr} ;{commentString}";
			}

			_rawFrame = string.Join(string.Empty, resultFrameStr);
			return _rawFrame;
		}
		/// <summary>
		/// ToGcodeCommandFrame
		/// </summary>
		/// <returns></returns>
		private static GcodeCommandFrame ToGcodeCommandFrame(IEnumerable<KeyValuePair<string, string>> frameSegments) {
			var gcodeCommandFrame = new GcodeCommandFrame();
			foreach (var frameSegment in frameSegments) {
				//команда
				var key = frameSegment.Key;
				//значение
				var value = frameSegment.Value;
				//получить свойство кадра
				var fieldInfo = gcodeCommandFrame.GetType().GetProperty(key);
				//свойство есть
				if (fieldInfo != null) {
					//получить информацию свойства поля кадра
					var fileldInfoType = fieldInfo.PropertyType;
					// тип универсален, и универсальный тип - Nullable
					if (fileldInfoType.IsGenericType && fileldInfoType.GetGenericTypeDefinition() == typeof(Nullable<>)) {
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
		#endregion
		/// <summary>
		/// ToGCode
		/// </summary>
		/// <returns></returns>
		public static GcodeCommandFrame ToGCode(string raw) {
			_rawFrame = normalizeRawFrame(raw.TrimString());
			var frameComment = string.Empty;
			//инициализация кадра
			var gcodeCommandFrame = new GcodeCommandFrame();
			//нет информации о кадре
			if (IsNullOrErorFrame) {
				return new GcodeCommandFrame();
			}
			//пустой комментарий
			if (EmptyComment) {
				gcodeCommandFrame.Comment = string.Empty;
				return gcodeCommandFrame;
			}
			//является комментарием
			if (IsComment) {

				gcodeCommandFrame.Comment = _rawFrame.ReplaceAtIndex(0, ' ').Trim();
				return gcodeCommandFrame;
			}

			//содержит комментарий
			if (ContainsComment) {
				var r = _rawFrame.Split(CommentChar);
				if (r.Length == 2) {
					_rawFrame = r[0].Trim();
					frameComment = r[1].Trim();
				}
			}



			gcodeCommandFrame = ToGcodeCommandFrame(HandleSegments());

			if (!string.IsNullOrWhiteSpace(frameComment)) {
				gcodeCommandFrame.Comment = frameComment;
			}

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
		public static string ToStringCommand(GcodeCommandFrame gcodeCommandFrame, bool ignoreComments = false) {
			var o = gcodeCommandFrame;
			var cmdSegmentStringBuilder = new StringBuilder();
			var objectProperties = ReflectionUtils.GetProperties(o);
			var commentString = string.Empty;
			var addedLines = 0;

			foreach (var objProp in objectProperties) {
				var commandSeparator = " ";
				var isNotEmpty = objProp.Value != null && !string.IsNullOrWhiteSpace(objProp.Value.Trim());

				if (isNotEmpty) {
					if (objProp.Key != "Comment") {
						var commandKey = objProp.Key;

						if (addedLines == 0) {
							commandSeparator = string.Empty;
						}

						if (objProp.Key == "CheckSum" && !string.IsNullOrWhiteSpace(objProp.Value)) {
							commandKey = "*";
						}

						var cmdFrameSegmentStr = $"{commandSeparator}{commandKey}{objProp.Value}";

						cmdSegmentStringBuilder.Append(cmdFrameSegmentStr);
						addedLines++;
					}
					else {
						if (!ignoreComments) {
							commentString = objProp.Value;
						}
					}
				}
			}

			var res = !string.IsNullOrWhiteSpace(commentString) ? $"{cmdSegmentStringBuilder} ;{commentString}" : cmdSegmentStringBuilder.ToString();
			return res.Trim();
		}
		/// <summary>
		/// ToJson
		/// </summary>
		/// <returns></returns>
		public static string ToJson(string raw) {
			_rawFrame = NormalizeRawFrame(raw);

			if (IsComment || IsNullOrErorFrame) {
				return $"{{Comment:{raw.Replace(";", null).Replace("\r", null).Trim()}}}";
			}

			var commentString = string.Empty;

			if (ContainsComment) {
				var arr = _rawFrame.Split(";");
				_rawFrame = arr[0].Trim();
				commentString = $",\"Comment\":\"{arr[1].Trim().Replace("\r", null)}\"";
			}


			var segments = HandleSegments();
			var segmentsKeyValuePair = segments as KeyValuePair<string, string>[] ?? segments.ToArray();

			var sb = new StringBuilder();
			sb.Append(ObjJsonStart);
			for (var i = 0; i < segmentsKeyValuePair.Length; i++) {
				var sep = ",";
				if (i == segmentsKeyValuePair.Length - 1) {

					sep = string.Empty;
				}

				var segment = segmentsKeyValuePair[i];
				var jsonObjLine = $"\"{segment.Key}\":\"{segment.Value}\"{sep}";

				sb.Append(jsonObjLine.Trim());
			}

			if (!string.IsNullOrWhiteSpace(commentString)) {
				sb.Append(commentString);
			}

			sb.Append(ObjJsonEnd);

			return sb.ToString();
		}
		/// <summary>
		/// ToJson
		/// </summary>
		/// <param name="gcodeCommandFrame"></param>
		/// <returns></returns>
		public static string ToJson(this GcodeCommandFrame gcodeCommandFrame) {
			var resJson = ToStringCommand(gcodeCommandFrame);
			return ToJson(resJson);
		}
		/// <summary>
		/// Normalize
		/// </summary>
		/// <param name="raw"></param>
		/// <returns></returns>
		public static string NormalizeRawFrame(string raw) {
			return normalizeRawFrame(raw);
		}
	}
}