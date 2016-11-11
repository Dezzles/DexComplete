using SharpLogging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DexComplete.Repository
{
	public class TMs
	{
		private readonly Repository.Games Games_;
		private readonly Data.PokedexModel Model_;

		public TMs(Repository.Games Games, Data.PokedexModel Model)
		{
			this.Games_ = Games;
			this.Model_ = Model;
		}
		public IEnumerable<Dto.TM> GetTMs(string GameId, SLLog Log)
		{
			Log = Utilities.Logging.GetLog(Log);
			var game = Games_.GetGameById(GameId, Log);
			if (game == null)
				return null;

			var ret = new List<Dto.TM>(); 
			var tmList = this.Model_.TMs.Where(u => u.TmSetId == game.TmSetId)
				.OrderBy(u => u.Index);
			foreach (var v in tmList)
			{
				ret.Add(new Dto.TM(v));
			}
			return ret;
		}
	}
}