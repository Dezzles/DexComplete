using DexComplete.Utilities;
using SharpLogging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DexComplete.Repository
{
	public class Updates
	{
		public static IEnumerable<Data.Update> GetUpdates(SLLog Log)
		{
			Log = Logging.GetLog(Log);

			using (Data.PokedexModel ctr = new Data.PokedexModel())
			{
				return ctr.Updates.OrderByDescending(v => v.Date).ToList();
			}
		}

		public static IEnumerable<Data.ComingSoon> GetComingSoon(SLLog Log)
		{
			Log = Logging.GetLog(Log);
			using (Data.PokedexModel ctr = new Data.PokedexModel())
			{
				var result = ctr.ComingSoon;
				return result.ToList();
			}

		}
	}
}