using DexComplete.Utilities;
using SharpLogging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DexComplete.Repository
{
	public class EggGroups
	{
		public static IEnumerable<Dto.IndexedItem> GetEggGroups(SLLog Log)
		{
			Log = Logging.GetLog(Log);
			using (Data.PokedexModel ctr = new Data.PokedexModel())
			{
				var res = new List<Dto.IndexedItem>();
				var groups = ctr.EggGroups.OrderBy(e => e.Index);
				foreach (var v in groups)
				{
					res.Add(new Dto.IndexedItem(v));
				}
				return res;
			}
		}
	}
}