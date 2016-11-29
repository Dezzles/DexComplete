using DexComplete.Utilities;
using SharpLogging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComplete.Services
{
	public class PokedexService
	{
		private readonly Repository.Pokedexes Pokedexes_;
		public PokedexService(Repository.Pokedexes Pokedexes)
		{
			Pokedexes_ = Pokedexes;
		}
		public IEnumerable<Models.PokedexModel> GetPokedexesByGame(string GameId)
		{
			
			var result = Pokedexes_.GetPokedexesByGame(GameId);
			if (result == null)
				throw new Code.ExceptionResponse("Invalid game");
			List<Models.PokedexModel> dexes = new List<Models.PokedexModel>();
			foreach (var q in result)
			{
				dexes.Add(new Models.PokedexModel()
					{
						PokedexId = q.PokedexId,
						Title = q.Title,
						Regional = q.Regional
					});
			}
			return dexes;
		}

		public Models.PokedexModel GetPokedex(string PokedexId)
		{
			
			var dex = Pokedexes_.GetPokedex(PokedexId);
			if (dex == null)
				throw new Code.ExceptionResponse("Invalid pokedex");
			var ret = new Models.PokedexModel()
			{
				PokedexId = dex.PokedexId,
				Title = dex.Title,
				Regional = dex.Regional
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
