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
		private readonly SLLog Log_;
		public Pokedexes(Repository.Games Games, Data.PokedexModel Model, SLLog Log)
		{
			this.Games_ = Games;
			this.Model_ = Model;
			this.Log_ = Log;
		}
		public IEnumerable<Dto.Pokedex> GetPokedexesByGame(string GameId)
		{
			
			var game = Games_.GetGameById(GameId);
			if (game == null)
			{
				Log_.Error("GetPokedexesByGame", new { message = "Game not found", GameId = GameId });
				return null;
			}
			return game.Pokedexes;
		}

		public Dto.Pokedex GetPokedex(String PokedexId)
		{
			
			var dex = Model_.Pokedexes.SingleOrDefault(u => u.PokedexId == PokedexId);
			if (dex == null)
			{
				Log_.Error("GetPokedex", new { message = "Pokedex not found", PokedexId = PokedexId });
				return null;
			}
			return new Dto.Pokedex(dex, true);
		}
	}
}