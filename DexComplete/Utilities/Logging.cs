using SharpLogging;
using SharpLogging.Outputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DexComplete.Utilities
{
	public static class Logging
	{
		private static SharpLogging.SharpLogging Logger;

		static Logging()
		{
			var str = /*HttpContext.Current.Server.MapPath/**/("D:/logs/dexcomplete.log");
			Logger = new SharpLogging.SharpLogging(new List<SLConfiguration>() {
				/*new SharpLogging.Outputs.SLDebug(),/**/
				new SharpLogging.Outputs.SLFile(str) { LogLevel = SharpLogging.LoggingLevel.SILLY } });
			Logger.Log(LoggingLevel.ERROR, "Test", Guid.NewGuid(), new { test = "test" });
		}

		public static SLLog GetLog(SLLog Log = null)
		{
			if (Log == null)
				Log = Logger.Instance();
			return Log;
		}

		public static SLLog Instance()
		{
			return Logger.Instance();
		}
	}
}