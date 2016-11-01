using DexComplete.Utilities;
using SharpLogging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComplete.View
{
	static class PokedexRepository
	{
		public static IEnumerable<Models.PokedexModel> ListPokedexes(string GameId, SLLog Log)
		{
			Log = Logging.GetLog(Log);
			using (Data.PokedexModel ctr = new Data.PokedexModel())
			{
				var query = ctr.Games.Where(e => e.GameId == GameId);
				if (query.Count() == 0)
					throw new Code.ExceptionResponse("Invalid game");
				List<Models.PokedexModel> dexes = new List<Models.PokedexModel>();
				Data.Game game = query.First();
				foreach (var q in game.Pokedexes)
				{
					dexes.Add(new Models.PokedexModel()
						{
							PokedexId = q.PokedexId,
							Title = q.Title
						});
				}
				return dexes;
			}
		}

		public static Models.PokedexModel GetPokedexByName(string Game, string Pokedex, SLLog Log)
		{
			Log = Logging.GetLog(Log);
			string pokedexId = "";
			using (Data.PokedexModel ctr = new Data.PokedexModel())
			{
				var query = ctr.Games.Where(e => e.GameId.ToLower() == Game.ToLower());
				if (query.Count() == 0)
					throw new Code.ExceptionResponse("Invalid game");
				var dex = query.First().Pokedexes.Where(e => e.Title.ToLower() == Pokedex.ToLower());
				if (dex.Count() == 0)
					throw new Code.ExceptionResponse("Invalid pokedex");
				pokedexId = dex.First().PokedexId;
			}
			return GetPokedex(pokedexId, Log);
		}

		public static IEnumerable<Models.PokedexModel> GetPokedexesByGame(string GameId, SLLog Log)
		{
			Log = Logging.GetLog(Log);
			List<Models.PokedexModel> ret = new List<Models.PokedexModel>();
			using (Data.PokedexModel ctr = new Data.PokedexModel())
			{
				var game = ctr.Games.SingleOrDefault(e => e.GameId == GameId);
				if (game == null)
					throw new Code.ExceptionResponse("Invalid game");

				foreach (var q in game.Pokedexes)
				{
					ret.Add(new Models.PokedexModel()
						{
							Title = q.Title,
							PokedexId = q.PokedexId
						});
				}
			}
			return ret;
		}

		public static Models.PokedexModel GetPokedex(string PokedexId, SLLog Log)
		{
			Log = Logging.GetLog(Log);
			using (Data.PokedexModel ctr = new Data.PokedexModel())
			{
				var dex = ctr.Pokedexes.SingleOrDefault(e => e.PokedexId == PokedexId);
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
							Id = entry.Pokemon.PokemonId,
							Index = entry.Index,
							Name = entry.Pokemon.Name
						});
				}
				ret.Pokemon = entries;
				return ret;
			}
		}

		public static Models.PokedexModel GetPokedex(string GameId, string PokedexId, SLLog Log)
		{
			Log = Logging.GetLog(Log);
			using (Data.PokedexModel ctr = new Data.PokedexModel())
			{
				var dex = ctr.Pokedexes.SingleOrDefault(e => (e.PokedexId == PokedexId) &&
					(e.Games.Any(u => u.GameId == GameId)));
				if (dex == null)
					throw new Code.ExceptionResponse("Invalid gme or pokedex");
				Models.PokedexModel ret = new Models.PokedexModel();
				ret.PokedexId = dex.PokedexId;
				ret.Title = dex.Title;
				var mons = new List<Models.PokemonModel>();
				foreach (var p in dex.Entries)
				{
					mons.Add(new Models.PokemonModel()
						{
							Id = p.Pokemon.PokemonId,
							Index = p.Index,
							Name = p.Pokemon.Name
						});
				}
				mons.OrderBy(e => e.Index);
				ret.Pokemon = mons;
				return ret;
			}
		}
	}
}
