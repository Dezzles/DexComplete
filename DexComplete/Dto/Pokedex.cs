using System.Collections.Generic;

namespace DexComplete.Dto
{
	public class Pokedex
	{
		public string PokedexId { get; set; }
		public string Title { get; set; }
		public bool Regional { get; set; }
		public IEnumerable<PokedexEntry> Entries { get; set; }

		public Pokedex(Data.Pokedex Dex, bool Recurse)
		{
			this.PokedexId = Dex.PokedexId;
			this.Title = Dex.Title;
			this.Regional = Dex.Regional;
			var res = new List<PokedexEntry>();
			foreach (var v in Dex.Entries)
			{
				res.Add(new PokedexEntry(v));
			}
			this.Entries = res;
		}
	}
}