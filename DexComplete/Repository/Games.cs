using DexComplete.Utilities;
using SharpLogging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DexComplete.Repository
{
	public class Games
	{
		private readonly Data.PokedexModel Model_;
		public Games(Data.PokedexModel Model)
		{
			Model_ = Model;
		}
		public IEnumerable<Dto.Game> GetGames(SLLog Log)
		{
			Log = Logging.GetLog(Log);
			var res = new List<Dto.Game>();
			var games = Model_.Games;
			foreach (var v in games)
			{
				res.Add(new Dto.Game(v));
			}
			return res;
		}

		public IEnumerable<Dto.Collection> GetCollectionsByGame(string GameId, SLLog Log)
		{
			Log = Logging.GetLog(Log);
			var result = Model_.Games.SingleOrDefault(u => u.GameId == GameId);
			if (result == null)
			{
				return null;
			}
			var coll = new List<Dto.Collection>();
			var sch = result.Collections;
			foreach (var v in sch)
			{
				coll.Add(new Dto.Collection(v));
			}
			return coll;
		}

		public Dto.Game GetGameById(string GameId, SLLog Log)
		{
			Log = Logging.GetLog(Log);
			var result = Model_.Games.SingleOrDefault(e => e.GameId == GameId.ToLower());
			if (result == null)
				return null;
			return new Dto.Game(result);
		}
	}
}