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
		private readonly Data.PokedexModel Model_;
		public EggGroups(Data.PokedexModel Model)
		{
			Model_ = Model;
		}
		public IEnumerable<Dto.IndexedItem> GetEggGroups(SLLog Log)
		{
			Log = Logging.GetLog(Log);
			var res = new List<Dto.IndexedItem>();
			var groups = Model_.EggGroups.OrderBy(e => e.Index);
			foreach (var v in groups)
			{
				res.Add(new Dto.IndexedItem(v));
			}
			return res;
		}
	}
}