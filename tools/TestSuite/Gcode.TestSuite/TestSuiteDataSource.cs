using System;
using System.IO;
using System.Net;

namespace Gcode.TestSuite
{
	public class TestSuiteDataSource
	{
		/// <summary>
		/// GcodeTestContentLocal 
		/// user specific. on other project locatiomn this test will fail
		/// </summary>
		public static string GcodeTestContentLocal {
			get {
				const string filePath = "C:\\Users\\User\\Documents\\Projects\\Github\\!Rb\\Gcode\\misc\\gcode\\pattern_blade_fp_piece2_v1.gcode";
				var fileContent = File.ReadAllText(filePath);
				return fileContent;
			}
		}
		/// <summary>
		/// GcodeTestContentRemote
		/// </summary>
		public static string GcodeTestContentRemote {
			get {
				const string uriStr = "https://downloads.s1.rus-bit.com/public/Shared/Gcode/pattern_blade_fp_piece2_v1.gcode";
				string fileContent;

				using (var webClient = new WebClient())
				{
					var uri = new Uri(uriStr);
					ServicePointManager.ServerCertificateValidationCallback += (send, certificate, chain, sslPolicyErrors) => true;
					fileContent = webClient.DownloadString(uri);
				}

				return fileContent;
			}
		}
	}
}
