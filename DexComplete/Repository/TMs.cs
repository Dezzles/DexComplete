using SharpLogging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DexComplete.Repository
{
	public class TMs
	{
		public static IEnumerable<Dto.TM> GetTMs(string GameId, SLLog Log)
		{
			Log = Utilities.Logging.GetLog(Log);
			var game = Games.GetGameById(GameId, Log);
			if (game == null)
				return null;

			using (Data.PokedexModel ctr = new Data.PokedexModel())
			{
				var ret = new List<Dto.TM>(); 
				var tmList = ctr.TMs.Where(u => u.TmSetId == game.TmSetId)
					.OrderBy(u => u.Index);
				foreach (var v in tmList)
				{
					ret.Add(new Dto.TM(v));
				}
				return ret;
			}
		}
	}
}