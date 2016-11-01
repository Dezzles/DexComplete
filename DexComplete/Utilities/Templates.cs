using SharpLogging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace DexComplete.Utilities
{
	public class Templates
	{
		public static string GetTemplate(string templateName, SLLog Log)
		{
			Log = Logging.GetLog(Log);
			var assembly = Assembly.GetExecutingAssembly();
			var textStreamReader = new StreamReader(assembly.GetManifestResourceStream("DexComplete.Templates." + templateName));
			return textStreamReader.ReadToEnd();
		}
	}
}