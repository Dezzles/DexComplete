using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DexComplete.Dto
{
	public class AbilitySet
	{
		public string PokemonId { get; set; }
		public IEnumerable<AbilityEntry> Entries { get; set; }
		public int AbilityId { get; set; }
		public AbilitySet(Data.AbilitySet Set)
		{
			this.PokemonId = Set.PokemonId;
			this.AbilityId = 99999;
			var entries = new List<AbilityEntry>();
			foreach (var v in Set.Entries)
			{
				if (v.Pokemon.Index < this.AbilityId)
					this.AbilityId = v.Pokemon.Index;
				entries.Add(new AbilityEntry(v));
			}
			Entries = entries;
		}
	}
}