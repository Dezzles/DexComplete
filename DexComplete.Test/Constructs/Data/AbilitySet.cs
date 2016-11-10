using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComplete.Test.Constructs.Data
{
	class AbilitySet
	{
		public static DexComplete.Data.AbilitySet Data_001()
		{
			var r = new List<DexComplete.Data.AbilityEntry>();
			r.Add(AbilityEntry.Data_001());
			r.Add(AbilityEntry.Data_002());
			r.Add(AbilityEntry.Data_003());

			return new DexComplete.Data.AbilitySet()
			{
				AbilityId = 1,
				Entries = r,
				PokemonId = "bulbasaur"
			};
		}
		public static DexComplete.Data.AbilitySet Data_002()
		{
			var r = new List<DexComplete.Data.AbilityEntry>();
			r.Add(AbilityEntry.Data_004());

			return new DexComplete.Data.AbilitySet()
			{
				AbilityId = 2,
				Entries = r,
				PokemonId = "charmander"
			};
		}
	}
}
