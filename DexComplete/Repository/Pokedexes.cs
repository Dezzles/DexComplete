using DexComplete.Utilities;
using SharpLogging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DexComplete.Repository
{
	public class Pokedexes
	{
		private readonly Repository.Games Games_;
		private readonly Data.PokedexModel Model_;
		public Pokedexes(Repository.Games Games, Data.PokedexModel Model)
		{
			this.Games_ = Games;
			this.Model_ = Model;
		}
		public IEnumerable<Dto.Pokedex> GetPokedexesByGame(string GameId, SLLog Log)
		{
			Log = Logging.GetLog(Log);
			var game = Games_.GetGameById(GameId, Log);
			if (game == null)
			{
				Log.Error("GetPokedexesByGame", new { message = "Game not found", GameId = GameId });
				return null;
			}
			return game.Pokedexes;
		}

		public Dto.Pokedex GetPokedex(String PokedexId, SLLog Log)
		{
			Log = Logging.GetLog(Log);
			var dex = Model_.Pokedexes.SingleOrDefault(u => u.PokedexId == PokedexId);
			if (dex == null)
			{
				Log.Error("GetPokedex", new { message = "Pokedex not found", PokedexId = PokedexId });
				return null;
			}
			return new Dto.Pokedex(dex, true);
		}
	}
}