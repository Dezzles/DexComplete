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
		public static string GetTemplate(string templateName)
		{
			var assembly = Assembly.GetExecutingAssembly();
			var textStreamReader = new StreamReader(assembly.GetManifestResourceStream("DexComplete.Templates." + templateName));
			return textStreamReader.ReadToEnd();
		}
	}
}