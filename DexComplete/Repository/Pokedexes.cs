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
		public static IEnumerable<Dto.Pokedex> GetPokedexesByGame(string GameId, SLLog Log)
		{
			Log = Logging.GetLog(Log);
			using (var ctr = new Data.PokedexModel())
			{
				var game = Repository.Games.GetGameById(GameId, Log);
				if (game == null)
				{
					Log.Error("GetPokedexesByGame", new { message = "Game not found", GameId = GameId });
					return null;
				}
				return game.Pokedexes;
			}
		}

		public static Dto.Pokedex GetPokedex(String PokedexId, SLLog Log)
		{
			Log = Logging.GetLog(Log);
			using (var ctr = new Data.PokedexModel())
			{
				var dex = ctr.Pokedexes.SingleOrDefault(u => u.PokedexId == PokedexId);
				if (dex == null)
				{
					Log.Error("GetPokedex", new { message = "Pokedex not found", PokedexId = PokedexId });
					return null;
				}
				return new Dto.Pokedex(dex, true);
			}
		}
	}
}