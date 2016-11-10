﻿using DexComplete.Utilities;
using SharpLogging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComplete.Services
{
	static class PokedexService
	{
		public static IEnumerable<Models.PokedexModel> GetPokedexesByGame(string GameId, SLLog Log)
		{
			Log = Logging.GetLog(Log);
			var result = Repository.Pokedexes.GetPokedexesByGame(GameId, Log);
			if (result == null)
				throw new Code.ExceptionResponse("Invalid game");
			List<Models.PokedexModel> dexes = new List<Models.PokedexModel>();
			foreach (var q in result)
			{
				dexes.Add(new Models.PokedexModel()
					{
						PokedexId = q.PokedexId,
						Title = q.Title
					});
			}
			return dexes;
		}

		public static Models.PokedexModel GetPokedex(string PokedexId, SLLog Log)
		{
			Log = Logging.GetLog(Log);
			var dex = Repository.Pokedexes.GetPokedex(PokedexId, Log);
			if (dex == null)
				throw new Code.ExceptionResponse("Invalid pokedex");
			var ret = new Models.PokedexModel()
			{
				PokedexId = dex.PokedexId,
				Title = dex.Title
			};
			var entries = new List<Models.PokemonModel>();
			var query2 = dex.Entries.OrderBy(e => e.Index);
			foreach (var entry in query2)
			{
				entries.Add(new Models.PokemonModel()
					{
						Index = entry.Index,
						Name = entry.Name
					});
			}
			ret.Pokemon = entries;
			return ret;
		}
	}
}