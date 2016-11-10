using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DexComplete.Dto
{
	public class Game
	{
		public string Title { get; set; }
		public string GameId { get; set; }
		public string TmSetId { get; set; }
		public IEnumerable<Pokedex> Pokedexes { get; set; }
		public IEnumerable<Collection> Collections { get; set; }
		public Generation Generation { get; set; }

		public Game(Data.Game G)
		{
			this.Title = G.Title;
			this.GameId = G.GameId;
			this.TmSetId = G.TMSetId;
			var dexes = new List<Pokedex>();
			var cols = new List<Collection>();
			this.Generation = new Generation(G.Generation);
			foreach (var v in G.Pokedexes)
			{
				dexes.Add(new Pokedex(v, false));
			}

			foreach (var v in G.Collections)
			{
				cols.Add(new Collection(v));
			}

			this.Pokedexes = dexes;
			this.Collections = cols;
		}
	}
}